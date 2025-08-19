using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.CopyTrading;

namespace Binance.Net.Clients.GeneralApi
{
    internal class BinanceRestClientGeneralApiCopyTrading : IBinanceRestClientGeneralApiCopyTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiCopyTrading(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get User Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCopyTradingUserStatus>> GetUserStatusAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/copyTrading/futures/userStatus", BinanceExchange.RateLimiter.SpotRestUid, 20, true);
            var data = await _baseClient.SendAsync<BinanceResult<BinanceCopyTradingUserStatus>>(request, parameters, ct).ConfigureAwait(false);

            if (!data.Success)
                return data.As<BinanceCopyTradingUserStatus>(default);

            if (data.Data?.Code != 0)
                return data.AsError<BinanceCopyTradingUserStatus>(new ServerError(data.Data!.Code.ToString(), _baseClient.GetErrorInfo(data.Data!.Code, data.Data!.Message)));

            return data.As(data.Data.Data);
        }

        #endregion

        #region Get Lead Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCopyTradingLeadSymbol[]>> GetLeadSymbolAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/copyTrading/futures/leadSymbol", BinanceExchange.RateLimiter.SpotRestUid, 20, true);
            var data = await _baseClient.SendAsync<BinanceResult<BinanceCopyTradingLeadSymbol[]>>(request, parameters, ct).ConfigureAwait(false);

            return data.As(data.Data.Data);
        }

        #endregion

    }
}
