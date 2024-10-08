using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal class BinanceSocketClientUsdFuturesApiTrading : IBinanceSocketClientUsdFuturesApiTrading
    {
        private readonly BinanceSocketClientUsdFuturesApi _client;
        private readonly ILogger _logger;

        internal BinanceSocketClientUsdFuturesApiTrading(ILogger logger, BinanceSocketClientUsdFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #endregion

        #region Streams
        #endregion
    }
}
