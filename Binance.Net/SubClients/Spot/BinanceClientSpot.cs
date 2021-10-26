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

        /// <inheritdoc />
        public IBinanceClientSpotSystem System { get; }
        /// <inheritdoc />
        public IBinanceClientSpotMarket Market { get; }
        /// <inheritdoc />
        public IBinanceClientSpotOrder Order { get; }
        /// <inheritdoc />
        public IBinanceClientUserStream UserStream { get; }
        /// <inheritdoc />
        public IBinanceClientSpotFuturesInteraction Futures { get; }

        private readonly BinanceClient _baseClient;

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
        /// <inheritdoc />
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
