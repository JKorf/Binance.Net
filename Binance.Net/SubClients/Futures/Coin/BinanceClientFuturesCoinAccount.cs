using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures account endpoints
    /// </summary>
    public class BinanceClientFuturesCoinAccount : BinanceClientFuturesAccount, IBinanceClientFuturesCoinAccount
    {
        private const string accountInfoEndpoint = "account";
        private const string futuresAccountBalanceEndpoint = "balance";
        private const string futuresAccountUserCommissionRateEndpoint = "commissionRate";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinAccount(BinanceClient baseClient, BinanceClientFutures futuresClient): base(baseClient, futuresClient) { }

        #region Account Information

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<BinanceFuturesCoinAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesCoinAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesCoinAccountInfo>(FuturesClient.GetUrl(accountInfoEndpoint, Api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Future Account Balance

        /// <summary>.
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinAccountBalance>>> GetBalanceAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesCoinAccountBalance>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinAccountBalance>>(FuturesClient.GetUrl(futuresAccountBalanceEndpoint, Api, Api == "dapi" ? "1" : "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Future Account User Commission Rate

        /// <summary>
        /// Gets account commission rates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>User commission rate information</returns>
        public async Task<WebCallResult<BinanceFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesAccountUserCommissionRate>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol},
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesAccountUserCommissionRate>(FuturesClient.GetUrl(futuresAccountUserCommissionRateEndpoint, Api, Api == "dapi" ? "1" : "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
  }
}
