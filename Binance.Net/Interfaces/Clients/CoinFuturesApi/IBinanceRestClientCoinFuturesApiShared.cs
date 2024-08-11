using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    public interface IBinanceRestClientCoinFuturesApiShared :
        ITickerRestClient,
        IFuturesSymbolRestClient,
        IKlineRestClient,
        ITradeRestClient
    {
    }
}
