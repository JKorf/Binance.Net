using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot.AutoInvest;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Auto Invest endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiAutoInvest
    {
        /// <summary>
        /// Get auto invest source and target assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/all/asset
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestAssets>> GetSourceAndTargetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get source assets info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data/Query-source-asset-list" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/source-asset/list
        /// </para>
        /// </summary>
        /// <param name="targetAsset">Filter by target asset</param>
        /// <param name="usageType">Usage type, "RECURRING" or "ONE_TIME"</param>
        /// <param name="flexibleAllowedToUse">Allowed to be used for flexible</param>
        /// <param name="sourceType">MAIN_SITE (default) or TR (Turkey users)</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestSourceAssets>> GetSourceAssetsAsync(string usageType, string? targetAsset = null, bool? flexibleAllowedToUse = null, string? sourceType = null, CancellationToken ct = default);

        /// <summary>
        /// Get target assets info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data/Get-target-asset-list" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/target-asset/list
        /// </para>
        /// </summary>
        /// <param name="targetAsset">Filter by target asset</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAutoInvestTargetAssets>> GetTargetAssetsAsync(string? targetAsset = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get target asset ROIs
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data/Get-target-asset-ROI-data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/target-asset/roi/list
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="roiType">ROI type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestRoi[]>> GetTargetAssetRoisAsync(string asset, AutoInvestRoiType roiType, CancellationToken ct = default);
        /// <summary>
        /// Get index info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data/Query-Index-Details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/index/info
        /// </para>
        /// </summary>
        /// <param name="indexId">The id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestIndex>> GetIndexInfoAsync(string indexId, CancellationToken ct = default);
        /// <summary>
        /// Get auto invest plans
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/market-data/Get-list-of-plans" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/plan/list
        /// </para>
        /// </summary>
        /// <param name="planType">Type of plans</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestPlan>> GetPlansAsync(AutoInvestPlanType planType, CancellationToken ct = default);

        /// <summary>
        /// Make a one time transaction
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/lending/auto-invest/one-off
        /// </para>
        /// </summary>
        /// <param name="sourceType">The source type, "MAIN_SITE" for normal, "TR" for Turkey users</param>
        /// <param name="requestId">Request id</param>
        /// <param name="subscriptionQuantity">The quantity to subscribe</param>
        /// <param name="sourceAsset">The source asset</param>
        /// <param name="flexibleAllowedToUse">true: use flexible wallet</param>
        /// <param name="indexId">Index id</param>
        /// <param name="subscriptionDetails">Subscription details of asset => percentage. Total percentage should add up to 100%</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestTradeResult>> OneTimeTransactionAsync(string sourceType, string requestId, decimal subscriptionQuantity, string sourceAsset, bool flexibleAllowedToUse, long indexId, Dictionary<string, decimal> subscriptionDetails, CancellationToken ct = default);

        /// <summary>
        /// Edit the status of a plan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Change-Plan-Status" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/lending/auto-invest/plan/edit-status
        /// </para>
        /// </summary>
        /// <param name="planId">The plan id</param>
        /// <param name="status">New status</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestEditStatusResult>> EditPlanStatusAsync(long planId, AutoInvestPlanStatus status, CancellationToken ct = default);

        /// <summary>
        /// Edit a plan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Investment-plan-adjustment" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/lending/auto-invest/plan/edit
        /// </para>
        /// </summary>
        /// <param name="planId">The plan id</param>
        /// <param name="subscriptionQuantity">The quantity</param>
        /// <param name="subscriptionCycle">The cycle</param>
        /// <param name="subscriptionStartDay">Start day, 1..31. Required if cycle is monthly</param>
        /// <param name="subscriptionStartWeekday">Start weekday, required if cycle is Weekly or Bi-Weekly</param>
        /// <param name="subscriptionStartTime">Start hour, 1..24</param>
        /// <param name="sourceAsset">Source asset</param>
        /// <param name="flexibleAllowedToUse">True:use flexible wallet</param>
        /// <param name="subscriptionDetails">Subscription details of asset => percentage. Total percentage should add up to 100%</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestEditResult>> EditPlanAsync(string planId, decimal subscriptionQuantity, AutoInvestSubscriptionCycle subscriptionCycle, string sourceAsset, Dictionary<string, decimal> subscriptionDetails, int? subscriptionStartDay = null, string? subscriptionStartWeekday = null, int? subscriptionStartTime = null, bool? flexibleAllowedToUse = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem index linked plan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Index-Linked-Plan-Redemption" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/lending/auto-invest/redeem
        /// </para>
        /// </summary>
        /// <param name="indexId">The index id</param>
        /// <param name="requestId">Request id</param>
        /// <param name="redemptionPercentage">Redemption percentage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestRedemptionResult>> RedeemIndexLinkedPlanAsync(string indexId, string requestId, int redemptionPercentage, CancellationToken ct = default);

        /// <summary>
        /// Get subscription transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Query-subscription-transaction-history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/history/list
        /// </para>
        /// </summary>
        /// <param name="planId">Filter by plan id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="targetAsset">Filter by target asset</param>
        /// <param name="planType">Filter by plan type</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestPlanTransactions>> GetSubscriptionTransactionHistoryAsync(long? planId = null, DateTime? startTime = null, DateTime? endTime = null, string? targetAsset = null, AutoInvestPlanType? planType = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get one time transaction status
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Query-One-Time-Transaction-Status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/one-off/status
        /// </para>
        /// </summary>
        /// <param name="transactionId">Transaction id</param>
        /// <param name="requestId">Request id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestOneTimeTransactionStatus>> GetOneTimeTransactionStatusAsync(long transactionId, string requestId, CancellationToken ct = default);

        /// <summary>
        /// Create new investment plan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Investment-plan-creation" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/lending/auto-invest/plan/add
        /// </para>
        /// </summary>
        /// <param name="sourceType">Source type, "MAIN_SITE" for normal, "TR" for Turkey users</param>
        /// <param name="requestId">Request id</param>
        /// <param name="planType">Plan type</param>
        /// <param name="subscriptionQuantity">Subscription quantity</param>
        /// <param name="subscriptionCycle">Subscription cycle</param>
        /// <param name="subscriptionStartDay">Subscription start day, 1..31. Required when cycle is montly</param>
        /// <param name="subscriptionStartWeekday">Subscription start weekday, "MON" .. "SUN". Required when cycle is Weekly or BiWeekly</param>
        /// <param name="subscriptionStartTime">Subscription start time hour, 0..24</param>
        /// <param name="sourceAsset">Source asset</param>
        /// <param name="flexibleAllowedToUse">True: flexible wallet</param>
        /// <param name="subscriptionDetails">Subscription details of asset => percentage. Total percentage should add up to 100%</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestTradeResult>> CreatePlanAsync(string sourceType, AutoInvestPlanType planType, decimal subscriptionQuantity, AutoInvestSubscriptionCycle subscriptionCycle, int subscriptionStartTime, string sourceAsset, Dictionary<string, decimal> subscriptionDetails, string? requestId = null, int? subscriptionStartDay = null, string? subscriptionStartWeekday = null, bool? flexibleAllowedToUse = null, CancellationToken ct = default);

        /// <summary>
        /// Get index linked plan redemption history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Query-Index-Linked-Plan-Redemption" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/redeem/history
        /// </para>
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestRedemption[]>> GetIndexLinkedPlanRedemptionHistoryAsync(long requestId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get holding details of a plan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Query-holding-details-of-the-plan" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/plan/id
        /// </para>
        /// </summary>
        /// <param name="planId">Filter by plan id</param>
        /// <param name="requestId">Request id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestPlanHoldings>> GetPlanHoldingsAsync(long? planId = null, string? requestId = null, CancellationToken ct = default);

        /// <summary>
        /// Get index linked plan position details
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Query-Index-Linked-Plan-Position-Details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/index/user-summary
        /// </para>
        /// </summary>
        /// <param name="indexId">The index id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestIndexPlanPosition>> GetIndexLinkedPlanPositionDetailsAsync(long indexId, CancellationToken ct = default);

        /// <summary>
        /// Get index linked plan rebalance history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/auto_invest/trade/Index-Linked-Plan-Rebalance-Details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/lending/auto-invest/rebalance/history
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceAutoInvestRebalanceInfo[]>> GetIndexLinkedPlanRebalanceHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

    }
}
