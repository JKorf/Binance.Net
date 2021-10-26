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

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinAccount(BinanceClient baseClient, BinanceClientFutures futuresClient): base(baseClient, futuresClient) { }

        #region Account Information

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinAccountBalance>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
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
    }
}
