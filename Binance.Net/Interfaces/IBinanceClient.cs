using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.LendingData;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.Mining;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot.SubAccountData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Interface for the Binance client
    /// </summary>
    public interface IBinanceClient : IRestClient
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
        /// <returns>True if successful ping, false if no response</returns>
        WebCallResult<long> Ping(CancellationToken ct = default);

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        WebCallResult<DateTime> GetServerTime(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        WebCallResult<BinanceExchangeInfo> GetExchangeInfo(CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        WebCallResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        WebCallResult<IEnumerable<BinanceRecentTrade>> GetSymbolTrades(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IEnumerable<BinanceRecentTrade>>> GetSymbolTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        WebCallResult<IEnumerable<BinanceRecentTrade>> GetHistoricalSymbolTrades(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        Task<WebCallResult<IEnumerable<BinanceRecentTrade>>> GetHistoricalSymbolTradesAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        WebCallResult<IEnumerable<BinanceAggregatedTrade>> GetAggregatedTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradesAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        WebCallResult<IEnumerable<BinanceKline>> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<WebCallResult<IEnumerable<BinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<BinanceAveragePrice> GetCurrentAvgPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        WebCallResult<Binance24HPrice> Get24HPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<WebCallResult<Binance24HPrice>> Get24HPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        WebCallResult<IEnumerable<Binance24HPrice>> Get24HPricesList(CancellationToken ct = default);

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        Task<WebCallResult<IEnumerable<Binance24HPrice>>> Get24HPricesListAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        WebCallResult<BinancePrice> GetPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        WebCallResult<IEnumerable<BinancePrice>> GetAllPrices(CancellationToken ct = default);

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        Task<WebCallResult<IEnumerable<BinancePrice>>> GetAllPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        WebCallResult<BinanceBookPrice> GetBookPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        WebCallResult<IEnumerable<BinanceBookPrice>> GetAllBookPrices(CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetAllBookPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        WebCallResult<BinanceSystemStatus> GetSystemStatus(CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The system status</returns>
        Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets information of coins for a user
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        WebCallResult<IEnumerable<BinanceUserCoin>> GetUserCoins(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets information of coins for a user
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        Task<WebCallResult<IEnumerable<BinanceUserCoin>>> GetUserCoinsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (balances)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>> GetDailySpotAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (balances)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>>> GetDailySpotAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>> GetDailyMarginAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>>> GetDailyMarginAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets and positions)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>> GetDailyFutureAccountSnapshot(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a daily account snapshot (assets and positions)
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="limit">The amount of days to retrieve</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>>> GetDailyFutureAccountSnapshotAsync(
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> DisableFastWithdrawSwitch(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will disable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> DisableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> EnableFastWithdrawSwitch(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// This request will enable fastwithdraw switch under your account.
        /// You need to enable "trade" option for the api key which requests this endpoint.
        ///
        /// When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.
        /// There is no on-chain transaction, no transaction ID and no withdrawal fee.
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> EnableFastWithdrawSwitchAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="withdrawOrderId">Custom client order id</param>
        /// <param name="network">The network to use</param>
        /// <param name="name">Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        WebCallResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, decimal amount, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="withdrawOrderId">Custom client order id</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="network">The network to use</param>
        /// <param name="name">Description of the address</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Withdrawal confirmation</returns>
        Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal amount, string? withdrawOrderId = null, string? network = null, string? addressTag = null, string? name = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        WebCallResult<IEnumerable<BinanceDeposit>> GetDepositHistory(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="coin">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="limit">Amount of results</param>
        /// <param name="offset">Offset the results</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of deposits</returns>
        Task<WebCallResult<IEnumerable<BinanceDeposit>>> GetDepositHistoryAsync(string? coin = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? offset = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        WebCallResult<IEnumerable<BinanceWithdrawal>> GetWithdrawalHistory(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of withdrawals</returns>
        Task<WebCallResult<IEnumerable<BinanceWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        WebCallResult<BinanceDepositAddress> GetDepositAddress(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="coin">Asset to get address for</param>
        /// <param name="network">Network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Deposit address</returns>
        Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        WebCallResult<BinanceAccountStatus> GetAccountStatus(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Account status</returns>
        Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        WebCallResult<BinanceTradingStatus> GetTradingStatus(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        WebCallResult<IEnumerable<BinanceDustLog>> GetDustLog(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        Task<WebCallResult<IEnumerable<BinanceDustLog>>> GetDustLogAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        WebCallResult<BinanceDustTransferResult> DustTransfer(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset dividend records
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// /// <param name="startTime">Filter by start time from</param>
        /// <param name="endTime">Filter by end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        WebCallResult<BinanceQueryRecords<BinanceDividendRecord>> GetAssetDividendRecords(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset dividend records
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// /// <param name="startTime">Filter by start time from</param>
        /// <param name="endTime">Filter by end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dividend records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        WebCallResult<Dictionary<string, BinanceAssetDetails>> GetAssetDetails(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Asset detail</returns>
        Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the withdrawal fee for an symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        WebCallResult<IEnumerable<BinanceTradeFee>> GetTradeFee(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trade fee for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        WebCallResult<IEnumerable<BinanceSubAccount>> GetSubAccounts(string? email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccount>>> GetSubAccountsAsync(string? email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        WebCallResult<IEnumerable<BinanceSubAccountTransfer>> GetSubAccountTransferForMasterHistory(string? email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the master account) 
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountTransfer>>> GetSubAccountTransferHistoryForMasterAsync(string? email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransferResult> TransferSubAccount(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransferResult>> TransferSubAccountAsync(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        WebCallResult<IEnumerable<BinanceBalance>> GetSubAccountAssets(string email, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets list of balances for a sub account
        /// </summary>
        /// <param name="email">For which account to get the assets</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        WebCallResult<BinanceSubAccountDepositAddress> GetSubAccountDepositAddress(string email, string coin,
            string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit address for a coin to a sub account
        /// </summary>
        /// <param name="email">The email of the account to deposit to</param>
        /// <param name="coin">The coin of the deposit</param>
        /// <param name="network">The coin network</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit address</returns>
        Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string coin, string? network = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get history for</param>
        /// <param name="coin">Filter for a coin</param>
        /// <param name="startTime">Only return deposits placed later this</param>
        /// <param name="endTime">Only return deposits placed before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset results by this</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit history</returns>
        WebCallResult<IEnumerable<BinanceSubAccountDeposit>> GetSubAccountDepositHistory(string email,
            string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null,
            int? offset = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the deposit history for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get history for</param>
        /// <param name="coin">Filter for a coin</param>
        /// <param name="startTime">Only return deposits placed later this</param>
        /// <param name="endTime">Only return deposits placed before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Offset results by this</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The deposit history</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? offset = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        WebCallResult<IEnumerable<BinanceSubAccountStatus>> GetSubAccountStatus(string? email = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Sub-account's Status on Margin/Futures(For Master Account)
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of sub accounts status</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables margin for a sub account
        /// </summary>
        /// <param name="email">The email of the account to enable margin for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin enable status</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountMarginEnabled>>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        WebCallResult<BinanceSubAccountMarginDetails> GetSubAccountMarginDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get margin details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin details</returns>
        Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        WebCallResult<BinanceSubAccountsMarginSummary> GetSubAccountsMarginSummary(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets margin summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin summary</returns>
        Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        WebCallResult<BinanceSubAccountFuturesEnabled> EnableFuturesForSubAccount(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Enables futures for a sub account
        /// </summary>
        /// <param name="email">The sub account email to enable futures for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures status</returns>
        Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        WebCallResult<BinanceSubAccountFuturesDetails> GetSubAccountFuturesDetails(string email,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures details for a sub account
        /// </summary>
        /// <param name="email">The email of the account to get future details for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures details</returns>
        Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        WebCallResult<BinanceSubAccountsFuturesSummary> GetSubAccountsFuturesSummary(int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets futures summary for sub accounts
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Futures summary</returns>
        Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>> GetSubAccountsFuturesPositionRisk(
            string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets futures position risk for a sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position risk</returns>
        Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a futures sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountFutures(string email, string asset,
            decimal amount, SubAccountTransferType type, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a futures sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal amount, SubAccountTransferType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a margin sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountMargin(string email, string asset,
            decimal amount, SubAccountTransferType type, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers from or to a margin sub account
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="type">The type of the transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal amount, SubAccountTransferType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToSubAccount(string email, string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers to another sub account of the same master
        /// </summary>
        /// <param name="email">Email of the sub account</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        WebCallResult<BinanceSubAccountTransaction> TransferSubAccountToMaster(string asset,
            decimal amount, int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Transfers to master account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The result of the transfer</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the sub account)
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of transfer</param>
        /// <param name="startTime">Only return transfers later than this</param>
        /// <param name="endTime">Only return transfers before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        WebCallResult<BinanceSubAccountTransaction> GetSubAccountTransferHistoryForSubAccount(
            string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null,
            DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the transfer history of a sub account (from the sub account)
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of transfer</param>
        /// <param name="startTime">Only return transfers later than this</param>
        /// <param name="endTime">Only return transfers before this</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer history</returns>
        Task<WebCallResult<BinanceSubAccountTransaction>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = null, SubAccountTransferSubAccountType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null,  int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">What kind of response should be returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed test order</returns>
        WebCallResult<BinancePlacedOrder> PlaceTestOrder(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">What kind of response should be returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed test order</returns>
        Task<WebCallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the base symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        WebCallResult<BinancePlacedOrder> PlaceOrder(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        Task<WebCallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">The new client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        WebCallResult<BinanceCanceledOrder> CancelOrder(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceCanceledOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        WebCallResult<BinanceOrder> GetOrder(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        WebCallResult<IEnumerable<BinanceOrder>> GetOpenOrders(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        WebCallResult<IEnumerable<BinanceOrder>> GetAllOrders(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetAllOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new OCO(One cancels other) order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="stopPrice">The stop price</param>
        /// <param name="stopLimitPrice">The price for the stop limit order</param>
        /// <param name="stopClientOrderId">Client id for the stop order</param>
        /// <param name="limitClientOrderId">Client id for the limit order</param>
        /// <param name="listClientOrderId">Client id for the order list</param>
        /// <param name="limitIcebergQuantity">Iceberg quantity for the limit order</param>
        /// <param name="stopIcebergQuantity">Iceberg quantity for the stop order</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>
        WebCallResult<BinanceOrderOcoList> PlaceOCOOrder(
            string symbol,
            OrderSide side,
            decimal quantity,
            decimal price,
            decimal stopPrice,
            decimal? stopLimitPrice = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            decimal? limitIcebergQuantity = null,
            decimal? stopIcebergQuantity = null,
            TimeInForce? stopLimitTimeInForce = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new OCO(One cancels other) order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="stopPrice">The stop price</param>
        /// <param name="stopLimitPrice">The price for the stop limit order</param>
        /// <param name="stopClientOrderId">Client id for the stop order</param>
        /// <param name="limitClientOrderId">Client id for the limit order</param>
        /// <param name="listClientOrderId">Client id for the order list</param>
        /// <param name="limitIcebergQuantity">Iceberg quantity for the limit order</param>
        /// <param name="stopIcebergQuantity">Iceberg quantity for the stop order</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOCOOrderAsync(string symbol,
            OrderSide side,
            decimal quantity,
            decimal price,
            decimal stopPrice,
            decimal? stopLimitPrice = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            decimal? limitIcebergQuantity = null,
            decimal? stopIcebergQuantity = null,
            TimeInForce? stopLimitTimeInForce = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending oco order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderListId">The id of the order list to cancel</param>
        /// <param name="listClientOrderId">The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        WebCallResult<BinanceOrderOcoList> CancelOCOOrder(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending oco order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderListId">The id of the order list to cancel</param>
        /// <param name="listClientOrderId">The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceOrderOcoList>> CancelOCOOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific oco order. Either listClientOrderId or listClientOrderId should be provided.
        /// </summary>
        /// <param name="orderListId">The list order id of the order</param>
        /// <param name="listClientOrderId">The client order id of the list order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        WebCallResult<BinanceOrderOcoList> GetOCOOrder(long? orderListId = null, string? listClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific oco order. Either orderListId or listClientOrderId should be provided.
        /// </summary>
        /// <param name="orderListId">The list order id of the order</param>
        /// <param name="listClientOrderId">The client order id of the list order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        Task<WebCallResult<BinanceOrderOcoList>> GetOCOOrderAsync(long? orderListId = null, string? listClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of oco orders matching the parameters
        /// </summary>
        /// <param name="fromId">Only return oco orders with id higher than this</param>
        /// <param name="startTime">Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        WebCallResult<IEnumerable<BinanceOrderOcoList>> GetOCOOrders(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of oco orders matching the parameters
        /// </summary>
        /// <param name="fromId">Only return oco orders with id higher than this</param>
        /// <param name="startTime">Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOCOOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open oco orders
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        WebCallResult<IEnumerable<BinanceOrderOcoList>> GetOpenOCOOrders(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open oco orders
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOpenOCOOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        WebCallResult<BinanceAccountInfo> GetAccountInfo(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        WebCallResult<IEnumerable<BinanceTrade>> GetMyTrades(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMyTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceMarginTransaction> Transfer(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceMarginTransaction>> TransferAsync(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceMarginTransaction> Borrow(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceMarginTransaction>> BorrowAsync(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceMarginTransaction> Repay(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceMarginTransaction>> RepayAsync(string asset, decimal amount, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The asset to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin asset</returns>
        WebCallResult<BinanceMarginAsset> GetMarginAsset(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginAsset>> GetMarginAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin asset</returns>
        WebCallResult<BinanceMarginPair> GetMarginPair(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginPair>> GetMarginPairAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        WebCallResult<IEnumerable<BinanceMarginAsset>> GetMarginAssets(CancellationToken ct = default);

        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        WebCallResult<IEnumerable<BinanceMarginPair>> GetMarginPairs(CancellationToken ct = default);

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginPairsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        WebCallResult<BinanceMarginPriceIndex> GetMarginPriceIndex(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Margin account new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQuantity">Used for iceberg orders</param>
        /// <param name="sideEffectType">Side effect type for this order</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        WebCallResult<BinancePlacedOrder> PlaceMarginOrder(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            SideEffectType? sideEffectType = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Margin account new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQuantity">Used for iceberg orders</param>
        /// <param name="sideEffectType">Side effect type for this order</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            SideEffectType? sideEffectType = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an active order for margin account
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        WebCallResult<BinanceCanceledOrder> CancelMarginOrder(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an active order for margin account
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceCanceledOrder>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers
        /// </summary>
        /// <param name="direction">The direction of the the transfers to retrieve</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        WebCallResult<BinanceQueryRecords<BinanceTransferHistory>> GetTransferHistory(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers
        /// </summary>
        /// <param name="direction">The direction of the the transfers to retrieve</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetTransferHistoryAsync(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get loan records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of loan transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="limit">The records count size need show</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        WebCallResult<BinanceQueryRecords<BinanceLoan>> GetLoans(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query loan records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of loan transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="limit">The records count size need show</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetLoansAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query repay records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of repay transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="size">The records count size need show</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        WebCallResult<BinanceQueryRecords<BinanceRepay>> GetRepays(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query repay records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of repay transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Filter by number</param>
        /// <param name="size">The records count size need show</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetRepaysAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        WebCallResult<BinanceQueryRecords<BinanceInterestHistory>> GetInterestHistory(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetInterestHistoryAsync(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// </summary>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>> GetForceLiquidationHistory(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// </summary>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetForceLiquidationHistoryAsync(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query margin account details
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        WebCallResult<BinanceMarginAccount> GetMarginAccountInfo(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query margin account details
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific margin account order</returns>
        WebCallResult<BinanceOrder> GetMarginAccountOrder(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific margin account order</returns>
        Task<WebCallResult<BinanceOrder>> GetMarginAccountOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open margin account orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open margin account orders</returns>
        WebCallResult<IEnumerable<BinanceOrder>> GetOpenMarginAccountOrders(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open margin account orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open margin account orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenMarginAccountOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all margin account orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account orders</returns>
        WebCallResult<IEnumerable<BinanceOrder>> GetAllMarginAccountOrders(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all margin account orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetAllMarginAccountOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user margin account trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account trades</returns>
        WebCallResult<IEnumerable<BinanceTrade>> GetMyMarginAccountTrades(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user margin account trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account trades</returns>
        Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMyMarginAccountTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max borrow amount
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        WebCallResult<decimal> GetMaxBorrowAmount(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max borrow amount
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        Task<WebCallResult<decimal>> GetMaxBorrowAmountAsync(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max transfer-out amount 
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        WebCallResult<decimal> GetMaxTransferAmount(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max transfer-out amount 
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        Task<WebCallResult<decimal>> GetMaxTransferAmountAsync(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        WebCallResult<string> StartUserStream(CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> KeepAliveUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> StopUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to 
        /// <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. 
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        WebCallResult<string> StartMarginUserStream(CancellationToken ct = default);

        /// <summary>
        /// Starts a user stream  for margin account by requesting a listen key. 
        /// This listen key can be used in subsequent requests to 
        /// <see cref="BinanceSocketClient.SubscribeToUserDataUpdates"/>. 
        /// The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Listen key</returns>
        Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user for margin account stream listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> KeepAliveMarginUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing. 
        /// Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> KeepAliveMarginUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> CloseMarginUserStream(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Close the user stream for margin account
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> CloseMarginUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Get product list
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="featured">Filter by featured</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of product</returns>
        WebCallResult<IEnumerable<BinanceSavingsProduct>> GetFlexibleProductList(ProductStatus? status = null,
            bool? featured = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get product list
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="featured">Filter by featured</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of product</returns>
        Task<WebCallResult<IEnumerable<BinanceSavingsProduct>>> GetFlexibleProductListAsync(ProductStatus? status = null, bool? featured = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the purchase quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        WebCallResult<BinancePurchaseQuotaLeft> GetLeftDailyPurchaseQuotaOfFlexableProduct(string productId,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the purchase quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinancePurchaseQuotaLeft>> GetLeftDailyPurchaseQuotaOfFlexableProductAsync(string productId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="amount">The amount to purchase</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        WebCallResult<BinanceLendingPurchaseResult> PurchaseFlexibleProduct(string productId,
            decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="amount">The amount to purchase</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseFlexibleProductAsync(string productId, decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the redemption quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        WebCallResult<BinanceRedemptionQuotaLeft> GetLeftDailyRedemptionQuotaOfFlexibleProduct(string productId, RedeemType type,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the redemption quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinanceRedemptionQuotaLeft>> GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(string productId, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Redeem type</param>
        /// <param name="amount">The amount to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> RedeemFlexibleProduct(string productId,
            decimal amount, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Redeem type</param>
        /// <param name="amount">The amount to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> RedeemFlexibleProductAsync(string productId, decimal amount, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Flexible product position</returns>
        WebCallResult<IEnumerable<BinanceFlexibleProductPosition>> GetFlexibleProductPosition(
            string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Flexible product position</returns>
        Task<WebCallResult<IEnumerable<BinanceFlexibleProductPosition>>> GetFlexibleProductPositionAsync(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get fixed and customized fixed project list
        /// </summary>
        /// <param name="type">Type of project</param>
        /// <param name="asset">Asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="sortAscending">If should sort ascending</param>
        /// <param name="sortBy">Sort by. Valid values: "START_TIME", "LOT_SIZE", "INTEREST_RATE", "DURATION"; default "START_TIME"</param>
        /// <param name="currentPage">Result page</param>
        /// <param name="size">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Project list</returns>
        WebCallResult<IEnumerable<BinanceProject>> GetFixedAndCustomizedFixedProjectList(
            ProjectType type, string? asset = null, ProductStatus? status = null, bool? sortAscending = null,
            string? sortBy = null, int? currentPage = null, int? size = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get fixed and customized fixed project list
        /// </summary>
        /// <param name="type">Type of project</param>
        /// <param name="asset">Asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="sortAscending">If should sort ascending</param>
        /// <param name="sortBy">Sort by. Valid values: "START_TIME", "LOT_SIZE", "INTEREST_RATE", "DURATION"; default "START_TIME"</param>
        /// <param name="currentPage">Result page</param>
        /// <param name="size">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Project list</returns>
        Task<WebCallResult<IEnumerable<BinanceProject>>> GetFixedAndCustomizedFixedProjectListAsync(
            ProjectType type, string? asset = null, ProductStatus? status = null, bool? sortAscending = null, string? sortBy = null, int? currentPage = null, int? size = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase customized fixed project
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        WebCallResult<BinanceLendingPurchaseResult> PurchaseCustomizedFixedProject(string projectId, int lot,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase customized fixed project
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseCustomizedFixedProjectAsync(string projectId, int lot, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get customized fixed project position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="projectId">The project id</param>
        /// <param name="status">Filter by status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Customized fixed project position</returns>
        WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>> GetCustomizedFixedProjectPositions(
            string asset, string? projectId = null, ProjectStatus? status = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get customized fixed project position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="projectId">The project id</param>
        /// <param name="status">Filter by status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Customized fixed project position</returns>
        Task<WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>>> GetCustomizedFixedProjectPositionsAsync(string asset, string? projectId = null, ProjectStatus? status = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Lending account</returns>
        WebCallResult<BinanceLendingAccount> GetLendingAccount(long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get lending account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Lending account</returns>
        Task<WebCallResult<BinanceLendingAccount>> GetLendingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get purchase records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The purchase records</returns>
        WebCallResult<IEnumerable<BinancePurchaseRecord>> GetPurchaseRecords(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get purchase records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The purchase records</returns>
        Task<WebCallResult<IEnumerable<BinancePurchaseRecord>>> GetPurchaseRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The redemption records</returns>
        WebCallResult<IEnumerable<BinanceRedemptionRecord>> GetRedemptionRecords(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The redemption records</returns>
        Task<WebCallResult<IEnumerable<BinanceRedemptionRecord>>> GetRedemptionRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The interest history</returns>
        WebCallResult<IEnumerable<BinanceLendingInterestHistory>> GetLendingInterestHistory(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The interest history</returns>
        Task<WebCallResult<IEnumerable<BinanceLendingInterestHistory>>> GetLendingInterestHistoryAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        WebCallResult<IEnumerable<BinanceMiningCoin>> GetMiningCoinList(CancellationToken ct = default);

        /// <summary>
        /// Gets mining coins info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Coins info</returns>
        Task<WebCallResult<IEnumerable<BinanceMiningCoin>>> GetMiningCoinListAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        WebCallResult<IEnumerable<BinanceMiningAlgorithm>> GetMiningAlgorithmList(CancellationToken ct = default);

        /// <summary>
        /// Gets mining algorithms info
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algorithms info</returns>
        Task<WebCallResult<IEnumerable<BinanceMiningAlgorithm>>> GetMiningAlgorithmListAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        WebCallResult<IEnumerable<BinanceMinerDetails>> GetMinerDetails(string algorithm, string userName,
            string workerName, CancellationToken ct = default);

        /// <summary>
        /// Gets miner details
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="workerName">Miners name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner details</returns>
        Task<WebCallResult<IEnumerable<BinanceMinerDetails>>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default);

        /// <summary>
        /// Gets miner list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="sortAscending">Sort in ascending order</param>
        /// <param name="sortColumn">Column to sort by</param>
        /// <param name="workerStatus">Filter by status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner list</returns>
        WebCallResult<BinanceMinerList> GetMinerList(string algorithm, string userName, int? page = null,
            bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets miner list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="sortAscending">Sort in ascending order</param>
        /// <param name="sortColumn">Column to sort by</param>
        /// <param name="workerStatus">Filter by status</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Miner list</returns>
        Task<WebCallResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = null, bool? sortAscending = null, string? sortColumn = null, MinerStatus? workerStatus = null, CancellationToken ct = default);

        /// <summary>
        /// Gets revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        WebCallResult<BinanceRevenueList> GetMiningRevenueList(string algorithm, string userName,
            string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets revenue list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account</param>
        /// <param name="page">Result page</param>
        /// <param name="coin">Coin</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = null, DateTime? startDate = null, DateTime? endDate = null, int? page = null, CancellationToken ct = default);

        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        WebCallResult<BinanceMiningStatistic> GetMiningStatistics(string algorithm, string userName,
            CancellationToken ct = default);

        /// <summary>
        /// Get mining statistics
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">User name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mining statistics</returns>
        Task<WebCallResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default);

        /// <summary>
        /// Gets mining account list
        /// </summary>
        /// <param name="algorithm">Algorithm</param>
        /// <param name="userName">Mining account user name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Revenue list</returns>
        Task<WebCallResult<BinanceMiningAccount>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default);
    }
}