using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Blvt;
using Binance.Net.Objects.BSwap;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Liquid swap endpoints
    /// </summary>
    public class BinanceClientLiquidSwap : IBinanceClientLiquidSwap
    {
        private const string bSwapApi = "sapi";
        private const string bSwapVersion = "1";

        private const string bSwapPoolsEndpoint = "bswap/pools";
        private const string bSwapPoolLiquidityEndpoint = "bswap/liquidity";
        private const string bSwapAddLiquidityEndpoint = "bswap/liquidityAdd";
        private const string bSwapRemoveLiquidityEndpoint = "bswap/liquidityRemove";
        private const string bSwapLiquidityOperationsEndpoint = "bswap/liquidityOps";
        private const string bSwapQuoteEndpoint = "bswap/quote ";
        private const string bSwapSwapEndpoint = "bswap/swap ";
        private const string bSwapSwapRecordsEndpoint = "bswap/swap ";


        private readonly BinanceClient _baseClient;

        internal BinanceClientLiquidSwap(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get info

        /// <summary>
        /// Get all swap pools
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPool>>> GetBSwapPoolsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBSwapPool>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
                             {
                                 {"timestamp", _baseClient.GetTimestamp()}
                             };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPool>>(_baseClient.GetUrlSpot(bSwapPoolsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get info

        /// <summary>
        /// Get liquidity info for a pool
        /// </summary>
        /// <param name="poolId">Get a specific pool</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPoolLiquidity>>> GetPoolLiquidityInfoAsync(int? poolId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBSwapPoolLiquidity>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("poolId", poolId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPoolLiquidity>>(_baseClient.GetUrlSpot(bSwapPoolLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Add liquidity

        /// <summary>
        /// Add liquidity to a pool
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBSwapOperationResult>> AddLiquidityAsync(string poolId, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapOperationResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"asset", asset},
                {"quantity", quantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrlSpot(bSwapAddLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Remove liquidity

        /// <summary>
        /// Remove liquidity from a pool
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="type">Remove type</param>
        /// <param name="shareAmount">Amount to remove</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBSwapOperationResult>> RemoveLiquidityAsync(string poolId, string asset, RemoveLiquidityType type, decimal shareAmount, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapOperationResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"asset", asset},
                {"type", JsonConvert.SerializeObject(type, new RemoveLiquidityTypeConverter(false))},
                {"shareAmount", shareAmount.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrlSpot(bSwapRemoveLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get liquidity operation records

        /// <summary>
        /// Get liquidity operation records
        /// </summary>
        /// <param name="operationId">Filter by operationId</param>
        /// <param name="poolId">Filter by poolId</param>
        /// <param name="operation">Filter by operation</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBSwapOperation>>> GetLiquidityOperationRecordsAsync(long? operationId = null, string? poolId = null, BSwapOperation? operation = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBSwapOperation>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("operationId", operationId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("poolId", poolId);
            parameters.AddOptionalParameter("operation", operation.HasValue ? JsonConvert.SerializeObject(new BSwapOperationConverter(false)): null);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapOperation>>(_baseClient.GetUrlSpot(bSwapLiquidityOperationsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Request quote

        /// <summary>
        /// Request a quote for swap quote asset (selling asset) for base asset (buying asset), essentially price/exchange rates. quoteQty is quantity of quote asset(to sell).
        /// Please be noted the quote is for reference only, the actual price will change as the liquidity changes, it's recommended to swap immediate after request a quote for slippage prevention.
        /// </summary>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBSwapQuote>> GetQuoteAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapQuote>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"quoteAsset", quoteAsset},
                {"baseAsset", baseAsset},
                {"quoteQuantity",quoteQuantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapQuote>(_baseClient.GetUrlSpot(bSwapQuoteEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Swap 

        /// <summary>
        /// Swap quote asset for base asset
        /// </summary>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceBSwapResult>> SwapAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"quoteAsset", quoteAsset},
                {"baseAsset", baseAsset},
                {"quoteQty",quoteQuantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapResult>(_baseClient.GetUrlSpot(bSwapSwapEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get swap history

        /// <summary>
        /// Get swap history records
        /// </summary>
        /// <param name="swapId">Filter by swapId</param>
        /// <param name="status">Filter by status</param>
        /// <param name="quoteAsset">Filter by quote asset</param>
        /// <param name="baseAsset">Filter by base asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceBSwapRecord>>> GetSwapHistoryAsync(long? swapId = null, BSwapStatus? status = null, string? quoteAsset = null, string? baseAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBSwapRecord>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("swapId", swapId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status.Value, new BSwapStatusConverter(false)): null);
            parameters.AddOptionalParameter("baseAsset", baseAsset);
            parameters.AddOptionalParameter("quoteAsset", quoteAsset);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapRecord>>(_baseClient.GetUrlSpot(bSwapSwapRecordsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
