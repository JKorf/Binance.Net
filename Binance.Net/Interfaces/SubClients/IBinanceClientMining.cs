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
        /// <param name="pageSize">Results per page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        WebCallResult<BinanceRevenueList> GetMiningRevenueList(string algorithm, string userName,
            string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get other revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

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

        /// <summary>
        /// Gets hash rate resale list
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Resale list</returns>
        Task<WebCallResult<BinanceHashrateResaleList>> GetHashrateResaleListAsync(int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets hash rate resale details
        /// </summary>
        /// <param name="configId">The mining id</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Resale details</returns>
        Task<WebCallResult<BinanceHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Hashrate resale request
        /// </summary>
        /// <param name="userName">Mining account</param>
        /// <param name="algorithm">Transfer algorithm</param>
        /// <param name="startDate">Resale start time</param>
        /// <param name="endDate">Resale end time</param>
        /// <param name="toUser">To mining account</param>
        /// <param name="hashRate">Results per page</param>
        /// <param name="ct">Resale hashrate h/s must be transferred (BTC is greater than 500000000000 ETH is greater than 500000)</param>
        /// <returns>Mining account</returns>
        Task<WebCallResult<int>> PlaceHashrateResaleRequest(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default);

        /// <summary>
        /// Cancel Hashrate Resale Configuration
        /// </summary>
        /// <param name="configId">Mining id</param>
        /// <param name="userName">Mining account</param>
        /// <param name="ct">Resale hashrate h/s must be transferred (BTC is greater than 500000000000 ETH is greater than 500000)</param>
        /// <returns>Success</returns>
        Task<WebCallResult<bool>> PlaceHashrateResaleRequest(int configId, string userName, CancellationToken ct = default);
    }
}