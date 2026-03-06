using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBinanceRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Gets withdraw and deposit details per asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/wallet/asset" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/asset/assetDetail
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset details keyed by asset name</returns>
        Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets general product data available on Binance
        /// NOTE: This is not an official endpoint and might be changed or removed at any point by Binance
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Product data</returns>
        Task<WebCallResult<BinanceProduct[]>> GetProductsAsync(CancellationToken ct = default);

        /// <summary>
        /// Pings the Binance API
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#test-connectivity" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ping
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the current Binance server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#check-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and symbol list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="returnPermissionSets">["<c>showPermissionSets</c>"] Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get data for, for example `ETHUSDT`</param>
        /// <param name="returnPermissionSets">["<c>showPermissionSets</c>"] Whether or not permission sets should be returned</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, bool? returnPermissionSets = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Symbols to get data for, for example `ETHUSDT`</param>
        /// <param name="returnPermissionSets">["<c>showPermissionSets</c>"] Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbol based on an account permission
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="permission">["<c>permissions</c>"] Account permission to filter symbols by</param>
        /// <param name="returnPermissionSets">["<c>showPermissionSets</c>"] Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType permission, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbols based on account permissions
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="permissions">["<c>permissions</c>"] Account permissions to filter symbols by</param>
        /// <param name="returnPermissionSets">["<c>showPermissionSets</c>"] Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType[] permissions, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the Binance platform
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#system-status-system" /><br />
        /// Endpoint:<br />
        /// /sapi/v1/system/status
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get historical trades for, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IBinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the historical trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#old-trade-lookup" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/historicalTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get recent trades for, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Result limit</param>
        /// <param name="fromId">["<c>fromId</c>"] From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IBinanceRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the same time, from the same order, with the same price will have the quantity aggregated.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#compressedaggregate-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the trades for, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">["<c>startTime</c>"] Time to start getting trades from</param>
        /// <param name="endTime">["<c>endTime</c>"] Time to stop getting trades from</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<WebCallResult<BinanceAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets candlestick data for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#klinecandlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] The candlestick timespan</param>
        /// <param name="startTime">["<c>startTime</c>"] Start time to get candlestick data</param>
        /// <param name="endTime">["<c>endTime</c>"] End time to get candlestick data</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IBinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets candlestick data for the provided symbol. Returns modified kline data optimized for candlestick chart presentation
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#uiklines" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/uiKlines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] The candlestick timespan</param>
        /// <param name="startTime">["<c>startTime</c>"] Start time to get candlestick data</param>
        /// <param name="endTime">["<c>endTime</c>"] End time to get candlestick data</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IBinanceKline[]>> GetUiKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#order-book" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the order book for, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the current average price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#current-average-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/avgPrice
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Current average price information</returns>
        Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets 24-hour ticker statistics for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol,
            CancellationToken ct = default);

        /// <summary>
        /// Gets 24-hour ticker statistics for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] The symbols to get the data for, for example `ETHUSDT`</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IBinanceTick[]>> GetTickersAsync(IEnumerable<string> symbols,
            SymbolStatusFilter? symbolStatus = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets 24-hour ticker statistics for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        Task<WebCallResult<IBinanceTick[]>> GetTickersAsync(SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets price change statistics for a trading day
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#trading-day-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/tradingDay
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="timeZone">["<c>timeZone</c>"] The timezone offset, for example -3 for UTC-3 or 5 for UTC+5</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trading day ticker statistics</returns>
        Task<WebCallResult<BinanceTradingDayTicker>> GetTradingDayTickerAsync(string symbol, string? timeZone = null, CancellationToken ct = default);

        /// <summary>
        /// Gets price change statistics for a trading day
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#trading-day-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/tradingDay
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] The symbols, for example `ETHUSDT`</param>
        /// <param name="timeZone">["<c>timeZone</c>"] The timezone offset, for example -3 for UTC-3 or 5 for UTC+5</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trading day ticker statistics</returns>
        Task<WebCallResult<BinanceTradingDayTicker[]>> GetTradingDayTickersAsync(IEnumerable<string> symbols, string? timeZone = null, SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ticker statistics over a rolling time window
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#rolling-window-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get data for, for example `ETHUSDT`</param>
        /// <param name="windowSize">["<c>windowSize</c>"] The window size to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rolling-window ticker statistics</returns>
        Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets ticker statistics over a rolling time window
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#rolling-window-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] The symbols to get data for, for example `ETHUSDT`</param>
        /// <param name="windowSize">["<c>windowSize</c>"] The window size to use</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Rolling-window ticker statistics</returns>
        Task<WebCallResult<IBinance24HPrice[]>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = null, SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get book price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Book price for the symbol</returns>
        Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] Symbols to get book price for, for example `ETHUSDT`</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceBookPrice[]>> GetBookPricesAsync(IEnumerable<string> symbols, SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceBookPrice[]>> GetBookPricesAsync(SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get the price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the prices for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>symbols</c>"] The symbols to get the price for, for example `ETHUSDT`</param>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<BinancePrice[]>> GetPricesAsync(IEnumerable<string> symbols, SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the prices for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbolStatus">["<c>symbolStatus</c>"] Filter by symbol status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<BinancePrice[]>> GetPricesAsync(SymbolStatusFilter? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all assets available for margin trading
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-all-margin-assets-market_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/allAssets
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginAsset[]>> GetMarginAssetsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all asset pairs available for margin trading
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Get-All-Cross-Margin-Pairs" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/allPairs
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        Task<WebCallResult<BinanceMarginPair[]>> GetMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the margin price index for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Margin-PriceIndex" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/priceIndex
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets isolated margin symbol information
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Get-All-Isolated-Margin-Symbol" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/isolated/allPairs
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Isolated margin symbols</returns>
        Task<WebCallResult<BinanceIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets historical leveraged token kline data
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/futures/en/#historical-blvt-nav-kline-candlestick" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/lvtKlines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The leveraged token symbol</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Historical leveraged token kline data</returns>
        Task<WebCallResult<BinanceBlvtKline[]>> GetLeveragedTokensHistoricalKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cross margin collateral ratios
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/crossMarginCollateralRatio
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cross margin collateral ratios</returns>
        Task<WebCallResult<BinanceCrossMarginCollateralRatio[]>> GetCrossMarginCollateralRatioAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures hourly interest rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-a-future-hourly-interest-rate-user_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/next-hourly-interest-rate
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>assets</c>"] Assets, for example `ETH`</param>
        /// <param name="isolated">["<c>isIsolated</c>"] Whether to query isolated margin (otherwise cross margin)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Hourly interest rates for the provided assets</returns>
        Task<WebCallResult<BinanceFuturesInterestRate[]>> GetFutureHourlyInterestRateAsync(IEnumerable<string> assets, bool isolated, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets cross and isolated margin delist schedules
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Get-Delist-Schedule" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/delist-schedule
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cross and isolated margin delist schedules</returns>
        Task<WebCallResult<BinanceMarginDelistSchedule[]>> GetMarginDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets isolated margin tier data
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Isolated-Margin-Tier-Data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/isolatedMarginTier
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get, for example `ETHUSDT`</param>
        /// <param name="tier">["<c>tier</c>"] Tier level to filter by</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Isolated margin tier data</returns>
        Task<WebCallResult<BinanceIsolatedMarginTierData[]>> GetIsolatedMarginTierDataAsync(string symbol, int? tier = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin available inventory
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Query-margin-avaliable-inventory" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/available-inventory
        /// </para>
        /// </summary>
        /// <param name="type">["<c>type</c>"] The margin type to query for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Available margin inventory for the specified type</returns>
        Task<WebCallResult<BinanceMarginAvailableInventory>> GetMarginAvailableInventoryAsync(MarginInventoryType type, CancellationToken ct = default);

        /// <summary>
        /// Gets liability coin leverage brackets in cross margin pro mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Liability-Coin-Leverage-Bracket-in-Cross-Margin-Pro-Mode" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/leverageBracket
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liability coin leverage brackets</returns>
        Task<WebCallResult<BinanceCrossMarginProLiabilityCoinLeverageBracket[]>> GetLiabilityCoinLeverageBracketInCrossMarginProModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets all convert pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/market-data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/convert/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="quoteAsset">["<c>fromAsset</c>"] Quote asset, for example `ETH`</param>
        /// <param name="baseAsset">["<c>toAsset</c>"] Base asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Convert asset pairs</returns>
        Task<WebCallResult<BinanceConvertAssetPair[]>> GetConvertListAllPairsAsync(string? quoteAsset = null, string? baseAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets quantity precision per asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/market-data/Query-order-quantity-precision-per-asset" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/convert/assetInfo
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quantity precision settings per asset</returns>
        Task<WebCallResult<BinanceConvertQuantityPrecisionAsset[]>> GetConvertQuantityPrecisionPerAssetAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets spot symbol delist schedules
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/spot/en/#get-symbols-delist-schedule-for-spot-market_data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/spot/delist-schedule
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Spot symbol delist schedules</returns>
        Task<WebCallResult<BinanceDelistSchedule[]>> GetDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default);
    }
}


