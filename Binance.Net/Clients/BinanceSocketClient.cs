using Binance.Net.Clients.CoinFuturesApi;
using Binance.Net.Clients.SpotApi;
using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Binance.Net.Clients
{
    /// <inheritdoc cref="IBinanceSocketClient" />
    public class BinanceSocketClient : BaseSocketClient, IBinanceSocketClient
    {
        /// <summary>
        /// Default options
        /// </summary>
        public static BinanceSocketOptions DefaultOptions { get; private set; } = new BinanceSocketOptions()
        {
            Environment = BinanceEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        #region Api clients

        /// <inheritdoc />
        public IBinanceSocketClientSpotApi SpotApi { get; set; }

        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesApi UsdFuturesApi { get; set; }

        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesApi CoinFuturesApi { get; set; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceSocketClient(Action<BinanceSocketOptions>? optionsDelegate = null) : base(null, "Binance")
        {
            var options = DefaultOptions.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            SpotApi = AddApiClient(new BinanceSocketClientSpotApi(_logger, options));
            UsdFuturesApi = AddApiClient(new BinanceSocketClientUsdFuturesApi(_logger, options));
            CoinFuturesApi = AddApiClient(new BinanceSocketClientCoinFuturesApi(_logger, options));
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionConfig">Option configuration delegate</param>
        [ActivatorUtilitiesConstructor]
        public BinanceSocketClient(IOptions<BinanceSocketOptions> optionConfig, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Binance")
        {
            var options = optionConfig?.Value ?? DefaultOptions.Copy();
            Initialize(options);

            SpotApi = AddApiClient(new BinanceSocketClientSpotApi(_logger, options));
            UsdFuturesApi = AddApiClient(new BinanceSocketClientUsdFuturesApi(_logger, options));
            CoinFuturesApi = AddApiClient(new BinanceSocketClientCoinFuturesApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BinanceSocketOptions> optionsDelegate)
        {
            optionsDelegate(DefaultOptions);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            UsdFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
        }
    }
}
