using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.Margin;
using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Clients.Rest.Margin
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientMarginMarketData : IBinanceClientMarginMarketData
    {
        private const string marginApi = "sapi";
        private const string marginVersion = "1";

        // Margin
        private const string marginAssetEndpoint = "margin/asset";
        private const string marginAssetsEndpoint = "margin/allAssets";
        private const string marginPairEndpoint = "margin/pair";
        private const string marginPairsEndpoint = "margin/allPairs";
        private const string marginPriceIndexEndpoint = "margin/priceIndex";

        private const string isolatedMarginSymbolEndpoint = "margin/isolated/pair";
        private const string isolatedMarginAllSymbolEndpoint = "margin/isolated/allPairs";

        private readonly Log _log;

        private readonly BinanceClientMargin _baseClient;

        internal BinanceClientMarginMarketData(Log log, BinanceClientMargin baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Query Margin Asset
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAsset>> GetMarginAssetAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                {"asset", asset}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginAsset>(_baseClient.GetUrl(marginAssetEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Pair

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPair>> GetMarginSymbolAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginPair>(_baseClient.GetUrl(marginPairEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginAsset>>(_baseClient.GetUrl(marginAssetsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginPair>>(_baseClient.GetUrl(marginPairsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin PriceIndex
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginPriceIndex>(_baseClient.GetUrl(marginPriceIndexEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
        #endregion

        #region Query isolated margin symbol
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbolAsync(string symbol,
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
                    _baseClient.GetUrl(isolatedMarginSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct,
                    parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(int? receiveWindow =
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceIsolatedMarginSymbol>>(_baseClient.GetUrl(isolatedMarginAllSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
