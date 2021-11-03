using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects;
using Binance.Net.Objects.Other;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientSpotSystemInfo: IBinanceClientSpotSystemInfo
    {
        private const string api = "api";
        private const string publicVersion = "3";

        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";
        private const string systemStatusEndpoint = "system/status";
        private const string tradingStatusEndpoint = "account/apiTradingStatus";
        private const string assetDetailsEndpoint = "asset/assetDetail";

        private readonly Log _log;

        private readonly BinanceClientSpot _baseClient;

        internal BinanceClientSpotSystemInfo(Log log, BinanceClientSpot baseClient)
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

                if (BinanceClientSpot.TimeSynced && !resetAutoTimestamp)
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
                    BinanceClientSpot.CalculatedTimeOffset = 0;
                    BinanceClientSpot.TimeSynced = true;
                    BinanceClientSpot.LastTimeSync = DateTime.UtcNow;
                    _log.Write(LogLevel.Information, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return result.As(result.Data.ServerTime);
                }

                BinanceClientSpot.CalculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                BinanceClientSpot.TimeSynced = true;
                BinanceClientSpot.LastTimeSync = DateTime.UtcNow;
                _log.Write(LogLevel.Information, $"Time offset set to {BinanceClientSpot.CalculatedTimeOffset}ms");
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

        #region System status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<BinanceSystemStatus>(_baseClient.GetUrl(systemStatusEndpoint, "sapi", "1"), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
        }

        #endregion

        #region Trading status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceTradingStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceTradingStatus>>(_baseClient.GetUrl(tradingStatusEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !string.IsNullOrEmpty(result.Data.Message) ? new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message!)) : result.As(result.Data.Data);
        }
        #endregion

        #region asset details
        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<Dictionary<string, BinanceAssetDetails>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<Dictionary<string, BinanceAssetDetails>>(_baseClient.GetUrl(assetDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Get products

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default)
        {
            var url = _baseClient.ClientOptions.BaseAddress.Replace("api.", "www.") + "exchange-api/v2/public/asset-service/product/get-products";

            var data = await _baseClient.SendRequestInternal<BinanceExchangeApiWrapper<IEnumerable<BinanceProduct>>>(new Uri(url), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!data)
                return data.As<IEnumerable<BinanceProduct>>(null);

            if (!data.Data.Success)
                return WebCallResult<IEnumerable<BinanceProduct>>.CreateErrorResult(new ServerError(data.Data.Code, data.Data.Message + " - " + data.Data.MessageDetail));

            return data.As(data.Data.Data);
        }
        #endregion
    }
}
