using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class BinanceApiAddresses
    {
        /// <summary>
        /// The address used by the BinanceClient for the Spot API
        /// </summary>
        public string RestClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the Spot API
        /// </summary>
        public string SocketClientAddress { get; set; }

        /// <summary>
        /// The address used by the BinanceClient for the USD futures API
        /// </summary>
        public string? UsdtFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the USD futures API
        /// </summary>
        public string? UsdtFuturesSocketClientAddress { get; set; }

        /// <summary>
        /// The address used by the BinanceClient for the COIN futures API
        /// </summary>
        public string? CoinFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the Coin futures API
        /// </summary>
        public string? CoinFuturesSocketClientAddress { get; set; }

        /// <summary>
        /// The default addresses to connect to the binance.com API
        /// </summary>
        public static BinanceApiAddresses Default = new BinanceApiAddresses
        {
            RestClientAddress = "https://api.binance.com",
            SocketClientAddress = "wss://stream.binance.com:9443/",
            UsdtFuturesRestClientAddress = "https://fapi.binance.com",
            UsdtFuturesSocketClientAddress = "wss://fstream.binance.com/",
            CoinFuturesRestClientAddress = "https://dapi.binance.com",
            CoinFuturesSocketClientAddress = "wss://dstream.binance.com/",
        };

        /// <summary>
        /// The addresses to connect to the binance testnet
        /// </summary>
        public static BinanceApiAddresses TestNet = new BinanceApiAddresses
        {
            RestClientAddress = "https://testnet.binance.vision",
            SocketClientAddress = "wss://stream.binancefuture.com",
            UsdtFuturesRestClientAddress = "https://testnet.binancefuture.com",
            UsdtFuturesSocketClientAddress = "wss://fstream.binancefuture.com",
            CoinFuturesRestClientAddress = "https://testnet.binancefuture.com",
            CoinFuturesSocketClientAddress = "wss://dstream.binancefuture.com",
        };

        /// <summary>
        /// The addresses to connect to binance.us. (binance.us futures not are not available)
        /// </summary>
        public static BinanceApiAddresses Us = new BinanceApiAddresses
        {
            RestClientAddress = "https://api.binance.us",
            SocketClientAddress = "wss://stream.binance.us:9443",
        };
    }
}
