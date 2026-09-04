using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;

namespace Binance.Net.Clients
{
    /// <inheritdoc />
    public class BinanceSharedApiClient : IBinanceSharedApiClient
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
            IBinanceSocketClient socketClient)
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
