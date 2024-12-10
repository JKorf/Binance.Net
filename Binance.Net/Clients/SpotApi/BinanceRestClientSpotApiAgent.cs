using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceRestClientSpotApiAgent : IBinanceRestClientSpotApiAgent
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientSpotApi _baseClient;

        internal BinanceRestClientSpotApiAgent(BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region If New User

        public async Task<WebCallResult<BinanceIfNewUser>> IfNewUser(string apiAgentCode, int? receiveWindow = null, CancellationToken ct = default)
        {
            apiAgentCode.ValidateNotNull(nameof(apiAgentCode));

            var parameters = new ParameterCollection
            {
                { "apiAgentCode", apiAgentCode }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/apiReferral/ifNewUser", BinanceExchange.RateLimiter.SpotRestIp, 100, true);
            return await _baseClient.SendAsync<BinanceIfNewUser>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
