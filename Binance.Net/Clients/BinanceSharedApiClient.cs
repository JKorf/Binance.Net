using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;

namespace Binance.Net.Clients
{
    /// <inheritdoc />
    public class BinanceSharedApiClient : SharedApiClientBase, IBinanceSharedApiClient
    {
        /// <inheritdoc />
        public IBinanceRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesSharedApi UsdFuturesRest { get; }
        /// <inheritdoc />
        public IBinanceRestClientCoinFuturesSharedApi CoinFuturesRest { get; }
        /// <inheritdoc />
        public IBinanceSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IBinanceSocketClientUsdFuturesSharedApi UsdFuturesSocket { get; }
        /// <inheritdoc />
        public IBinanceSocketClientCoinFuturesSharedApi CoinFuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceSharedApiClient(
            IBinanceRestClient restClient,
            IBinanceSocketClient socketClient,
            IOptions<BinanceOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                  restClient.SpotApi.SharedApi,
                  restClient.UsdFuturesApi.SharedApi,
                  restClient.CoinFuturesApi.SharedApi,
                  socketClient.SpotApi.SharedApi,
                  socketClient.UsdFuturesApi.SharedApi,
                  socketClient.CoinFuturesApi.SharedApi)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdFuturesRest = restClient.UsdFuturesApi.SharedApi;
            CoinFuturesRest = restClient.CoinFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            UsdFuturesSocket = socketClient.UsdFuturesApi.SharedApi;
            CoinFuturesSocket = socketClient.CoinFuturesApi.SharedApi;
        }
    }
}
