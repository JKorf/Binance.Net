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
        /// Subscribes to ETH staking
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
        /// <returns>Subscription result</returns>
        Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeems ETH staking
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
        /// <returns>Redemption result</returns>
        Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ETH staking history
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
        /// <returns>ETH staking history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ETH redemption history
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
        /// <returns>ETH redemption history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ETH rewards history
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
        /// <returns>ETH rewards history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ETH staking quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-current-eth-staking-quota-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/eth/quota
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>ETH staking quota details</returns>
        Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets BETH rate history
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
        /// <returns>BETH rate history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ETH staking account
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#eth-staking-account-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/eth-staking/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>ETH staking account information</returns>
        Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Wraps BETH
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
        /// <returns>Wrap result</returns>
        Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets wrap history
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
        /// <returns>Wrap history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets unwrap history
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
        /// <returns>Unwrap history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL staking account info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/account" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>SOL staking account information</returns>
        Task<WebCallResult<BinanceSolStakingAccount>> GetSolStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL staking quotas
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/account/Get-SOL-staking-quota-details" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/quota
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>SOL staking quota details</returns>
        Task<WebCallResult<BinanceSolStakingQuota>> GetSolStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to SOL staking
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
        /// <returns>Subscription result</returns>
        Task<WebCallResult<BinanceSolStakingResult>> SubscribeSolStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeems SOL staking
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
        /// <returns>Redemption result</returns>
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
        /// <returns>Claim result</returns>
        Task<WebCallResult<BinanceStakingResult>> ClaimSolBoostRewardsAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL staking history
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
        /// <returns>SOL staking history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceStakingHistory>>> GetSolStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL redemption history
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
        /// <returns>SOL redemption history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRedemptionHistory>>> GetSolRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets BNSOL rewards history
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
        /// <returns>BNSOL rewards history</returns>
        Task<WebCallResult<BinanceSolRewards>> GetBnSolRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets BNSOL rate history
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
        /// <returns>BNSOL rate history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBnsolRateHistory>>> GetBnSolRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL boost reward history
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
        /// <returns>SOL boost reward history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBnsolRewardHistory>>> GetSolBoostRewardsHistoryAsync(SolRewardType type, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets SOL unclaimed rewards
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/staking/sol-staking/history/Get-Unclaimed-rewards" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/sol-staking/sol/history/unclaimedRewards
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Unclaimed SOL rewards</returns>
        Task<WebCallResult<BinanceSolUnclaimedReward[]>> GetSolUnclaimedRewardsAsync(long? receiveWindow = null, CancellationToken ct = default);
    }
}
