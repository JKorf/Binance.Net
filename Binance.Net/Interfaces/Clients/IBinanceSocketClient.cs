﻿using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Binance websocket API
    /// </summary>
    public interface IBinanceSocketClient: ISocketClient
    {
        /// <summary>
        /// Coin futures streams
        /// </summary>
        IBinanceSocketClientCoinFuturesStreams CoinFuturesStreams { get; }
        /// <summary>
        /// Spot streams
        /// </summary>
        IBinanceSocketClientSpotStreams SpotStreams { get; }
        /// <summary>
        /// Usd futures streams
        /// </summary>
        IBinanceSocketClientUsdFuturesStreams UsdFuturesStreams { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(BinanceApiCredentials credentials);
    }
}
