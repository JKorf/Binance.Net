using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiBrokerage : IBinanceRestClientGeneralApiBrokerage
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiBrokerage(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Sub accounts

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountCreateResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccount", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageSubAccount>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts permissions

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"margin", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount/margin", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageEnableMarginResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"futures", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount/futures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageEnableFuturesResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageEnableLeverageTokenResult>> EnableLeverageTokenForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"blvt", true},  // only true for now
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount/blvt", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageEnableLeverageTokenResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts API keys

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, bool isSpotTradingEnabled,
            bool? isMarginTradingEnabled = null, bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"canTrade", isSpotTradingEnabled}
                             };
            parameters.AddOptionalParameter("marginTrade", isMarginTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("futuresTrade", isFuturesTradingEnabled.ToString().ToLower());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageApiKeyCreateResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/sapi/v1/broker/subAccountApi", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                             };
            parameters.AddOptionalParameter("subAccountApiKey", apiKey);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("size", size);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccountApi", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiKeyPermissionAsync(string subAccountId, string apiKey,
            bool isSpotTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"canTrade", isSpotTradingEnabled.ToString().ToLower()},
                                 {"marginTrade", isMarginTradingEnabled.ToString().ToLower()},
                                 {"futuresTrade", isFuturesTradingEnabled.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/permission", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountApiKey>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageAddIpRestrictionResult>> AddIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/ipRestriction", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageAddIpRestrictionResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageIpRestriction>> ChangeIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, bool ipRestrict, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipRestrict", ipRestrict.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/ipRestriction", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageIpRestriction>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccountApi/ipRestriction", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageIpRestriction>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageIpRestrictionBase>> DeleteIpRestrictionForSubAccountApiKeyAsync(string subAccountId,
            string apiKey, string ipAddress, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            apiKey.ValidateNotNull(nameof(apiKey));
            ipAddress.ValidateNotNull(nameof(ipAddress));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"subAccountApiKey", apiKey},
                                 {"ipAddress", ipAddress}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/sapi/v1/broker/subAccountApi/ipRestriction/ipList", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageIpRestrictionBase>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts commission

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId,
            decimal makerCommission, decimal takerCommission, decimal? marginMakerCommission = null, decimal? marginTakerCommission = null,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"makerCommission", makerCommission.ToString(CultureInfo.InvariantCulture)},
                                 {"takerCommission", takerCommission.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("marginMakerCommission", marginMakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("marginTakerCommission", marginTakerCommission?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/commission", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountCommission>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol,
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"symbol", symbol},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/commission/futures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountFuturesCommission>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId,
            string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccountApi/commission/futures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSubAccountCoinFuturesCommission>> ChangeSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId,
            string pair, int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));
            pair.ValidateNotNull(nameof(pair));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"pair", pair},
                                 {"makerAdjustment", makerAdjustment.ToString(CultureInfo.InvariantCulture)},
                                 {"takerAdjustment", takerAdjustment.ToString(CultureInfo.InvariantCulture)}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccountApi/commission/coinFutures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSubAccountCoinFuturesCommission>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId,
            string? pair = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccountApi/commission/coinFutures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts asset summary

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageSpotAssetInfo>> GetSubAccountSpotAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccount/spotSummary", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageSpotAssetInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageMarginAssetInfo>> GetSubAccountMarginAssetInfoAsync(
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccount/marginSummary", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageMarginAssetInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageFuturesAssetInfo>> GetSubAccountFuturesAssetInfoAsync(FuturesAccountType futuresType,
            string? subAccountId = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("futuresType", futuresType);
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v2/broker/subAccount/futuresSummary", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageFuturesAssetInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub accounts BNB burn

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>> ChangeBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"spotBNBBurn", spotBnbBurn.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount/bnbBurn/spot", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageChangeBnbBurnMarginInterestResult>> ChangeBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                                 {"interestBNBBurn", interestBnbBurn.ToString().ToLower()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/subAccount/bnbBurn/marginInterest", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageChangeBnbBurnMarginInterestResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccount/bnbBurn/status", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageBnbBurnStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer & history

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageTransferResult>> TransferUniversalAsync(string asset, decimal quantity,
            string? fromId, BrokerageAccountType fromAccountType, string? toId, BrokerageAccountType toAccountType,
            string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection()
            {
                {"asset", asset},
                {"amount", quantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddEnum("fromAccountType", fromAccountType);
            parameters.AddEnum("toAccountType", toAccountType);
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/universalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>> GetTransferHistoryUniversalAsync(
            string? fromId = null, string? toId = null, string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null,
            int? page = null, int? limit = null, bool showAllStatus = false, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
                             {
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/universalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageTransferResult>> TransferAsync(string asset, decimal quantity,
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection
                             {
                                 {"asset", asset},
                                 {"amount", quantity.ToString(CultureInfo.InvariantCulture)},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/transfer", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageTransferFuturesResult>> TransferFuturesAsync(string asset, decimal quantity, FuturesAccountType futuresType,
            string? fromId, string? toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection
                             {
                                 {"asset", asset},
                                 {"amount", quantity.ToString(CultureInfo.InvariantCulture)},
                             };
            parameters.AddEnum("futuresType", futuresType);
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/broker/transfer/futures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageTransferFuturesResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string? fromId = null, string? toId = null,
            string? clientTransferId = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, bool showAllStatus = false,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
                             {
                                 {"showAllStatus", showAllStatus.ToString().ToLower()},
                             };
            parameters.AddOptionalParameter("fromId", fromId);
            parameters.AddOptionalParameter("toId", toId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/transfer", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageTransferTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageTransferFuturesTransactions>> GetTransferFuturesHistoryAsync(string subAccountId,
            FuturesAccountType futuresType, DateTime? startDate = null, DateTime? endDate = null,
            int? page = null, int? limit = null, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection()
                             {
                                 {"subAccountId", subAccountId}
                             };
            parameters.AddEnum("futuresType", futuresType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/transfer/futures", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageTransferFuturesTransactions>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>> GetSubAccountDepositHistoryAsync(string? subAccountId = null,
            string? asset = null, SubAccountDepositStatus? status = null, DateTime? startDate = null, DateTime? endDate = null,
            int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("subAccountId", subAccountId);
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/subAccount/depositHist", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Broker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/info", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<BinanceBrokerageAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Broker commission rebates

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId,
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            subAccountId.ValidateNotNull(nameof(subAccountId));

            var parameters = new ParameterCollection
                             {
                                 {"subAccountId", subAccountId},
                             };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endDate));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/rebate/recentRecord", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageRebate>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBrokerageFuturesRebate>>> GetBrokerFuturesCommissionRebatesHistoryAsync(FuturesAccountType futuresType,
            DateTime startDate, DateTime endDate, int? page = null, int? size = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
                             {
                                 {"startTime", DateTimeConverter.ConvertToMilliseconds(startDate)!},
                                 {"endTime",  DateTimeConverter.ConvertToMilliseconds(endDate)!}
                             };
            parameters.AddEnum("futuresType", futuresType);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/broker/rebate/futures/recentRecord", BinanceExchange.RateLimiter.SpotRestIp, 0, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBrokerageFuturesRebate>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
