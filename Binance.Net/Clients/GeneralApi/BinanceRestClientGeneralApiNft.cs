using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.NFT;

namespace Binance.Net.Clients.GeneralApi
{
    internal class BinanceRestClientGeneralApiNFT: IBinanceRestClientGeneralApiNft
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiNFT(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region  Get NFT Deposit History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceListRecords<BinanceNftDeposit>>> GetNftDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/nft/history/deposit", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get NFT Withdraw History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceListRecords<BinanceNftWithdraw>>> GetNftWithdrawHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/nft/history/withdraw", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftWithdraw>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get NFT Transaction History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceListRecords<BinanceNftTransaction>>> GetNftTransactionHistoryAsync(NftOrderType orderType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("orderType", orderType);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/nft/history/transactions", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region  Get NFT Asset

        /// <inheritdoc />
        public async Task<HttpResult<BinanceListRecords<BinanceNftAsset>>> GetNftAssetAsync(int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            parameters.Add("page", page);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/nft/user/getAsset", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftAsset>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
