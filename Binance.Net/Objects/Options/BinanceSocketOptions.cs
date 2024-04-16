using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for the BinanceSocketClient
    /// </summary>
    public class BinanceSocketOptions : SocketExchangeOptions<BinanceEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BinanceSocketOptions Default { get; set; } = new BinanceSocketOptions()
        {
            Environment = BinanceEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// Options for the Spot API
        /// </summary>
        public BinanceSocketApiOptions SpotOptions { get; private set; } = new BinanceSocketApiOptions();

        /// <summary>
        /// Options for the Usd Futures API
        /// </summary>
        public BinanceSocketApiOptions UsdFuturesOptions { get; private set; } = new BinanceSocketApiOptions();

        /// <summary>
        /// Options for the Coin Futures API
        /// </summary>
        public BinanceSocketApiOptions CoinFuturesOptions { get; private set; } = new BinanceSocketApiOptions(); 

        internal BinanceSocketOptions Copy()
        {
            var options = Copy<BinanceSocketOptions>();
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
