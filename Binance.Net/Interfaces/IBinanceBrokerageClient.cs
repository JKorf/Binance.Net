﻿using Binance.Net.Objects.Brokerage.SubAccountData;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces
{
    public interface IBinanceBrokerageClient : IRestClient
    {
        Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string id, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string id, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string id, 
            bool isTradingEnabled, bool? isMarginTradingEnabled = null, 
            bool? isFuturesTradingEnabled = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<object> DeleteSubAccountApiKeyAsync(string id, string apiKey, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> GetSubAccountApiKeyAsync(string id, string apiKey = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiPermissionAsync(string id, string apiKey, 
            bool isTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string id = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string id, decimal makerCommission, decimal takerCommission,
            decimal? marginMakerCommission = null, decimal? marginTakerCommission = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string id, string symbol, 
            int makerAdjustment, int takerAdjustment, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string id, 
            string symbol = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageTransferResult>> TransferAsync(string asset, decimal amount, 
            string fromId = null, string toId = null, string clientTransferId = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string id, string clientTransferId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<string>> GetBrokerCommissionRebatesHistoryAsync(string subAccountId = null, 
            DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnSpotAndMarginResult>> EnableOrDisableBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default);
        
        Task<WebCallResult<BinanceBrokerageEnableOrDisableBnbBurnMarginInterestResult>> EnableOrDisableBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn, 
            int? receiveWindow = null, CancellationToken ct = default);

        Task<WebCallResult<BinanceBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId,
            int? receiveWindow = null, CancellationToken ct = default);
    }
}