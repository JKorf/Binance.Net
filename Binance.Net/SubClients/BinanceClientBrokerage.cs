using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Brokerage.SubAccountData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Brokerage endpoints
    /// </summary>
    public class BinanceClientBrokerage : IBinanceClientBrokerage
    {
        private const string brokerageApi = "sapi";
        private const string brokerageVersion = "1";

        private const string subAccountEndpoint = "broker/subAccount";
        private const string brokerAccountInfoEndpoint = "broker/info";
        private const string enableMarginForSubAccountEndpoint = "broker/subAccount/margin";
        private const string enableFuturesForSubAccountEndpoint = "broker/subAccount/futures";
        private const string enableLeverageTokenForSubAccountEndpoint = "broker/subAccount/blvt";
        private const string apiKeyEndpoint = "broker/subAccountApi";
        private const string apiKeyPermissionEndpoint = "broker/subAccountApi/permission";
        private const string apiKeyCommissionEndpoint = "broker/subAccountApi/commission";
        private const string apiKeyCommissionFuturesEndpoint = "broker/subAccountApi/commission/futures";
        private const string apiKeyCommissionCoinFuturesEndpoint = "broker/subAccountApi/commission/coinFutures";
        private const string apiKeyIpRestrictionEndpoint = "broker/subAccountApi/ipRestriction";
        private const string apiKeyIpRestrictionListEndpoint = "broker/subAccountApi/ipRestriction/ipList";
        private const string transferEndpoint = "broker/transfer";
        private const string transferUniversalEndpoint = "broker/universalTransfer";
        private const string transferFuturesEndpoint = "broker/transfer/futures";
        private const string rebatesRecentEndpoint = "broker/rebate/recentRecord";
        private const string rebatesHistoryEndpoint = "broker/rebate/historicalRecord";
        private const string rebatesFuturesHistoryEndpoint = "broker/rebate/futures/recentRecord";
        private const string changeBnbBurnForSubAccountSpotAndMarginEndpoint = "broker/subAccount/bnbBurn/spot";
        private const string changeBnbBurnForSubAccountMarginInterestEndpoint = "broker/subAccount/bnbBurn/marginInterest";
        private const string bnbBurnForSubAccountStatusEndpoint = "broker/subAccount/bnbBurn/status";
        private const string spotSummaryEndpoint = "broker/subAccount/spotSummary";
        private const string marginSummaryEndpoint = "broker/subAccount/marginSummary";
        private const string futuresSummaryEndpoint = "broker/subAccount/futuresSummary";
        private const string depositHistoryEndpoint = "broker/subAccount/depositHist";

        private readonly BinanceClient _baseClient;

        internal BinanceClientBrokerage(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Sub accounts

        /// <summary>
        /// Create a Sub Account
        /// <para>This request will generate a sub account under your brokerage master account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Created sub-account id</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountCreateResult>(_baseClient.GetUrlSpot(subAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub accounts</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccount>>(_baseClient.GetUrlSpot(subAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
        
        #region Sub accounts permissions
        
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

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableMarginResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"margin", true},  // only true for now
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageEnableMarginResult>(_baseClient.GetUrlSpot(enableMarginForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableFuturesResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"futures", true},  // only true for now
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageEnableFuturesResult>(_baseClient.GetUrlSpot(enableFuturesForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable Leverage Token for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Leverage Token result</returns>
        public async Task<WebCallResult<BinanceBrokerageEnableLeverageTokenResult>> EnableLeverageTokenForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageEnableLeverageTokenResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"blvt", true},  // only true for now
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageEnableLeverageTokenResult>(_baseClient.GetUrlSpot(enableLeverageTokenForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts API keys
        
        /// <summary>
        /// Create Api Key for Sub Account
        /// <para>This request will generate a api key for a sub account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="isSpotTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, bool isSpotTradingEnabled, 
            bool? isMarginTradingEnabled = null, bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageApiKeyCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"canTrade", isSpotTradingEnabled},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageApiKeyCreateResult>(_baseClient.GetUrlSpot(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Sub Account Api Key
        /// <para>This request will delete a api key for a sub account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey"></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        public async Task<object> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 500, max 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountApiKey>>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccountApiKey>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountApiKey", apiKey);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("size", size);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountApiKey>>(_baseClient.GetUrlSpot(apiKeyEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Change Sub Account Api Permission
        /// <para>This request will change the api permission for a sub account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="isSpotTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiKeyPermissionAsync(string subAccountId, string apiKey,
            bool isSpotTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isSpotTradingEnabled.ToString().ToLower()},
                                 {"marginTrade", isMarginTradingEnabled.ToString().ToLower()},
                                 {"futuresTrade", isFuturesTradingEnabled.ToString().ToLower()},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountApiKey>(_baseClient.GetUrlSpot(apiKeyPermissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Add IP Restriction for Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="ipAddress">IP address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Restriction result</returns>
        public async Task<WebCallResult<BinanceBrokerageAddIpRestrictionResult>> AddIpRestrictionForSubAccountApiKeyAsync(string subAccountId, 
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageAddIpRestrictionResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageAddIpRestrictionResult>(_baseClient.GetUrlSpot(apiKeyIpRestrictionListEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Enable or Disable IP Restriction for Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="ipRestrict">IP restrict</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Restriction result</returns>
        public async Task<WebCallResult<BinanceBrokerageIpRestriction>> ChangeIpRestrictionForSubAccountApiKeyAsync(string subAccountId, 
            string apiKey, bool ipRestrict, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageIpRestriction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipRestrict", ipRestrict.ToString().ToLower()},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageIpRestriction>(_baseClient.GetUrlSpot(apiKeyIpRestrictionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get IP Restriction for Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Restriction result</returns>
        public async Task<WebCallResult<BinanceBrokerageIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string subAccountId, 
            string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageIpRestriction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageIpRestriction>(_baseClient.GetUrlSpot(apiKeyIpRestrictionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete IP Restriction for Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="ipAddress">IP address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Restriction result</returns>
        public async Task<WebCallResult<BinanceBrokerageIpRestrictionBase>> DeleteIpRestrictionForSubAccountApiKeyAsync(string subAccountId, 
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageIpRestrictionBase>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageIpRestrictionBase>(_baseClient.GetUrlSpot(apiKeyIpRestrictionListEndpoint, brokerageApi, brokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
        
        #region Sub accounts commission
        
        /// <summary>
        /// Change Sub Account Commission
        /// <para>This request will change the commission for a sub account</para>
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
        public async Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId, 
            decimal makerCommission, decimal takerCommission, decimal? marginMakerCommission = null, decimal? marginTakerCommission = null, 
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCommission>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"makerCommission", makerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"takerCommission", takerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginMakerCommission", marginMakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("marginTakerCommission", marginTakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountCommission>(_baseClient.GetUrlSpot(apiKeyCommissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Change Sub Account USDT-Ⓜ Futures Commission Adjustment
        /// <para>This request will change the USDT-Ⓜ futures commission for a sub account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>The sub-account's USDT-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's USDT-Ⓜ futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="makerAdjustment">Maker adjustment (100 for 0.01%)</param>
        /// <param name="takerAdjustment">Taker adjustment (100 for 0.01%)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account USDT-Ⓜ futures commission result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol,
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            symbol.ValidateNotNull(nameof(symbol));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountFuturesCommission>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"symbol", symbol},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountFuturesCommission>(_baseClient.GetUrlSpot(apiKeyCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account USDT-Ⓜ Futures Commission Adjustment
        /// <para>The sub-account's USDT-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If symbol not sent, commission adjustment of all symbols will be returned</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's USDT-Ⓜ futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account USDT-Ⓜ futures commissions result</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId,
            string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(_baseClient.GetUrlSpot(apiKeyCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Change Sub Account COIN-Ⓜ Futures Commission Adjustment
        /// <para>This request will change the COIN-Ⓜ futures commission for a sub account</para>
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>The sub-account's COIN-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's COIN-Ⓜ futures commission adjustment on any symbol</para>
        /// <para>Different symbols have the same commission for the same pair</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="pair">Pair</param>
        /// <param name="makerAdjustment">Maker adjustment (100 for 0.01%)</param>
        /// <param name="takerAdjustment">Taker adjustment (100 for 0.01%)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account coin futures commission result</returns>
        public async Task<WebCallResult<BinanceBrokerageSubAccountCoinFuturesCommission>> ChangeSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId, 
            string pair, int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            pair.ValidateNotNull(nameof(pair));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountCoinFuturesCommission>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"pair", pair},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountCoinFuturesCommission>(_baseClient.GetUrlSpot(apiKeyCommissionCoinFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account COIN-Ⓜ Futures Commission Adjustment
        /// <para>The sub-account's COIN-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If pair not sent, commission adjustment of all symbols will be returned</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's COIN-Ⓜ futures commission adjustment on any symbol</para>
        /// <para>Different symbols have the same commission for the same pair</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="pair">Pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account coin futures commissions result</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId,
            string? pair = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(_baseClient.GetUrlSpot(apiKeyCommissionCoinFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        #endregion

        #region Sub accounts asset summary

        /// <summary>
        /// Query Sub Account Spot Asset info
        /// <para>If subAccountId is not sent, the size must be sent</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 10, max 20)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset info</returns>
        public async Task<WebCallResult<BinanceBrokerageSpotAssetInfo>> GetSubAccountSpotAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSpotAssetInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSpotAssetInfo>(_baseClient.GetUrlSpot(spotSummaryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Margin Asset info
        /// <para>If subAccountId is not sent, the size must be sent</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 10, max 20)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset info</returns>
        public async Task<WebCallResult<BinanceBrokerageMarginAssetInfo>> GetSubAccountMarginAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageMarginAssetInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageMarginAssetInfo>(_baseClient.GetUrlSpot(marginSummaryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Futures Asset info
        /// <para>If subAccountId is not sent, the size must be sent</para>
        /// </summary>
        /// <param name="futuresType">Futures type</param>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 10, max 20)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset info</returns>
        public async Task<WebCallResult<BinanceBrokerageFuturesAssetInfo>> GetSubAccountFuturesAssetInfoAsync(BinanceBrokerageFuturesType futuresType,
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageFuturesAssetInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageFuturesAssetInfo>(_baseClient.GetUrlSpot(futuresSummaryEndpoint, brokerageApi, "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        #endregion

        #region Sub accounts BNB burn
        
        /// <summary>
        /// Enable Or Disable BNB Burn for Sub Account SPOT and MARGIN
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="spotBnbBurn">"true" or "false", spot and margin whether use BNB to pay for transaction fees or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result</returns>
        public async Task<WebCallResult<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>> ChangeBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"spotBNBBurn", spotBnbBurn.ToString().ToLower()},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>(_baseClient.GetUrlSpot(changeBnbBurnForSubAccountSpotAndMarginEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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
        public async Task<WebCallResult<BinanceBrokerageChangeBnbBurnMarginInterestResult>> ChangeBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageChangeBnbBurnMarginInterestResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"interestBNBBurn", interestBnbBurn.ToString().ToLower()},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageChangeBnbBurnMarginInterestResult>(_baseClient.GetUrlSpot(changeBnbBurnForSubAccountMarginInterestEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageBnbBurnStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageBnbBurnStatus>(_baseClient.GetUrlSpot(bnbBurnForSubAccountStatusEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        #endregion

        #region Transfer & history
        
        /// <summary>
        /// Sub Account Transfer Universal
        /// <para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>
        /// <para>Transfer from master account if fromId not sent</para>
        /// <para>Transfer to master account if toId not sent</para>
        /// <para>Transfer between futures account is not supported</para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="amount">Amount</param>
        /// <param name="fromId">From id</param>
        /// <param name="fromAccountType">From type</param>
        /// <param name="toId">To id</param>
        /// <param name="toAccountType">To type</param>
        /// <param name="clientTransferId">Client transfer id, must be unique</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer result</returns>
        public async Task<WebCallResult<BinanceBrokerageTransferResult>> TransferUniversalAsync(string asset, decimal amount,
            string? fromId, BrokerageAccountType fromAccountType, string? toId, BrokerageAccountType toAccountType,
            string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                                 {"fromAccountType", JsonConvert.SerializeObject(fromAccountType, new BrokerageAccountTypeConverter(false))},
                                 {"toAccountType", JsonConvert.SerializeObject(toAccountType, new BrokerageAccountTypeConverter(false))},
                                 {"timestamp", _baseClient.GetTimestamp()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageTransferResult>(_baseClient.GetUrlSpot(transferUniversalEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Transfer History Universal
        /// <para>Either fromId or toId must be sent. Return fromId equal master account by default</para>
        /// <para>Only get the latest history of past 30 days</para>
        /// <para>If showAllStatus is true, the status in response will show four types: INIT,PROCESS,SUCCESS,FAILURE</para>
        /// </summary>
        /// <param name="fromId">From id</param>
        /// <param name="toId">To id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit (default 500, max 500)</param>
        /// <param name="showAllStatus">Show all status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>> GetTransferHistoryUniversalAsync(
            string? fromId = null, string? toId = null, string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null, 
            int? page = null, int? limit = null, bool showAllStatus = false, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()},
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>(_baseClient.GetUrlSpot(transferUniversalEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Sub Account Transfer (Spot)
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
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageTransferResult>(_baseClient.GetUrlSpot(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Sub Account Transfer (Futures)
        /// <para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>
        /// <para>Transfer from master account if fromId not sent</para>
        /// <para>Transfer to master account if toId not sent</para>
        /// <para>Each master account could transfer 5000 times/min</para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="amount">Amount</param>
        /// <param name="futuresType">Futures type</param>
        /// <param name="fromId">From id</param>
        /// <param name="toId">To id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer result</returns>
        public async Task<WebCallResult<BinanceBrokerageTransferFuturesResult>> TransferFuturesAsync(string asset, decimal amount, BinanceBrokerageFuturesType futuresType,
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferFuturesResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageTransferFuturesResult>(_baseClient.GetUrlSpot(transferFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account Transfer History (Spot)
        /// <para>If showAllStatus is true, the status in response will show four types: INIT,PROCESS,SUCCESS,FAILURE</para>
        /// <para>If showAllStatus is false, the status in response will show three types: INIT,PROCESS,SUCCESS</para>
        /// </summary>
        /// <param name="fromId">From id</param>
        /// <param name="toId">To id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit (default 500, max 500)</param>
        /// <param name="showAllStatus">Show all status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string? fromId = null, string? toId = null, 
            string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, bool showAllStatus = false,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()},
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageTransferTransaction>>(_baseClient.GetUrlSpot(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Sub Account Transfer History (Futures)
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="futuresType">Futures type</param>
        /// <param name="startDate">From date (default 30 days records)</param>
        /// <param name="endDate">To date (default 30 days records)</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="limit">Limit (default 50, max 500)</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public async Task<WebCallResult<BinanceBrokerageTransferFuturesTransactions>> GetTransferFuturesHistoryAsync(string subAccountId, 
            BinanceBrokerageFuturesType futuresType, DateTime? startDate = null, DateTime? endDate = null, 
            int? page = null, int? limit = null, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferFuturesTransactions>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("startTime", startDate.HasValue ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate.HasValue ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageTransferFuturesTransactions>(_baseClient.GetUrlSpot(transferFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get Sub Account Deposit History
        /// <para>Please notice the default startDate and endDate to make sure that time interval is within 0-7 days</para>
        /// <para>If both startDate and endDate are sent, time between startDate and endDate must be less than 7 days</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="coin">Coin</param>
        /// <param name="status">Status</param>
        /// <param name="startDate">From date (default 7 days from current timestamp)</param>
        /// <param name="endDate">To date (default present timestamp)</param>
        /// <param name="limit">Limit (default 500)</param>
        /// <param name="offset">Offset (default 0)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>> GetSubAccountDepositHistoryAsync(string? subAccountId = null, 
            string? coin = null, BinanceBrokerageSubAccountDepositStatus? status = null, DateTime? startDate = null, DateTime? endDate = null,
            int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("status", status.HasValue ? ((int)status).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("startTime", startDate.HasValue ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate.HasValue ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>(_baseClient.GetUrlSpot(depositHistoryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        #endregion

        #region Broker
        
        /// <summary>
        /// Broker Account Information
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Broker information</returns>
        public async Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageAccountInfo>(_baseClient.GetUrlSpot(brokerAccountInfoEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Broker commission rebates

        /// <summary>
        /// Query Broker Commission Rebate Recent Record (Spot)
        /// <para>Only get the latest history of past 7 days</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 500, max 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rebates history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId,
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageRebate>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()},
                                 {"subAccountId", subAccountId},
                             };
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageRebate>>(_baseClient.GetUrlSpot(rebatesRecentEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Query Broker Futures Commission Rebate Record
        /// </summary>
        /// <param name="futuresType">Futures type</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 10, max 100)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rebate records</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageFuturesRebate>>> GetBrokerFuturesCommissionRebatesHistoryAsync(BinanceBrokerageFuturesType futuresType,
            DateTime startDate, DateTime endDate, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageFuturesRebate>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"futuresType", ((int)futuresType).ToString(CultureInfo.InvariantCulture)},
                                 {"startTime", JsonConvert.SerializeObject(startDate, new TimestampConverter())},
                                 {"endTime", JsonConvert.SerializeObject(endDate, new TimestampConverter())},
                                 {"timestamp", _baseClient.GetTimestamp()},
                             };
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageFuturesRebate>>(_baseClient.GetUrlSpot(rebatesFuturesHistoryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
