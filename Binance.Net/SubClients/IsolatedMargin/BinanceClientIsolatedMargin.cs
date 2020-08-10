using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.IsolatedMargin;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.IsolatedMargin
{
    /// <summary>
    /// Isolated margin endpoints
    /// </summary>
    public class BinanceClientIsolatedMargin : IBinanceClientIsolatedMargin
    {
        private const string createIsolatedMarginAccountEndpoint = "margin/isolated/create";
        private const string transferIsolatedMarginAccountEndpoint = "margin/isolated/transfer";
        private const string isolatedMarginTransferHistoryEndpoint = "margin/isolated/transfer";
        private const string isolatedMarginAccountEndpoint = "margin/isolated/account";
        private const string isolatedMarginSymbolEndpoint = "margin/isolated/pair";
        private const string isolatedMarginAllSymbolEndpoint = "margin/isolated/allPair";

        /// <summary>
        /// Isolated margin user stream endpoints
        /// </summary>
        public IBinanceClientIsolatedMarginUserStream UserStream { get; }

        private readonly BinanceClient _baseClient;

        internal BinanceClientIsolatedMargin(BinanceClient baseClient)
        {
            _baseClient = baseClient;

            UserStream = new BinanceClientIsolatedMarginUserStream(_baseClient);
        }

        /// <summary>
        /// Create isolated margin account
        /// </summary>
        /// <param name="baseAsset">The base asset</param>
        /// <param name="quoteAsset">The quote asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<CreateIsolatedMarginAccountResult>> CreateIsolatedMarginAccountAsync(
            string baseAsset, string quoteAsset, int? receiveWindow = null, CancellationToken ct = default)
        {
            baseAsset.ValidateNotNull(nameof(baseAsset));
            quoteAsset.ValidateNotNull(nameof(quoteAsset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<CreateIsolatedMarginAccountResult>(timestampResult.ResponseStatusCode,
                    timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"base", baseAsset},
                {"quote", quoteAsset},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<CreateIsolatedMarginAccountResult>(
                    _baseClient.GetUrl(false, createIsolatedMarginAccountEndpoint, "sapi", "1"), HttpMethod.Post, ct,
                    parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Transfer from or to isolated margin account
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="symbol">Isolated symbol</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceMarginTransaction>> IsolatedMarginAccountTransfer(string asset,
            string symbol, IsolatedMarginTransferDirection from, IsolatedMarginTransferDirection to, decimal amount,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            symbol.ValidateNotNull(nameof(symbol));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginTransaction>(timestampResult.ResponseStatusCode,
                    timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"asset", asset},
                {"symbol", symbol},
                {"transFrom", JsonConvert.SerializeObject(from, new IsolatedMarginTransferDirectionConverter(false))},
                {"transTo", JsonConvert.SerializeObject(to, new IsolatedMarginTransferDirectionConverter(false))},
                {"amount", amount.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<BinanceMarginTransaction>(
                    _baseClient.GetUrl(false, transferIsolatedMarginAccountEndpoint, "sapi", "1"), HttpMethod.Post, ct,
                    parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Get history of transfer to and from the isolated margin account
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="from">Filter by direction</param>
        /// <param name="to">Filter by direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="current">Current page</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceQueryRecords<BinanceIsolatedMarginTransfer>>>
            GetIsolatedMarginAccountTransferHistory(string symbol, string? asset = null,
                IsolatedMarginTransferDirection? from = null, IsolatedMarginTransferDirection? to = null,
                DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10,
                int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceQueryRecords<BinanceIsolatedMarginTransfer>>(
                    timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("from",
                !from.HasValue
                    ? null
                    : JsonConvert.SerializeObject(from, new IsolatedMarginTransferDirectionConverter(false)));
            parameters.AddOptionalParameter("to",
                !to.HasValue
                    ? null
                    : JsonConvert.SerializeObject(to, new IsolatedMarginTransferDirectionConverter(false)));
            parameters.AddOptionalParameter("startTime",
                startTime != null
                    ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture)
                    : null);
            parameters.AddOptionalParameter("endTime",
                endTime != null
                    ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture)
                    : null);
            parameters.AddOptionalParameter("current", current?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<BinanceQueryRecords<BinanceIsolatedMarginTransfer>>(
                    _baseClient.GetUrl(false, isolatedMarginTransferHistoryEndpoint, "sapi", "1"), HttpMethod.Get, ct,
                    parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Isolated margin account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceIsolatedMarginAccount>(timestampResult.ResponseStatusCode,
                    timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<BinanceIsolatedMarginAccount>(
                    _baseClient.GetUrl(false, isolatedMarginAccountEndpoint, "sapi", "1"), HttpMethod.Get, ct,
                    parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbol(string symbol,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceIsolatedMarginSymbol>(timestampResult.ResponseStatusCode,
                    timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol},
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<BinanceIsolatedMarginSymbol>(
                    _baseClient.GetUrl(false, isolatedMarginSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct,
                    parameters, true).ConfigureAwait(false);
        }


        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbols(int? receiveWindow =
            null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>(timestampResult.ResponseStatusCode,
                    timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow
                                                              ?.ToString(CultureInfo.InvariantCulture) ??
                                                          _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(
                                                              CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceIsolatedMarginSymbol>>(_baseClient.GetUrl(false
                    , isolatedMarginAllSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true)
                .ConfigureAwait(false);
        }
    }
}
