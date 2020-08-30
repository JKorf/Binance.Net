﻿using System.Collections.Generic;
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

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to BinanceSocketClient.Spot.SubscribeToUserDataUpdates
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        public WebCallResult<string> StartIsolatedMarginUserStream(string symbol, CancellationToken ct = default) => StartIsolatedMarginUserStreamAsync(symbol, ct).Result;

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to BinanceSocketClient.Spot.SubscribeToUserDataUpdates
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
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
            return new WebCallResult<string>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ListenKey, result.Error);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <summary>
        /// Sends a keep alive for the current user for margin account stream listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<object> KeepAliveIsolatedMarginUserStream(string symbol, string listenKey, CancellationToken ct = default) => KeepAliveIsolatedMarginUserStreamAsync(symbol, listenKey, ct).Result;

        /// <summary>
        /// Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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

        #region Close a ListenKey 

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<object> CloseIsolatedMarginUserStream(string symbol, string listenKey, CancellationToken ct = default) => CloseIsolatedMarginUserStreamAsync(symbol, listenKey, ct).Result;

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
