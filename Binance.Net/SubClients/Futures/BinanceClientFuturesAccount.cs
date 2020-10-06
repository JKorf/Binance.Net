using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures
{
    /// <summary>
    /// Futures account endpoints
    /// </summary>
    public abstract class BinanceClientFuturesAccount
    {

        /// <summary>
        /// Api path
        /// </summary>
        protected abstract string Api { get; }
        /// <summary>
        /// Signed version
        /// </summary>
        protected const string SignedV2 = "2";
        
        /// <summary>
        /// Base client
        /// </summary>
        protected readonly BinanceClient BaseClient;
        /// <summary>
        /// Futures client
        /// </summary>
        protected readonly BinanceClientFutures FuturesClient;

        internal BinanceClientFuturesAccount(BinanceClient baseClient, BinanceClientFutures futuresClient)
        {
            BaseClient = baseClient;
            FuturesClient = futuresClient;
        }
    }
}
