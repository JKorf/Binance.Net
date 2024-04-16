using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.PortfolioMargin;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceRestClientSpotApiAccount : IBinanceRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientSpotApi _baseClient;

        internal BinanceRestClientSpotApiAccount(BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Account info
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/account", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Fiat Payments History 
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFiatPayment>>> GetFiatPaymentHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "transactionType", side == OrderSide.Buy ? "0": "1" }
            };
            parameters.AddOptionalParameter("beginTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/fiat/payments", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceResult<IEnumerable<BinanceFiatPayment>>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Data!);
        }

        #endregion

        #region Get Fiat Deposit Withdraw History 
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFiatWithdrawDeposit>>> GetFiatDepositWithdrawHistoryAsync(TransactionType side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "transactionType", side == TransactionType.Deposit ? "0": "1" }
            };
            parameters.AddOptionalParameter("beginTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/fiat/orders", BinanceExchange.RateLimiter.SpotRestUid, 9000, true);
            var result = await _baseClient.SendAsync<BinanceResult<IEnumerable<BinanceFiatWithdrawDeposit>>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Data!);
        }

        #endregion

        #region Withdraw
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal quantity, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, bool? transactionFeeFlag = null, WalletType? walletType = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            address.ValidateNotNull(nameof(address));

            var parameters = new ParameterCollection
            {
                { "coin", asset },
                { "address", address },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("name", name);
            parameters.AddOptionalParameter("withdrawOrderId", withdrawOrderId);
            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("transactionFeeFlag", transactionFeeFlag);
            parameters.AddOptionalParameter("addressTag", addressTag);
            parameters.AddOptionalParameter("walletType", walletType != null ? JsonConvert.SerializeObject(walletType, new WalletTypeConverter(false)) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/capital/withdraw/apply", BinanceExchange.RateLimiter.SpotRestUid, 600, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceWithdrawalPlaced>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw History
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = null, string? withdrawOrderId = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("withdrawOrderId", withdrawOrderId);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("offset", offset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/withdraw/history", BinanceExchange.RateLimiter.SpotRestUid, 0, true, endpointLimitCount: 10, endpointLimitPeriod: TimeSpan.FromSeconds(1));
            return await _baseClient.SendAsync<IEnumerable<BinanceWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Deposit history        
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceDeposit>>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/deposit/hisrec", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceDeposit>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection
            {
                { "coin", asset }
            };
            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/deposit/address", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceDepositAddress> (request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Daily snapshots
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceSpotAccountSnapshot>>(AccountType.Spot, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceMarginAccountSnapshot>>(AccountType.Margin, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);

        // TODO Should be moved
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) =>
            await GetDailyAccountSnapshot<IEnumerable<BinanceFuturesAccountSnapshot>>(AccountType.Futures, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);


        private async Task<WebCallResult<T>> GetDailyAccountSnapshot<T>(AccountType accountType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) where T : class
        {
            limit?.ValidateIntBetween(nameof(limit), 7, 30);

            var parameters = new ParameterCollection
            {
                { "type", EnumConverter.GetString(accountType) }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/accountSnapshot", BinanceExchange.RateLimiter.SpotRestIp, 2400, true);
            var result = await _baseClient.SendAsync<BinanceSnapshotWrapper<T>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<T>(default);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.SnapshotData);
        }
        #endregion

        #region Account status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/account/status", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAccountStatus>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Funding Wallet
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFundingAsset>>> GetFundingWalletAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("needBtcValuation", needBtcValuation?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/get-funding-asset", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFundingAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region API Key Permission
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAPIKeyPermissions>> GetAPIKeyPermissionsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/account/apiRestrictions", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAPIKeyPermissions>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region User coins
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUserAsset>>> GetUserAssetsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/config/getall", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUserAsset>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Balances
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUserBalance>>> GetBalancesAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("needBtcValuation", needBtcValuation);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v3/asset/getUserAsset", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUserBalance>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Wallet Balances
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceWalletBalance>>> GetWalletBalancesAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/wallet/balance", BinanceExchange.RateLimiter.SpotRestIp, 60, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceWalletBalance>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Asset Dividend Records
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/assetDividend", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceDividendRecord>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Disable Fast Withdraw Switch
        /// <inheritdoc />
        public async Task<WebCallResult> DisableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/account/disableFastWithdrawSwitch", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Enable Fast Withdraw Switch
        /// <inheritdoc />
        public async Task<WebCallResult> EnableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/account/enableFastWithdrawSwitch", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region DustLog
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceDustLogList>> GetDustLogAsync(DateTime? startTime = null, DateTime? endTime = null, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("accountType", accountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/dibblet", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceDustLogList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Dust Transfer
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var assetsArray = assets.ToArray();

            assetsArray.ValidateNotNull(nameof(assets));
            foreach (var asset in assetsArray)
                asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection()
            {
                { "asset", assetsArray }
            };
            parameters.AddOptionalEnum("accountType", accountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/dust", BinanceExchange.RateLimiter.SpotRestUid, 10, true);
            return await _baseClient.SendAsync<BinanceDustTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Dust Elligable
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceElligableDusts>> GetAssetsForDustTransferAsync(AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("accountType", accountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/dust-btc", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceElligableDusts>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get BNB Burn Status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/bnbBurn", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceBnbBurnStatus>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Set BNB Burn Status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBnbBurnStatus>> SetBnbBurnStatusAsync(bool? spotTrading = null, bool? marginInterest = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if (spotTrading == null && marginInterest == null)
                throw new ArgumentException("SpotTrading or MarginInterest should be provided");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("spotBNBBurn", spotTrading);
            parameters.AddOptionalParameter("interestBNBBurn", marginInterest);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/bnbBurn", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceBnbBurnStatus>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region User Universal Transfer
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> TransferAsync(UniversalTransferType type, string asset, decimal quantity, string? fromSymbol = null, string? toSymbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "type", JsonConvert.SerializeObject(type, new UniversalTransferTypeConverter(false)) },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("fromSymbol", fromSymbol);
            parameters.AddOptionalParameter("toSymbol", toSymbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/transfer", BinanceExchange.RateLimiter.SpotRestUid, 900, true);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get User Universal Transfers
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceTransfer>>> GetTransfersAsync(UniversalTransferType type, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "type", JsonConvert.SerializeObject(type, new UniversalTransferTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", pageSize?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/transfer", BinanceExchange.RateLimiter.SpotRestUid, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceTransfer>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Create a ListenKey 
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2, true);
            var result = await _baseClient.SendAsync<BinanceListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Level Information

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginLevel>> GetMarginLevelInformationAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new ParameterCollection
            {
                { "email", email },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/tradeCoeff", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceMarginLevel>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account Borrow

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> MarginBorrowAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol should be specified when using isolated margin");

            var parameters = new ParameterCollection
            {
                { "asset", asset },
                { "type", "BORROW" },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString().ToLower());
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/borrow-repay", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account Repay

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection
            {
                { "asset", asset },
                { "type", "REPAY" },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString().ToLower());
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/borrow-repay", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cross Margin Adjust Max Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCrossMarginLeverageResult>> CrossMarginAdjustMaxLeverageAsync(int maxLeverage, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "maxLeverage", maxLeverage },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/max-leverage", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceCrossMarginLeverageResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetMarginTransferHistoryAsync(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new ParameterCollection
            {
                { "direction", JsonConvert.SerializeObject(direction, new TransferDirectionConverter(false)) }
            };
            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/transfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceTransferHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Loan Record

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetMarginLoansAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new ParameterCollection
            {
                { "asset", asset },
                { "type", "BORROW" }
            };
            parameters.AddOptionalParameter("txId", transactionId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);

            // TxId or startTime must be sent. txId takes precedence.
            if (!transactionId.HasValue)
            {
                parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime ?? DateTime.MinValue));
            }
            else
            {
                parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            }

            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", current?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("archived", archived);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/borrow-repay", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceLoan>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Repay Record

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetMarginRepaysAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection
            {
                { "asset", asset },
                { "type", "REPAY" }
            };
            parameters.AddOptionalParameter("txId", transactionId?.ToString(CultureInfo.InvariantCulture));

            // TxId or startTime must be sent. txId takes precedence.
            if (!transactionId.HasValue)
            {
                parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime ?? DateTime.MinValue));
            }
            else
            {
                parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            }

            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", current?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", size?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("archived", archived);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/borrow-repay", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceRepay>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cross Margin Interest Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceInterestMarginData>>> GetInterestMarginDataAsync(string? asset = null, string? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset?.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection();

            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("vipLevel", vipLevel?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = asset == null ? 5 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/crossMarginData", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceInterestMarginData>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetMarginInterestHistoryAsync(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("archived", archived);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/interestHistory", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceInterestHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Interest Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceInterestRateHistory>>> GetMarginInterestRateHistoryAsync(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset?.ValidateNotNull(nameof(asset));
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new ParameterCollection
            {
                { "asset", asset! }
            };
            parameters.AddOptionalParameter("vipLevel", vipLevel?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/interestRateHistory", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceInterestRateHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Force Liquidation Record
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetMarginForcedLiquidationHistoryAsync(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/forceLiquidationRec", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceForcedLiquidation>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated margin tier data
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginTierData>>> GetIsolatedMarginTierDataAsync(string symbol, int? tier = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("tier", tier);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolatedMarginTier", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceIsolatedMarginTierData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account Details

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/account", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceMarginAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Max Borrow

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAmount>> GetMarginMaxBorrowAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection
            {
                { "asset", asset }
            };

            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/maxBorrowable", BinanceExchange.RateLimiter.SpotRestIp, 50, true);
            return await _baseClient.SendAsync<BinanceMarginAmount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Max Transfer-Out Amount

        /// <inheritdoc />
        public async Task<WebCallResult<decimal>> GetMarginMaxTransferAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));
            var parameters = new ParameterCollection
            {
                { "asset", asset }
            };

            parameters.AddOptionalParameter("isolatedSymbol", isolatedSymbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/maxTransferable", BinanceExchange.RateLimiter.SpotRestIp, 50, true);
            var result = await _baseClient.SendAsync<BinanceMarginAmount>(request, parameters, ct).ConfigureAwait(false);

            if (!result)
                return result.As<decimal>(default);

            return result.As(result.Data.Quantity);
        }

        #endregion

        #region Query isolated margin account

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolated/account", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceIsolatedMarginAccount>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Disable isolated margin account

        /// <inheritdoc />
        public async Task<WebCallResult<CreateIsolatedMarginAccountResult>> DisableIsolatedMarginAccountAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                {"symbol", symbol}
            };

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/margin/isolated/account", BinanceExchange.RateLimiter.SpotRestUid, 300, true);
            return await _baseClient.SendAsync<CreateIsolatedMarginAccountResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Enable isolated margin account

        /// <inheritdoc />
        public async Task<WebCallResult<CreateIsolatedMarginAccountResult>> EnableIsolatedMarginAccountAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                {"symbol", symbol}
            };

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/isolated/account", BinanceExchange.RateLimiter.SpotRestUid, 300, true);
            return await _baseClient.SendAsync<CreateIsolatedMarginAccountResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get enabled isolated margin account

        /// <inheritdoc />
        public async Task<WebCallResult<IsolatedMarginAccountLimit>> GetEnabledIsolatedMarginAccountLimitAsync(
            int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolated/accountLimit", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IsolatedMarginAccountLimit>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Margin order rate limit
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCurrentRateLimit>>> GetMarginOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/rateLimit/order", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceCurrentRateLimit>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Create a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveMarginUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey },
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> StopMarginUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartIsolatedMarginUserStreamAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                {"symbol", symbol}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Ping/Keep-alive a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey },
                {"symbol", symbol}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Invalidate a ListenKey
        /// <inheritdoc />
        public async Task<WebCallResult> CloseIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey },
                {"symbol", symbol}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Trading status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/account/apiTradingStatus", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceTradingStatus>>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<BinanceTradingStatus>(default);

            return !string.IsNullOrEmpty(result.Data.Message) ? result.AsError<BinanceTradingStatus>(new ServerError(result.Data.Message!)) : result.As(result.Data.Data);
        }
        #endregion

        #region Order rate limit
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCurrentRateLimit>>> GetOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/rateLimit/order", BinanceExchange.RateLimiter.SpotRestIp, 40, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceCurrentRateLimit>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Rebate

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceRebateWrapper>> GetRebateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/rebate/taxQuery", BinanceExchange.RateLimiter.SpotRestUid, 12000, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceRebateWrapper>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<BinanceRebateWrapper>(default);

            if (result.Data?.Code != 0)
                return result.AsError<BinanceRebateWrapper>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Blvt

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtUserLimit>>> GetLeveragedTokensUserLimitAsync(string? tokenName = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tokenName", tokenName);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/blvt/userLimit", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBlvtUserLimit>>(request, parameters, ct).ConfigureAwait(false);       
        }

        #endregion

        #region Portfolio margin

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePortfolioMarginInfo>> GetPortfolioMarginAccountInfoAsync (long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/portfolio/account", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            return await _baseClient.SendAsync<BinancePortfolioMarginInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePortfolioMarginCollateralRate>>> GetPortfolioMarginCollateralRateAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/portfolio/collateralRate", BinanceExchange.RateLimiter.SpotRestIp, 50, true);
            return await _baseClient.SendAsync<IEnumerable<BinancePortfolioMarginCollateralRate>>(request, parameters, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<BinancePortfolioMarginLoan>> GetPortfolioMarginBankruptcyLoanAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/portfolio/pmLoan", BinanceExchange.RateLimiter.SpotRestUid, 500, true);
            return await _baseClient.SendAsync<BinancePortfolioMarginLoan>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> PortfolioMarginBankruptcyLoanRepayAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/portfolio/repay", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Auto Conversion config

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAutoConversionSettings>> GetAutoConvertStableCoinConfigAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/contract/convertible-coins", BinanceExchange.RateLimiter.SpotRestUid, 600, true);
            return await _baseClient.SendAsync<BinanceAutoConversionSettings>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Auto Conversion config

        /// <inheritdoc />
        public async Task<WebCallResult> SetAutoConvertStableCoinConfigAsync(string asset, bool enable, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "coin", asset },
                { "enable", enable }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/capital/contract/convertible-coins", BinanceExchange.RateLimiter.SpotRestUid, 600, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert BUSD

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBusdConvertResult>> ConvertBusdAsync(string clientTransferId, string asset, decimal quantity, string targetAsset, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "clientTranId", clientTransferId },
                { "asset", asset },
                { "amount", quantity },
                { "targetAsset", targetAsset }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/convert-transfer", BinanceExchange.RateLimiter.SpotRestUid, 5, true);
            return await _baseClient.SendAsync<BinanceBusdConvertResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert BUSD history

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBusdHistory>>> GetBusdConvertHistoryAsync(long? transferId = null, string? clientTransferId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tranId", transferId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/convert-transfer/queryByPage", BinanceExchange.RateLimiter.SpotRestUid, 5, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceBusdHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cloud Mining History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCloudMiningHistory>>> GetCloudMiningHistoryAsync(long? transferId = null, string? clientTransferId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tranId", transferId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/ledger-transfer/cloud-mining/queryByPage", BinanceExchange.RateLimiter.SpotRestUid, 600, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCloudMiningHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Fee Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginFeeData>>> GetIsolatedMarginFeeDataAsync(string? symbol = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("vipLevel", vipLevel);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 10 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolatedMarginData", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceIsolatedMarginFeeData>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Liability Exchange Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSmallLiabilityAsset>>> GetCrossMarginSmallLiabilityExchangeAssetsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/exchange-small-liability", BinanceExchange.RateLimiter.SpotRestIp, 100, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceSmallLiabilityAsset>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Small Liability Exchange Assets

        /// <inheritdoc />
        public async Task<WebCallResult> CrossMarginSmallLiabilityExchangeAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "assetNames", string.Join(",", assets) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/exchange-small-liability", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Liability Exchange History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceSmallLiabilityHistory>>> GetCrossMarginSmallLiabilityExchangeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("current", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("size", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/exchange-small-liability-history", BinanceExchange.RateLimiter.SpotRestUid, 100, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceSmallLiabilityHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region GetTradeFee

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/tradeFee", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceTradeFee>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
