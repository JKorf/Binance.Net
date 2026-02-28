using Binance.Net.Enums;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance COIN-M futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBinanceRestClientCoinFuturesApiExchangeData
    {
        /// <summary>
        /// Pings the Binance Futures API
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/ping
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Check-Server-time" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/time
        /// </para>
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Exchange-Information" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Recent-Trades-List" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for, for example `BTCUSD_PERP`</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IBinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Old-Trades-Lookup" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/historicalTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for, for example `BTCUSD_PERP`</param>
        /// <param name="limit">Max amount of results, max 500</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IBinanceRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Compressed-Aggregate-Trades-List" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for, for example `BTCUSD_PERP`</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<WebCallResult<BinanceAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Kline-Candlestick-Data" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IBinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get premium index kline data for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Premium-Index-Kline-Data" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/premiumIndexKlines
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<BinanceMarkIndexKline[]>> GetPremiumIndexKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate info for symbols that had FundingRateCap/ FundingRateFloor / fundingIntervalHours adjustment
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Get-Funding-Infoo" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/fundingInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Get-Funding-Rate-History-of-Perpetual-Futures" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/fundingRate
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="startTime">Start time to get funding rate history</param>
        /// <param name="endTime">End time to get funding rate history</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The funding rate history for the provided symbol</returns>
        Task<WebCallResult<BinanceFuturesFundingRateHistory[]>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Accounts)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Top-Long-Short-Account-Ratio" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/topLongShortAccountRatio
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Accounts) info</returns>
        Task<WebCallResult<BinanceFuturesLongShortRatio[]>> GetTopLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Top Trader Long/Short Ratio (Positions)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Top-Trader-Long-Short-Ratio" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/topLongShortPositionRatio
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get top trader long/short ratio (positions)</param>
        /// <param name="endTime">End time to get top trader long/short ratio (positions)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Top Trader Long/Short Ratio (Positions) info</returns>
        Task<WebCallResult<BinanceFuturesLongShortRatio[]>> GetTopLongShortPositionRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Global Long/Short Ratio (Accounts)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Long-Short-Ratio" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/globalLongShortAccountRatio
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get global long/short ratio (accounts)</param>
        /// <param name="endTime">End time to get global long/short ratio (accounts)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Global Long/Short Ratio (Accounts) info</returns>
        Task<WebCallResult<BinanceFuturesLongShortRatio[]>> GetGlobalLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Kline/candlestick bars for the mark price of a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Mark-Price-Kline-Candlestick-Data" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/markPriceKlines
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The interval of the klines</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceMarkIndexKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Order-Book" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for, for example `BTCUSD_PERP`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        Task<WebCallResult<BinanceFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Index-Price-and-Mark-Price" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/premiumIndex
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="pair">Filter by pair, for example `BTCUSD`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceFuturesCoinMarkPrice[]>> GetMarkPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided pair
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Continuous-Contract-Kline-Candlestick-Data" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/continuousKlines
        /// </para>
        /// </summary>
        /// <param name="pair">The symbol to get the data for, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IBinanceKline[]>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided pair
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Index-Price-Kline-Candlestick-Data" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/indexPriceKlines
        /// </para>
        /// </summary>
        /// <param name="pair">The symbol to get the data for, for example `BTCUSD`</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<BinanceMarkIndexKline[]>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours change
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/24hr-Ticker-Price-Change-Statistics" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="pair">Filter by pair, for example `BTCUSD`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IBinance24HPrice[]>> GetTickersAsync(string? symbol = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Symbol-Order-Book-Ticker" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to get book price for, for example `BTCUSD_PERP`</param>
        /// <param name="pair">Filter by pair, for example `BTCUSD`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceFuturesBookPrice[]>> GetBookPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Open-Interest" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/openInterest
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `BTCUSD_PERP`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        Task<WebCallResult<BinanceFuturesCoinOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets Open Interest History
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Open-Interest-Statistics" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/openInterestHist
        /// </para>
        /// </summary>
        /// <param name="pair">The pair to get the data for, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        Task<WebCallResult<BinanceFuturesCoinOpenInterestHistory[]>> GetOpenInterestHistoryAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Taker-Buy-Sell-Volume" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/takerBuySellVol
        /// </para>
        /// </summary>
        /// <param name="pair">The pair to get the data for, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        Task<WebCallResult<BinanceFuturesCoinBuySellVolumeRatio[]>> GetTakerBuySellVolumeRatioAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Gets basis
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Basis" /><br />
        /// Endpoint:<br />
        /// GET /futures/data/basis
        /// </para>
        /// </summary>
        /// <param name="pair">The pair to get the data for, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Basis</returns>
        Task<WebCallResult<BinanceFuturesBasis[]>> GetBasisAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the prices of all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/market-data/rest-api/Symbol-Price-Ticker" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">Retrieve for a symbol, for example `BTCUSD_PERP`</param>
        /// <param name="pair">Retrieve prices for a specific pair, for example `BTCUSD`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<BinanceFuturesCoinPrice[]>> GetPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price constituents for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/rest-api/Index-Constituents" /><br />
        /// Endpoint:<br />
        /// GET /dapi/v1/constituents
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BinanceConstituents>> GetIndexPriceConstituentsAsync(string symbol, CancellationToken ct = default);
    }
}
