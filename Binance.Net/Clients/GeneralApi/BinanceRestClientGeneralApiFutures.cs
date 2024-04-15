using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class BinanceRestClientGeneralApiFutures : IBinanceRestClientGeneralApiFutures
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiFutures(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region New Future Account Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> TransferFuturesAccountAsync(string asset, decimal quantity, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(transferType, new FuturesTransferTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/futures/transfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get future account transaction history

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "asset", asset },
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime)! }
            };

            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/futures/transfer", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSpotFuturesTransfer>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
