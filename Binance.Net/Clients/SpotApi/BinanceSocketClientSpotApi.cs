using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Options;
using Binance.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using System.Net.WebSockets;
using System.Text.Json;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class BinanceSocketClientSpotApi : SocketApiClient, IBinanceSocketClientSpotApi
    {
        #region fields
        /// <inheritdoc />
        public new BinanceSocketOptions ClientOptions => (BinanceSocketOptions)base.ClientOptions;
        /// <inheritdoc />
        public new BinanceSocketApiOptions ApiOptions => (BinanceSocketApiOptions)base.ApiOptions;

        internal BinanceExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;

        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _streamPath = MessagePath.Get().Property("stream");
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        protected override ErrorMapping ErrorMapping => BinanceErrors.SpotErrors;

        private readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "outboundAccountPosition",
            "balanceUpdate",
            "executionReport",
            "listStatus",
            "listenKeyExpired",
            "eventStreamTerminated",
            "externalLockUpdate",
            "MARGIN_LEVEL_STATUS_CHANGE",
            "USER_LIABILITY_CHANGE"
        };
        #endregion

        /// <inheritdoc />
        public IBinanceSocketClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceSocketClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceSocketClientSpotApiTrading Trading { get; }

        public override JsonSerializerOptions JsonSerializerOptions => SerializerOptions.WithConverters(BinanceExchange._serializerContext);

        #region constructor/destructor

        internal BinanceSocketClientSpotApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.SpotSocketStreamAddress, options, options.SpotOptions)
        {
            Account = new BinanceSocketClientSpotApiAccount(logger, this);
            ExchangeData = new BinanceSocketClientSpotApiExchangeData(logger, this);
            Trading = new BinanceSocketClientSpotApiTrading(logger, this);

            // When sending more than 4000 bytes the server responds very delayed (somehow connected to the websocket keep alive interval)
            // See https://dev.binance.vision/t/socket-live-subscribing-server-delay/9645/2
            // To prevent issues we keep below this
            MessageSendSizeLimit = 4000;

            RateLimiter = BinanceExchange.RateLimiter.SpotSocket;

            SetDedicatedConnection(ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), true);
        }
        #endregion

        public IBinanceSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
                
        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<int?>(_idPath);
            if (id != null)
                return id.ToString();

            var stream = message.GetValue<string>(_streamPath); ;
            var e = message.GetValue<string>(_ePath);
            if (e != null && _userEvents.Contains(e))
                return stream + e;

            return stream;
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var subscription = new BinanceSubscription<T>(_logger, topics.ToList(), onData, false);
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<CallResult<HighPerfUpdateSubscription>> SubscribeHighPerfAsync<T, U>(string url, string[] topics, Func<U, ValueTask> onData, CancellationToken ct) where T: BinanceCombinedStream<U>
        {
            var subscription = new BinanceHighPerfSubscription<T>(topics, x =>
            {
                if (x.Data == null)
                {
                    // It's probably a different message (sub confirm for instance), ignore
                    return new ValueTask();
                }

                if (!topics.Contains(x.Stream))
                    return new ValueTask();

                return onData(x.Data);
            });
            return base.SubscribeHighPerfAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal async Task<CallResult<BinanceResponse<T>>> QueryAsync<T>(string url, string method, Dictionary<string, object> parameters, bool authenticated = false, bool sign = false, int weight = 1, CancellationToken ct = default)
        {
            if (authenticated)
            {
                if (AuthenticationProvider == null)
                    throw new InvalidOperationException("No credentials provided for authenticated endpoint");

                var authProvider = (BinanceAuthenticationProvider)AuthenticationProvider;
                if (sign)
                {
                    parameters = authProvider.AuthenticateSocketParameters(parameters);
                }
                else
                {
                    parameters.Add("apiKey", authProvider.ApiKey);
                }
            }

            var request = new BinanceSocketQuery
            {
                Method = method,
                Params = parameters,
                Id = ExchangeHelpers.NextId()
            };

            var query = new BinanceSpotQuery<BinanceResponse<T>>(this, request, false, weight);
            var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
            if (!result.Success && result.Error is BinanceRateLimitError rle)
            {
                if (rle.RetryAfter != null && RateLimiter != null && ClientOptions.RateLimiterEnabled)
                {
                    _logger.LogWarning("Ratelimit error from server, pausing requests until {Until}", rle.RetryAfter.Value);
                    await RateLimiter.SetRetryAfterGuardAsync(rle.RetryAfter.Value).ConfigureAwait(false);
                }
            }

            return result;
        }

        internal async Task<CallResult<BinanceResponse<T>>> QueryAsync<T>(
            string url,
            string method,
            Dictionary<string, object> parameters,
            Func<BinanceSocketClientSpotApi, BinanceSocketQuery, Query<BinanceResponse<T>>> queryFactory,
            bool authenticated = false,
            bool sign = false,
            CancellationToken ct = default)
        {
            if (authenticated)
            {
                if (AuthenticationProvider == null)
                    throw new InvalidOperationException("No credentials provided for authenticated endpoint");

                var authProvider = (BinanceAuthenticationProvider)AuthenticationProvider;
                if (sign)
                {
                    parameters = authProvider.AuthenticateSocketParameters(parameters);
                }
                else
                {
                    parameters.Add("apiKey", authProvider.ApiKey);
                }
            }

            var request = new BinanceSocketQuery
            {
                Method = method,
                Params = parameters,
                Id = ExchangeHelpers.NextId()
            };

            var query = queryFactory(this, request);
            var result = await QueryAsync(url, query, ct).ConfigureAwait(false);
            if (!result.Success && result.Error is BinanceRateLimitError rle)
            {
                if (rle.RetryAfter != null && RateLimiter != null && ClientOptions.RateLimiterEnabled)
                {
                    _logger.LogWarning("Ratelimit error from server, pausing requests until {Until}", rle.RetryAfter.Value);
                    await RateLimiter.SetRetryAfterGuardAsync(rle.RetryAfter.Value).ConfigureAwait(false);
                }
            }

            return result;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type)
        {
            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(quantity, quoteQuantity, price, stopPrice);

            if (_exchangeInfo == null || _lastExchangeInfoUpdate == null || (DateTime.UtcNow - _lastExchangeInfoUpdate.Value).TotalMinutes > ApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);

            if (_exchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("", "Unable to retrieve trading rules, validation failed");

            return BinanceHelpers.ValidateTradeRules(_logger, ApiOptions.TradeRulesBehaviour, _exchangeInfo, symbol, quantity, quoteQuantity, price, stopPrice, type);
        }
    }
}
