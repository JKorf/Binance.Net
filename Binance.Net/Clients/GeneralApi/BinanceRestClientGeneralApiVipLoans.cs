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
        public async Task<WebCallResult<IEnumerable<BinanceVipLoanBorrowInterestRate>>> GetBorrowInterestRateAsync(string loanAsset, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "loanCoin", loanAsset },
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/request/interestRate", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceVipLoanBorrowInterestRate>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get VIP Loan Interest Rate History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanInterestRate>>> GetVipLoanInterestaRateHistoryAsync(string loanAsset, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", loanAsset },
            };
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/interestRateHistory", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanInterestRate>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Loanable Assets Data
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanAsset>>> GetVipLoanAssetDataAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("loanCoin", loanAsset);
            parameters.AddOptional("vipLevel", vipLevel);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/loanable/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Collateral Assets Data
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanCollateralAsset>>> GetVipLoanCollateralAssetDataAsync(string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("collateralCoin", collateralAsset);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/collateral/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanCollateralAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region User Information

        #region Get VIP Loan Ongoing Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanOngoingOrderData>>> GetVipLoanOngoinOrdersAsync(long? orderId = null, long? collateralAccountId = null, string? loanAsset = null, string? collateralAsset = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("collateralAccountId",collateralAccountId);
            parameters.AddOptional("loanCoin", loanAsset);
            parameters.AddOptional("collateralAsset", collateralAsset);
            parameters.AddOptional("current", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/ongoing/orders", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanOngoingOrderData>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get VIP Loan Repayment History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanRepayHistoryData>>> GetVipLoanRepaymentHistoryAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("loanCoin", loanAsset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/repay/history", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanRepayHistoryData>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get VIP Loan Accrued Interest
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanAccuredInterest>>> GetVipLoanAccuredInterestAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("loanCoin", loanAsset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("current", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/accruedInterest", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanAccuredInterest>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Locked Value of VIP Collateral Account
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanCollateralAccountLockedValue>>> GetVipLoanCollateralAccountAsync(long? orderId = null, long? collateralAccountId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("collateralAccountId", collateralAccountId);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/collateral/account", BinanceExchange.RateLimiter.SpotRestIp, 6000, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanCollateralAccountLockedValue>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Application Status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanApplicationStatus>>> GetApplicationStatusAsync(long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("current", page);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/request/data", BinanceExchange.RateLimiter.SpotRestUid, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceVipLoanApplicationStatus>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region Trade

        #region VIP Loan Renew
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceVipLoanRenewData>> RenewVipLoanAsync(long orderId, int loanTerm, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "orderId", orderId },
                { "loanTerm", loanTerm },
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/renew", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanRenewData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region VIP Loan Repay
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceVipLoanRepayData>> RepayVipLoanAsync(long orderId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "orderId", orderId },
                { "amount", quantity },
            };
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/repay", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanRepayData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
        
        #region VIP Loan Borrow
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceVipLoanBorrowData>> BorrowVipLoanAsync(long loanAccountId, string loanAsset, decimal loanQuantity, string collateralAccountId, string collateralAsset, bool flexibleRate, int? loanTerm = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "loanAccountId", loanAccountId },
                { "loanCoin", loanAsset },
                { "loanAmount", loanQuantity },
                { "collateralAccountId", collateralAccountId },
                { "collateralCoin", collateralAsset },
                { "isFlexibleRate", flexibleRate },
            };
            parameters.AddOptional("loanTerm", loanTerm);
            parameters.AddOptionalString("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/vip/borrow", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceVipLoanBorrowData>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
