using CryptoExchange.Net.Interfaces;
using System;

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
        /// Endpoints related to savings
        /// </summary>
        public IBinanceRestClientGeneralApiSavings Savings { get; }

        /// <summary>
        /// Endpoints related to crypto loans
        /// </summary>
        public IBinanceClientGeneralApiCryptoLoans CryptoLoans { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IBinanceRestClientGeneralApiMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IBinanceRestClientGeneralApiSubAccount SubAccount { get; }
    }
}
