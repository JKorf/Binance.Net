﻿using System;
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
        private const string miningStatisticsEndpoint = "mining/statistics/user/status";
        private const string miningAccountListEndpoint = "mining/statistics/user/list";

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
            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMiningCoin>>>(_baseClient.GetUrlSpot(coinListEndpoint, "sapi", "1"), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
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
            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMiningAlgorithm>>>(_baseClient.GetUrlSpot(algorithmEndpoint, "sapi", "1"), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
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
                {"workerName", workerName}
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceMinerDetails>>>(_baseClient.GetUrlSpot(minerDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, false).ConfigureAwait(false);
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
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sortAscending", sortAscending == null ? null : sortAscending == true ? "1" : "0");
            parameters.AddOptionalParameter("sortColumn", sortColumn);
            parameters.AddOptionalParameter("workerStatus", workerStatus == null ? null : JsonConvert.SerializeObject(workerStatus, new MinerStatusConverter(false)));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMinerList>>(_baseClient.GetUrlSpot(minerListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, false).ConfigureAwait(false);
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
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        public WebCallResult<BinanceRevenueList> GetMiningRevenueList(string algorithm, string userName,
            string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null,
            CancellationToken ct = default)
            => GetMiningRevenueListAsync(algorithm, userName, coin, startDate, endDate, page, ct).Result;

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
        public async Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Dictionary<string, object>()
            {
                {"algo", algorithm},
                {"userName", userName},
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", startDate.HasValue ? JsonConvert.SerializeObject(startDate.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endDate", endDate.HasValue ? JsonConvert.SerializeObject(endDate.Value, new TimestampConverter()) : null);

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceRevenueList>>(_baseClient.GetUrlSpot(miningRevenueEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, false).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceRevenueList>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceRevenueList>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
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
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMiningStatistic>>(_baseClient.GetUrlSpot(miningStatisticsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, false).ConfigureAwait(false);
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
            };

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceMiningAccount>>(_baseClient.GetUrlSpot(miningAccountListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, false).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<BinanceMiningAccount>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            if (result.Data?.Code != 0)
                return WebCallResult<BinanceMiningAccount>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, new ServerError(result.Data!.Code, result.Data.Message));

            return new WebCallResult<BinanceMiningAccount>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion

        #endregion
    }
}
