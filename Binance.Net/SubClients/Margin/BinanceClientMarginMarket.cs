using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Margin
{
    /// <summary>
    /// Margin market endpoints
    /// </summary>
    public class BinanceClientMarginMarket : IBinanceClientMarginMarket
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

        private readonly BinanceClient _baseClient;

        internal BinanceClientMarginMarket(BinanceClient baseClient)
        {
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

            return await _baseClient.SendRequestInternal<BinanceMarginAsset>(_baseClient.GetUrlSpot(marginAssetEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceMarginPair>(_baseClient.GetUrlSpot(marginPairEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginAsset>>(_baseClient.GetUrlSpot(marginAssetsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginPair>>(_baseClient.GetUrlSpot(marginPairsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceMarginPriceIndex>(_baseClient.GetUrlSpot(marginPriceIndexEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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
                    _baseClient.GetUrlSpot(isolatedMarginSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct,
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceIsolatedMarginSymbol>>(_baseClient.GetUrlSpot(isolatedMarginAllSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
