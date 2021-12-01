using System;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    public interface IBinanceClientGeneralApi : IDisposable
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
        /// Endpoints related to lending/saving
        /// </summary>
        public IBinanceClientGeneralApiLending Lending { get; }

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
