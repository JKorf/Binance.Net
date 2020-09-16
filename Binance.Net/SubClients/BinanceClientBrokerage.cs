using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Brokerage.SubAccountData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

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
        private const string apiKeySubAccountEndpoint = "broker/subAccountApi";
        private const string apiKeySubAccountPermissionEndpoint = "broker/subAccountApi/permission";
        private const string apiKeySubAccountCommissionEndpoint = "broker/subAccountApi/commission";
        private const string apiKeySubAccountCommissionFuturesEndpoint = "broker/subAccountApi/commission/futures";
        private const string transferEndpoint = "broker/transfer";
        private const string rebatesRecentEndpoint = "broker/rebate/recentRecord";
        private const string rebatesHistoryEndpoint = "broker/rebate/historicalRecord";
        private const string changeBnbBurnForSubAccountSpotAndMarginEndpoint = "broker/subAccount/bnbBurn/spot";
        private const string changeBnbBurnForSubAccountMarginInterestEndpoint = "broker/subAccount/bnbBurn/marginInterest";
        private const string bnbBurnForSubAccountStatusEndpoint = "broker/subAccount/bnbBurn/status";

        private readonly BinanceClient _baseClient;

        internal BinanceClientBrokerage(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

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

            return await _baseClient.SendRequestInternal<BinanceBrokerageEnableLeverageTokenResult>(_baseClient.GetUrlSpot(enableFuturesForSubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageApiKeyCreateResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"canTrade", isTradingEnabled},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageApiKeyCreateResult>(_baseClient.GetUrlSpot(apiKeySubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(apiKeySubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountApiKey>>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = null, int? receiveWindow = null, CancellationToken ct = default)
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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountApiKey>>(_baseClient.GetUrlSpot(apiKeySubAccountEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageSubAccountApiKey>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isTradingEnabled.ToString().ToLower()},
                                 {"marginTrade", isMarginTradingEnabled.ToString().ToLower()},
                                 {"futuresTrade", isFuturesTradingEnabled.ToString().ToLower()},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountApiKey>(_baseClient.GetUrlSpot(apiKeySubAccountPermissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="page">Page (default 1)</param>
        /// <param name="size">Size (default 500, max 500)</param>
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

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountCommission>(_baseClient.GetUrlSpot(apiKeySubAccountCommissionEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceBrokerageSubAccountFuturesCommission>(_baseClient.GetUrlSpot(apiKeySubAccountCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(_baseClient.GetUrlSpot(apiKeySubAccountCommissionFuturesEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

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
            string fromId, string toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBrokerageTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"asset", asset},
                                 {"amount", amount},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBrokerageTransferResult>(_baseClient.GetUrlSpot(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Sub Account Transfer History
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit (default 500, max 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string subAccountId, string? clientTransferId = null,
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"subAccountId", subAccountId},
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageTransferTransaction>>(_baseClient.GetUrlSpot(transferEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Query Broker Commission Rebate Recent Record
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
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string? subAccountId = null,
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBrokerageRebate>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBrokerageRebate>>(_baseClient.GetUrlSpot(rebatesRecentEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
        public async Task<WebCallResult<string>> GetBrokerCommissionRebatesHistoryAsync(string? subAccountId = null,
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<string>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("startTime", startDate != null ? JsonConvert.SerializeObject(startDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endDate != null ? JsonConvert.SerializeObject(endDate, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<string>(_baseClient.GetUrlSpot(rebatesHistoryEndpoint, brokerageApi, brokerageVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

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
    }
}
