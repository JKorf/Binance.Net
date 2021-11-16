using CryptoExchange.Net.Interfaces;

namespace Binance.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Spot endpoints
    /// </summary>
    public interface IBinanceClientSpot : IRestClient
    {
        /// <summary>
        /// Set the API creentials to use for this client
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiSecret">The API secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBinanceClientSpotAccount Account { get; }
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        public IBinanceClientSpotBrokerage Brokerage { get; }
        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        public IBinanceClientSpotFutures Futures { get; }
        /// <summary>
        /// Endpoints related to lending/saving
        /// </summary>
        public IBinanceClientSpotLending Lending { get; }
        /// <summary>
        /// Endpoints related to BLvt
        /// </summary>
        public IBinanceClientSpotLeveragedTokens LeveragedTokens { get; }
        /// <summary>
        /// Endpoints related to BSwap
        /// </summary>
        public IBinanceClientSpotLiquidSwap LiquidSwap { get; }
        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IBinanceClientSpotExchangeData ExchangeData { get; }
        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IBinanceClientSpotMining Mining { get; }
        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IBinanceClientSpotSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBinanceClientSpotTrading Trading { get; }
    }
}
