using Binance.Net.Interfaces;
using Binance.Net.Objects.Brokerage;
using Binance.Net.Objects.Brokerage.SubAccountData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance Brokerage REST Api
    /// </summary>
    public class BinanceBrokerageClient : RestClient, IBinanceBrokerageClient
    {
        #region Fields
        
        private static BinanceBrokerageClientOptions defaultOptions = new BinanceBrokerageClientOptions();
        private static BinanceBrokerageClientOptions DefaultOptions => defaultOptions.Copy();
        
        private readonly bool autoTimestamp;
        private readonly TimeSpan autoTimestampRecalculationInterval;
        private readonly TimeSpan timestampOffset;
        private readonly TimeSpan defaultReceiveWindow;
        
        private double calculatedTimeOffset;
        private bool timeSynced;
        private DateTime lastTimeSync;
        
        private const string Api = "api";
        private const string BrokerageApi = "sapi";
        
        private const string PublicVersion = "3";
        private const string BrokerageVersion = "1";
        
        private const string CheckTimeEndpoint = "time";
        
        private const string SubAccountEndpoint = "broker/subAccount";
        private const string BrokerAccountInfoEndpoint = "broker/info";
        private const string EnableMarginForSubAccountEndpoint = "broker/subAccount/margin";
        private const string EnableFuturesForSubAccountEndpoint = "broker/subAccount/futures";
        private const string ApiKeySubAccountEndpoint = "broker/subAccountApi";
        private const string ApiKeySubAccountPermissionEndpoint = "broker/subAccountApi/permission";
        private const string ApiKeySubAccountCommissionEndpoint = "broker/subAccountApi/commission";
        private const string ApiKeySubAccountCommissionFuturesEndpoint = "broker/subAccountApi/commission/futures";
        private const string TransferEndpoint = "broker/transfer";
        private const string RebatesRecentEndpoint = "broker/rebate/recentRecord";
        private const string RebatesHistoryEndpoint = "broker/rebate/historicalRecord";
        private const string EnableOrDisableBnbBurnForSubAccountSpotAndMarginEndpoint = "broker/subAccount/bnbBurn/spot";
        private const string EnableOrDisableBnbBurnForSubAccountMarginInterestEndpoint = "broker/subAccount/bnbBurn/marginInterest";
        private const string BnbBurnForSubAccountStatusEndpoint = "broker/subAccount/bnbBurn/status";

        #endregion

        #region Constructors
        
        /// <summary>
        /// Create a new instance of BinanceBrokerageClient using credentials and the default options
        /// </summary>
        public BinanceBrokerageClient(ApiCredentials credentials)
            : base(DefaultOptions, new BinanceAuthenticationProvider(credentials, ArrayParametersSerialization.MultipleValues))
        {
            postParametersPosition = PostParameters.InUri;
        }

        /// <summary>
        /// Create a new instance of BinanceBrokerageClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceBrokerageClient(BinanceBrokerageClientOptions options) 
            : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.MultipleValues))
        {
            autoTimestamp = options.AutoTimestamp;
            autoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            timestampOffset = options.TimestampOffset;
            defaultReceiveWindow = options.ReceiveWindow;
            postParametersPosition = PostParameters.InUri;
        }
        
        #endregion

        #region Brokerge API
        
        /// <summary>
        /// Generate a sub account under your brokerage master account
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Created sub-account id</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountCreateResult>(GetUrl(SubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Margin for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Margin result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableMarginResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"margin", true},  // only true for now
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableMarginResult>(GetUrl(EnableMarginForSubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Futures for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Futures result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableFuturesResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"futures", true},  // only true for now
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableFuturesResult>(GetUrl(EnableFuturesForSubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Create Api Key for Sub Account
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="isTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, bool isTradingEnabled, bool? isMarginTradingEnabled = null, 
            bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageApiKeyCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"canTrade", isTradingEnabled},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled);
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageApiKeyCreateResult>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Delete Sub Account Api Key
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey"></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        public async Task<object> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            subAccountId.ValidateNotNull(nameof(apiKey));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<object>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> GetSubAccountApiKeyAsync(string subAccountId, string apiKey = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountApiKey", apiKey);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountApiKey>(GetUrl(ApiKeySubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Change Sub Account Api Permission
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="isTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiPermissionAsync(string subAccountId, string apiKey, 
            bool isTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            subAccountId.ValidateNotNull(nameof(apiKey));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isTradingEnabled},
                                 {"marginTrade", isMarginTradingEnabled},
                                 {"futuresTrade", isFuturesTradingEnabled},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountApiKey>(GetUrl(ApiKeySubAccountPermissionEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub accounts</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string subAccountId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<IEnumerable<BinanceBrokerageSubAccount>>(GetUrl(SubAccountEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Change Sub Account Commission
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>If margin disabled, it is not allowed to send marginMakerCommission or marginTakerCommission</para>
        /// <para>If margin enabled, marginMakerCommission or marginTakerCommission has default value as spotMakerCommission or spotTakerCommission</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="makerCommission">Maker commission</param>
        /// <param name="takerCommission">Taker commission</param>
        /// <param name="marginMakerCommission">Margin maker commission</param>
        /// <param name="marginTakerCommission">Margin taker commission</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account commission result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId, decimal makerCommission, decimal takerCommission,
            decimal? marginMakerCommission = null, decimal? marginTakerCommission = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCommission>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"makerCommission", makerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"takerCommission", takerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginMakerCommission", marginMakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("marginTakerCommission", marginTakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountCommission>(GetUrl(ApiKeySubAccountCommissionEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Change Sub Account Futures Commission Adjustment
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>The sub-account's futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="makerAdjustment">Maker adjustment (100 for 0.01%)</param>
        /// <param name="takerAdjustment">Taker adjustment (100 for 0.01%)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account futures commission result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol, 
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            subAccountId.ValidateNotNull(nameof(symbol));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountFuturesCommission>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"symbol", symbol},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageSubAccountFuturesCommission>(GetUrl(ApiKeySubAccountCommissionFuturesEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Futures Commission Adjustment
        /// <para>The sub-account's futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If symbol not sent, commission adjustment of all symbols will be returned</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account futures commissions result</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, 
            string symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(GetUrl(ApiKeySubAccountCommissionFuturesEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Broker Account Information
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Broker information</returns>
        public async Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageAccountInfo>(GetUrl(BrokerAccountInfoEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Sub Account Transfer
        /// <para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>
        /// <para>Transfer from master account if fromId not sent</para>
        /// <para>Transfer to master account if toId not sent</para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="amount">Amount</param>
        /// <param name="fromId">From id</param>
        /// <param name="toId">To id</param>
        /// <param name="clientTransferId">Client transfer id, must be unique</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer result</returns>
        public async Task<WebCallResult<BinanceBrokerageTransferResult>> TransferAsync(string asset, decimal amount, 
            string fromId = null, string toId = null, string clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", amount},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageTransferResult>(GetUrl(TransferEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Transfer History
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string subAccountId, string clientTransferId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<IEnumerable<BinanceBrokerageTransferTransaction>>(GetUrl(TransferEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Broker Commission Rebate Recent Record
        /// <para>Only get the latest history of past 7 days</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="limit">Limit (Default 500, max 1000)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rebates history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageRebate>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<IEnumerable<BinanceBrokerageRebate>>(GetUrl(RebatesRecentEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Broker Commission Rebate History
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="limit">Limit (default 1000)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A download link for an offline file</returns>
        public async Task<WebCallResult<string>> GetBrokerCommissionRebatesHistoryAsync(string subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<string>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<string>(GetUrl(RebatesHistoryEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Or Disable BNB Burn for Sub Account SPOT and MARGIN
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="spotBnbBurn">"true" or "false", spot and margin whether use BNB to pay for transaction fees or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult>> EnableOrDisableBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"spotBNBBurn", spotBnbBurn},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult>(GetUrl(EnableOrDisableBnbBurnForSubAccountSpotAndMarginEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Or Disable BNB Burn for Sub Account Margin Interest
        /// <para>Sub account must be enabled margin before using this switch</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="interestBnbBurn">"true" or "false", margin loan whether uses BNB to pay for margin interest or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult>> EnableOrDisableBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"interestBNBBurn", interestBnbBurn},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult>(GetUrl(EnableOrDisableBnbBurnForSubAccountMarginInterestEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get BNB Burn Status for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Status</returns>
        public async Task<WebCallResult<BinanceBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageBnbBurnStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinanceBrokerageBnbBurnStatus>(GetUrl(BnbBurnForSubAccountStatusEndpoint, BrokerageApi, BrokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        #endregion

        #region Private methods
        
        private async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = GetUrl(CheckTimeEndpoint, Api, PublicVersion);
            if (!autoTimestamp)
            {
                var result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ServerTime ?? default, result.Error);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                if (!result)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

                if (timeSynced && !resetAutoTimestamp)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);

                if (TotalRequestsMade == 1)
                {
                    // If this was the first request make another one to calculate the offset since the first one can be slower
                    localTime = DateTime.UtcNow;
                    result = await SendRequest<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                    if (!result)
                        return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);
                }

                // Calculate time offset between local and server
                var offset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                if (offset >= 0 && offset < 500)
                {
                    // Small offset, probably mainly due to ping. Don't adjust time
                    calculatedTimeOffset = 0;
                    timeSynced = true;
                    lastTimeSync = DateTime.UtcNow;
                    log.Write(LogVerbosity.Info, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
                }

                calculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                timeSynced = true;
                lastTimeSync = DateTime.UtcNow;
                log.Write(LogVerbosity.Info, $"Time offset set to {calculatedTimeOffset}ms");
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
            }
        }
        
        private Uri GetUrl(string endpoint, string api, string version) => new Uri($"{BaseAddress}/{api}/v{version}/{endpoint}");

        private async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (autoTimestamp && (!timeSynced || DateTime.UtcNow - lastTimeSync > autoTimestampRecalculationInterval))
                return await GetServerTimeAsync(timeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }
        
        private static long ToUnixTimestamp(DateTime time) => (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;

        private string GetTimestamp()
        {
            var offset = autoTimestamp ? calculatedTimeOffset : 0;
            offset += timestampOffset.TotalMilliseconds;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString();
        }
        
        #endregion
    }
}