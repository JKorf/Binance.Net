using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot.SubAccountData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients
{
    /// <summary>
    /// Sub accounts endpoints
    /// </summary>
    public class BinanceClientSubAccount : IBinanceClientSubAccount
    {
        private const string subAccountListEndpoint = "sub-account/list.html";
        private const string subAccountTransferHistoryEndpoint = "sub-account/transfer/history.html";
        private const string transferSubAccountEndpoint = "sub-account/transfer.html";
        private const string subAccountStatusEndpoint = "sub-account/status";
        private const string subAccountAssetsEndpoint = "sub-account/assets.html";

        private const string subAccountDepositAddressEndpoint = "capital/deposit/subAddress";
        private const string subAccountDepositHistoryEndpoint = "capital/deposit/subHisrec";


        private const string subAccountEnableMarginEndpoint = "sub-account/margin/enable";
        private const string subAccountMarginDetailsEndpoint = "sub-account/margin/account";
        private const string subAccountMarginSummaryEndpoint = "sub-account/margin/accountSummary";
        private const string subAccountTransferMarginSpotEndpoint = "sub-account/margin/transfer";

        private const string subAccountEnableFuturesEndpoint = "sub-account/futures/enable";
        private const string subAccountFuturesDetailsEndpoint = "sub-account/futures/account";
        private const string subAccountFuturesSummaryEndpoint = "sub-account/futures/accountSummary";
        private const string subAccountTransferFuturesSpotEndpoint = "sub-account/futures/transfer";
        private const string subAccountFuturesPositionRiskEndpoint = "sub-account/futures/positionRisk";

        private const string subAccountTransferToSubEndpoint = "sub-account/transfer/subToSub";
        private const string subAccountTransferToMasterEndpoint = "sub-account/transfer/subToMaster";
        private const string subAccountTransferHistorySubAccountEndpoint = "sub-account/transfer/subUserHistory";

        private const string subAccountSpotSummaryEndpoint = "sub-account/spotSummary";

        private readonly BinanceClient _baseClient;

        internal BinanceClientSubAccount(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Sub-Account Endpoints

        #region Query Sub-account List(For Master Account)

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        public WebCallResult<IEnumerable<BinanceSubAccount>> GetSubAccounts(string? email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default) => GetSubAccountsAsync(email, accountStatus, page, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccount>>> GetSubAccountsAsync(string? email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccount>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("status", accountStatus != null ? JsonConvert.SerializeObject(accountStatus, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountWrapper>(_baseClient.GetUrlSpot(subAccountListEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<IEnumerable<BinanceSubAccount>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<IEnumerable<BinanceSubAccount>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message!)) : new WebCallResult<IEnumerable<BinanceSubAccount>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.SubAccounts, null);
        }

        #endregion

        #region Query Sub-account Transfer History(For Master Account)

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountTransfer>> GetSubAccountTransferForMasterHistory(string? email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default) => GetSubAccountTransferHistoryForMasterAsync(email, startTime, endTime, page, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountTransfer>>> GetSubAccountTransferHistoryForMasterAsync(string? email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountTransfer>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountTransferWrapper>(_baseClient.GetUrlSpot(subAccountTransferHistoryEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<IEnumerable<BinanceSubAccountTransfer>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<IEnumerable<BinanceSubAccountTransfer>>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message!)) : new WebCallResult<IEnumerable<BinanceSubAccountTransfer>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Transfers, null);
        }

        #endregion

        #region Sub-account Transfer(For Master Account)

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransferResult> TransferSubAccount(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default) => TransferSubAccountAsync(fromEmail, toEmail, asset, amount, receiveWindow, ct).Result;

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransferResult>> TransferSubAccountAsync(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default)
        {
            fromEmail.ValidateNotNull(nameof(fromEmail));
            toEmail.ValidateNotNull(nameof(toEmail));
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "fromEmail", fromEmail },
                { "toEmail", toEmail },
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransferResult>(_baseClient.GetUrlSpot(transferSubAccountEndpoint, "wapi", "3"), HttpMethod.Post, ct, parameters, true, true, PostParameters.InUri).ConfigureAwait(false);
        }

        #endregion

        #region Query Sub-account Assets(For Master Account)

        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        public WebCallResult<IEnumerable<BinanceBalance>> GetSubAccountAssets(string email, int? receiveWindow = null,
            CancellationToken ct = default) =>
            GetSubAccountAssetsAsync(email, receiveWindow, ct).Result;


        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceBalance>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceSubAccountAsset>(_baseClient.GetUrlSpot(subAccountAssetsEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return WebCallResult<IEnumerable<BinanceBalance>>.CreateErrorResult(result.ResponseStatusCode,
                    result.ResponseHeaders, result.Error!);

            if (!result.Data.Success)
                return WebCallResult<IEnumerable<BinanceBalance>>.CreateErrorResult(result.ResponseStatusCode,
                    result.ResponseHeaders, new ServerError(result.Data.Message));

            return new WebCallResult<IEnumerable<BinanceBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Balances, null);
        }
        #endregion

        #region Get Sub-account Deposit Address (For Master Account)
        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        public WebCallResult<BinanceSubAccountDepositAddress> GetSubAccountDepositAddress(string email, string coin,
            string? network = null, int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountDepositAddressAsync(email, coin, network, receiveWindow, ct).Result;


        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        public async Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            coin.ValidateNotNull(nameof(coin));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountDepositAddress>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "coin", coin },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("network", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountDepositAddress>(_baseClient.GetUrlSpot(subAccountDepositAddressEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Get Sub-account Deposit History (For Master Account)

        /// <summary>
        /// Gets the deposit history for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get history for</param>
        /// <param name="coin">Filter for a coin</param>
        /// <param name="startTime">Only return deposits placed later this</param>
        /// <param name="endTime">Only return deposits placed before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset results by this</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit history</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountDeposit>> GetSubAccountDepositHistory(string email,
            string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null,
            int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountDepositHistoryAsync(email, coin, startTime, endTime, limit, offset, receiveWindow, ct)
                .Result;

        /// <summary>
        /// Gets the deposit history for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get history for</param>
        /// <param name="coin">Filter for a coin</param>
        /// <param name="startTime">Only return deposits placed later this</param>
        /// <param name="endTime">Only return deposits placed before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset results by this</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountDeposit>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("coin", coin);
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", offset?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountDeposit>>(_baseClient.GetUrlSpot(subAccountDepositHistoryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Sub-account's Status on Margin/Futures(For Master Account)

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountStatus>> GetSubAccountStatus(string? email = null, int? receiveWindow = null, CancellationToken ct = default) => GetSubAccountStatusAsync(email, receiveWindow, ct).Result;

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountStatus>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountStatus>>(_baseClient.GetUrlSpot(subAccountStatusEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Enable Margin for Sub-account (For Master Account)

        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default)
            => EnableMarginForSubAccountAsync(email, receiveWindow, ct).Result;


        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountMarginEnabled>>(_baseClient.GetUrlSpot(subAccountEnableMarginEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Margin Account (For Master Account)

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        public WebCallResult<BinanceSubAccountMarginDetails> GetSubAccountMarginDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountMarginDetailsAsync(email, receiveWindow, ct).Result;

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        public async Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountMarginDetails>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountMarginDetails>(_baseClient.GetUrlSpot(subAccountMarginDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Margin Account (For Master Account)

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        public WebCallResult<BinanceSubAccountsMarginSummary> GetSubAccountsMarginSummary(
            int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountsMarginSummaryAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        public async Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountsMarginSummary>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountsMarginSummary>(_baseClient.GetUrlSpot(subAccountMarginSummaryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Enable Futures for Sub-account (For Master Account) 
        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        public WebCallResult<BinanceSubAccountFuturesEnabled> EnableFuturesForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default)
            => EnableFuturesForSubAccountAsync(email, receiveWindow, ct).Result;

        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        public async Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountFuturesEnabled>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesEnabled>(_baseClient.GetUrlSpot(subAccountEnableFuturesEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Detail on Sub-account's Futures Account (For Master Account) 

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        public WebCallResult<BinanceSubAccountFuturesDetails> GetSubAccountFuturesDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountFuturesDetailsAsync(email, receiveWindow, ct).Result;

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        public async Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountFuturesDetails>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountFuturesDetails>(_baseClient.GetUrlSpot(subAccountFuturesDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Summary of Sub-account's Futures Account (For Master Account)

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        public WebCallResult<BinanceSubAccountsFuturesSummary> GetSubAccountsFuturesSummary(int? receiveWindow = null,
            CancellationToken ct = default)
            => GetSubAccountsFuturesSummaryAsync(receiveWindow, ct).Result;

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        public async Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountsFuturesSummary>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountsFuturesSummary>(_baseClient.GetUrlSpot(subAccountFuturesSummaryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Futures Postion-Risk of Sub-account (For Master Account)

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>> GetSubAccountsFuturesPositionRisk(
            string email, int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountsFuturesPositionRiskAsync(email, receiveWindow, ct).Result;

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountFuturesPositionRisk>>(_baseClient.GetUrlSpot(subAccountFuturesPositionRiskEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Futures Transfer for Sub-account (For Master Account)

        /// <summary>
        /// Transfers from or to a futures sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransaction> TransferSubAccountFutures(string email, string asset,
            decimal amount, SubAccountFuturesTransferType type, int? receiveWindow = null,
            CancellationToken ct = default)
            => TransferSubAccountFuturesAsync(email, asset, amount, type, receiveWindow, ct).Result;

        /// <summary>
        /// Transfers from or to a futures sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal amount, SubAccountFuturesTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "asset", asset },
                { "type", JsonConvert.SerializeObject(type, new SubAccountFuturesTransferTypeConverter(false)) },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrlSpot(subAccountTransferFuturesSpotEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Margin Transfer for Sub-account (For Master Account)
        /// <summary>
        /// Transfers from or to a margin sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransaction> TransferSubAccountMargin(string email, string asset,
            decimal amount, SubAccountMarginTransferType type, int? receiveWindow = null,
            CancellationToken ct = default)
            => TransferSubAccountMarginAsync(email, asset, amount, type, receiveWindow, ct).Result;

        /// <summary>
        /// Transfers from or to a margin sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal amount, SubAccountMarginTransferType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "email", email },
                { "asset", asset },
                { "type", JsonConvert.SerializeObject(type, new SubAccountMarginTransferTypeConverter(false)) },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrlSpot(subAccountTransferMarginSpotEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Sub-account of Same Master (For Sub-account)
        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToSubAccount(string email, string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default)
            => TransferSubAccountToSubAccountAsync(email, asset, amount, receiveWindow, ct).Result;

        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default)
        {
            email.ValidateNotNull(nameof(email));
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "toEmail", email },
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrlSpot(subAccountTransferToSubEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Transfer to Master (For Sub-account)
        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToMaster(string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default)
            => TransferSubAccountToMasterAsync(asset, amount, receiveWindow, ct).Result;

        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountTransaction>(_baseClient.GetUrlSpot(subAccountTransferToMasterEndpoint, "sapi", "1"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Sub-account Transfer History (For Sub-account)
        /// <summary>
        /// Gets the transfer history of a sub account (from the sub account)
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of transfer</param>
        /// <param name="startTime">Only return transfers later than this</param>
        /// <param name="endTime">Only return transfers before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>> GetSubAccountTransferHistoryForSubAccount(
            string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null,
            DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountTransferHistoryForSubAccountAsync(asset, type, startTime, endTime, limit, receiveWindow, ct)
                .Result;


        /// <summary>
        /// Gets the transfer history of a sub account (from the sub account)
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of transfer</param>
        /// <param name="startTime">Only return transfers later than this</param>
        /// <param name="endTime">Only return transfers before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        public async Task<WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("type", type);
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type, new SubAccountTransferSubAccountTypeConverter(false)));
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceSubAccountTransferSubAccount>>(_baseClient.GetUrlSpot(subAccountTransferHistorySubAccountEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Query Sub-account Spot Assets Summary (For Master Account)

        /// <summary>
        /// Get BTC valued asset summary of subaccounts.
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="page">The page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Btc asset values</returns>
        public WebCallResult<BinanceSubAccountSpotAssetsSummary> GetSubAccountBtcValues(
            string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
            => GetSubAccountBtcValuesAsync(email, page, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Get BTC valued asset summary of subaccounts.
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="page">The page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Btc asset values</returns>
        public async Task<WebCallResult<BinanceSubAccountSpotAssetsSummary>> GetSubAccountBtcValuesAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceSubAccountSpotAssetsSummary>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() },
            };

            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceSubAccountSpotAssetsSummary>(_baseClient.GetUrlSpot(subAccountSpotSummaryEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
