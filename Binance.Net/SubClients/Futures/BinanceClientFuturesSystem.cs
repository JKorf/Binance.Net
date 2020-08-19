using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures
{
    /// <summary>
    /// Futures system endpoints
    /// </summary>
    public class BinanceClientFuturesSystem : IBinanceClientFuturesSystem
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";

        private const string api = "fapi";
        private const string publicVersion = "1";

        private readonly BinanceClient _baseClient;
        private readonly BinanceClientFutures _futuresClient;
        private readonly Log _log;

        internal BinanceClientFuturesSystem(Log log, BinanceClient baseClient, BinanceClientFutures futuresClient)
        {
            _baseClient = baseClient;
            _futuresClient = futuresClient;
            _log = log;
        }


        #region Test Connectivity

        /// <summary>
        /// Pings the Binance Futures API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        public WebCallResult<long> Ping(CancellationToken ct = default) => PingAsync(ct).Result;

        /// <summary>
        /// Pings the Binance Futures API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(true, pingEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return new WebCallResult<long>(result.ResponseStatusCode, result.ResponseHeaders, result.Error == null ? sw.ElapsedMilliseconds : 0, result.Error);
        }

        #endregion

        #region Check Server Time

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        public WebCallResult<DateTime> GetServerTime(bool resetAutoTimestamp = false, CancellationToken ct = default) => GetServerTimeAsync(resetAutoTimestamp, ct).Result;

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrl(true, checkTimeEndpoint, api, publicVersion);
            if (!_baseClient.AutoTimestamp)
            {
                var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ServerTime ?? default, result.Error);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                if (!result)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

                if (_futuresClient.TimeSynced && !resetAutoTimestamp)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);

                if (_baseClient.TotalRequestsMade == 1)
                {
                    // If this was the first request make another one to calculate the offset since the first one can be slower
                    localTime = DateTime.UtcNow;
                    result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                    if (!result)
                        return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);
                }

                // Calculate time offset between local and server
                var offset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                if (offset >= 0 && offset < 500)
                {
                    // Small offset, probably mainly due to ping. Don't adjust time
                    _futuresClient.CalculatedTimeOffset = 0;
                    _futuresClient.TimeSynced = true;
                    _futuresClient.LastTimeSync = DateTime.UtcNow;
                    _log.Write(LogVerbosity.Info, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
                }

                _futuresClient.CalculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                _futuresClient.TimeSynced = true;
                _futuresClient.LastTimeSync = DateTime.UtcNow;
                _log.Write(LogVerbosity.Info, $"Time offset set to {_futuresClient.CalculatedTimeOffset}ms");
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
            }
        }

        #endregion

        #region Exchange Information

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public WebCallResult<BinanceFuturesExchangeInfo> GetExchangeInfo(CancellationToken ct = default) => GetExchangeInfoAsync(ct).Result;

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public async Task<WebCallResult<BinanceFuturesExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesExchangeInfo>(_baseClient.GetUrl(true, exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _futuresClient.ExchangeInfo = exchangeInfoResult.Data;
            _futuresClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogVerbosity.Info, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion
    }
}
