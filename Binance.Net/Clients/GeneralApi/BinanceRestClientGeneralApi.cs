using Binance.Net.Clients.MessageHandlers;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Net.Http.Headers;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IBinanceRestClientGeneralApi" />
    internal class BinanceRestClientGeneralApi : RestApiClient, IBinanceRestClientGeneralApi
    {
        #region fields 
        /// <inheritdoc />
        public new BinanceRestApiOptions ApiOptions => (BinanceRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new BinanceRestOptions ClientOptions => (BinanceRestOptions)base.ClientOptions;

        protected override IRestMessageHandler MessageHandler { get; } = new BinanceRestMessageHandler(BinanceErrors.SpotErrors);
        private readonly BinanceRestClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiFutures Futures { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiLoans CryptoLoans { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiAutoInvest AutoInvest { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiMining Mining { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiStaking Staking { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiSimpleEarn SimpleEarn { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiCopyTrading CopyTrading { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiGiftCard GiftCard { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiNft Nft { get; }
        #endregion

        #region constructor/destructor

        internal BinanceRestClientGeneralApi(ILogger logger, HttpClient? httpClient, BinanceRestClient baseClient, BinanceRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            Brokerage = new BinanceRestClientGeneralApiBrokerage(this);
            Futures = new BinanceRestClientGeneralApiFutures(this);
            CryptoLoans = new BinanceRestClientGeneralApiLoans(this);
            AutoInvest = new BinanceRestClientGeneralApiAutoInvest(this);
            Mining = new BinanceRestClientGeneralApiMining(this);
            SubAccount = new BinanceRestClientGeneralApiSubAccount(this);
            Staking = new BinanceRestClientGeneralApiStaking(this);
            SimpleEarn = new BinanceRestClientGeneralApiSimpleEarn(this);
            CopyTrading = new BinanceRestClientGeneralApiCopyTrading(this);
            GiftCard = new BinanceRestClientGeneralApiGiftCard(this);
            Nft = new BinanceRestClientGeneralApiNFT(this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
            ArraySerialization = ArrayParametersSerialization.MultipleValues;
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();
        
    }
}
