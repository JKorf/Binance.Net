using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Clients.Rest.CoinFutures;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Objects.Models.Spot.Lending;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientGeneralLending: IBinanceClientGeneralLending
    {
        // Lending
        private const string flexibleProductListEndpoint = "lending/daily/product/list";
        private const string leftDailyPurchaseQuotaEndpoint = "lending/daily/userLeftQuota";
        private const string purchaseFlexibleProductEndpoint = "lending/daily/purchase";
        private const string leftDailyRedemptionQuotaEndpoint = "lending/daily/userRedemptionQuota";
        private const string redeemFlexibleProductEndpoint = "lending/daily/redeem";
        private const string flexiblePositionEndpoint = "lending/daily/token/position";
        private const string fixedAndCustomizedFixedProjectListEndpoint = "lending/project/list";
        private const string purchaseCustomizedFixedProjectEndpoint = "lending/customizedFixed/purchase";
        private const string fixedAndCustomizedProjectPositionEndpoint = "lending/project/position/list";
        private const string lendingAccountEndpoint = "lending/union/account";
        private const string purchaseRecordEndpoint = "lending/union/purchaseRecord";
        private const string redemptionRecordEndpoint = "lending/union/redemptionRecord";
        private const string lendingInterestHistoryEndpoint = "lending/union/interestHistory";
        private const string positionChangedEndpoint = "lending/positionChanged";

        private const string cryptoLoanIncomingEndpoint = "loan/income";


        private readonly Log _log;

        private readonly BinanceClientGeneral _baseClient;

        internal BinanceClientGeneralLending(BinanceClientGeneral baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Flexible Product List
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSavingsProduct>>> GetFlexibleProductListAsync(ProductStatus? status = null, bool? featured = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new ProductStatusConverter(false)));
            parameters.AddOptionalParameter("featured", featured == true ? "TRUE" : "ALL");
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSavingsProduct>>(_baseClient.GetUrl(flexibleProductListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Left Daily Purchase Quota of Flexible Product
        /// <inheritdoc />
        public async Task<WebCallResult<BinancePurchaseQuotaLeft>> GetLeftDailyPurchaseQuotaOfFlexableProductAsync(string productId, long? receiveWindow = null, CancellationToken ct = default)
        {
            productId.ValidateNotNull(nameof(productId));

            var parameters = new Dictionary<string, object>
            {
                { "productId", productId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinancePurchaseQuotaLeft>(_baseClient.GetUrl(leftDailyPurchaseQuotaEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Purchase Flexible Product
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseFlexibleProductAsync(string productId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            productId.ValidateNotNull(nameof(productId));

            var parameters = new Dictionary<string, object>
            {
                { "productId", productId },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceLendingPurchaseResult>(_baseClient.GetUrl(purchaseFlexibleProductEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Left Daily Redemption Quota of Flexible Product
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceRedemptionQuotaLeft>> GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(string productId, RedeemType type, long? receiveWindow = null, CancellationToken ct = default)
        {
            productId.ValidateNotNull(nameof(productId));

            var parameters = new Dictionary<string, object>
            {
                { "productId", productId },
                { "type",  JsonConvert.SerializeObject(type, new RedeemTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceRedemptionQuotaLeft>(_baseClient.GetUrl(leftDailyRedemptionQuotaEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Redeem Flexible Product
        /// <inheritdoc />
        public async Task<WebCallResult<object>> RedeemFlexibleProductAsync(string productId, decimal quantity, RedeemType type, long? receiveWindow = null, CancellationToken ct = default)
        {
            productId.ValidateNotNull(nameof(productId));

            var parameters = new Dictionary<string, object>
            {
                { "productId", productId },
                { "type", JsonConvert.SerializeObject(type, new RedeemTypeConverter(false)) },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(redeemFlexibleProductEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Flexible Product Position
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFlexibleProductPosition>>> GetFlexibleProductPositionAsync(string asset, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFlexibleProductPosition>>(_baseClient.GetUrl(flexiblePositionEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Fixed And Customized Fixed Project List
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceProject>>> GetFixedAndCustomizedFixedProjectListAsync(
            ProjectType type, string? asset = null, ProductStatus? status = null, bool? sortAscending = null, string? sortBy = null, int? currentPage = null, int? size = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "type", JsonConvert.SerializeObject(type, new ProjectTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new ProductStatusConverter(false)));
            parameters.AddOptionalParameter("isSortAsc", sortAscending.ToString().ToLower());
            parameters.AddOptionalParameter("sortBy", sortBy);
            parameters.AddOptionalParameter("current", currentPage);
            parameters.AddOptionalParameter("size", size);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceProject>>(_baseClient.GetUrl(fixedAndCustomizedFixedProjectListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Purchase Customized Fixed Project
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseCustomizedFixedProjectAsync(string projectId, int lot, long? receiveWindow = null, CancellationToken ct = default)
        {
            projectId.ValidateNotNull(nameof(projectId));

            var parameters = new Dictionary<string, object>
            {
                { "projectId", projectId },
                { "lot", lot.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceLendingPurchaseResult>(_baseClient.GetUrl(purchaseCustomizedFixedProjectEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Customized Fixed Project Position
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>>> GetCustomizedFixedProjectPositionsAsync(string asset, string? projectId = null, ProjectStatus? status = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset }
            };
            parameters.AddOptionalParameter("projectId", projectId);
            parameters.AddOptionalParameter("status", status == null ? null : JsonConvert.SerializeObject(status, new ProjectStatusConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceCustomizedFixedProjectPosition>>(_baseClient.GetUrl(fixedAndCustomizedProjectPositionEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Lending Account
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceLendingAccount>> GetLendingAccountAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceLendingAccount>(_baseClient.GetUrl(lendingAccountEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Purchase Records
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePurchaseRecord>>> GetPurchaseRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "lendingType", JsonConvert.SerializeObject(lendingType, new LendingTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime",  DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime",  DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinancePurchaseRecord>>(_baseClient.GetUrl(purchaseRecordEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Redemption Record
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceRedemptionRecord>>> GetRedemptionRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "lendingType", JsonConvert.SerializeObject(lendingType, new LendingTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime",  DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime",  DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceRedemptionRecord>>(_baseClient.GetUrl(redemptionRecordEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Interest History
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceLendingInterestHistory>>> GetLendingInterestHistoryAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "lendingType", JsonConvert.SerializeObject(lendingType, new LendingTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime",  DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime",  DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceLendingInterestHistory>>(_baseClient.GetUrl(lendingInterestHistoryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region ChangeToDailyPosition
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceLendingChangeToDailyResult>> ChangeToDailyPositionAsync(string projectId, int lot, long? positionId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            projectId.ValidateNotNull(nameof(projectId));

            var parameters = new Dictionary<string, object>
            {
                { "projectId", projectId },
                { "lot", lot.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("positionId", positionId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceLendingChangeToDailyResult>(_baseClient.GetUrl(positionChangedEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region GetCryptoLoansIncomeHistory
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCryptoLoanIncome>>> GetCryptoLoansIncomeHistoryAsync(string asset, LoanIncomeType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "asset", asset }
            };
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type.Value, new LoanIncomeTypeConverter(false)) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime",  DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime",  DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceCryptoLoanIncome>>(_baseClient.GetUrl(cryptoLoanIncomingEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 6000).ConfigureAwait(false);
        }
        #endregion
    }
}
