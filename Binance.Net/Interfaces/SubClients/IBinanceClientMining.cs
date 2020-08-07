using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.Mining;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients
{
    /// <summary>
    /// Mining interface
    /// </summary>
    public interface IBinanceClientMining
    {
        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        WebCallResult<IEnumerable<BinanceMiningCoin>> GetMiningCoinList(CancellationToken ct = default);

        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        Task<WebCallResult<IEnumerable<BinanceMiningCoin>>> GetMiningCoinListAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        WebCallResult<IEnumerable<BinanceMiningAlgorithm>> GetMiningAlgorithmList(CancellationToken ct = default);

        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        Task<WebCallResult<IEnumerable<BinanceMiningAlgorithm>>> GetMiningAlgorithmListAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        WebCallResult<IEnumerable<BinanceMinerDetails>> GetMinerDetails(string algorithm, string userName,
            string workerName, CancellationToken ct = default);

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        Task<WebCallResult<IEnumerable<BinanceMinerDetails>>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default);

        /// <summary>
        /// Gets miner list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="sortAscending">Sort in ascending order</param>
        /// <param name="sortColumn">Column to sort by</param>
        /// <param name="workerStatus">Filter by status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner list</returns>
        WebCallResult<BinanceMinerList> GetMinerList(string algorithm, string userName, int? page = null,
            bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets miner list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="sortAscending">Sort in ascending order</param>
        /// <param name="sortColumn">Column to sort by</param>
        /// <param name="workerStatus">Filter by status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner list</returns>
        Task<WebCallResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        WebCallResult<BinanceRevenueList> GetMiningRevenueList(string algorithm, string userName,
            string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        WebCallResult<BinanceMiningStatistic> GetMiningStatistics(string algorithm, string userName,
            CancellationToken ct = default);

        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        Task<WebCallResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default);

        /// <summary>
        /// Gets mining account list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account user name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceMiningAccount>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default);
    }
}