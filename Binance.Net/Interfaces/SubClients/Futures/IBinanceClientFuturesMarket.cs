using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Futures
{
    /// <summary>
    /// Futures market interface
    /// </summary>
    public interface IBinanceClientFuturesMarket: IBinanceClientMarket
    {
        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        WebCallResult<BinanceFuturesMarkPrice> GetMarkPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<BinanceFuturesMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        WebCallResult<IEnumerable<BinanceFuturesMarkPrice>> GetAllMarkPrices(CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesMarkPrice>>> GetAllMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="startTime">Start time to get funding rate history</param>
        /// <param name="endTime">End time to get funding rate history</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The funding rate history for the provided symbol</returns>
        WebCallResult<IEnumerable<BinanceFuturesFundingRateHistory>> GetFundingRates(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="startTime">Start time to get funding rate history</param>
        /// <param name="endTime">End time to get funding rate history</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The funding rate history for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesFundingRateHistory>>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get all Liquidation Orders
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="startTime">Start time to get  liquidation orders history</param>
        /// <param name="endTime">End time to get liquidation orders history</param>
        /// <param name="limit">Max number of results. Default:100 Max:1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The all liquidation orders</returns>
        WebCallResult<IEnumerable<BinanceFuturesLiquidation>> GetAllLiquidationOrders(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get all Liquidation Orders
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="startTime">Start time to get  liquidation orders history</param>
        /// <param name="endTime">End time to get liquidation orders history</param>
        /// <param name="limit">Max number of results. Default:100 Max:1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The all liquidation orders</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesLiquidation>>> GetAllLiquidationOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        WebCallResult<BinanceFuturesOpenInterest> GetOpenInterest(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        Task<WebCallResult<BinanceFuturesOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets Open Interest History
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        WebCallResult<IEnumerable<BinanceFuturesOpenInterestHistory>> GetOpenInterestHistory(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Open Interest History
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesOpenInterestHistory>>> GetOpenInterestHistoryAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Accounts)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Accounts) info</returns>
        WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>> GetTopLongShortAccountRatio(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Accounts)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Accounts) info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Positions)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (positions)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (positions)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Positions) info</returns>
        WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>> GetTopLongShortPositionRatio(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Positions)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (positions)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (positions)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Positions) info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortPositionRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Global Long/Short Ratio (Accounts)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get global long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get global long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Global Long/Short Ratio (Accounts) info</returns>
        WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>> GetGlobalLongShortAccountRatio(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Global Long/Short Ratio (Accounts)
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get global long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get global long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Global Long/Short Ratio (Accounts) info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetGlobalLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        WebCallResult<IEnumerable<BinanceFuturesBuySellVolumeRatio>> GetTakerBuySellVolumeRatio(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);
    }
}