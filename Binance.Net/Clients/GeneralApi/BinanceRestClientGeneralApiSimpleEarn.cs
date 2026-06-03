using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.SimpleEarn;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Binance.Net.Clients.GeneralApi
{
    internal class BinanceRestClientGeneralApiSimpleEarn : IBinanceRestClientGeneralApiSimpleEarn
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
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("asset", asset);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/list", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleProduct>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Products

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>> GetLockedProductsAsync(string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("asset", asset);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/list", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe Flexible Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeFlexibleProductAsync(string productId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "productId", productId },
                { "amount", quantity }
            };
            parameters.Add("autoSubscribe", autoSubscribe);
            parameters.Add("sourceAccount", sourceAccount);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/subscribe", BinanceExchange.RateLimiter.EndpointLimit, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<BinanceSimpleEarnPurchase>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Subscribe Locked Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeLockedProductAsync(string projectId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, RedeemDestination? redeemDestination = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "projectId", projectId },
                { "amount", quantity }
            };
            parameters.Add("autoSubscribe", autoSubscribe);
            parameters.Add("sourceAccount", sourceAccount);
            parameters.Add("redeemTo", redeemDestination);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/subscribe", BinanceExchange.RateLimiter.EndpointLimit, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<BinanceSimpleEarnPurchase>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem Flexible Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemFlexibleProductAsync(string productId, bool? redeemAll = null, decimal? quantity = null, AccountSource? destinationAccount = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "productId", productId },
            };
            parameters.Add("redeemAll", redeemAll);
            parameters.Add("amount", quantity);
            parameters.Add("destAccount", destinationAccount);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/redeem", BinanceExchange.RateLimiter.EndpointLimit, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<BinanceSimpleEarnRedemption>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem Locked Product

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemLockedProductAsync(string positionId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "positionId", positionId },
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/redeem", BinanceExchange.RateLimiter.EndpointLimit, 1, true,
                limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<BinanceSimpleEarnRedemption>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Product Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>> GetFlexibleProductPositionsAsync(string? asset = null, string? productId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("asset", asset);
            parameters.Add("productId", productId);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/position", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Product Positions

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>> GetLockedProductPositionsAsync(string? asset = null, string? positionId = null, string? projectId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("asset", asset);
            parameters.Add("positionId", positionId);
            parameters.Add("projectId", projectId);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/position", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnAccount>> GetAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/account", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Subscription Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>> GetFlexibleSubscriptionRecordsAsync(string? productId = null, string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("productId", productId);
            parameters.Add("purchaseId", purchaseId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/subscriptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Subscription Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>> GetLockedSubscriptionRecordsAsync(string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("purchaseId", purchaseId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/subscriptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Redemption Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>> GetFlexibleRedemptionRecordsAsync(string? productId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("productId", productId);
            parameters.Add("redeemId", redeemId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/redemptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Redemption Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>> GetLockedRedemptionRecordsAsync(string? positionId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("positionId", positionId);
            parameters.Add("redeemId", redeemId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/redemptionRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Reward Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>> GetFlexibleRewardRecordsAsync(RewardType type, string? productId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("type", type);
            parameters.Add("productId", productId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/rewardsRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Reward Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>> GetLockedRewardRecordsAsync(string? positionId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("positionId", positionId);
            parameters.Add("asset", asset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/history/rewardsRecord", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Flexible Auto Subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnResult>> SetFlexibleAutoSubscribeAsync(string productId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "productId", productId },
                { "autoSubscribe", autoSubscribe }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/flexible/setAutoSubscribe", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Locked Auto Subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnResult>> SetLockedAutoSubscribeAsync(string positionId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "positionId", positionId },
                { "autoSubscribe", autoSubscribe }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/setAutoSubscribe", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Personal Quota Left

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetFlexiblePersonalQuotaLeftAsync(string productId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "productId", productId }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/personalLeftQuota", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnPersonalQuotaLeft>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Personal Quota Left

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetLockedPersonalQuotaLeftAsync(string projectId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "projectId", projectId }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/personalLeftQuota", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnPersonalQuotaLeft>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Flexible Subscription Preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnFlexiblePreview>> GetFlexibleSubscriptionPreviewAsync(string productId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "productId", productId },
                { "amount", quantity }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/subscriptionPreview", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnFlexiblePreview>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Subscription Preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnLockedPreview[]>> GetLockedSubscriptionPreviewAsync(string projectId, decimal quantity, bool? autoSubscribe = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "projectId", projectId },
                { "amount", quantity }
            };
            parameters.Add("autoSubscribe", autoSubscribe);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/locked/subscriptionPreview", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnLockedPreview[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Locked Redeem Option

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnResult>> SetLockedRedeemOptionAsync(string positionId, RedeemDestination redeemDestination, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "positionId", positionId },
                { "redeemTo", redeemDestination }
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/simple-earn/locked/setRedeemOption", BinanceExchange.RateLimiter.SpotRestIp, 50, true);
            return await _baseClient.SendAsync<BinanceSimpleEarnResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>> GetRateHistoryAsync(string productId, AprPeriod? aprPeriod = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("productId", productId);
            parameters.Add("aprPeriod", aprPeriod);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/rateHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Collateral Records

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>> GetCollateralRecordsAsync(string productId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("productId", productId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("size", pageSize);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/simple-earn/flexible/history/collateralRecord", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
