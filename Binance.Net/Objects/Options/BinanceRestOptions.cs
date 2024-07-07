using CryptoExchange.Net.Objects.Options;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Options for the BinanceRestClient
    /// </summary>
    public class BinanceRestOptions : RestExchangeOptions<BinanceEnvironment>
    {
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

        internal BinanceRestOptions Copy()
        {
            var options = Copy<BinanceRestOptions>();
            options.ReceiveWindow = ReceiveWindow;
            options.SpotOptions = SpotOptions.Copy();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy();
            options.CoinFuturesOptions = CoinFuturesOptions.Copy();
            return options;
        }
    }
}
