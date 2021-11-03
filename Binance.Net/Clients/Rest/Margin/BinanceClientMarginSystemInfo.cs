using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.Margin;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Margin
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientMarginSystemInfo : IBinanceClientMarginSystemInfo
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";

        private const string api = "api";
        private const string publicVersion = "3";

        private readonly Log _log;

        private readonly BinanceClientMargin _baseClient;

        internal BinanceClientMarginSystemInfo(Log log, BinanceClientMargin baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(pingEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return new WebCallResult<long>(result.ResponseStatusCode, result.ResponseHeaders, result.Error == null ? sw.ElapsedMilliseconds : 0, result.Error);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrl(checkTimeEndpoint, api, publicVersion);
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

                if (BinanceClientMargin.TimeSynced && !resetAutoTimestamp)
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
                    BinanceClientMargin.CalculatedTimeOffset = 0;
                    BinanceClientMargin.TimeSynced = true;
                    BinanceClientMargin.LastTimeSync = DateTime.UtcNow;
                    _log.Write(LogLevel.Information, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return result.As(result.Data.ServerTime);
                }

                BinanceClientMargin.CalculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                BinanceClientMargin.TimeSynced = true;
                BinanceClientMargin.LastTimeSync = DateTime.UtcNow;
                _log.Write(LogLevel.Information, $"Time offset set to {BinanceClientMargin.CalculatedTimeOffset}ms");
                return result.As(result.Data.ServerTime);
            }
        }

        #endregion

        #region Exchange Information
        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
             => GetExchangeInfoAsync(Array.Empty<string>(), ct);

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default)
             => GetExchangeInfoAsync(new string[] { symbol }, ct);

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (symbols.Count() > 1)
                parameters.Add("symbols", JsonConvert.SerializeObject(symbols));
            else if (symbols.Any())
                parameters.Add("symbol", symbols.First());

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array).ConfigureAwait(false);
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
