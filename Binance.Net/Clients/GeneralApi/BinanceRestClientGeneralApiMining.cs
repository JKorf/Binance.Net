using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Mining;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiMining : IBinanceRestClientGeneralApiMining
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiMining(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }


        #region Acquiring CoinName
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMiningCoin[]>> GetMiningCoinListAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/pub/coinList", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningCoin[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMiningCoin[]>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMiningCoin[]>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion Acquiring CoinName

        #region Acquiring Algorithm 
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMiningAlgorithm[]>> GetMiningAlgorithmListAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/pub/algoList", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningAlgorithm[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMiningAlgorithm[]>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMiningAlgorithm[]>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Request Detail Miner List

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMinerDetails[]>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));
            workerName.ValidateNotNull(nameof(workerName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName},
                {"workerName", workerName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/worker/detail", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMinerDetails[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMinerDetails[]>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMinerDetails[]>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Request Miner List
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sortAscending", sortAscending == null ? null : sortAscending == true ? "1" : "0");
            parameters.AddOptionalParameter("sortColumn", sortColumn);
            parameters.AddOptionalEnum("workerStatus", workerStatus);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/worker/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMinerList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMinerList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMinerList>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Revenue List
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/payment/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceRevenueList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceRevenueList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceRevenueList>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Other Revenue List
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/payment/other", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceOtherRevenueList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceOtherRevenueList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceOtherRevenueList>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Statistics list
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/statistics/user/status", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningStatistic>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMiningStatistic>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMiningStatistic>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Account List
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMiningAccount[]>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/statistics/user/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningAccount[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMiningAccount[]>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMiningAccount[]>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Hashrate Resale List
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceHashrateResaleList>> GetHashrateResaleListAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/hash-transfer/config/details/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceHashrateResaleList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceHashrateResaleList>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceHashrateResaleList>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Details
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                { "configId", configId.ToString(CultureInfo.InvariantCulture) },
                { "userName", userName }
            };

            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/hash-transfer/profit/details", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceHashrateResaleDetails>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceHashrateResaleDetails>(default);

            if (result.Data?.Code != 0)
                result.AsError<BinanceHashrateResaleDetails>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Request

        /// <inheritdoc />
        public async Task<WebCallResult<int>> PlaceHashrateResaleRequestAsync(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));
            algorithm.ValidateNotNull(nameof(algorithm));
            toUser.ValidateNotNull(nameof(toUser));

            var parameters = new ParameterCollection()
            {
                { "userName", userName },
                { "algo", algorithm },
                { "startDate", DateTimeConverter.ConvertToMilliseconds(startDate)! },
                { "endDate", DateTimeConverter.ConvertToMilliseconds(endDate)! },
                { "toPoolUser", toUser },
                { "hashRate", hashRate }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/mining/hash-transfer/config", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<int>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<int>(default);

            if (result.Data?.Code != 0)
                return result.AsError<int>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Cancel Hashrate Resale Configuration

        /// <inheritdoc />
        public async Task<WebCallResult<bool>> CancelHashrateResaleRequestAsync(int configId, string userName, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new ParameterCollection()
            {
                { "configId", configId },
                { "userName", userName }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/mining/hash-transfer/config/cancel", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<bool>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<bool>(default);

            if (result.Data?.Code != 0)
                return result.AsError<bool>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Get Mining Account Earnings
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMiningEarnings>> GetMiningAccountEarningsAsync(string algo, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "algo", algo }
            };
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/mining/payment/uid", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningEarnings>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceMiningEarnings>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceMiningEarnings>(new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return result.As(result.Data.Data);
        }

        #endregion Acquiring CoinName
    }
}
