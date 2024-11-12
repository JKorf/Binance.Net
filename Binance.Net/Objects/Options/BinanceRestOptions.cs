using Binance.Net.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for the BinanceRestClient
    /// </summary>
    public class BinanceRestOptions : RestExchangeOptions<BinanceEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static BinanceRestOptions Default { get; set; } = new BinanceRestOptions()
        {
            Environment = BinanceEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Spot API options
        /// </summary>
        public BinanceRestApiOptions SpotOptions { get; private set; } = new BinanceRestApiOptions();

        /// <summary>
        /// Usd futures API options
        /// </summary>
        public BinanceRestApiOptions UsdFuturesOptions { get; private set; } = new BinanceRestApiOptions();

        /// <summary>
        /// Coin futures API options
        /// </summary>
        public BinanceRestApiOptions CoinFuturesOptions { get; private set; } = new BinanceRestApiOptions();

        internal BinanceRestOptions Copy() => Set(Copy<BinanceRestOptions>());

        internal BinanceRestOptions Set(BinanceRestOptions targetOptions)
        {
            targetOptions = base.Set<BinanceRestOptions>(targetOptions);
            targetOptions.ReceiveWindow = ReceiveWindow;
            targetOptions.SpotOptions = SpotOptions.Copy();
            targetOptions.UsdFuturesOptions = UsdFuturesOptions.Copy();
            targetOptions.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return targetOptions;
        }
    }
}
