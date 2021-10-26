using Binance.Net.Objects.Brokerage.SubAccountData;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Client providing access to the Binance Brokerage REST Api
    /// </summary>
    public interface IBinanceBrokerageClient : IRestClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if successful ping, false if no response</returns>
        new Task<CallResult<long>> PingAsync(CancellationToken ct = default);
        
        /// <summary>
        /// Generate a sub account under your brokerage master account
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Created sub-account id</returns>
        Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Enable Margin for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Margin result</returns>
        Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Enable Futures for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Enable Futures result</returns>
        Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Create Api Key for Sub Account
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="isTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, 
            bool isTradingEnabled, bool? isMarginTradingEnabled = null, 
            bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Delete Sub Account Api Key
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey"></param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<object> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Sub Account Api Key
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountApiKey>>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Change Sub Account Api Permission
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>
        /// <para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="isTradingEnabled">Is spot trading enabled</param>
        /// <param name="isMarginTradingEnabled">Is margin trading enabled</param>
        /// <param name="isFuturesTradingEnabled">Is futures trading enabled</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Api key result</returns>
        Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiPermissionAsync(string subAccountId, string apiKey, 
            bool isTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub accounts</returns>
        Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string? subAccountId = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Change Sub Account Commission
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>If margin disabled, it is not allowed to send marginMakerCommission or marginTakerCommission</para>
        /// <para>If margin enabled, marginMakerCommission or marginTakerCommission has default value as spotMakerCommission or spotTakerCommission</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="makerCommission">Maker commission</param>
        /// <param name="takerCommission">Taker commission</param>
        /// <param name="marginMakerCommission">Margin maker commission</param>
        /// <param name="marginTakerCommission">Margin taker commission</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account commission result</returns>
        Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId, decimal makerCommission, decimal takerCommission,
            decimal? marginMakerCommission = null, decimal? marginTakerCommission = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Change Sub Account Futures Commission Adjustment
        /// <para>You need to enable "trade" option for the api key which requests this endpoint</para>
        /// <para>The sub-account's futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="makerAdjustment">Maker adjustment (100 for 0.01%)</param>
        /// <param name="takerAdjustment">Taker adjustment (100 for 0.01%)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account futures commission result</returns>
        Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol, 
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Sub Account Futures Commission Adjustment
        /// <para>The sub-account's futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>
        /// <para>If symbol not sent, commission adjustment of all symbols will be returned</para>
        /// <para>If futures disabled, it is not allowed to set subaccount's futures commission adjustment on any symbol</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Sub account futures commissions result</returns>
        Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, 
            string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Broker Account Information
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Broker information</returns>
        Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Sub Account Transfer
        /// <para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>
        /// <para>Transfer from master account if fromId not sent</para>
        /// <para>Transfer to master account if toId not sent</para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="fromId">From id</param>
        /// <param name="toId">To id</param>
        /// <param name="clientTransferId">Client transfer id, must be unique</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer result</returns>
        Task<WebCallResult<BinanceBrokerageTransferResult>> TransferAsync(string asset, decimal quantity, 
            string fromId, string toId, string? clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Sub Account Transfer History
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="clientTransferId">Client transfer id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string subAccountId, string? clientTransferId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Broker Commission Rebate Recent Record
        /// <para>Only get the latest history of past 7 days</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="limit">Limit (Default 500, max 1000)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rebates history</returns>
        Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string? subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Query Broker Commission Rebate History
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="startDate">From date</param>
        /// <param name="endDate">To date</param>
        /// <param name="limit">Limit (default 1000)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>A download link for an offline file</returns>
        Task<WebCallResult<string>> GetBrokerCommissionRebatesHistoryAsync(string? subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Enable Or Disable BNB Burn for Sub Account SPOT and MARGIN
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="spotBnbBurn">"true" or "false", spot and margin whether use BNB to pay for transaction fees or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result</returns>
        Task<WebCallResult<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>> ChangeBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Enable Or Disable BNB Burn for Sub Account Margin Interest
        /// <para>Sub account must be enabled margin before using this switch</para>
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="interestBnbBurn">"true" or "false", margin loan whether uses BNB to pay for margin interest or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Result</returns>
        Task<WebCallResult<BinanceBrokerageChangeBnbBurnMarginInterestResult>> ChangeBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get BNB Burn Status for Sub Account
        /// </summary>
        /// <param name="subAccountId">Sub account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Status</returns>
        Task<WebCallResult<BinanceBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId, int? receiveWindow = null, CancellationToken ct = default);
    }
}