using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.Mining;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Mining endpoints
    /// </summary>
    public class BinanceClientMining : IBinanceClientMining
    {
        // Mining
        private const string coinListEndpoint = "mining/pub/coinList";
        private const string algorithmEndpoint = "mining/pub/algoList";
        private const string minerDetailsEndpoint = "mining/worker/detail";
        private const string minerListEndpoint = "mining/worker/list";
        private const string miningRevenueEndpoint = "mining/payment/list";
        private const string miningOtherRevenueEndpoint = "mining/payment/other";
        private const string miningStatisticsEndpoint = "mining/statistics/user/status";
        private const string miningAccountListEndpoint = "mining/statistics/user/list";
        private const string miningHashrateResaleListEndpoint = "mining/hash-transfer/config/details/list";
        private const string miningHashrateResaleDetailsEndpoint = "mining/hash-transfer/profit/details";
        private const string miningHashrateResaleRequest = "mining/hash-transfer/config";
        private const string miningHashrateResaleCancel = "mining/hash-transfer/config/cancel";

        private readonly BinanceClient _baseClient;

        internal BinanceClientMining(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Mining endpoints
        #region Acquiring CoinName
        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        public WebCallResult<IEnumerable<BinanceMiningCoin>> GetMiningCoinList(CancellationToken ct = default)
            => GetMiningCoinListAsync(ct).Result;

        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMiningCoin>>> GetMiningCoinListAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMiningCoin>>>(_baseClient.GetUrlSpot(coinListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<IEnumerable<BinanceMiningCoin>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<IEnumerable<BinanceMiningCoin>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<IEnumerable<BinanceMiningCoin>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion Acquiring CoinName

        #region Acquiring Algorithm 
        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        public WebCallResult<IEnumerable<BinanceMiningAlgorithm>> GetMiningAlgorithmList(CancellationToken ct = default)
            => GetMiningAlgorithmListAsync(ct).Result;

        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMiningAlgorithm>>> GetMiningAlgorithmListAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMiningAlgorithm>>>(_baseClient.GetUrlSpot(algorithmEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<IEnumerable<BinanceMiningAlgorithm>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<IEnumerable<BinanceMiningAlgorithm>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<IEnumerable<BinanceMiningAlgorithm>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Request Detail Miner List

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        public WebCallResult<IEnumerable<BinanceMinerDetails>> GetMinerDetails(string algorithm, string userName,
            string workerName, CancellationToken ct = default)
            => GetMinerDetailsAsync(algorithm, userName, workerName, ct).Result;

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMinerDetails>>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));
            workerName.ValidateNotNull(nameof(workerName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"workerName", workerName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMinerDetails>>>(_baseClient.GetUrlSpot(minerDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<IEnumerable<BinanceMinerDetails>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<IEnumerable<BinanceMinerDetails>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<IEnumerable<BinanceMinerDetails>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Request Miner List
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
        public WebCallResult<BinanceMinerList> GetMinerList(string algorithm, string userName, int? page = null,
            bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null,
            CancellationToken ct = default)
            => GetMinerListAsync(algorithm, userName, page, sortAscending, sortColumn, workerStatus, ct).Result;

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
        public async Task<WebCallResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sortAscending", sortAscending == null ? null : sortAscending == true ? "1" : "0");
            parameters.AddOptionalParameter("sortColumn", sortColumn);
            parameters.AddOptionalParameter("workerStatus", workerStatus == null ? null : JsonConvert.SerializeObject(workerStatus, new MinerStatusConverter(false)));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMinerList>>(_baseClient.GetUrlSpot(minerListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceMinerList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceMinerList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceMinerList>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Revenue List
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
        public WebCallResult<BinanceRevenueList> GetMiningRevenueList(string algorithm, string userName,
            string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null,
            CancellationToken ct = default)
            => GetMiningRevenueListAsync(algorithm, userName, coin, startDate, endDate, page, pageSize, ct).Result;

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
        public async Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", startDate.HasValue ? JsonConvert.SerializeObject(startDate.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endDate", endDate.HasValue ? JsonConvert.SerializeObject(endDate.Value, new TimestampConverter()) : null);

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceRevenueList>>(_baseClient.GetUrlSpot(miningRevenueEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceRevenueList>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Other Revenue List
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
        public async Task<WebCallResult<BinanceOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", startDate.HasValue ? JsonConvert.SerializeObject(startDate.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endDate", endDate.HasValue ? JsonConvert.SerializeObject(endDate.Value, new TimestampConverter()) : null);

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceOtherRevenueList>>(_baseClient.GetUrlSpot(miningOtherRevenueEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceOtherRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceOtherRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceOtherRevenueList>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion

        #region Statistics list
        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        public WebCallResult<BinanceMiningStatistic> GetMiningStatistics(string algorithm, string userName,
            CancellationToken ct = default)
            => GetMiningStatisticsAsync(algorithm, userName, ct).Result;

        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        public async Task<WebCallResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMiningStatistic>>(_baseClient.GetUrlSpot(miningStatisticsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceMiningStatistic>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceMiningStatistic>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceMiningStatistic>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion

        #region Account List
        /// <summary>
        /// Gets mining account list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account user name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        public async Task<WebCallResult<BinanceMiningAccount>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMiningAccount>>(_baseClient.GetUrlSpot(miningAccountListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceMiningAccount>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceMiningAccount>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceMiningAccount>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion

        #region Hashrate Resale List

        /// <summary>
        /// Gets hash rate resale list
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Resale list</returns>
        public async Task<WebCallResult<BinanceHashrateResaleList>> GetHashrateResaleListAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceHashrateResaleList>>(_baseClient.GetUrlSpot(miningHashrateResaleListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceHashrateResaleList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceHashrateResaleList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceHashrateResaleList>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Hashrate Resale Details

        /// <summary>
        /// Gets hash rate resale details
        /// </summary>
        /// <param name="configId">The mining id</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Results per page</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Resale details</returns>
        public async Task<WebCallResult<BinanceHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                { "configId", configId.ToString(CultureInfo.InvariantCulture) },
                { "userName", userName },
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceHashrateResaleDetails>>(_baseClient.GetUrlSpot(miningHashrateResaleDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceHashrateResaleDetails>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceHashrateResaleDetails>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceHashrateResaleDetails>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Hashrate Resale Request

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
        public async Task<WebCallResult<int>> PlaceHashrateResaleRequest(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));
            algorithm.ValidateNotNull(nameof(algorithm));
            toUser.ValidateNotNull(nameof(toUser));

            var parameters = new Dictionary<string, object>()
            {
                { "userName", userName },
                { "algo", algorithm },
                { "startDate", JsonConvert.SerializeObject(startDate, new TimestampConverter()) },
                { "endDate", JsonConvert.SerializeObject(endDate, new TimestampConverter()) },
                { "toPoolUser", toUser },
                { "hashRate", hashRate },
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<int>>(_baseClient.GetUrlSpot(miningHashrateResaleRequest, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<int>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<int>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<int>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #region Cancel Hashrate Resale Configuration

        /// <summary>
        /// Cancel Hashrate Resale Configuration
        /// </summary>
        /// <param name="configId">Mining id</param>
        /// <param name="userName">Mining account</param>
        /// <param name="ct">Resale hashrate h/s must be transferred (BTC is greater than 500000000000 ETH is greater than 500000)</param>
        /// <returns>Success</returns>
        public async Task<WebCallResult<bool>> PlaceHashrateResaleRequest(int configId, string userName, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                { "configId", configId },
                { "userName", userName },
                {"timestamp", _baseClient.GetTimestamp()}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<bool>>(_baseClient.GetUrlSpot(miningHashrateResaleCancel, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<bool>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<bool>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<bool>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion

        #endregion
    }
}
