using Binance.Net.Enums;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Interfaces.Clients.Rest.UsdFutures
{
    public interface IBinanceClientUsdFuturesExchangeData
    {
        /// <summary>
        /// Pings the Binance Futures API
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#test-connectivity" /></para>
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#check-server-time" /></para>
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#exchange-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get klines for a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#get-funding-rate-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="startTime">Start time to get funding rate history</param>
        /// <param name="endTime">End time to get funding rate history</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The funding rate history for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesFundingRateHistory>>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Accounts)
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-accounts" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-positions" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#long-short-ratio" /></para>
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
        /// Kline/candlestick bars for the mark price of a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#mark-price-kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol get the data for</param>
        /// <param name="interval">The interval of the klines</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        Task<WebCallResult<BinanceFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the most recent trades for a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#recent-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history for a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#old-trades-lookup-market_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <param name="limit">The max amount of results</param>
        /// <param name="fromId">Retrun trades after this trade id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#mark-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<BinanceFuturesMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for all symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#mark-price" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesMarkPrice>>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours change
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IBinance24HPrice>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours change
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#open-interest" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        Task<WebCallResult<BinanceFuturesOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets Open Interest History
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#open-interest-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesOpenInterestHistory>>> GetOpenInterestHistoryAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#taker-buy-sell-volume" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets composite index info
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#composite-index-symbol-information" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesCompositeIndexInfo>>> GetCompositeIndexInfoAsync(
            string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the prices of all symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided pair
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#continuous-contract-kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="pair">The symbol to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get Kline/candlestick data for the index price of a pair.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#index-price-kline-candlestick-data" /></para>
        /// </summary>
        /// <param name="pair">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset indexex for Multi-Assets mode for all symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesAssetIndex>>> GetAssetIndexesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get asset index for Multi-Assets mode for a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesAssetIndex>> GetAssetIndexAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#compressed-aggregate-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
    }
}
