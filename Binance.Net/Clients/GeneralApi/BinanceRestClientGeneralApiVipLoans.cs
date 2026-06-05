using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.VipLoans;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiVipLoans: IBinanceRestClientGeneralApiVipLoans
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiVipLoans(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Market Data

        #region Get Borrow Interest Rate
        /// <inheritdoc />
        public async Task<HttpResult<IEnumerable<BinanceVipLoanBorrowInterestRate>>> GetBorrowInterestRateAsync(string loanAsset, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "loanCoin", loanAsset },
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/request/interestRate", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceVipLoanBorrowInterestRate>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get VIP Loan Interest Rate History
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanInterestRate>>> GetVipLoanInterestaRateHistoryAsync(string loanAsset, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "coin", loanAsset },
            };
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/interestRateHistory", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanInterestRate>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Loanable Assets Data
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanAsset>>> GetVipLoanAssetDataAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("loanCoin", loanAsset);
            parameters.Add("vipLevel", vipLevel);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/loanable/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Collateral Assets Data
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanCollateralAsset>>> GetVipLoanCollateralAssetDataAsync(string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("collateralCoin", collateralAsset);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/collateral/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanCollateralAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region User Information

        #region Get VIP Loan Ongoing Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanOngoingOrderData>>> GetVipLoanOngoinOrdersAsync(long? orderId = null, long? collateralAccountId = null, string? loanAsset = null, string? collateralAsset = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("collateralAccountId",collateralAccountId);
            parameters.Add("loanCoin", loanAsset);
            parameters.Add("collateralAsset", collateralAsset);
            parameters.Add("current", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/ongoing/orders", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanOngoingOrderData>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get VIP Loan Repayment History
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanRepayHistoryData>>> GetVipLoanRepaymentHistoryAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("loanCoin", loanAsset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/repay/history", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanRepayHistoryData>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get VIP Loan Accrued Interest
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanAccuredInterest>>> GetVipLoanAccuredInterestAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("loanCoin", loanAsset);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("current", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/accruedInterest", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanAccuredInterest>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Locked Value of VIP Collateral Account
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanCollateralAccountLockedValue>>> GetVipLoanCollateralAccountAsync(long? orderId = null, long? collateralAccountId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("collateralAccountId", collateralAccountId);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/collateral/account", BinanceExchange.RateLimiter.SpotRestIp, 6000, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanCollateralAccountLockedValue>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Application Status
        /// <inheritdoc />
        public async Task<HttpResult<BinanceQueryRecords<BinanceVipLoanApplicationStatus>>> GetApplicationStatusAsync(long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("current", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/request/data", BinanceExchange.RateLimiter.SpotRestUid, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanApplicationStatus>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region Trade

        #region VIP Loan Renew
        /// <inheritdoc />
        public async Task<HttpResult<BinanceVipLoanRenewData>> RenewVipLoanAsync(long orderId, int loanTerm, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "orderId", orderId },
                { "loanTerm", loanTerm },
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/renew", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanRenewData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region VIP Loan Repay
        /// <inheritdoc />
        public async Task<HttpResult<BinanceVipLoanRepayData>> RepayVipLoanAsync(long orderId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "orderId", orderId },
                { "amount", quantity },
            };
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/repay", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanRepayData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
        
        #region VIP Loan Borrow
        /// <inheritdoc />
        public async Task<HttpResult<BinanceVipLoanBorrowData>> BorrowVipLoanAsync(long loanAccountId, string loanAsset, decimal loanQuantity, string collateralAccountId, string collateralAsset, bool flexibleRate, int? loanTerm = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "loanAccountId", loanAccountId },
                { "loanCoin", loanAsset },
                { "loanAmount", loanQuantity },
                { "collateralAccountId", collateralAccountId },
                { "collateralCoin", collateralAsset },
                { "isFlexibleRate", flexibleRate },
            };
            parameters.Add("loanTerm", loanTerm);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/borrow", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanBorrowData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
