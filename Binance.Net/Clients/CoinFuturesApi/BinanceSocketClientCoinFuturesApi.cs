using Binance.Net.Converters;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Options;
using Binance.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using System.IO;
using System.Net.WebSockets;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc cref="IBinanceSocketClientCoinFuturesApi" />
    internal partial class BinanceSocketClientCoinFuturesApi : SocketApiClient, IBinanceSocketClientCoinFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _streamPath = MessagePath.Get().Property("stream");
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        protected override ErrorMapping ErrorMapping => BinanceErrors.FuturesErrors;

        private readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "ACCOUNT_CONFIG_UPDATE",
            "MARGIN_CALL",
            "ACCOUNT_UPDATE",
            "ORDER_TRADE_UPDATE",
            "listenKeyExpired",
            "STRATEGY_UPDATE",
            "GRID_UPDATE"
        };
        #endregion

        /// <inheritdoc />
        public new BinanceSocketOptions ClientOptions => (BinanceSocketOptions)base.ClientOptions;

        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesApiTrading Trading { get; }

        #region constructor/destructor

        internal BinanceSocketClientCoinFuturesApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.CoinFuturesSocketAddress!, options, options.CoinFuturesOptions)
        {
            // When sending more than 4000 bytes the server responds very delayed (somehow connected to the websocket keep alive interval)
            // See https://dev.binance.vision/t/socket-live-subscribing-server-delay/9645/2
            // To prevent issues we keep below this
            MessageSendSizeLimit = 4000;

            Account = new BinanceSocketClientCoinFuturesApiAccount(logger, this);
            ExchangeData = new BinanceSocketClientCoinFuturesApiExchangeData(logger, this);
            Trading = new BinanceSocketClientCoinFuturesApiTrading(logger, this);

            RateLimiter = BinanceExchange.RateLimiter.FuturesSocket;
        }
        #endregion 

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);


        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        public IBinanceSocketClientCoinFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        #region methods

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

        #endregion

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

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var subscription = new BinanceSubscription<T>(_logger, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }


        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);
    }
}
