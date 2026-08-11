using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot.Affiliate;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceRestClientSpotApiAffiliate : IBinanceRestClientSpotApiAffiliate
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientSpotApi _baseClient;

        internal BinanceRestClientSpotApiAffiliate(BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceAffiliateResponse<BinanceInviteePerformance>>> GetPerformanceByInviteeAsync(string userId, DateTime? startDate = null, DateTime? endDate = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("userId", userId);
            parameters.AddOptionalParameter("startDate", startDate?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("endDate", endDate?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/affiliate/performance/invitee", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAffiliateResponse<BinanceInviteePerformance>>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceAffiliateResponse<BinanceCodePerformance>>> GetPerformanceByCodeAsync(string code, DateTime? startDate = null, DateTime? endDate = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("code", code);
            parameters.AddOptionalParameter("startTime", startDate?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("endDate", endDate?.ToString("yyyy-MM-dd"));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/affiliate/performance/code", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAffiliateResponse<BinanceCodePerformance>>(request, parameters, ct).ConfigureAwait(false);
        }
    }
}
