using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.SimpleEarn;
using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Clients.GeneralApi
{
    internal class BinanceRestClientGeneralApiSimpleEarn: IBinanceRestClientGeneralApiSimpleEarn
    {
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnFlexibleProduct>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/list"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnPurchase>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/subscribe"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnPurchase>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/subscribe"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnRedemption>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/redeem"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnRedemption>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/redeem"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/position"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Account

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnAccount>> GetAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnAccount>(_baseClient.GetUrl("sapi/v1/simple-earn/account"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/history/subscriptionRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/history/subscriptionRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/history/redemptionRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/history/redemptionRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/history/rewardsRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/history/rewardsRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnResult>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/setAutoSubscribe"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnResult>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/setAutoSubscribe"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnPersonalQuotaLeft>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/personalLeftQuota"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnPersonalQuotaLeft>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/personalLeftQuota"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnFlexiblePreview>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/subscriptionPreview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Locked Subscription Preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSimpleEarnLockedPreview>> GetLockedSubscriptionPreviewAsync(string projectId, decimal quantity, bool? autoSubscribe = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "projectId", projectId },
                { "amount", quantity }
            };
            parameters.AddOptional("autoSubscribe", autoSubscribe);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            return await _baseClient.SendRequestInternal<BinanceSimpleEarnLockedPreview>(_baseClient.GetUrl("sapi/v1/simple-earn/locked/subscriptionPreview"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/history/rateHistory"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>(_baseClient.GetUrl("sapi/v1/simple-earn/flexible/history/collateralRecord"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
