using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Interfaces.Socket.Futures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    public interface IBinanceSocketClientCoinFuturesApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IOrderBookSocketClient,
        IKlineSocketClient,
        IFuturesOrderSocketClient,
        IBalanceSocketClient,
        IPositionSocketClient
    {
    }
}
