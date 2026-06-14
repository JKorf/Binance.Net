using Binance.Net.Clients.MessageHandlers;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
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
using CryptoExchange.Net.TokenManagement;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc cref="IBinanceSocketClientCoinFuturesApi" />
    internal partial class BinanceSocketClientCoinFuturesApi : SocketApiClient<BinanceEnvironment, BinanceAuthenticationProvider, BinanceCredentials>, IBinanceSocketClientCoinFuturesApi
    {
        #region fields
        protected override ErrorMapping ErrorMapping => BinanceErrors.FuturesErrors;
        private readonly ILoggerFactory? _loggerFactory;
        private BinanceRestClient? _tokenClient;
        internal TokenManager TokenManager { get; }
        private BinanceRestClient TokenClient
        {
            get
            {
                if (_tokenClient == null)
                {
                    _tokenClient = new BinanceRestClient(null, _loggerFactory, Options.Create(new BinanceRestOptions
                    {
                        ApiCredentials = ApiCredentials,
                        Environment = ClientOptions.Environment,
                        Proxy = ClientOptions.Proxy
                    }));
                }

                return _tokenClient;
            }
        }
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

        internal BinanceSocketClientCoinFuturesApi(ILoggerFactory? loggerFactory, BinanceSocketOptions options) :
            base(loggerFactory, BinanceExchange.Metadata.Id, options.Environment.CoinFuturesSocketAddress!, options, options.CoinFuturesOptions)
        {
            // When sending more than 4000 bytes the server responds very delayed (somehow connected to the websocket keep alive interval)
            // See https://dev.binance.vision/t/socket-live-subscribing-server-delay/9645/2
            // To prevent issues we keep below this
            MessageSendSizeLimit = 4000;

            _loggerFactory = loggerFactory;

            Account = new BinanceSocketClientCoinFuturesApiAccount(_logger, this);
            ExchangeData = new BinanceSocketClientCoinFuturesApiExchangeData(_logger, this);
            Trading = new BinanceSocketClientCoinFuturesApiTrading(_logger, this);

            RateLimiter = BinanceExchange.RateLimiter.FuturesSocket;

            TokenManager = new TokenManager(
                BinanceExchange.Metadata.Id,
                loggerFactory,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                startToken: StartListenKeyAsync,
                keepAliveToken: KeepAliveListenKeyAsync,
                stopToken: StopListenKeyAsync);
        }
        #endregion 

        /// <inheritdoc />
        protected override BinanceAuthenticationProvider CreateAuthenticationProvider(BinanceCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType type) => new BinanceSocketCoinFuturesMessageHandler();
        public IBinanceSocketClientCoinFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        
        internal async Task<QueryResult<BinanceResponse<T>>> QueryAsync<T>(string url, string method, Parameters parameters, bool authenticated = false, bool sign = false, int weight = 1, CancellationToken ct = default)
        {
            if (authenticated)
            {
                if (AuthenticationProvider == null)
                    throw new InvalidOperationException("No credentials provided for authenticated endpoint");

                if (sign)
                    parameters = AuthenticationProvider.ProcessRequest(this, parameters);
                else
                    parameters.Add("apiKey", AuthenticationProvider.Key);
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

        internal Task<WebSocketResult<UpdateSubscription>> SubscribeAsync<T>(string url, string dataType, IEnumerable<string> topics, Action<DateTime, string?, T> onData, CancellationToken ct)
        {
            var subscription = new BinanceSubscription<T>(_logger, dataType, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeHighPerfAsync<T, U>(
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

        internal Task<WebSocketResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.StartRiskDataUserStreamAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }

        private async Task<CallResult> KeepAliveListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.KeepAliveRiskDataUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }

        private async Task<CallResult> StopListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.StopRiskDataUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }
    }
}
