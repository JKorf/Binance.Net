namespace Binance.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class BinanceApiAddresses
    {
        /// <summary>
        /// The address used by the BinanceClient for the Spot API
        /// </summary>
        public string RestClientAddress { get; set; } = string.Empty;
        /// <summary>
        /// The address used by the BinanceSocketClient for the Spot streams
        /// </summary>
        public string SocketClientStreamAddress { get; set; } = string.Empty;
        /// <summary>
        /// The address used by the BinanceSocketClient for the Spot API
        /// </summary>
        public string SocketClientApiAddress { get; set; } = string.Empty;
        /// <summary>
        /// The address used by the BinanceSocketClient for connecting to the BLVT streams
        /// </summary>
        public string? BlvtSocketClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceClient for the USD futures API
        /// </summary>
        public string? UsdFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the USD futures streams
        /// </summary>
        public string? UsdFuturesSocketClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the USD futures API
        /// </summary>
        public string? UsdFuturesSocketApiClientAddress { get; set; }

        /// <summary>
        /// The address used by the BinanceClient for the COIN futures API
        /// </summary>
        public string? CoinFuturesRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the Coin futures streams
        /// </summary>
        public string? CoinFuturesSocketClientAddress { get; set; }
        /// <summary>
        /// The address used by the BinanceSocketClient for the Coin futures API
        /// </summary>
        public string? CoinFuturesSocketApiClientAddress { get; set; }

        /// <summary>
        /// The default addresses to connect to the binance.com API
        /// </summary>
        public static BinanceApiAddresses Default = new BinanceApiAddresses
        {
            RestClientAddress = "https://api.binance.com",
            SocketClientStreamAddress = "wss://stream.binance.com:9443/",
            SocketClientApiAddress = "wss://ws-api.binance.com:443/",
            BlvtSocketClientAddress = "wss://nbstream.binance.com/",
            UsdFuturesRestClientAddress = "https://fapi.binance.com",
            UsdFuturesSocketClientAddress = "wss://fstream.binance.com/",
            UsdFuturesSocketApiClientAddress = "wss://ws-fapi.binance.com/",
            CoinFuturesRestClientAddress = "https://dapi.binance.com",
            CoinFuturesSocketClientAddress = "wss://dstream.binance.com/",
            CoinFuturesSocketApiClientAddress = "wss://ws-dapi.binance.com/",
        };

        /// <summary>
        /// The addresses to connect to the binance testnet
        /// </summary>
        public static BinanceApiAddresses TestNet = new BinanceApiAddresses
        {
            RestClientAddress = "https://testnet.binance.vision",
            SocketClientStreamAddress = "wss://testnet.binance.vision",
            SocketClientApiAddress = "wss://testnet.binance.vision",
            BlvtSocketClientAddress = "wss://fstream.binancefuture.com",
            UsdFuturesRestClientAddress = "https://testnet.binancefuture.com",
            UsdFuturesSocketClientAddress = "wss://fstream.binancefuture.com",
            UsdFuturesSocketApiClientAddress = "wss://testnet.binancefuture.com",
            CoinFuturesRestClientAddress = "https://testnet.binancefuture.com",
            CoinFuturesSocketClientAddress = "wss://dstream.binancefuture.com",
            CoinFuturesSocketApiClientAddress = "wss://testnet.binancefuture.com",
        };

        /// <summary>
        /// The addresses to connect to binance.us. (binance.us futures not are not available)
        /// </summary>
        public static BinanceApiAddresses Us = new BinanceApiAddresses
        {
            RestClientAddress = "https://api.binance.us",
            SocketClientApiAddress = "wss://ws-api.binance.us:443",
            SocketClientStreamAddress = "wss://stream.binance.us:9443",
        };
    }
}
