using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.PortfolioMargin;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceRestClientSpotApiAccount : IBinanceRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientSpotApi _baseClient;

        internal BinanceRestClientSpotApiAccount(BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Account info
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(bool? omitZeroBalances = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("omitZeroBalances", omitZeroBalances?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/account", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Fiat Payments History 
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFiatPayment[]>> GetFiatPaymentHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
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
            var result = await _baseClient.SendAsync<BinanceResult<BinanceFiatPayment[]>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data?.Data!);
        }

        #endregion

        #region Get Fiat Deposit Withdraw History 
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFiatWithdrawDeposit[]>> GetFiatDepositWithdrawHistoryAsync(TransactionType side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
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
            var result = await _baseClient.SendAsync<BinanceResult<BinanceFiatWithdrawDeposit[]>>(request, parameters, ct).ConfigureAwait(false);
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
            parameters.AddOptionalEnum("walletType", walletType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/capital/withdraw/apply", BinanceExchange.RateLimiter.SpotRestUid, 900, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceWithdrawalPlaced>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Withdraw History
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceWithdrawal[]>> GetWithdrawalHistoryAsync(string? asset = null, string? withdrawOrderId = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("withdrawOrderId", withdrawOrderId);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("offset", offset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/withdraw/history", BinanceExchange.RateLimiter.SpotRestUid, 18000, true,
                limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceWithdrawal[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdrawal Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceWithdrawalAddress[]>> GetWithdrawalAddressesAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/withdraw/address/list", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceWithdrawalAddress[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Deposit history        
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceDeposit[]>> GetDepositHistoryAsync(string? asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, bool includeSource = false, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("includeSource", includeSource.ToString());

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/deposit/hisrec", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceDeposit[]>(request, parameters, ct).ConfigureAwait(false);
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
            return await _baseClient.SendAsync<BinanceDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Daily snapshots
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSpotAccountSnapshot[]>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await GetDailyAccountSnapshot<BinanceSnapshotWrapper<BinanceSpotAccountSnapshot[]>>(AccountType.Spot, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);
            if (result.Data.Code != 200)
                return result.AsError<BinanceSpotAccountSnapshot[]>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.SnapshotData);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAccountSnapshot[]>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await GetDailyAccountSnapshot<BinanceSnapshotWrapper<BinanceMarginAccountSnapshot[]>>(AccountType.Margin, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);
            if (result.Data.Code != 200)
                return result.AsError<BinanceMarginAccountSnapshot[]>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.SnapshotData);
        }

        // TODO Should be moved
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesAccountSnapshot[]>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await GetDailyAccountSnapshot<BinanceSnapshotWrapper<BinanceFuturesAccountSnapshot[]>>(AccountType.Futures, startTime, endTime, limit, receiveWindow, ct).ConfigureAwait(false);
            if (result.Data.Code != 200)
                return result.AsError<BinanceFuturesAccountSnapshot[]>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.SnapshotData);
        }

        private async Task<WebCallResult<T>> GetDailyAccountSnapshot<T>(AccountType accountType, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default) where T : class
        {
            limit?.ValidateIntBetween(nameof(limit), 7, 30);

            var parameters = new ParameterCollection();
            parameters.AddEnum("type", accountType);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/accountSnapshot", BinanceExchange.RateLimiter.SpotRestIp, 2400, true);
            var result = await _baseClient.SendAsync<T>(request, parameters, ct).ConfigureAwait(false);
            return result;
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
        public async Task<WebCallResult<BinanceFundingAsset[]>> GetFundingWalletAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("needBtcValuation", needBtcValuation?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/get-funding-asset", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceFundingAsset[]>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<BinanceUserAsset[]>> GetUserAssetsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/capital/config/getall", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceUserAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Balances
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUserBalance[]>> GetBalancesAsync(string? asset = null, bool? needBtcValuation = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("needBtcValuation", needBtcValuation);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v3/asset/getUserAsset", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            return await _baseClient.SendAsync<BinanceUserBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Wallet Balances
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceWalletBalance[]>> GetWalletBalancesAsync(string quoteAsset = "BTC", int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("quoteAsset", quoteAsset);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/wallet/balance", BinanceExchange.RateLimiter.SpotRestIp, 60, true);
            return await _baseClient.SendAsync<BinanceWalletBalance[]>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/dribblet", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
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
        public async Task<WebCallResult<BinanceEligibleDusts>> GetAssetsForDustTransferAsync(AccountType? accountType = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("accountType", accountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/asset/dust-btc", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceEligibleDusts>(request, parameters, ct).ConfigureAwait(false);
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
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddEnum("type", type);
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
            var parameters = new ParameterCollection();
            parameters.AddEnum("type", type);
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 2);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Level Information

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginLevel>> GetMarginLevelInformationAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
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

            var parameters = new ParameterCollection();
            parameters.AddEnum("direction", direction);
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
        public async Task<WebCallResult<BinanceInterestMarginData[]>> GetInterestMarginDataAsync(string? asset = null, string? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            asset?.ValidateNotNull(nameof(asset));

            var parameters = new ParameterCollection();

            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("vipLevel", vipLevel?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = asset == null ? 5 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/crossMarginData", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinanceInterestMarginData[]>(request, parameters, ct, weight).ConfigureAwait(false);
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
        public async Task<WebCallResult<BinanceInterestRateHistory[]>> GetMarginInterestRateHistoryAsync(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
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
            return await _baseClient.SendAsync<BinanceInterestRateHistory[]>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<BinanceIsolatedMarginTierData[]>> GetIsolatedMarginTierDataAsync(string symbol, int? tier = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("tier", tier);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolatedMarginTier", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceIsolatedMarginTierData[]>(request, parameters, ct).ConfigureAwait(false);
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
        public async Task<WebCallResult<BinanceCurrentRateLimit[]>> GetMarginOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/rateLimit/order", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceCurrentRateLimit[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Create a ListenKey

        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/userDataStream", BinanceExchange.RateLimiter.SpotRestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1);
            var result = await _baseClient.SendAsync<BinanceListenKey>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/userDataStream/isolated", BinanceExchange.RateLimiter.SpotRestIp, 1);
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
        public async Task<WebCallResult<BinanceCurrentRateLimit[]>> GetOrderRateLimitStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/rateLimit/order", BinanceExchange.RateLimiter.SpotRestIp, 40, true);
            return await _baseClient.SendAsync<BinanceCurrentRateLimit[]>(request, parameters, ct).ConfigureAwait(false);
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

        #region Portfolio margin

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePortfolioMarginInfo>> GetPortfolioMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/portfolio/account", BinanceExchange.RateLimiter.SpotRestIp, 5, true);
            return await _baseClient.SendAsync<BinancePortfolioMarginInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePortfolioMarginCollateralRate[]>> GetPortfolioMarginCollateralRateAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/portfolio/collateralRate", BinanceExchange.RateLimiter.SpotRestIp, 50, false);
            return await _baseClient.SendAsync<BinancePortfolioMarginCollateralRate[]>(request, parameters, ct).ConfigureAwait(false);
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

        #region Convert BUSD history

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceBusdHistory>>> GetBusdConvertHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("tranId", transferId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/convert-transfer/queryByPage", BinanceExchange.RateLimiter.SpotRestUid, 5, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceBusdHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Cloud Mining History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceCloudMiningHistory>>> GetCloudMiningHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? clientTransferId = null, string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptional("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("tranId", transferId);
            parameters.AddOptionalParameter("clientTranId", clientTransferId);
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/ledger-transfer/cloud-mining/queryByPage", BinanceExchange.RateLimiter.SpotRestUid, 600, true);
            return await _baseClient.SendAsync<BinanceQueryRecords<BinanceCloudMiningHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Isolated Margin Fee Data

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIsolatedMarginFeeData[]>> GetIsolatedMarginFeeDataAsync(string? symbol = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("vipLevel", vipLevel);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 10 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolatedMarginData", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinanceIsolatedMarginFeeData[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Small Liability Exchange Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSmallLiabilityAsset[]>> GetCrossMarginSmallLiabilityExchangeAssetsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/exchange-small-liability", BinanceExchange.RateLimiter.SpotRestIp, 100, true);
            return await _baseClient.SendAsync<BinanceSmallLiabilityAsset[]>(request, parameters, ct).ConfigureAwait(false);
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

        #region Get Trade Fee

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTradeFee[]>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var path = "sapi/v1/asset/tradeFee";
            if (_baseClient.ClientOptions.Environment == BinanceEnvironment.Us)
                path = "sapi/v1/asset/query/trading-fee";

            var request = _definitions.GetOrCreate(HttpMethod.Get, path, BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceTradeFee[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account VIP level and margin/futures enabled status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceVipLevelAndStatus>> GetAccountVipLevelAndStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/account/info", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceVipLevelAndStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade Fee

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCommissions>> GetCommissionRatesAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/account/commission", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceCommissions>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
