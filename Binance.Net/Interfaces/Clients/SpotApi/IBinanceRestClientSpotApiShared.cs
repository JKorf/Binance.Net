using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    public interface IBinanceRestClientSpotApiShared:
        ITickerClient,
        ISpotSymbolClient,
        IKlineClient,
        ITradeClient
    {
    }
}
