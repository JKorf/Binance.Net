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
        #region fields
        #endregion

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
        /// Create a new instance of the BinanceRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceSocketClient(Action<BinanceSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public BinanceSocketClient(IOptions<BinanceSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Binance")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new BinanceSocketClientSpotApi(_logger, options.Value));
            UsdFuturesApi = AddApiClient(new BinanceSocketClientUsdFuturesApi(_logger, options.Value));
            CoinFuturesApi = AddApiClient(new BinanceSocketClientCoinFuturesApi(_logger, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BinanceSocketOptions> optionsDelegate)
        {
            var options = BinanceSocketOptions.Default.Copy();
            optionsDelegate(options);
            BinanceSocketOptions.Default = options;
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
