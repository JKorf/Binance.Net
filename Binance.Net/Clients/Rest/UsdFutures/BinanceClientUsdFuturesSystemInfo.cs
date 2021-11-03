using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

namespace Binance.Net.Clients.Rest.UsdFutures
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientUsdFuturesSystemInfo : IBinanceClientUsdFuturesSystemInfo
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string tradingStatusEndpoint = "apiTradingStatus";
        private const string exchangeInfoEndpoint = "exchangeInfo";

        private const string api = "fapi";

        private readonly Log _log;

        private readonly BinanceClientUsdFutures _baseClient;

        internal BinanceClientUsdFuturesSystemInfo(Log log, BinanceClientUsdFutures baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Trading status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesTradingStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceFuturesTradingStatus>(_baseClient.GetUrl(tradingStatusEndpoint, api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);

        }
        #endregion

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
        public async Task<WebCallResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesUsdtExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
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
