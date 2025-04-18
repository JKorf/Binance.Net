using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models.Spot.AutoInvest;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiAutoInvest : IBinanceRestClientGeneralApiAutoInvest
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiAutoInvest(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Source And Target Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestAssets>> GetSourceAndTargetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/all/asset", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Source Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestSourceAssets>> GetSourceAssetsAsync(string usageType, string? targetAsset = null, bool? flexibleAllowedToUse = null, string? sourceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("targetAsset", targetAsset);
            parameters.Add("usageType", usageType);
            parameters.AddOptional("flexibleAllowedToUse", flexibleAllowedToUse);
            parameters.AddOptional("sourceType", sourceType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/source-asset/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestSourceAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Target Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestTargetAssets>> GetTargetAssetsAsync(string? targetAsset = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("targetAsset", targetAsset);
            parameters.AddOptional("page", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/target-asset/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestTargetAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Target Asset Rois

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestRoi[]>> GetTargetAssetRoisAsync(string asset, AutoInvestRoiType roiType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("targetAsset", asset);
            parameters.AddEnum("hisRoiType", roiType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/target-asset/roi/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestRoi[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Info

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestIndex>> GetIndexInfoAsync(string indexId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("indexId", indexId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/index/info", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestIndex>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Plans

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestPlan>> GetPlansAsync(AutoInvestPlanType planType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("planType", planType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/plan/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestPlan>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region One Time Transaction

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestTradeResult>> OneTimeTransactionAsync(string sourceType, string requestId, decimal subscriptionQuantity, string sourceAsset, bool flexibleAllowedToUse, long indexId, Dictionary<string, decimal> subscriptionDetails, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("sourceType", sourceType);
            parameters.Add("requestId", requestId);
            parameters.Add("subscriptionAmount", subscriptionQuantity);
            parameters.Add("sourceAsset", sourceAsset);
            parameters.Add("flexibleAllowedToUse", flexibleAllowedToUse);
            parameters.Add("indexid", indexId);
            parameters.Add("details", subscriptionDetails.Select(x => new
            {
                targetAsset = x.Key,
                percentage = x.Value
            }).ToList());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/lending/auto-invest/one-off", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestTradeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Plan Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestEditStatusResult>> EditPlanStatusAsync(long planId, AutoInvestPlanStatus status, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("planId", planId);
            parameters.AddEnum("status", status);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/lending/auto-invest/plan/edit-status", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestEditStatusResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Plan

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestEditResult>> EditPlanAsync(string planId, decimal subscriptionQuantity, AutoInvestSubscriptionCycle subscriptionCycle, string sourceAsset, Dictionary<string, decimal> subscriptionDetails, int? subscriptionStartDay = null, string? subscriptionStartWeekday = null, int? subscriptionStartTime = null, bool? flexibleAllowedToUse = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("planId", planId);
            parameters.Add("subscriptionAmount", subscriptionQuantity);
            parameters.AddEnum("subscriptionCycle", subscriptionCycle);
            parameters.Add("sourceAsset", sourceAsset);
            parameters.Add("details", subscriptionDetails.Select(x => new
            {
                targetAsset = x.Key,
                percentage = x.Value
            }).ToList());
            parameters.AddOptional("subscriptionStartDay", subscriptionStartDay);
            parameters.AddOptional("subscriptionStartWeekday", subscriptionStartWeekday);
            parameters.AddOptional("subscriptionStartTime", subscriptionStartTime);
            parameters.AddOptional("flexibleAllowedToUse", flexibleAllowedToUse);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/lending/auto-invest/plan/edit", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestEditResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Redeem Index Linked Plan

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestRedemptionResult>> RedeemIndexLinkedPlanAsync(string indexId, string requestId, int redemptionPercentage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("indexId", indexId);
            parameters.Add("requestId", requestId);
            parameters.Add("redemptionPercentage", redemptionPercentage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/lending/auto-invest/redeem", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestRedemptionResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Subscription Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestPlanTransactions>> GetSubscriptionTransactionHistoryAsync(long? planId = null, DateTime? startTime = null, DateTime? endTime = null, string? targetAsset = null, AutoInvestPlanType? planType = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("planId", planId);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("targetAsset", targetAsset);
            parameters.AddOptionalEnum("planType", planType);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/history/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestPlanTransactions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get One Time Transaction Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestOneTimeTransactionStatus>> GetOneTimeTransactionStatusAsync(long transactionId, string requestId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("transactionId", transactionId);
            parameters.Add("requestId", requestId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/one-off/status", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestOneTimeTransactionStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Create Plan

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestTradeResult>> CreatePlanAsync(string sourceType, AutoInvestPlanType planType, decimal subscriptionQuantity, AutoInvestSubscriptionCycle subscriptionCycle, int subscriptionStartTime, string sourceAsset, Dictionary<string, decimal> subscriptionDetails, string? requestId = null, int? subscriptionStartDay = null, string? subscriptionStartWeekday = null, bool? flexibleAllowedToUse = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("sourceType", sourceType);
            parameters.AddEnum("planType", planType);
            parameters.Add("subscriptionAmount", subscriptionQuantity);
            parameters.AddEnum("subscriptionCycle", subscriptionCycle);
            parameters.Add("subscriptionStartTime", subscriptionStartTime);
            parameters.Add("sourceAsset", sourceAsset);
            parameters.Add("details", subscriptionDetails.Select(x => new
            {
                targetAsset = x.Key,
                percentage = x.Value
            }).ToList());
            parameters.AddOptional("requestId", requestId);
            parameters.AddOptional("subscriptionStartDay", subscriptionStartDay);
            parameters.AddOptional("subscriptionStartWeekday", subscriptionStartWeekday);
            parameters.AddOptional("flexibleAllowedToUse", flexibleAllowedToUse);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/sapi/v1/lending/auto-invest/plan/add", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestTradeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Linked Plan Redemption History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestRedemption[]>> GetIndexLinkedPlanRedemptionHistoryAsync(long requestId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("requestId", requestId);
            parameters.AddOptionalMillisecondsString("", startTime);
            parameters.AddOptionalMillisecondsString("", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            parameters.AddOptional("asset", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/redeem/history", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestRedemption[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Plan Holdings

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestPlanHoldings>> GetPlanHoldingsAsync(long? planId = null, string? requestId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("planId", planId);
            parameters.AddOptional("requestId", requestId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/plan/id", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestPlanHoldings>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Linked Plan Position Details

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestIndexPlanPosition>> GetIndexLinkedPlanPositionDetailsAsync(long indexId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("indexId", indexId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/index/user-summary", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestIndexPlanPosition>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Linked Plan Rebalance History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoInvestRebalanceInfo[]>> GetIndexLinkedPlanRebalanceHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/sapi/v1/lending/auto-invest/rebalance/history", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceAutoInvestRebalanceInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
