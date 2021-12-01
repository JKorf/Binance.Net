using Binance.Net.Interfaces.Clients.General;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients
{
    public interface IBinanceClient: IRestClient
    {
        IBinanceClientGeneral GeneralApi { get; }
        IBinanceClientCoinFuturesMarket CoinFuturesApi { get; }
        IBinanceClientSpotMarket SpotApi { get; }
        IBinanceClientUsdFuturesMarket UsdFuturesApi { get; }
    }
}
