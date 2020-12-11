using System;
using System.Diagnostics;
using System.Net.Http;
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
    public abstract class BinanceClientFuturesSystem : IBinanceClientFuturesSystem
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";

        /// <summary>
        /// Api path
        /// </summary>
        protected abstract string Api { get; }
        /// <summary>
        /// version
        /// </summary>
        protected const string publicVersion = "1";
        
        /// <summary>
        /// Client
        /// </summary>
        protected readonly BinanceClient _baseClient;
        private readonly BinanceClientFutures _futuresClient;
        /// <summary>
        /// Log
        /// </summary>
        protected readonly Log _log;

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
            var result = await _baseClient.SendRequestInternal<object>(_futuresClient.GetUrl(pingEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
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
            var url = _futuresClient.GetUrl(checkTimeEndpoint, Api, publicVersion);
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
            return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ServerTime ?? default, result.Error);
        }

        #endregion
    }
}
