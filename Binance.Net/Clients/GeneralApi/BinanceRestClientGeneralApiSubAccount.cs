using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.SubAccountData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    public class BinanceRestClientGeneralApiSubAccount : IBinanceRestClientGeneralApiSubAccount
    {
        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiSubAccount(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Query Sub-account List(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccount>>> GetSubAccountsAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, bool? isFreeze = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isFreeze", isFreeze);

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountWrapper>(_baseClient.GetUrl("sub-account/list", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result ? result.As<IEnumerable<BinanceSubAccount>>(result.Data.SubAccounts) : result.As<IEnumerable<BinanceSubAccount>>(default);
        }

        #endregion

        #region Query Sub-account Transfer History(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountTransfer>>> GetSubAccountTransferHistoryForMasterAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountTransfer>>(_baseClient.GetUrl("sub-account/sub/transfer/history", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Sub-account Transfer(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTransaction>> TransferSubAccountAsync(TransferAccountType fromAccountType, TransferAccountType toAccountType, string asset, decimal quantity, string? fromEmail = null, string? toEmail = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(fromEmail) && string.IsNullOrEmpty(toEmail))
                throw new ArgumentException("fromEmail and/or toEmail should be provided");
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "fromAccountType", JsonConvert.SerializeObject(fromAccountType, new TransferAccountTypeConverter(false)) },
                { "toAccountType", JsonConvert.SerializeObject(toAccountType, new TransferAccountTypeConverter(false)) },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceTransaction>(_baseClient.GetUrl("sub-account/universalTransfer", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Query Sub-account Assets(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountAsset>(_baseClient.GetUrl("sub-account/assets", "sapi", "3"), HttpMethod.Get, ct, parameters, true, weight: 60, gate: BinanceExchange.RateLimiters.SpotApi_Uid).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<BinanceBalance>>(default);

            if (!result.Data.Success)
                return result.AsError<IEnumerable<BinanceBalance>>(new ServerError(result.Data!.Message));

            return result.As(result.Data.Balances);
        }
        #endregion

        #region Get Sub-account Deposit Address (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "coin", asset }
            };

            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountDepositAddress>(_baseClient.GetUrl("capital/deposit/subAddress", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Get Sub-account Deposit History (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountDeposit>>(_baseClient.GetUrl("capital/deposit/subHisrec", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Sub-account's Status on Margin/Futures(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountStatus>>(_baseClient.GetUrl("sub-account/status", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Enable Margin for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountMarginEnabled>(_baseClient.GetUrl("sub-account/margin/enable", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Margin Account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountMarginDetails>(_baseClient.GetUrl("sub-account/margin/account", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Margin Account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountsMarginSummary>(_baseClient.GetUrl("sub-account/margin/accountSummary", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Enable Futures for Sub-account (For Master Account) 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesEnabled>(_baseClient.GetUrl("sub-account/futures/enable", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Futures Account (For Master Account) 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesDetails>(_baseClient.GetUrl("sub-account/futures/account", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountFuturesDetailsV2>> GetSubAccountFuturesDetailsAsync(FuturesAccountType futuresAccountType, string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "futuresType", JsonConvert.SerializeObject(futuresAccountType, new FuturesAccountTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesDetailsV2>(_baseClient.GetUrl("sub-account/futures/account", "sapi", "2"), HttpMethod.Get, ct, parameters, true, weight: 1, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Futures Account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountsFuturesSummary>(_baseClient.GetUrl("sub-account/futures/accountSummary", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Futures Postion-Risk of Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountFuturesPositionRisk>>(_baseClient.GetUrl("sub-account/futures/positionRisk", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountFuturesPositionRiskV2>> GetSubAccountsFuturesPositionRiskAsync(FuturesAccountType futuresAccountType, string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "futuresType", JsonConvert.SerializeObject(futuresAccountType, new FuturesAccountTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesPositionRiskV2>(_baseClient.GetUrl("sub-account/futures/positionRisk", "sapi", "2"), HttpMethod.Get, ct, parameters, true, weight: 1, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Futures Transfer for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal quantity, SubAccountFuturesTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "asset", asset },
                { "type", JsonConvert.SerializeObject(type, new SubAccountFuturesTransferTypeConverter(false)) },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrl("sub-account/futures/transfer", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Margin Transfer for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal quantity, SubAccountMarginTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "asset", asset },
                { "type", JsonConvert.SerializeObject(type, new SubAccountMarginTransferTypeConverter(false)) },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrl("sub-account/margin/transfer", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Sub-account of Same Master (For Sub-account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "toEmail", email },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrl("sub-account/transfer/subToSub", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Master (For Sub-account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrl("sub-account/transfer/subToMaster", "sapi", "1"), HttpMethod.Post, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Sub-account Transfer History (For Sub-account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new SubAccountTransferSubAccountTypeConverter(false)));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountTransferSubAccount>>(_baseClient.GetUrl("sub-account/transfer/subUserHistory", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Query Sub-account Spot Assets Summary (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountSpotAssetsSummary>> GetSubAccountBtcValuesAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountSpotAssetsSummary>(_baseClient.GetUrl("sub-account/spotSummary", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Create a Virtual Sub-account(For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountEmail>> CreateVirtualSubAccountAsync(string subAccountString, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "subAccountString", subAccountString }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountEmail>(_baseClient.GetUrl("sub-account/virtualSubAccount", "sapi", "1"), HttpMethod.Post, ct, parameters, true, postPosition: HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Enable Leverage Token for Sub-account (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountBlvt>> EnableBlvtForSubAccountAsync(string email, bool enable, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "enableBlvt", enable }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountBlvt>(_baseClient.GetUrl("sub-account/blvt/enable", "sapi", "1"), HttpMethod.Post, ct, parameters, true, HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Query Universal Transfer History (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>> GetUniversalTransferHistoryAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("fromEmail", fromEmail);
            parameters.AddOptionalParameter("toEmail", toEmail);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountUniversalTransfersList>(_baseClient.GetUrl("sub-account/universalTransfer", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result ? result.As<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>(result.Data.Transactions) : result.As<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>(default);
        }

        #endregion

        #region IP restrictions
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIpRestriction>> UpdateIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, bool ipRestrict, IEnumerable<string>? ipAddresses, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "subAccountApiKey", apiKey },
                { "status", ipRestrict ? 2: 1 }
            };

            if (ipAddresses != null)
                parameters.AddOptionalParameter("ipAddress", string.Join(",", ipAddresses));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceIpRestriction>(_baseClient.GetUrl("sub-account/subAccountApi/ipRestriction", "sapi", "2"), HttpMethod.Post, ct, parameters, true, weight: 3000, postPosition: HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Uid).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIpRestriction>> RemoveIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, IEnumerable<string>? ipAddresses, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "subAccountApiKey", apiKey }
            };

            if(ipAddresses != null)
                parameters.AddOptionalParameter("ipAddress", string.Join(",", ipAddresses));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceIpRestriction>(_baseClient.GetUrl("sub-account/subAccountApi/ipRestriction/ipList", "sapi", "1"), HttpMethod.Delete, ct, parameters, true, weight: 3000, postPosition: HttpMethodParameterPosition.InUri, gate: BinanceExchange.RateLimiters.SpotApi_Uid).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "subAccountApiKey", apiKey },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceIpRestriction>(_baseClient.GetUrl("sub-account/subAccountApi/ipRestriction", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 3000, gate: BinanceExchange.RateLimiters.SpotApi_Uid).ConfigureAwait(false);
        }
        #endregion

        #region Query Sub-account Futures Asset Transfer History (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountAssetTransferHistory>>> GetFuturesAssetTransferHistoryAsync(string email, FuturesAccountType accountType, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "email", email },
                { "futuresType", EnumConverter.GetString(accountType) },
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountAssetTransferHistoryList>(_baseClient.GetUrl("sub-account/futures/internalTransfer", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result ? result.As(result.Data.Transfers) : result.As<IEnumerable<BinanceSubAccountAssetTransferHistory>>(default);
        }

        #endregion

        #region Sub-account Futures Asset Transfer (For Master Account)

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSubAccountTransaction>> FuturesAssetTransferAsync(string fromEmail, string toEmail, FuturesAccountType accountType, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                { "fromEmail", fromEmail },
                { "toEmail", toEmail },
                { "futuresType", EnumConverter.GetString(accountType) },
                { "asset", asset },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrl("sub-account/futures/internalTransfer", "sapi", "1"), HttpMethod.Post, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion
    }
}
