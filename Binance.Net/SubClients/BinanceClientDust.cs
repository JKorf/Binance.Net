using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Dust endpoints
    /// </summary>
    public class BinanceClientDust : IBinanceClientDust
    {
        private const string dustLogEndpoint = "userAssetDribbletLog.html";
        private const string dustTransferEndpoint = "asset/dust";

        private readonly BinanceClient _baseClient;

        internal BinanceClientDust(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region DustLog

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        public WebCallResult<IEnumerable<BinanceDustLog>> GetDustLog(int? receiveWindow = null, CancellationToken ct = default) => GetDustLogAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        public async Task<WebCallResult<IEnumerable<BinanceDustLog>>> GetDustLogAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceDustLog>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceDustLogListWrapper>(_baseClient.GetUrl(false, dustLogEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError("Unknown server error while requesting dust log")) : new WebCallResult<IEnumerable<BinanceDustLog>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Results!.Rows, null);
        }

        #endregion

        #region Dust Transfer

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        public WebCallResult<BinanceDustTransferResult> DustTransfer(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default) => DustTransferAsync(assets, receiveWindow, ct).Result;

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        public async Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default)
        {
            var assetsArray = assets.ToArray();

            assetsArray.ValidateNotNull(nameof(assets));
            foreach (var asset in assetsArray)
                asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceDustTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", assetsArray },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceDustTransferResult>(_baseClient.GetUrl(false, dustTransferEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
