using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    public interface IBinanceRestClientUsdFuturesApiShared :
        ITickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        ITradeHistoryRestClient,
        ILeverageRestClient,
        IPositionRestClient,
        IMarkKlineRestClient,
        IIndexKlineRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient
    {
    }
}
