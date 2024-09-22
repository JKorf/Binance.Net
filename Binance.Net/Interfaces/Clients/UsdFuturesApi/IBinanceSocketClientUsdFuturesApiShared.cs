using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Futures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Shared interface for USD-M Futures socket API usage
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApiShared:
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IOrderBookSocketClient,
        IKlineSocketClient,
        IBalanceSocketClient,
        IPositionSocketClient,
        IFuturesOrderSocketClient
    {
    }
}
