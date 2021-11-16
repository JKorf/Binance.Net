using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Objects.Models.Spot.BSwap;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientSpotLiquidSwap: IBinanceClientSpotLiquidSwap
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
        private const string bSwapPoolsConfigureEndpoint = "bswap/poolConfigure";
        private const string bSwapAddLiquidityPreviewEndpoint = "bswap/addLiquidityPreview ";
        private const string bSwapRemoveLiquidityPreviewEndpoint = "bswap/removeLiquidityPreview ";

        private readonly Log _log;

        private readonly BinanceClientSpot _baseClient;

        internal BinanceClientSpotLiquidSwap(Log log, BinanceClientSpot baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Get swap pool

        /// <inheritdoc />
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPool>>(_baseClient.GetUrl(bSwapPoolsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get info

        /// <inheritdoc />
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPoolLiquidity>>(_baseClient.GetUrl(bSwapPoolLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: poolId == null ? 10: 1).ConfigureAwait(false);
        }

        #endregion

        #region Add liquidity

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapOperationResult>> AddLiquidityAsync(int poolId, string asset, decimal quantity, LiquidityType? type = null, int? receiveWindow = null, CancellationToken ct = default)
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
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type.Value, new LiquidityTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrl(bSwapAddLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Remove liquidity

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapOperationResult>> RemoveLiquidityAsync(int poolId, string asset, LiquidityType type, decimal shareQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapOperationResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"asset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"shareAmount", shareQuantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrl(bSwapRemoveLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Get liquidity operation records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapOperation>>> GetLiquidityOperationRecordsAsync(long? operationId = null, int? poolId = null, BSwapOperation? operation = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
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
            parameters.AddOptionalParameter("operation", operation.HasValue ? JsonConvert.SerializeObject(new BSwapOperationConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapOperation>>(_baseClient.GetUrl(bSwapLiquidityOperationsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }

        #endregion

        #region Request quote

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapQuote>> GetQuoteAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapQuote>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"quoteAsset", quoteAsset},
                {"baseAsset", baseAsset},
                {"quoteQty", quoteQuantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapQuote>(_baseClient.GetUrl(bSwapQuoteEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Swap 

        /// <inheritdoc />
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

            return await _baseClient.SendRequestInternal<BinanceBSwapResult>(_baseClient.GetUrl(bSwapSwapEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Get swap history
        /// <inheritdoc />
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
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status.Value, new BSwapStatusConverter(false)) : null);
            parameters.AddOptionalParameter("baseAsset", baseAsset);
            parameters.AddOptionalParameter("quoteAsset", quoteAsset);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapRecord>>(_baseClient.GetUrl(bSwapSwapRecordsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }

        #endregion

        #region Get pool configure

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPoolConfig>>> GetBSwapPoolConfigureAsync(int poolId, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBSwapPoolConfig>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "poolId", poolId },
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPoolConfig>>(_baseClient.GetUrl(bSwapPoolsConfigureEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, signed: true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Add liquidity preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapPreviewResult>> AddLiquidityPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapPreviewResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"quoteAsset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"quoteQty", quantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapPreviewResult>(_baseClient.GetUrl(bSwapAddLiquidityPreviewEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Remove liquidity preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapPreviewResult>> RemoveLiquidityPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceBSwapPreviewResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"quoteAsset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"shareAmount", quantity.ToString(CultureInfo.InvariantCulture)},
                {"timestamp", _baseClient.GetTimestamp()}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapPreviewResult>(_baseClient.GetUrl(bSwapRemoveLiquidityPreviewEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion
    }
}
