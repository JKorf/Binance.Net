using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    public interface IBinanceRestClientCoinFuturesApiShared :
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
