using CryptoExchange.Net.Interfaces;
using System;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance general API endpoints
    /// </summary>
    public interface IBinanceClientGeneralApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        public IBinanceClientGeneralApiBrokerage Brokerage { get; }

        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        public IBinanceClientGeneralApiFutures Futures { get; }

        /// <summary>
        /// Endpoints related to savings
        /// </summary>
        public IBinanceClientGeneralApiSavings Savings { get; }

        /// <summary>
        /// Endpoints related to crypto loans
        /// </summary>
        public IBinanceClientGeneralApiCryptoLoans CryptoLoans { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IBinanceClientGeneralApiMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IBinanceClientGeneralApiSubAccount SubAccount { get; }
    }
}
