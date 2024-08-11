using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    public interface IBinanceRestClientUsdFuturesApiShared :
        ITickerRestClient,
        IFuturesSymbolRestClient,
        IKlineRestClient,
        ITradeRestClient
    {
    }
}
