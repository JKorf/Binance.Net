using Binance.Net.Objects;

namespace Binance.Net
{
    /// <summary>
    /// Binance environments
    /// </summary>
    public class BinanceEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest API address
        /// </summary>
        public string SpotRestAddress { get; }

        /// <summary>
        /// Spot Socket Streams address
        /// </summary>
        public string SpotSocketStreamAddress { get; }

        /// <summary>
        /// Spot Socket API address
        /// </summary>
        public string SpotSocketApiAddress { get; }

        /// <summary>
        /// Blvt Socket API address
        /// </summary>
        public string? BlvtSocketAddress { get; }

        /// <summary>
        /// Usd futures Rest address
        /// </summary>
        public string? UsdFuturesRestAddress { get; }

        /// <summary>
        /// Usd futures Socket address
        /// </summary>
        public string? UsdFuturesSocketAddress { get; }

        /// <summary>
        /// Coin futures Rest address
        /// </summary>
        public string? CoinFuturesRestAddress { get; }

        /// <summary>
        /// Coin futures Socket address
        /// </summary>
        public string? CoinFuturesSocketAddress { get; }

        internal BinanceEnvironment(
            string name, 
            string spotRestAddress, 
            string spotSocketStreamAddress, 
            string spotSocketApiAddress,
            string? blvtSocketAddress, 
            string? usdFuturesRestAddress, 
            string? usdFuturesSocketAddress,
            string? coinFuturesRestAddress,
            string? coinFuturesSocketAddress) :
            base(name)
        {
            SpotRestAddress = spotRestAddress;
            SpotSocketStreamAddress = spotSocketStreamAddress;
            SpotSocketApiAddress = spotSocketApiAddress;
            BlvtSocketAddress = blvtSocketAddress;
            UsdFuturesRestAddress = usdFuturesRestAddress;
            UsdFuturesSocketAddress = usdFuturesSocketAddress;
            CoinFuturesRestAddress = coinFuturesRestAddress;
            CoinFuturesSocketAddress = coinFuturesSocketAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static BinanceEnvironment Live { get; } 
            = new BinanceEnvironment(TradeEnvironmentNames.Live, 
                                     BinanceApiAddresses.Default.RestClientAddress,
                                     BinanceApiAddresses.Default.SocketClientStreamAddress,
                                     BinanceApiAddresses.Default.SocketClientApiAddress,
                                     BinanceApiAddresses.Default.BlvtSocketClientAddress,
                                     BinanceApiAddresses.Default.UsdFuturesRestClientAddress,
                                     BinanceApiAddresses.Default.UsdFuturesSocketClientAddress,
                                     BinanceApiAddresses.Default.CoinFuturesRestClientAddress,
                                     BinanceApiAddresses.Default.CoinFuturesSocketClientAddress);

        /// <summary>
        /// Testnet environment
        /// </summary>
        public static BinanceEnvironment Testnet { get; }
            = new BinanceEnvironment(TradeEnvironmentNames.Testnet,
                                     BinanceApiAddresses.TestNet.RestClientAddress,
                                     BinanceApiAddresses.TestNet.SocketClientStreamAddress,
                                     BinanceApiAddresses.TestNet.SocketClientApiAddress,
                                     BinanceApiAddresses.TestNet.BlvtSocketClientAddress,
                                     BinanceApiAddresses.TestNet.UsdFuturesRestClientAddress,
                                     BinanceApiAddresses.TestNet.UsdFuturesSocketClientAddress,
                                     BinanceApiAddresses.TestNet.CoinFuturesRestClientAddress,
                                     BinanceApiAddresses.TestNet.CoinFuturesSocketClientAddress);

        /// <summary>
        /// Binance.us environment
        /// </summary>
        public static BinanceEnvironment Us { get; }
            = new BinanceEnvironment("Us",
                                     BinanceApiAddresses.Us.RestClientAddress,
                                     BinanceApiAddresses.Us.SocketClientStreamAddress,
                                     BinanceApiAddresses.Us.SocketClientApiAddress,
                                     null,
                                     null,
                                     null,
                                     null,
                                     null);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <param name="spotSocketApiAddress"></param>
        /// <param name="blvtSocketAddress"></param>
        /// <param name="usdFuturesRestAddress"></param>
        /// <param name="usdFuturesSocketAddress"></param>
        /// <param name="coinFuturesRestAddress"></param>
        /// <param name="coinFuturesSocketAddress"></param>
        /// <returns></returns>
        public static BinanceEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress,
                        string spotSocketApiAddress,
                        string? blvtSocketAddress,
                        string? usdFuturesRestAddress,
                        string? usdFuturesSocketAddress,
                        string? coinFuturesRestAddress,
                        string? coinFuturesSocketAddress)
            => new BinanceEnvironment(name, spotRestAddress, spotSocketStreamsAddress, spotSocketApiAddress, blvtSocketAddress, usdFuturesRestAddress, usdFuturesSocketAddress, coinFuturesRestAddress, coinFuturesSocketAddress);
    }
}
