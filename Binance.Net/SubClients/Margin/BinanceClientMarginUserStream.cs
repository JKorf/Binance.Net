using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Spot.UserData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Margin
{
    /// <summary>
    /// Margin user stream endpoints
    /// </summary>
    public class BinanceClientMarginUserStream : IBinanceClientUserStream
    {
        // Margin
        private const string getListenKeyEndpoint = "userDataStream";
        private const string keepListenKeyAliveEndpoint = "userDataStream";
        private const string closeListenKeyEndpoint = "userDataStream";

        private readonly BinanceClient _baseClient;

        internal BinanceClientMarginUserStream(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Create a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<string>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var result = await _baseClient.SendRequestInternal<BinanceListenKey>(_baseClient.GetUrlSpot(getListenKeyEndpoint, "sapi", "1"), HttpMethod.Post, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<object>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey },
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(keepListenKeyAliveEndpoint, "sapi", "1"), HttpMethod.Put, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrlSpot(closeListenKeyEndpoint, "sapi", "1"), HttpMethod.Delete, ct, parameters).ConfigureAwait(false);
        }

        #endregion
    }
}
