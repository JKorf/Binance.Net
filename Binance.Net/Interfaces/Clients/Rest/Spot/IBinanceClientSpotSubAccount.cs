using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.SubAccountData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public interface IBinanceClientSpotSubAccount
    {
        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-list-for-master-account" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-asset-transfer-history-for-master-account" /></para>
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
        /// Transfers an asset form/to a sub account. If fromEmail or toEmail is not send it is interpreted as from/to the master account. Transfer between futures accounts is not supported
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#sub-account-futures-asset-transfer-for-master-account" /></para>
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="fromAccountType">Account type to transfer from</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="toAccountType">Account type to transfer to</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceTransaction>> TransferSubAccountAsync(TransferAccountType fromAccountType, TransferAccountType toAccountType, string asset, decimal quantity, string? fromEmail = null, string? toEmail = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets list of balances for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-assets-for-master-account" /></para>
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset to a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-address-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="asset">The asset of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string asset, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-history-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The email of the account to get history for</param>
        /// <param name="asset">Filter for an asset</param>
        /// <param name="startTime">Only return deposits placed later this</param>
        /// <param name="endTime">Only return deposits placed before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset results by this</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit history</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-39-s-status-on-margin-futures-for-master-account" /></para>
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables margin for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#enable-margin-for-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        Task<WebCallResult<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin details for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-margin-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin summary for sub accounts
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-margin-account-for-master-account" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables futures for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#enable-futures-for-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures details for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-futures-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures summary for sub accounts
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-futures-account-for-master-account" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures position risk for a sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-futures-position-risk-of-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a futures sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#futures-transfer-for-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal quantity, SubAccountFuturesTransferType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a margin sub account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-transfer-for-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal quantity, SubAccountMarginTransferType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to another sub account of the same master
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#transfer-to-sub-account-of-same-master-for-sub-account" /></para>
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to master account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#transfer-to-master-for-sub-account" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal quantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the sub account)
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#sub-account-transfer-history-for-sub-account" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-assets-summary-for-master-account" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#create-a-virtual-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="subAccountString">String based with which a subaccount email will be generated. Should not contain special characters</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSubAccountEmail>> CreateVirtualSubAccountAsync(string subAccountString, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enable or disable blvt
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#enable-leverage-token-for-sub-account-for-master-account" /></para>
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="enable">Enable or disable (only true for now)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSubAccountBlvt>> EnableBlvtForSubAccountAsync(string email, bool enable, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of universal transfers
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-universal-transfer-history-for-master-account" /></para>
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
        Task<WebCallResult<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>> GetUniversalTransferHistoryAsync(string? fromEmail = null, string? toEmail = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Toggle IP restriction for an API key
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#enable-or-disable-ip-restriction-for-a-sub-account-api-key-for-master-account" /></para>
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="enable">Enable or disable</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIpRestriction>> ToggleIpRestrictionForApiKeyAsync(string apiKey, bool enable, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Add IP addresses to the ip whitelist for an API key
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#add-ip-list-for-a-sub-account-api-key-for-master-account" /></para>
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="ipAddresses">Addresses to whitelist</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIpRestriction>> AddIpToWhitelistForApiKeyAsync(string apiKey, IEnumerable<string> ipAddresses, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Remove IP addresses from the ip whitelist for an API key
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#delete-ip-list-for-a-sub-account-api-key-for-master-account" /></para>
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="ipAddresses">Addresses to remove from whitelist</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIpRestriction>> RemoveIpFromWhitelistForApiKeyAsync(string apiKey, IEnumerable<string> ipAddresses, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current whitelisted ip addresses for an API key
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-ip-restriction-for-a-sub-account-api-key-for-master-account" /></para>
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIpRestriction>> GetIpWhitelistForApiKeyAsync(string apiKey, int? receiveWindow = null, CancellationToken ct = default);
    }
}
