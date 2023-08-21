using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Lending;
using Binance.Net.Objects.Models.Spot.Staking;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Staking endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiStaking
    {
        /// <summary>
        /// Set auto staking for a product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#set-auto-staking-user_data" /></para>
        /// </summary>
        /// <param name="product">The staking product</param>
        /// <param name="positionId">The position</param>
        /// <param name="renewable">Renewable</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> SetAutoStakingAsync(StakingProductType product, string positionId, bool renewable, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get personal staking quota
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-personal-left-quota-of-staking-product-user_data" /></para>
        /// </summary>
        /// <param name="product">The staking product</param>
        /// <param name="productId">Product id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingPersonalQuota>> GetStakingPersonalQuotaAsync(StakingProductType product, string productId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get avaialble staking products list
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-list-user_data" /></para>
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="asset">Filter for asset</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max items per page</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceStakingProduct>>> GetStakingProductsAsync(StakingProductType product, string? asset = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase a staking product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#purchase-staking-product-user_data" /></para>
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="productId">Product id</param>
        /// <param name="quantity">Quantity to purchase</param>
        /// <param name="renewable">Renewable</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingPositionResult>> PurchaseStakingProductAsync(StakingProductType product, string productId, decimal quantity, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a staking product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-staking-product-user_data" /></para>
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="productId">Product id</param>
        /// <param name="quantity">Quantity to purchase</param>
        /// <param name="renewable">Renewable</param>
        /// <param name="positionId">Position id, required for Staking or LockedDefi types</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> RedeemStakingProductAsync(StakingProductType product, string productId, string? positionId = null, decimal? quantity = null, bool? renewable = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking positions
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-position-user_data" /></para>
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="productId">Product id</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceStakingPosition>>> GetStakingPositionsAsync(StakingProductType product, string? productId = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-staking-history-user_data" /></para>
        /// </summary>
        /// <param name="product">Product type</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceStakingHistory>>> GetStakingHistoryAsync(StakingProductType product, StakingTransactionType transactionType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ETH staking
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-eth-staking-trade" /></para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem from ETH staking
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-eth-trade" /></para>
        /// </summary>
        /// <param name="quantity">Amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH staking history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-staking-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceEthStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH redemption history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-redemption-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceEthRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH rewards history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-eth-rewards-distribution-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ETH staking quotas
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-current-eth-staking-quota-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Beth rate history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-beth-rate-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get eth staking account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#eth-staking-account-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Wrap Beth
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#wrap-beth-trade" /></para>
        /// </summary>
        /// <param name="quantity">Quantity to wrap</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Unwarp Beth
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#unwrap-wbeth-trade" /></para>
        /// </summary>
        /// <param name="quantity">Quantity to unwrap</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceStakingResult>> UnwrapBethAsync(decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get wrap history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-wrap-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get unwrap history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-unwrap-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
