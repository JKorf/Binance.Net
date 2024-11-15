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
        /// Usd futures Socket address for the request API
        /// </summary>
        public string? UsdFuturesSocketApiAddress { get; }

        /// <summary>
        /// Coin futures Rest address
        /// </summary>
        public string? CoinFuturesRestAddress { get; }

        /// <summary>
        /// Coin futures Socket address
        /// </summary>
        public string? CoinFuturesSocketAddress { get; }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BinanceEnvironment(): base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <inheritdoc />
        public override T GetEnvironmentByName<T>(string? name)
        {
            var result = name switch
            {
                TradeEnvironmentNames.Live => Live,
                TradeEnvironmentNames.Testnet => Testnet,
                "us" => Us,
                "" => Live,
                null => Live,
                _ => default
            };
            return (T)(TradeEnvironment)result!;
        }

        internal BinanceEnvironment(
            string name, 
            string spotRestAddress, 
            string spotSocketStreamAddress, 
            string spotSocketApiAddress,
            string? blvtSocketAddress, 
            string? usdFuturesRestAddress, 
            string? usdFuturesSocketAddress,
            string? usdFuturesSocketApiAddress,
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
            UsdFuturesSocketApiAddress = usdFuturesSocketApiAddress;
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
                                     BinanceApiAddresses.Default.UsdFuturesSocketApiClientAddress,
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
                                     BinanceApiAddresses.TestNet.UsdFuturesSocketApiClientAddress,
                                     BinanceApiAddresses.TestNet.CoinFuturesRestClientAddress,
                                     BinanceApiAddresses.TestNet.CoinFuturesSocketClientAddress);

        /// <summary>
        /// Binance.us environment
        /// </summary>
        public static BinanceEnvironment Us { get; }
            = new BinanceEnvironment("us",
                                     BinanceApiAddresses.Us.RestClientAddress,
                                     BinanceApiAddresses.Us.SocketClientStreamAddress,
                                     BinanceApiAddresses.Us.SocketClientApiAddress,
                                     null,
                                     null,
                                     null,
                                     null,
                                     null,
                                     null);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static BinanceEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress,
                        string spotSocketApiAddress,
                        string? blvtSocketAddress,
                        string? usdFuturesRestAddress,
                        string? usdFuturesSocketAddress,
                        string? usdFuturesSocketApiAddress,
                        string? coinFuturesRestAddress,
                        string? coinFuturesSocketAddress)
            => new BinanceEnvironment(name, spotRestAddress, spotSocketStreamsAddress, spotSocketApiAddress, blvtSocketAddress, usdFuturesRestAddress, usdFuturesSocketAddress, usdFuturesSocketApiAddress, coinFuturesRestAddress, coinFuturesSocketAddress);
    }
}
