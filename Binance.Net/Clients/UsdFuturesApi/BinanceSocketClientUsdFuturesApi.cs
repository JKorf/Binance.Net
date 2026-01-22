using Binance.Net.Clients.MessageHandlers;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Options;
using Binance.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.HighPerf;
using System.Net.WebSockets;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <summary>
    /// Client providing access to the Binance Usd futures websocket Api
    /// </summary>
    internal partial class BinanceSocketClientUsdFuturesApi : SocketApiClient, IBinanceSocketClientUsdFuturesApi
    {
        /// <inheritdoc />
        public new BinanceSocketOptions ClientOptions => (BinanceSocketOptions)base.ClientOptions;
        /// <inheritdoc />
        public new BinanceSocketApiOptions ApiOptions => (BinanceSocketApiOptions)base.ApiOptions;

        #region fields

        internal BinanceFuturesUsdtExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;

        protected override ErrorMapping ErrorMapping => BinanceErrors.FuturesErrors;

        #endregion

        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesApiTrading Trading { get; }

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientUsdFuturesStreams
        /// </summary>
        internal BinanceSocketClientUsdFuturesApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.UsdFuturesSocketAddress!, options, options.UsdFuturesOptions)
        {
            Account = new BinanceSocketClientUsdFuturesApiAccount(logger, this);
            ExchangeData = new BinanceSocketClientUsdFuturesApiExchangeData(logger, this);
            Trading = new BinanceSocketClientUsdFuturesApiTrading(logger, this);

            // When sending more than 4000 bytes the server responds very delayed (somehow connected to the websocket keep alive interval on framework level)
            // See https://dev.binance.vision/t/socket-live-subscribing-server-delay/9645/2
            // To prevent issues we keep below this
            MessageSendSizeLimit = 4000;

            RateLimiter = BinanceExchange.RateLimiter.FuturesSocket;
        }
        #endregion 

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType type) => new BinanceSocketUsdFuturesMessageHandler();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        public IBinanceSocketClientUsdFuturesApiShared SharedClient => this;


        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, string dataType, IEnumerable<string> topics, Action<DateTime, string?, T> onData, CancellationToken ct)
        {
            var subscription = new BinanceSubscription<T>(_logger, dataType, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<CallResult<HighPerfUpdateSubscription>> SubscribeHighPerfAsync<T, U>(
            string url,
            string[] topics,
            Action<U> onData,
            CancellationToken ct) where T : BinanceCombinedStream<U>
        {
            var subscription = new BinanceHighPerfSubscription<T>(topics, x =>
            {
                if (x.Data == null)
                {
                    // It's probably a different message (sub confirm for instance), ignore
                    return;
                }

                onData(x.Data);
            });

            return base.SubscribeHighPerfAsync(
                url.AppendPath("stream"),
                subscription,
                HighPerfConnectionFactory ??= new HighPerfJsonSocketConnectionFactory(SerializerOptions.WithConverters(BinanceExchange._serializerContext)),
                ct);
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

                var binanceAuthProvider = (BinanceAuthenticationProvider)AuthenticationProvider;
                if (sign)
                    parameters = binanceAuthProvider.ProcessRequest(this, parameters);
                else
                    parameters.Add("apiKey", AuthenticationProvider.ApiKey);
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
    }
}
