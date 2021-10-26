using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Futures;
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

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_futuresClient.GetUrl(pingEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return new WebCallResult<long>(result.ResponseStatusCode, result.ResponseHeaders, result.Error == null ? sw.ElapsedMilliseconds : 0, result.Error);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _futuresClient.GetUrl(checkTimeEndpoint, Api, publicVersion);
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion
    }
}
