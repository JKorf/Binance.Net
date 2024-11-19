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
        internal static BinanceSocketOptions Default { get; set; } = new BinanceSocketOptions()
        {
            Environment = BinanceEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSocketOptions()
        {
            Default?.Set(this);
        }

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

        internal BinanceSocketOptions Set(BinanceSocketOptions targetOptions)
        {
            targetOptions = base.Set<BinanceSocketOptions>(targetOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            targetOptions.UsdFuturesOptions = UsdFuturesOptions.Set(targetOptions.UsdFuturesOptions);
            targetOptions.CoinFuturesOptions = CoinFuturesOptions.Set(targetOptions.CoinFuturesOptions);
            return targetOptions;
        }
    }
}
