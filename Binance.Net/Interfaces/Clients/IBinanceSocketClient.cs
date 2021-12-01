using Binance.Net.Interfaces.Clients.Socket;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients
{
    public interface IBinanceSocketClient: ISocketClient
    {
        IBinanceSocketClientCoinFuturesMarket CoinFuturesStreams { get; }
        IBinanceSocketClientSpotMarket SpotStreams { get; }
        IBinanceSocketClientUsdFuturesMarket UsdFuturesStreams { get; }
    }
}
