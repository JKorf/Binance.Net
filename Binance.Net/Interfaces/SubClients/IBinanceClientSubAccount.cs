using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot.SubAccountData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients
{
    /// <summary>
    /// Sub accounts interface
    /// </summary>
    public interface IBinanceClientSubAccount
    {
        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="isFreeze">Is freezed</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        WebCallResult<IEnumerable<BinanceSubAccount>> GetSubAccounts(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, bool? isFreeze = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="isFreeze">Is freezed</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccount>>> GetSubAccountsAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, bool? isFreeze = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="fromEmail">Filter the history by from email</param>
        /// <param name="toEmail">Filter the history by to email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        WebCallResult<IEnumerable<BinanceSubAccountTransfer>> GetSubAccountTransferForMasterHistory(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="fromEmail">Filter the history by from email</param>
        /// <param name="toEmail">Filter the history by to email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountTransfer>>> GetSubAccountTransferHistoryForMasterAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

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
        WebCallResult<BinanceSubAccountTransferResult> TransferSubAccount(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

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
        Task<WebCallResult<BinanceSubAccountTransferResult>> TransferSubAccountAsync(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        WebCallResult<IEnumerable<BinanceBalance>> GetSubAccountAssets(string email, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        WebCallResult<BinanceSubAccountDepositAddress> GetSubAccountDepositAddress(string email, string coin,
            string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

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
        WebCallResult<IEnumerable<BinanceSubAccountDeposit>> GetSubAccountDepositHistory(string email,
            string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null,
            int? offset = null, int? receiveWindow = null, CancellationToken ct = default);

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
        Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        WebCallResult<IEnumerable<BinanceSubAccountStatus>> GetSubAccountStatus(string? email = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        WebCallResult<BinanceSubAccountMarginDetails> GetSubAccountMarginDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        WebCallResult<BinanceSubAccountsMarginSummary> GetSubAccountsMarginSummary(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        WebCallResult<BinanceSubAccountFuturesEnabled> EnableFuturesForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        WebCallResult<BinanceSubAccountFuturesDetails> GetSubAccountFuturesDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        WebCallResult<BinanceSubAccountsFuturesSummary> GetSubAccountsFuturesSummary(int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>> GetSubAccountsFuturesPositionRisk(
            string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

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
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountFutures(string email, string asset,
            decimal amount, SubAccountFuturesTransferType type, int? receiveWindow = null,
            CancellationToken ct = default);

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
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal amount, SubAccountFuturesTransferType type, int? receiveWindow = null, CancellationToken ct = default);

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
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountMargin(string email, string asset,
            decimal amount, SubAccountMarginTransferType type, int? receiveWindow = null,
            CancellationToken ct = default);

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
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal amount, SubAccountMarginTransferType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToSubAccount(string email, string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToMaster(string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

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
        WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>> GetSubAccountTransferHistoryForSubAccount(
            string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null,
            DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

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
        Task<WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BTC valued asset summary of subaccounts.
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="page">The page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Btc asset values</returns>
        WebCallResult<BinanceSubAccountSpotAssetsSummary> GetSubAccountBtcValues(
            string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BTC valued asset summary of subaccounts.
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="page">The page</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Btc asset values</returns>
        Task<WebCallResult<BinanceSubAccountSpotAssetsSummary>> GetSubAccountBtcValuesAsync(string? email = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Create a virtual sub account
        /// </summary>
        /// <param name="email">Virtual email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<BinanceSubAccountEmail> CreateVirtualSubAccount(
            string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Create a virtual sub account
        /// </summary>
        /// <param name="email">Virtual email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSubAccountEmail>> CreateVirtualSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enable or disable blvt
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="enable">Enable or disable (only true for now)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<BinanceSubAccountBlvt> EnableBlvtForSubAccount(
            string email, bool enable, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enable or disable blvt
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="enable">Enable or disable (only true for now)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSubAccountBlvt>> EnableBlvtForSubAccountAsync(string email, bool enable, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of universal transfers
        /// </summary>
        /// <param name="fromEmail">Filter the list by from email (fromEmail and toEmail cannot be present at same time)</param>
        /// <param name="toEmail">Filter the list by to email (fromEmail and toEmail cannot be present at same time)</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return (Default 500, max 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of universal transfers</returns>
        WebCallResult<IEnumerable<BinanceSubAccountUniversalTransferTransaction>> GetUniversalTransferHistory(string? fromEmail = null, string? toEmail = null, long? startTime = null, long? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of universal transfers
        /// </summary>
        /// <param name="fromEmail">Filter the list by from email (fromEmail and toEmail cannot be present at same time)</param>
        /// <param name="toEmail">Filter the list by to email (fromEmail and toEmail cannot be present at same time)</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return (Default 500, max 500)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of universal transfers</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>> GetUniversalTransferHistoryAsync(string? fromEmail = null, string? toEmail = null, long? startTime = null, long? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
    }
}