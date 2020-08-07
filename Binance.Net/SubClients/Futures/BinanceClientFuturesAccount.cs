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
    public class BinanceClientFuturesAccount : IBinanceClientFuturesAccount
    {
        private const string api = "fapi";
        private const string signedV2 = "2";

        private const string futuresAccountBalanceEndpoint = "balance";
        private const string accountInfoEndpoint = "account";


        private readonly BinanceClient _baseClient;
        private readonly BinanceClientFutures _futuresClient;

        internal BinanceClientFuturesAccount(BinanceClient baseClient, BinanceClientFutures futuresClient)
        {
            _baseClient = baseClient;
            _futuresClient = futuresClient;
        }
        #region Future Account Balance

        /// <summary>
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> GetFuturesAccountBalance(long? receiveWindow = null, CancellationToken ct = default) => GetFuturesAccountBalanceAsync(receiveWindow, ct).Result;

        /// <summary>.
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>> GetFuturesAccountBalanceAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _futuresClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesAccountBalance>>(_baseClient.GetUrl(true, futuresAccountBalanceEndpoint, api, signedV2), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Account Information

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public WebCallResult<BinanceFuturesAccountInfo> GetAccountInfo(long? receiveWindow = null, CancellationToken ct = default) => GetAccountInfoAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _futuresClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceFuturesAccountInfo>(_baseClient.GetUrl(true, accountInfoEndpoint, api, signedV2), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
