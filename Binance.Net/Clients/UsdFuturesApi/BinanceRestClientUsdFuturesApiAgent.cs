﻿using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientUsdFuturesApiAgent : IBinanceRestClientUsdFuturesApiAgent
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientUsdFuturesApi _baseClient;

        internal BinanceRestClientUsdFuturesApiAgent(BinanceRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region If New User

        public async Task<WebCallResult<BinanceFuturesIfNewUser>> GetIfNewUserAsync(string brokerId, int? receiveWindow = null, CancellationToken ct = default)
        {
            brokerId.ValidateNotNull(nameof(brokerId));

            var parameters = new ParameterCollection
            {
                { "brokerId", brokerId }
            };
            parameters.AddEnum("type", IfNewUserMarginedFuturesType.UsdtMarginedFutures);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/apiReferral/ifNewUser", BinanceExchange.RateLimiter.FuturesRest, 100, true);
            return await _baseClient.SendAsync<BinanceFuturesIfNewUser>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
