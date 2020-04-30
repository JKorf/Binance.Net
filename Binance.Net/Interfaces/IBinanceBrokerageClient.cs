using Binance.Net.Objects.Brokerage.SubAccountData;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
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
    }
}