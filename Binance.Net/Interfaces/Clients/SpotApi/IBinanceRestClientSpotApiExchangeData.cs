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
        /// Gets the withdraw/deposit details for an asset
        /// <para><a href="https://developers.binance.com/docs/wallet/asset" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get general data for the products available on Binance
        /// NOTE: This is not an official endpoint and might be changed or removed at any point by Binance
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default);

        /// <summary>
        /// Pings the Binance API
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#test-connectivity" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#check-server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and symbol list
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /></para>
        /// </summary>
        /// <param name="returnPermissionSets">Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get data for, for example `ETHUSDT`</param>
        /// <param name="returnPermissionSets">Whether or not permission sets should be returned</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, bool? returnPermissionSets = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbols
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to get data for, for example `ETHUSDT`</param>
        /// <param name="returnPermissionSets">Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbol based on an account permission
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /></para>
        /// </summary>
        /// <param name="permission">account type</param>
        /// <param name="returnPermissionSets">Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType permission, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information about the exchange including rate limits and information on the provided symbols based on account permissions
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/general-endpoints#exchange-information" /></para>
        /// </summary>
        /// <param name="permissions">account type</param>
        /// <param name="returnPermissionSets">Whether or not permission sets should be returned</param>
        /// <param name="symbolStatus">Filter by symbol status, Trading, Halt or Break</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType[] permissions, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the Binance platform
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#system-status-system" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#recent-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for, for example `ETHUSDT`</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the historical trades for a symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#old-trade-lookup" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for, for example `ETHUSDT`</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the same time, from the same order, with the same price will have the quantity aggregated.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#compressedaggregate-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for, for example `ETHUSDT`</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#klinecandlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided symbol. Returns modified kline data, optimized for the presentation of candlestick charts
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#uiklines" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<IBinanceKline>>> GetUiKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets current average price for a symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#current-average-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol,
            CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to get the data for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(IEnumerable<string> symbols,
            CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price change stats for a trading day
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#trading-day-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="timeZone">The timezone offset, for example -3 for UTC-3 or 5 for UTC+5</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceTradingDayTicker>> GetTradingDayTickerAsync(string symbol, string? timeZone = null, CancellationToken ct = default);

        /// <summary>
        /// Get price change stats for a trading day
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#trading-day-ticker" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="timeZone">The timezone offset, for example -3 for UTC-3 or 5 for UTC+5</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceTradingDayTicker>>> GetTradingDayTickersAsync(IEnumerable<string> symbols, string? timeZone = null, CancellationToken ct = default);

        /// <summary>
        /// Get data based on the last x time, specified as windowSize
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#rolling-window-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get data for, for example `ETHUSDT`</param>
        /// <param name="windowSize">The window size to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get data based on the last x time, specified as windowSize
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#rolling-window-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to get data for, for example `ETHUSDT`</param>
        /// <param name="windowSize">The window size to use</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get book price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to get book price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        ///  Gets the prices of symbols
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to get the price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the prices of all symbols
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/market-data-endpoints#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all assets available for margin trading
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-all-margin-assets-market_data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Get-All-Cross-Margin-Pairs" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Margin-PriceIndex" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin symbol info
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Get-All-Isolated-Margin-Symbol" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

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
        Task<WebCallResult<IEnumerable<BinanceBlvtKline>>> GetLeveragedTokensHistoricalKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin collateral ratio
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceCrossMarginCollateralRatio>>> GetCrossMarginCollateralRatioAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get futures hourly interest rate
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-a-future-hourly-interest-rate-user_data" /></para>
        /// </summary>
        /// <param name="assets">Assets, for example `ETH`</param>
        /// <param name="isolated">Isolated or cross</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesInterestRate>>> GetFutureHourlyInterestRateAsync(IEnumerable<string> assets, bool isolated, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross and isolated delist schedule
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Get-Delist-Schedule" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceMarginDelistSchedule>>> GetMarginDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Isolated Margin Tier Data
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Isolated-Margin-Tier-Data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get, for example `ETHUSDT`</param>
        /// <param name="tier">Tier level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceIsolatedMarginTier>>> GetIsolatedMarginTierDataAsync(string symbol, int? tier = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Margin Available Inventory
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Query-margin-avaliable-inventory" /></para>
        /// </summary>
        /// <param name="type">The margin type to query for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceMarginAvailableInventory>> GetMarginAvaliableInventoryAsync(MarginInventoryType type, CancellationToken ct = default);

        /// <summary>
        /// Get Liability Coin Leverage Bracket in Cross Margin Pro Mode
        /// <para><a href="https://developers.binance.com/docs/margin_trading/market-data/Query-Liability-Coin-Leverage-Bracket-in-Cross-Margin-Pro-Mode" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceCrossMarginProLiabilityCoinLeverageBracket>>> GetLiabilityCoinLeverageBracketInCrossMarginProModeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list all convert pairs
        /// <para><a href="https://developers.binance.com/docs/convert/market-data" /></para>
        /// </summary>
        /// <param name="quoteAsset">Quote asset, for example `ETH`</param>
        /// <param name="baseAsset">Base asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceConvertAssetPair>>> GetConvertListAllPairsAsync(string? quoteAsset = null, string? baseAsset = null, CancellationToken ct = default);

        /// <summary>
        /// Get quantity precision per asset
        /// <para><a href="https://developers.binance.com/docs/convert/market-data/Query-order-quantity-precision-per-asset" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceConvertQuantityPrecisionAsset>>> GetConvertQuantityPrecisionPerAssetAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get spot symbols delist schedule
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-symbols-delist-schedule-for-spot-market_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceDelistSchedule>>> GetDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default);
    }
}
