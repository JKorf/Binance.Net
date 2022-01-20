using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.SubClients.Spot
{
    /// <summary>
    /// Spot endpoints
    /// </summary>
    public class BinanceClientSpot : IBinanceClientSpot
    {
        private const string tradingStatusEndpoint = "account/apiTradingStatus";

        /// <summary>
        /// Spot system endpoints
        /// </summary>
        public IBinanceClientSpotSystem System { get; }
        /// <summary>
        /// Spot market endpoints
        /// </summary>
        public IBinanceClientSpotMarket Market { get; }
        /// <summary>
        /// Spot order endpoints
        /// </summary>
        public IBinanceClientSpotOrder Order { get; }
        /// <summary>
        /// Spot user stream endpoints
        /// </summary>
        public IBinanceClientUserStream UserStream { get; }

        /// <summary>
        /// Spot/futures endpoints
        /// </summary>
        public IBinanceClientSpotFuturesInteraction Futures { get; }

        private BinanceClient _baseClient;

        internal BinanceClientSpot(Log log, BinanceClient baseClient)
        {
            _baseClient = baseClient;

            System = new BinanceClientSpotSystem(log, baseClient);
            Market = new BinanceClientSpotMarket(baseClient);
            Order = new BinanceClientSpotOrder(log, baseClient);
            UserStream = new BinanceClientSpotUserStream(baseClient);
            Futures = new BinanceClientSpotFuturesInteraction(baseClient);
        }

        #region Trading status
        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        public async Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceTradingStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<BinanceTradingStatus>>(_baseClient.GetUrlSpot(tradingStatusEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !string.IsNullOrEmpty(result.Data.Message) ? new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message!)) : result.As(result.Data.Data);
        }
        #endregion
    }
}
