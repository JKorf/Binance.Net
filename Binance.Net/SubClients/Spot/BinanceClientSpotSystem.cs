using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

namespace Binance.Net.SubClients.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientSpotSystem: IBinanceClientSpotSystem
    {
        private const string api = "api";
        private const string publicVersion = "3";

        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";
        private const string systemStatusEndpoint = "systemStatus.html";

        private readonly Log _log;

        private readonly BinanceClient _baseClient;

        internal BinanceClientSpotSystem(Log log, BinanceClient baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(pingEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrlSpot(checkTimeEndpoint, api, publicVersion);
            if (!_baseClient.AutoTimestamp)
            {
                var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                return result.As(result.Data?.ServerTime ?? default);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct).ConfigureAwait(false);
                if (!result)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default, result.Error);

                if (BinanceClient.TimeSynced && !resetAutoTimestamp)
                    return result.As(result.Data.ServerTime);

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
                    BinanceClient.CalculatedTimeOffset = 0;
                    BinanceClient.TimeSynced = true;
                    BinanceClient.LastTimeSync = DateTime.UtcNow;
                    _log.Write(LogLevel.Information, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return result.As(result.Data.ServerTime);
                }

                BinanceClient.CalculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                BinanceClient.TimeSynced = true;
                BinanceClient.LastTimeSync = DateTime.UtcNow;
                _log.Write(LogLevel.Information, $"Time offset set to {BinanceClient.CalculatedTimeOffset}ms");
                return result.As(result.Data.ServerTime);
            }
        }

        #endregion

        #region Exchange Information
        /// <summary>
        /// Gets information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
             => GetExchangeInfoAsync(Array.Empty<string>(), ct);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default)
             => GetExchangeInfoAsync(new string[] { symbol }, ct);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbols
        /// </summary>
        /// <param name="symbols">Symbols to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (symbols.Count() > 1)
                //parameters.Add("symbols", $"[\"{string.Join("\",\"", symbols)}\"]");
                parameters.Add("symbols", symbols.ToArray());
            else if (symbols.Any())
                parameters.Add("symbol", symbols.First());

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrlSpot(exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient.ExchangeInfo = exchangeInfoResult.Data;
            _baseClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }
        #endregion

        #region System status
        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        public async Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<BinanceSystemStatus>(_baseClient.GetUrlSpot(systemStatusEndpoint, "wapi", "3"), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
        }

        #endregion

    }
}
