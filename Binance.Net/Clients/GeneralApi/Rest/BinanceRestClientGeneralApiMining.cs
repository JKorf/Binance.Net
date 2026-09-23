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
        public async Task<HttpResult<BinanceMiningCoin[]>> GetMiningCoinListAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/pub/coinList", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningCoin[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMiningCoin[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMiningCoin[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion Acquiring CoinName

        #region Acquiring Algorithm 
        /// <inheritdoc />
        public async Task<HttpResult<BinanceMiningAlgorithm[]>> GetMiningAlgorithmListAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/pub/algoList", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningAlgorithm[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMiningAlgorithm[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMiningAlgorithm[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Request Detail Miner List

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMinerDetails[]>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));
            workerName.ValidateNotNull(nameof(workerName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName},
                {"workerName", workerName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/worker/detail", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMinerDetails[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMinerDetails[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMinerDetails[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Request Miner List
        /// <inheritdoc />
        public async Task<HttpResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sortAscending", sortAscending == null ? null : sortAscending == true ? "1" : "0");
            parameters.AddOptionalParameter("sortColumn", sortColumn);
            parameters.Add("workerStatus", workerStatus);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/worker/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMinerList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMinerList>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMinerList>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Revenue List
        /// <inheritdoc />
        public async Task<HttpResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/payment/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceRevenueList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceRevenueList>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceRevenueList>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Other Revenue List
        /// <inheritdoc />
        public async Task<HttpResult<BinanceOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startDate));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endDate));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/payment/other", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceOtherRevenueList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceOtherRevenueList>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceOtherRevenueList>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion

        #region Statistics list
        /// <inheritdoc />
        public async Task<HttpResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/statistics/user/status", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningStatistic>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMiningStatistic>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMiningStatistic>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion

        #region Account List
        /// <inheritdoc />
        public async Task<HttpResult<BinanceMiningAccount[]>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default)
        {
            algorithm.ValidateNotNull(nameof(algorithm));
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                {"algo", algorithm},
                {"userName", userName}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/statistics/user/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningAccount[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMiningAccount[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMiningAccount[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion

        #region Hashrate Resale List
        /// <inheritdoc />
        public async Task<HttpResult<BinanceHashrateResaleList>> GetHashrateResaleListAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/hash-transfer/config/details/list", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceHashrateResaleList>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceHashrateResaleList>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceHashrateResaleList>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Details
        /// <inheritdoc />
        public async Task<HttpResult<BinanceHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "configId", configId.ToString(CultureInfo.InvariantCulture) },
                { "userName", userName }
            };

            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/hash-transfer/profit/details", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceHashrateResaleDetails>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceHashrateResaleDetails>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceHashrateResaleDetails>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Hashrate Resale Request

        /// <inheritdoc />
        public async Task<HttpResult<int>> PlaceHashrateResaleRequestAsync(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));
            algorithm.ValidateNotNull(nameof(algorithm));
            toUser.ValidateNotNull(nameof(toUser));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "userName", userName },
                { "algo", algorithm },
                { "startDate", DateTimeConverter.ConvertToMilliseconds(startDate)! },
                { "endDate", DateTimeConverter.ConvertToMilliseconds(endDate)! },
                { "toPoolUser", toUser },
                { "hashRate", hashRate }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/mining/hash-transfer/config", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<int>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<int>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<int>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Cancel Hashrate Resale Configuration

        /// <inheritdoc />
        public async Task<HttpResult<bool>> CancelHashrateResaleRequestAsync(int configId, string userName, CancellationToken ct = default)
        {
            userName.ValidateNotNull(nameof(userName));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "configId", configId },
                { "userName", userName }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/mining/hash-transfer/config/cancel", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<bool>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<bool>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<bool>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Mining Account Earnings
        /// <inheritdoc />
        public async Task<HttpResult<BinanceMiningEarnings>> GetMiningAccountEarningsAsync(string algo, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "algo", algo }
            };
            parameters.AddOptionalParameter("startDate", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endDate", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("pageIndex", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pageSize", pageSize?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/mining/payment/uid", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceMiningEarnings>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceMiningEarnings>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceMiningEarnings>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion Acquiring CoinName
    }
}
