using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot.Blvt;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Binance Spot leveraged tokens endpoints
    /// </summary>
    public interface IBinanceClientSpotLeveragedTokens
    {
        /// <summary>
        /// Get blvt info
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-blvt-info-market_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBlvtInfo>>> GetBlvtInfoAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to a token
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-blvt-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token to subscribe to</param>
        /// <param name="cost">Cost of the subscription</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBlvtSubscribeResult>> SubscribeBlvtAsync(string tokenName, decimal cost, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get subscription records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-subscription-record-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Filter by token</param>
        /// <param name="id">Filter by id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanecBlvtSubscription>>> GetSubscriptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a token
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-blvt-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token to redeem</param>
        /// <param name="quantity">Quantity to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBlvtRedeemResult>> RedeemBlvtAsync(string tokenName, decimal quantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-redemption-record-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Filter by token</param>
        /// <param name="id">Filter by id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBlvtRedemption>>> GetRedemptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get's historical klines
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#historical-blvt-nav-kline-candlestick" /></para>
        /// </summary>
        /// <param name="symbol">The token</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by startTime</param>
        /// <param name="endTime">Filter by endTime</param>
        /// <param name="limit">Number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBlvtKline>>> GetHistoricalBlvtKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
    }
}
