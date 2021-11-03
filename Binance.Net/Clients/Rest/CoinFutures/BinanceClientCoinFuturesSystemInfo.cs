using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.Margin;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

namespace Binance.Net.Clients.Rest.CoinFutures
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientCoinFuturesSystemInfo : IBinanceClientCoinFuturesSystemInfo
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";

        private const string api = "dapi";
        private const string signedVersion = "1";

        private readonly Log _log;

        private readonly BinanceClientCoinFutures _baseClient;

        internal BinanceClientCoinFuturesSystemInfo(Log log, BinanceClientCoinFutures baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(pingEndpoint, api, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return new WebCallResult<long>(result.ResponseStatusCode, result.ResponseHeaders, result.Error == null ? sw.ElapsedMilliseconds : 0, result.Error);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrl(checkTimeEndpoint, api, "1");
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesCoinExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient.ExchangeInfo = exchangeInfoResult.Data;
            _baseClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion
    }
}
