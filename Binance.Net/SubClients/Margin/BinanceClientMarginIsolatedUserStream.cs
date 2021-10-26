using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients.IsolatedMargin;
using Binance.Net.Objects.Spot.UserData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Margin
{
    /// <summary>
    /// Margin user stream endpoints
    /// </summary>
    public class BinanceClientIsolatedMarginUserStream : IBinanceClientIsolatedMarginUserStream
    {
        // Margin
        private const string getListenKeyEndpoint = "userDataStream/isolated";
        private const string keepListenKeyAliveEndpoint = "userDataStream/isolated";
        private const string closeListenKeyEndpoint = "userDataStream/isolated";

        private readonly BinanceClient _baseClient;

        internal BinanceClientIsolatedMarginUserStream(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Create a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartIsolatedMarginUserStreamAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<string>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>()
            {
                {"symbol", symbol}
            };

            var result = await _baseClient.SendRequestInternal<BinanceListenKey>(_baseClient.GetUrlSpot(getListenKeyEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<object>> KeepAliveIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey },
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(keepListenKeyAliveEndpoint, "sapi", "1"), HttpMethod.Put, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult<object>> CloseIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey },
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(closeListenKeyEndpoint, "sapi", "1"), HttpMethod.Delete, ct, parameters).ConfigureAwait(false);
        }

        #endregion
    }
}
