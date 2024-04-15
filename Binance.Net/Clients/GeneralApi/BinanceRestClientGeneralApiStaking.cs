using Binance.Net.Objects.Models.Spot.Staking;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class BinanceRestClientGeneralApiStaking : IBinanceRestClientGeneralApiStaking
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiStaking(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/eth-staking/eth/stake", BinanceExchange.RateLimiter.SpotRestIp, 150, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceStakingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/eth-staking/eth/redeem", BinanceExchange.RateLimiter.SpotRestIp, 150, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceStakingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/eth/history/stakingHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceEthStakingHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/eth/history/redemptionHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceEthRedemptionHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/eth/history/rewardsHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceEthRewardsHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/eth/quota", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceEthStakingQuota>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/eth/history/rateHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceBethRateHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/account", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceEthStakingAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/eth-staking/wbeth/wrap", BinanceExchange.RateLimiter.SpotRestIp, 150, true, endpointLimitCount: 1, endpointLimitPeriod: TimeSpan.FromSeconds(3));
            return await _baseClient.SendAsync<BinanceStakingResult>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/wbeth/history/wrapHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceBethWrapHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/eth-staking/wbeth/history/unwrapHistory", BinanceExchange.RateLimiter.SpotRestIp, 150, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceBethWrapHistory>>(request, parameters, ct).ConfigureAwait(false);
        }
    }
}
