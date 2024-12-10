using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientCoinFuturesApiAgent : IBinanceRestClientCoinFuturesApiAgent
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientCoinFuturesApi _baseClient;

        internal BinanceRestClientCoinFuturesApiAgent(BinanceRestClientCoinFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region If New User

        public async Task<WebCallResult<BinanceIfNewUser>> IfNewUser(string brokerId, int? receiveWindow = null, CancellationToken ct = default)
        {
            brokerId.ValidateNotNull(nameof(brokerId));

            var parameters = new ParameterCollection
            {
                { "brokerId", brokerId }
            };
            parameters.AddEnum("type", IfNewUserMarginedFuturesType.CoinMarginedFutures);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/apiReferral/ifNewUser", BinanceExchange.RateLimiter.SpotRestIp, 100, true);
            return await _baseClient.SendAsync<BinanceIfNewUser>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

    }
}
