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
        public async Task<WebCallResult<BinanceListRecords<BinanceNftDeposit>>> GetNftDepositHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/nft/history/deposit", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get NFT Withdraw History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceListRecords<BinanceNftWithdraw>>> GetNftWithdrawHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/nft/history/withdraw", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftWithdraw>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get NFT Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceListRecords<BinanceNftTransaction>>> GetNftTransactionHistoryAsync(NftOrderType orderType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("orderType", orderType);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/nft/history/transactions", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region  Get NFT Asset

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceListRecords<BinanceNftAsset>>> GetNftAssetAsync(int? limit = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("page", page);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/nft/user/getAsset", BinanceExchange.RateLimiter.SpotRestIp, 3000, true);
            return await _baseClient.SendAsync<BinanceListRecords<BinanceNftAsset>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
