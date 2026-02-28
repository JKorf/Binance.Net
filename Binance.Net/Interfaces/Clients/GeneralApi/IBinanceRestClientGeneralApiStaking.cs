using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Staking;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Staking endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiStaking
    {
        /// <summary>
        /// Subscribe to ETH staking
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-eth-staking-trade" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/eth-staking/eth/stake
        /// </para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem from ETH staking
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-eth-trade" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/eth-staking/eth/redeem
        /// </para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH staking history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-staking-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/history/stakingHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH redemption history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-redemption-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/history/redemptionHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH rewards history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-rewards-distribution-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/history/rewardsHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH staking quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-current-eth-staking-quota-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/quota
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Beth rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-beth-rate-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/history/rateHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get eth staking account
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#eth-staking-account-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Wrap Beth
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#wrap-beth-trade" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/eth-staking/wbeth/wrap
        /// </para>
        /// </summary>
        /// <param name="quantity">Quantity to wrap</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get wrap history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-wrap-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/wbeth/history/wrapHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get unwrap history
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-unwrap-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/wbeth/history/unwrapHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL staking account info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/account" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolStakingAccount>> GetSolStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL staking quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/account/Get-SOL-staking-quota-details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/quota
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolStakingQuota>> GetSolStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to SOL staking
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/staking" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/sol-staking/sol/stake
        /// </para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolStakingResult>> SubscribeSolStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem from SOL staking
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/staking/Redeem-SOL" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/sol-staking/sol/redeem
        /// </para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolStakingResult>> RedeemSolStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Claim Boost APR Airdrop rewards
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/staking/Claim-Boost-rewards" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/sol-staking/sol/claim
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> ClaimSolBoostRewardsAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL staking history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/stakingHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceStakingHistory>>> GetSolStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL redemption history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-SOL-redemption-history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/redemptionHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRedemptionHistory>>> GetSolRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BN SOL rewards history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-BNSOL-rewards-history" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/bnsolRewardsHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolRewards>> GetBnSolRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BN SOL rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-BNSOL-Rate-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/rateHistory
        /// </para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBnsolRateHistory>>> GetBnSolRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL boost reward history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-Boost-rewards-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/boostRewardsHistory
        /// </para>
        /// </summary>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBnsolRewardHistory>>> GetSolBoostRewardsHistoryAsync(SolRewardType type, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get SOL unclaimed rewards
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-Unclaimed-rewards" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/unclaimedRewards
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSolUnclaimedReward[]>> GetSolUnclaimedRewardsAsync(long? receiveWindow = null, CancellationToken ct = default);
    }
}
