namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance general API endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        public IBinanceRestClientGeneralApiBrokerage Brokerage { get; }

        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        public IBinanceRestClientGeneralApiFutures Futures { get; }

        /// <summary>
        /// Endpoints related to crypto loans
        /// </summary>
        public IBinanceRestClientGeneralApiLoans CryptoLoans { get; }

        /// <summary>
        /// Endpoints related to auto invest
        /// </summary>
        public IBinanceRestClientGeneralApiAutoInvest AutoInvest { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IBinanceRestClientGeneralApiMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IBinanceRestClientGeneralApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to staking
        /// </summary>
        IBinanceRestClientGeneralApiStaking Staking { get; }

        /// <summary>
        /// Endpoints related to Binance Simple Earn
        /// </summary>
        IBinanceRestClientGeneralApiSimpleEarn SimpleEarn { get; }

        /// <summary>
        /// Endpoints related to Binance Copy Trading
        /// </summary>
        IBinanceRestClientGeneralApiCopyTrading CopyTrading { get; }
    }
}
