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
        private const string isolatedMarginAllSymbolEndpoint = "margin/isolated/allPair";

        private readonly BinanceClient _baseClient;

        internal BinanceClientMarginMarket(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }
        
        #region Query Margin Asset
        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The asset to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin asset</returns>
        public WebCallResult<BinanceMarginAsset> GetMarginAsset(string asset, CancellationToken ct = default) => GetMarginAssetAsync(asset, ct).Result;
        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
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

        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin asset</returns>
        public WebCallResult<BinanceMarginPair> GetMarginPair(string symbol, CancellationToken ct = default) => GetMarginPairAsync(symbol, ct).Result;
        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        public async Task<WebCallResult<BinanceMarginPair>> GetMarginPairAsync(string symbol, CancellationToken ct = default)
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

        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        public WebCallResult<IEnumerable<BinanceMarginAsset>> GetMarginAssets(CancellationToken ct = default) => GetMarginAssetsAsync(ct).Result;
        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginAsset>>(_baseClient.GetUrlSpot(marginAssetsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        public WebCallResult<IEnumerable<BinanceMarginPair>> GetMarginPairs(CancellationToken ct = default) => GetMarginPairsAsync(ct).Result;
        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginPairsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginPair>>(_baseClient.GetUrlSpot(marginPairsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin PriceIndex
        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        public WebCallResult<BinanceMarginPriceIndex> GetMarginPriceIndex(string symbol, CancellationToken ct = default) => GetMarginPriceIndexAsync(symbol, ct).Result;
        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
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
        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
