using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.SubAccountData;
using CryptoExchange.Net.Objects.Errors;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiSubAccount : IBinanceRestClientGeneralApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiSubAccount(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Query Sub-account List(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccount[]>> GetSubAccountsAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, bool? isFreeze = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isFreeze", isFreeze);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/list", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceSubAccountWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.Success ? HttpResult.Ok(result, result.Data.SubAccounts ?? []) : HttpResult.Fail<BinanceSubAccount[]>(result);
        }

        #endregion

        #region Query Sub-account Transfer History(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransfer[]>> GetSubAccountTransferHistoryForMasterAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/sub/transfer/history", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountTransfer[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Sub-account Transfer(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceTransaction>> TransferSubAccountAsync(TransferAccountType fromAccountType, TransferAccountType toAccountType, string asset, decimal quantity, string? fromEmail = null, string? toEmail = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(fromEmail) && string.IsNullOrEmpty(toEmail))
                throw new ArgumentException("fromEmail and/or toEmail should be provided");
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("fromAccountType", fromAccountType);
            parameters.Add("toAccountType", toAccountType);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/universalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceTransaction>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Sub-account Assets(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceBalance[]>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v3/sub-account/assets", BinanceExchange.RateLimiter.SpotRestUid, 60, true);
            var result = await _baseClient.SendAsync<BinanceSubAccountAsset>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceBalance[]>(result);

            if (!result.Data.Success)
                return HttpResult.Fail<BinanceBalance[]>(result, new ServerError(ErrorInfo.Unknown with { Message = result.Data!.Message }));

            return HttpResult.Ok(result, result.Data.Balances);
        }
        #endregion

        #region Get Sub-account Deposit Address (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "coin", asset }
            };

            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/capital/deposit/subAddress", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountDepositAddress>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Sub-account Deposit History (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountDeposit[]>> GetSubAccountDepositHistoryAsync(string email, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/capital/deposit/subHisrec", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountDeposit[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Sub-account's Status on Margin/Futures(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountStatus[]>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/status", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceSubAccountStatus[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Enable Margin for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/margin/enable", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountMarginEnabled>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Margin Account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/margin/account", BinanceExchange.RateLimiter.SpotRestIp, 10, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountMarginDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Margin Account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/margin/accountSummary", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceSubAccountsMarginSummary>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Enable Futures for Sub-account (For Master Account) 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/enable", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountFuturesEnabled>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Futures Account (For Master Account) 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/account", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceSubAccountFuturesDetails>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountFuturesDetailsV2>> GetSubAccountFuturesDetailsAsync(FuturesAccountType futuresAccountType, string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
            };

            parameters.Add("futuresType", futuresAccountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v2/sub-account/futures/account", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountFuturesDetailsV2>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Futures Account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/accountSummary", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountsFuturesSummary>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Futures Postion-Risk of Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountFuturesPositionRisk[]>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/positionRisk", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceSubAccountFuturesPositionRisk[]>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountFuturesPositionRiskV2>> GetSubAccountsFuturesPositionRiskAsync(FuturesAccountType futuresAccountType, string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
            };
            parameters.Add("futuresType", futuresAccountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v2/sub-account/futures/positionRisk", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountFuturesPositionRiskV2>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Futures Transfer for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal quantity, FuturesTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("type", type);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/transfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Margin Transfer for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal quantity, SubAccountMarginTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("type", type);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/margin/transfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Sub-account of Same Master (For Sub-account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "toEmail", email },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/transfer/subToSub", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Master (For Sub-account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/transfer/subToMaster", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Sub-account Transfer History (For Sub-account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransferSubAccount[]>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("asset", asset);
            parameters.Add("type", type);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/transfer/subUserHistory", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountTransferSubAccount[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Query Sub-account Spot Assets Summary (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountSpotAssetsSummary>> GetSubAccountBtcValuesAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/spotSummary", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountSpotAssetsSummary>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create a Virtual Sub-account(For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountEmail>> CreateVirtualSubAccountAsync(string subAccountString, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "subAccountString", subAccountString }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/virtualSubAccount", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountEmail>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Enable Leverage Token for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountBlvt>> EnableBlvtForSubAccountAsync(string email, bool enable, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "enableBlvt", enable }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/blvt/enable", BinanceExchange.RateLimiter.SpotRestIp, 1, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceSubAccountBlvt>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Universal Transfer History (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountUniversalTransferTransaction[]>> GetUniversalTransferHistoryAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/universalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceSubAccountUniversalTransfersList>(request, parameters, ct).ConfigureAwait(false);
            return result.Success ? HttpResult.Ok(result, result.Data.Transactions) : HttpResult.Fail<BinanceSubAccountUniversalTransferTransaction[]>(result);
        }

        #endregion

        #region IP restrictions
        /// <inheritdoc />
        public async Task<HttpResult<BinanceIpRestriction>> UpdateIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, bool ipRestrict, IEnumerable<string>? ipAddresses, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "subAccountApiKey", apiKey },
                { "status", ipRestrict ? 2: 1 }
            };

            if (ipAddresses != null)
                parameters.AddOptionalParameter("ipAddress", string.Join(",", ipAddresses));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v2/sub-account/subAccountApi/ipRestriction", BinanceExchange.RateLimiter.SpotRestUid, 3000, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceIpRestriction>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceIpRestriction>> RemoveIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, IEnumerable<string>? ipAddresses, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "subAccountApiKey", apiKey }
            };

            if (ipAddresses != null)
                parameters.AddOptionalParameter("ipAddress", string.Join(",", ipAddresses));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "sapi/v1/sub-account/subAccountApi/ipRestriction/ipList", BinanceExchange.RateLimiter.SpotRestUid, 3000, true, parameterPosition: HttpMethodParameterPosition.InUri);
            return await _baseClient.SendAsync<BinanceIpRestriction>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
                { "subAccountApiKey", apiKey },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/subAccountApi/ipRestriction", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceIpRestriction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Query Sub-account Futures Asset Transfer History (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountAssetTransferHistory[]>> GetFuturesAssetTransferHistoryAsync(string email, FuturesAccountType accountType, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "email", email },
            };
            parameters.Add("futuresType", accountType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/internalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceSubAccountAssetTransferHistoryList>(request, parameters, ct).ConfigureAwait(false);
            return result.Success ? HttpResult.Ok(result, result.Data.Transfers) : HttpResult.Fail<BinanceSubAccountAssetTransferHistory[]>(result);
        }

        #endregion

        #region Sub-account Futures Asset Transfer (For Master Account)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSubAccountTransaction>> FuturesAssetTransferAsync(string fromEmail, string toEmail, FuturesAccountType accountType, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "fromEmail", fromEmail },
                { "toEmail", toEmail },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("futuresType", accountType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/sub-account/futures/internalTransfer", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceSubAccountTransaction>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
    }
}
