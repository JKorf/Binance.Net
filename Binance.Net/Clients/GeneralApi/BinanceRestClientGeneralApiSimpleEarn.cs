using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.SimpleEarn;

namespace Binance.Net.Clients.GeneralApi
{
    internal class BinanceRestClientGeneralApiSimpleEarn: IBinanceRestClientGeneralApiSimpleEarn
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiSimpleEarn(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Flexible Products

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleProduct>>> GetFlexibleProductsAsync(string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/list", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleProduct>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Products

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>> GetLockedProductsAsync(string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/list", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe Flexible Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeFlexibleProductAsync(string productId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "productId", productId },
                { "amount", quantity }
            };
            parameters.AddOptional("autoSubscribe", autoSubscribe);
            parameters.AddOptionalEnum("sourceAccount", sourceAccount);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/subscribe", BinanceExchange.RateLimiter.SpotRestIp, 1, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceSimpleEarnPurchase>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe Locked Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeLockedProductAsync(string projectId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "projectId", projectId },
                { "amount", quantity }
            };
            parameters.AddOptional("autoSubscribe", autoSubscribe);
            parameters.AddOptionalEnum("sourceAccount", sourceAccount);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/subscribe", BinanceExchange.RateLimiter.SpotRestIp, 1, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceSimpleEarnPurchase>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem Flexible Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemFlexibleProductAsync(string productId, bool? redeemAll = null, decimal? quantity = null, AccountSource? destinationAccount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "productId", productId },
            };
            parameters.AddOptional("redeemAll", redeemAll);
            parameters.AddOptional("amount", quantity);
            parameters.AddOptionalEnum("destAccount", destinationAccount);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/redeem", BinanceExchange.RateLimiter.SpotRestIp, 1, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceSimpleEarnRedemption>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem Locked Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemLockedProductAsync(string positionId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "positionId", positionId },
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/redeem", BinanceExchange.RateLimiter.SpotRestIp, 1, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceSimpleEarnRedemption>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Product Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>> GetFlexibleProductPositionsAsync(string? asset = null, string? productId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            parameters.AddOptional("productId", productId);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/position", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Product Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>> GetLockedProductPositionsAsync(string? asset = null, string? positionId = null, string? projectId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            parameters.AddOptional("positionId", positionId);
            parameters.AddOptional("projectId", projectId);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/position", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnAccount>> GetAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/account", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Subscription Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>> GetFlexibleSubscriptionRecordsAsync(string? productId = null, string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("productId", productId);
            parameters.AddOptional("purchaseId", purchaseId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/subscriptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Subscription Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>> GetLockedSubscriptionRecordsAsync(string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("purchaseId", purchaseId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/subscriptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Redemption Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>> GetFlexibleRedemptionRecordsAsync(string? productId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("productId", productId);
            parameters.AddOptional("redeemId", redeemId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/redemptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Redemption Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>> GetLockedRedemptionRecordsAsync(string? positionId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("positionId", positionId);
            parameters.AddOptional("redeemId", redeemId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/redemptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Reward Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>> GetFlexibleRewardRecordsAsync(RewardType type, string? productId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("type", type);
            parameters.AddOptional("productId", productId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/rewardsRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Reward Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>> GetLockedRewardRecordsAsync(string? positionId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("positionId", positionId);
            parameters.AddOptional("asset", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/rewardsRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Flexible Auto Subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnResult>> SetFlexibleAutoSubscribeAsync(string productId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "productId", productId },
                { "autoSubscribe", autoSubscribe }
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/setAutoSubscribe", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Locked Auto Subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnResult>> SetLockedAutoSubscribeAsync(string positionId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "positionId", positionId },
                { "autoSubscribe", autoSubscribe }
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/setAutoSubscribe", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Personal Quota Left

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetFlexiblePersonalQuotaLeftAsync(string productId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "productId", productId }
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/personalLeftQuota", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnPersonalQuotaLeft>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Personal Quota Left

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetLockedPersonalQuotaLeftAsync(string projectId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "projectId", projectId }
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/personalLeftQuota", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnPersonalQuotaLeft>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Subscription Preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnFlexiblePreview>> GetFlexibleSubscriptionPreviewAsync(string productId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "productId", productId },
                { "amount", quantity }
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/subscriptionPreview", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnFlexiblePreview>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Subscription Preview

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSimpleEarnLockedPreview>>> GetLockedSubscriptionPreviewAsync(string projectId, decimal quantity, bool? autoSubscribe = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "projectId", projectId },
                { "amount", quantity }
            };
            parameters.AddOptional("autoSubscribe", autoSubscribe);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/subscriptionPreview", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceSimpleEarnLockedPreview>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>> GetRateHistoryAsync(string productId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("productId", productId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/rateHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Collateral Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>> GetCollateralRecordsAsync(string productId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("productId", productId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/collateralRecord", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
