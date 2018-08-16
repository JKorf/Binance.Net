using System;
using System.Threading.Tasks;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.RateLimiter;

namespace Binance.Net.Interfaces
{
    public interface IBinanceClient
    {
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.PingAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<long> Ping();

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if succesful ping, false if no response</returns>
        Task<CallResult<long>> PingAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetServerTimeAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<DateTime> GetServerTime(bool resetAutoTimestamp = false);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <returns>Server time</returns>
        Task<CallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetExchangeInfoAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceExchangeInfo> GetExchangeInfo();

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <returns>Exchange info</returns>
        Task<CallResult<BinanceExchangeInfo>> GetExchangeInfoAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetOrderBookAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null);

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The order book for the symbol</returns>
        Task<CallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAggregatedTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceAggregatedTrades[]> GetAggregatedTrades(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        Task<CallResult<BinanceAggregatedTrades[]>> GetAggregatedTradesAsync(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetRecentTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceRecentTrade[]> GetRecentTrades(string symbol, int? limit = null);

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <returns>List of recent trades</returns>
        Task<CallResult<BinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetHistoricalTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceRecentTrade[]> GetHistoricalTrades(string symbol, int? limit = null, long? fromId = null);

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <returns>List of recent trades</returns>
        Task<CallResult<BinanceRecentTrade[]>> GetHistoricalTradesAsync(string symbol, int? limit = null, long? fromId = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetKlinesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceKline[]> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        Task<CallResult<BinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.Get24HPriceAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<Binance24HPrice> Get24HPrice(string symbol);

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <returns>Data over the last 24 hours</returns>
        Task<CallResult<Binance24HPrice>> Get24HPriceAsync(string symbol);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.Get24HPricesListAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<Binance24HPrice[]> Get24HPricesList();

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <returns>List of data over the last 24 hours</returns>
        Task<CallResult<Binance24HPrice[]>> Get24HPricesListAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAllPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinancePrice> GetPrice(string symbol);

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <returns>Price of symbol</returns>
        Task<CallResult<BinancePrice>> GetPriceAsync(string symbol);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAllPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinancePrice[]> GetAllPrices();

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <returns>List of prices</returns>
        Task<CallResult<BinancePrice[]>> GetAllPricesAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetBookPriceAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceBookPrice> GetBookPrice(string symbol);

        /// <summary>
        /// Gets the best price/qantity on the order book for a symbol.
        /// </summary>
        /// <returns>List of book prices</returns>
        Task<CallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAllBookPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceBookPrice[]> GetAllBookPrices();

        /// <summary>
        /// Gets the best price/qantity on the order book for all symbols.
        /// </summary>
        /// <returns>List of book prices</returns>
        Task<CallResult<BinanceBookPrice[]>> GetAllBookPricesAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetOpenOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceOrder[]> GetOpenOrders(string symbol = null, int? receiveWindow = null);

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of open orders</returns>
        Task<CallResult<BinanceOrder[]>> GetOpenOrdersAsync(string symbol = null, int? receiveWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAllOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceOrder[]> GetAllOrders(string symbol, long? orderId = null, int? limit = null, int? receiveWindow = null);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of orders</returns>
        Task<CallResult<BinanceOrder[]>> GetAllOrdersAsync(string symbol, long? orderId = null, int? limit = null, int? receiveWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.PlaceOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinancePlacedOrder> PlaceOrder(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null);

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed order</returns>
        Task<CallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.PlaceTestOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinancePlacedOrder> PlaceTestOrder(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null);

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">What kind of response should be returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed test order</returns>
        Task<CallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.QueryOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceOrder> QueryOrder(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The specific order</returns>
        Task<CallResult<BinanceOrder>> QueryOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.CancelOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceCanceledOrder> CancelOrder(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null);

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        Task<CallResult<BinanceCanceledOrder>> CancelOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetAccountInfoAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceAccountInfo> GetAccountInfo(long? recvWindow = null);

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The account information</returns>
        Task<CallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetMyTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceTrade[]> GetMyTrades(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null);

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of trades</returns>
        Task<CallResult<BinanceTrade[]>> GetMyTradesAsync(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.WithdrawAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, decimal amount, string addressTag = null, string name = null, int? recvWindow = null);

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="name">Name for the transaction</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal confirmation</returns>
        Task<CallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal amount, string addressTag = null, string name = null, int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetDepositHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceDepositList> GetDepositHistory(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null);

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of deposits</returns>
        Task<CallResult<BinanceDepositList>> GetDepositHistoryAsync(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetWithdrawHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceWithdrawalList> GetWithdrawHistory(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null);

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of withdrawals</returns>
        Task<CallResult<BinanceWithdrawalList>> GetWithdrawHistoryAsync(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetDepositAddressAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceDepositAddress> GetDepositAddress(string asset, int? recvWindow = null);

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Deposit address</returns>
        Task<CallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.GetWithdrawalFeeAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<decimal> GetWithdrawalFee(string asset, int? recvWindow = null);

        /// <summary>
        /// Gets the withdrawal fee for an asset
        /// </summary>
        /// <param name="asset">Asset to get withdrawal fee for</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal fee</returns>
        Task<CallResult<decimal>> GetWithdrawalFeeAsync(string asset, int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="GetAccountStatusAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceAccountStatus> GetAccountStatus(int? recvWindow = null);

        /// <summary>
        /// Gets the status of the account associated with the apikey/secret
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Account status</returns>
        Task<CallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="GetSystemStatusAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceSystemStatus> GetSystemStatus();

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <returns>The system status</returns>
        Task<CallResult<BinanceSystemStatus>> GetSystemStatusAsync();

        /// <summary>
        /// Synchronized version of the <see cref="GetDustLog"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceDustLog[]> GetDustLog(int? recvWindow = null);

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The history of dust conversions</returns>
        Task<CallResult<BinanceDustLog[]>> GetDustLogAsync(int? recvWindow = null);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.StartUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceListenKey> StartUserStream();

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserStream"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <returns>Listen key</returns>
        Task<CallResult<BinanceListenKey>> StartUserStreamAsync();

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.KeepAliveUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<object> KeepAliveUserStream(string listenKey);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <returns></returns>
        Task<CallResult<object>> KeepAliveUserStreamAsync(string listenKey);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceClient.StopUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<object> StopUserStream(string listenKey);

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <returns></returns>
        Task<CallResult<object>> StopUserStreamAsync(string listenKey);

        void AddRateLimiter(IRateLimiter limiter);
        void RemoveRateLimiters();
        void Dispose();
        IRequestFactory RequestFactory { get; set; }
    }
}