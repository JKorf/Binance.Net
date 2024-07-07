using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Loans;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiLoans : IBinanceRestClientGeneralApiLoans
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiLoans(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Income History
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCryptoLoanIncome>>> GetIncomeHistoryAsync(string asset, LoanIncomeType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "asset", asset }
            };
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type.Value, new LoanIncomeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/income", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceCryptoLoanIncome>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Borrow
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCryptoLoanBorrow>> BorrowAsync(string loanAsset, string collateralAsset, int loanTerm, decimal? loanQuantity = null, decimal? collateralQuantity = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "loanCoin", loanAsset },
                { "collateralCoin", collateralAsset },
                { "loanTerm", loanTerm },
            };
            parameters.AddOptionalParameter("loanAmount", loanQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("collateralAmount", collateralQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/loan/borrow", BinanceExchange.RateLimiter.SpotRestUid, 36000, true);
            return await _baseClient.SendAsync<BinanceCryptoLoanBorrow>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Borrow History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanBorrowRecord>>> GetBorrowHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("loanAsset", loanAsset);
            parameters.AddOptionalParameter("collateralAsset", collateralAsset);
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/borrow/history", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanBorrowRecord>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Borrow Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanOpenBorrowOrder>>> GetOpenBorrowOrdersAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("loanAsset", loanAsset);
            parameters.AddOptionalParameter("collateralAsset", collateralAsset);
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/ongoing/orders", BinanceExchange.RateLimiter.SpotRestIp, 300, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanOpenBorrowOrder>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Repay
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCryptoLoanRepay>> RepayAsync(long orderId, decimal quantity, bool? repayWithBorrowedAsset = null, bool? collateralReturn = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "orderId", orderId },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("type", repayWithBorrowedAsset == null ? null : repayWithBorrowedAsset.Value ? "1" : "2");
            parameters.AddOptionalParameter("collateralReturn", collateralReturn);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/loan/repay", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceCryptoLoanRepay>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Repay History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanRepayRecord>>> GetRepayHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("loanAsset", loanAsset);
            parameters.AddOptionalParameter("collateralAsset", collateralAsset);
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/repay/history", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanRepayRecord>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Adjust LTV
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCryptoLoanLtvAdjust>> AdjustLTVAsync(long orderId, decimal quantity, bool addOrRmove, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "orderId", orderId },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", addOrRmove ? "ADDITIONAL" : "REDUCED" }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/loan/adjust/ltv", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceCryptoLoanLtvAdjust>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Repay History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanLtvAdjustRecord>>> GetLtvAdjustHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("loanAsset", loanAsset);
            parameters.AddOptionalParameter("collateralAsset", collateralAsset);
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/ltv/adjustment/history", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanLtvAdjustRecord>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Loanable Assets
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanAsset>>> GetLoanableAssetsAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("vipLevel", vipLevel);
            parameters.AddOptionalParameter("loanAsset", loanAsset);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/loanable/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Collateral Assets
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanCollateralAsset>>> GetCollateralAssetsAsync(string? collateralAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("vipLevel", vipLevel);
            parameters.AddOptionalParameter("collateralCoin", collateralAsset);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/collateral/data", BinanceExchange.RateLimiter.SpotRestIp, 400, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanCollateralAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Collateral Assets
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCryptoLoanRepayRate>> GetCollateralRepayRateAsync(string loanAsset, string collateralAsset, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "loanCoin", loanAsset },
                { "collateralCoin", collateralAsset },
                { "repayAmount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/loan/repay/collateral/rate", BinanceExchange.RateLimiter.SpotRestIp, 6000, true);
            return await _baseClient.SendAsync<BinanceCryptoLoanRepayRate>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Customize Margin Call
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanMarginCallResult>>> CustomizeMarginCallAsync(decimal marginCall, string? orderId = null, string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "marginCall", marginCall.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("collateralCoin", collateralAsset);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/loan/customize/margin_call", BinanceExchange.RateLimiter.SpotRestUid, 6000, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCryptoLoanMarginCallResult>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
    }
}
