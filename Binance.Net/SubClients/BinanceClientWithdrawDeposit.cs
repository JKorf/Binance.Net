using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Withdraw/Deposit endpoints
    /// </summary>
    public class BinanceClientWithdrawDeposit
    {
        private const string assetDetailsEndpoint = "assetDetail.html";

        private readonly BinanceClient _baseClient;

        internal BinanceClientWithdrawDeposit(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        public WebCallResult<Dictionary<string, BinanceAssetDetails>> GetAssetDetails(int? receiveWindow = null, CancellationToken ct = default) => GetAssetDetailsAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<Dictionary<string, BinanceAssetDetails>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceAssetDetailsWrapper>(_baseClient.GetUrl(false, assetDetailsEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, null, _baseClient.ParseErrorResponseInternal(JToken.Parse(result.Data.Message))) : new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
    }
}
