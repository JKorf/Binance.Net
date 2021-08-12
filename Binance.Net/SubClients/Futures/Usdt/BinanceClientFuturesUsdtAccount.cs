using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures account endpoints
    /// </summary>
    public class BinanceClientFuturesUsdtAccount : BinanceClientFuturesAccount, IBinanceClientFuturesUsdtAccount
    {
        private const string accountInfoEndpoint = "account";
        private const string futuresAccountBalanceEndpoint = "balance";
        private const string futuresAccountMultiAssetsModeEndpoint = "multiAssetsMargin";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        internal BinanceClientFuturesUsdtAccount(BinanceClient baseClient, BinanceClientFutures futuresClient) : base(baseClient, futuresClient) { }

        #region Account Information

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesAccountInfo>(FuturesClient.GetUrl(accountInfoEndpoint, Api, SignedV2), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Future Account Balance

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

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesAccountBalance>>(FuturesClient.GetUrl(futuresAccountBalanceEndpoint, Api, Api == "dapi" ? "1" : "2"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Future Account Balance

        /// <summary>.
        /// Get user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Multi asset mode</returns>
        public async Task<WebCallResult<BinanceFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesMultiAssetMode>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesMultiAssetMode>(FuturesClient.GetUrl(futuresAccountMultiAssetsModeEndpoint, Api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <summary>.
        /// Set user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// </summary>
        /// <param name="enabled">Enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Succes</returns>
        public async Task<WebCallResult<BinanceResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "multiAssetsMargin", enabled.ToString() },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceResult>(FuturesClient.GetUrl(futuresAccountMultiAssetsModeEndpoint, Api, "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion


    }
}
