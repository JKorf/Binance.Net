using Binance.Net.Interfaces.Clients.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Interfaces.Clients.General
{
    public interface IBinanceClientGeneral
    {
        /// <summary>
        /// Endpoints related to brokerage
        /// </summary>
        public IBinanceClientGeneralBrokerage Brokerage { get; }

        /// <summary>
        /// Endpoints related to futures account interactions
        /// </summary>
        public IBinanceClientGeneralFutures Futures { get; }

        /// <summary>
        /// Endpoints related to lending/saving
        /// </summary>
        public IBinanceClientGeneralLending Lending { get; }

        /// <summary>
        /// Endpoints related to mining
        /// </summary>
        public IBinanceClientGeneralMining Mining { get; }

        /// <summary>
        /// Endpoints related to requesting data for and controlling sub accounts
        /// </summary>
        public IBinanceClientGeneralSubAccount SubAccount { get; }
    }
}
