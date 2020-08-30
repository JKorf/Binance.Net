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
        private const string futuresAccountBalanceEndpoint = "balance";

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
        #region Future Account Balance

        /// <summary>
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> GetBalance(long? receiveWindow = null, CancellationToken ct = default) => GetBalanceAsync(receiveWindow, ct).Result;

        /// <summary>.
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>> GetBalanceAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesAccountBalance>>(FuturesClient.GetUrl(futuresAccountBalanceEndpoint, Api, Api == "dapi" ? "1": "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
