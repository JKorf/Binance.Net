---
title: IBinanceClientCoinFuturesApiTrading
has_children: false
parent: IBinanceClientCoinFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > CoinFuturesApi > Trading`  
*Binance COIN-M futures trading endpoints, placing and mananging orders.*
  

***

## CancelAllOrdersAfterTimeoutAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#auto-cancel-all-open-orders-trade](https://binance-docs.github.io/apidocs/delivery/en/#auto-cancel-all-open-orders-trade)  
<p>

*Cancel all open orders of the specified symbol at the end of the specified countdown. This rest endpoint means to ensure your open orders are canceled in case of an outage. The endpoint should be called repeatedly as heartbeats*  
*so that the existing countdown time can be canceled and replaced by a new one.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.CancelAllOrdersAfterTimeoutAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|countDownTime|The time after which all open orders should cancel, or 0 to cancel an existing timer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#cancel-all-open-orders-trade](https://binance-docs.github.io/apidocs/delivery/en/#cancel-all-open-orders-trade)  
<p>

*Cancels all open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.CancelAllOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelMultipleOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#cancel-multiple-orders-trade](https://binance-docs.github.io/apidocs/delivery/en/#cancel-multiple-orders-trade)  
<p>

*Cancels muliple orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.CancelMultipleOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>>> CancelMultipleOrdersAsync(string symbol, List<long>? orderIdList = default, List<string>? origClientOrderIdList = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderIdList|The list of order ids to cancel|
|_[Optional]_ origClientOrderIdList|The list of client order ids to cancel|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#cancel-order-trade](https://binance-docs.github.io/apidocs/delivery/en/#cancel-order-trade)  
<p>

*Cancels a pending order*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCancelOrder>> CancelOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetForcedOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#user-39-s-force-orders-user_data](https://binance-docs.github.io/apidocs/delivery/en/#user-39-s-force-orders-user_data)  
<p>

*Gets a list of users forced orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetForcedOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetForcedOrdersAsync(string? symbol = default, AutoCloseType? closeType = default, DateTime? startTime = default, DateTime? endTime = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get forced orders for|
|_[Optional]_ closeType|Filter by reason for close|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrderAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#query-current-open-order-user_data](https://binance-docs.github.io/apidocs/delivery/en/#query-current-open-order-user_data)  
<p>

*Retrieves data for a specific open order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetOpenOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#current-all-open-orders-user_data](https://binance-docs.github.io/apidocs/delivery/en/#current-all-open-orders-user_data)  
<p>

*Gets a list of open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetOpenOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOpenOrdersAsync(string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get open orders for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#query-order-user_data](https://binance-docs.github.io/apidocs/delivery/en/#query-order-user_data)  
<p>

*Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesOrder>> GetOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#all-orders-user_data](https://binance-docs.github.io/apidocs/delivery/en/#all-orders-user_data)  
<p>

*Gets all orders for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOrdersAsync(string? symbol = default, long? orderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get orders for|
|_[Optional]_ orderId|If set, only orders with an order id higher than the provided will be returned|
|_[Optional]_ startTime|If set, only orders placed after this time will be returned|
|_[Optional]_ endTime|If set, only orders placed before this time will be returned|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#account-trade-list-user_data](https://binance-docs.github.io/apidocs/delivery/en/#account-trade-list-user_data)  
<p>

*Gets all user trades for provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.GetUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCoinTrade>>> GetUserTradesAsync(string? symbol = default, string? pair = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? fromId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol to get trades for|
|_[Optional]_ pair|Symbol to get trades for|
|_[Optional]_ startTime|Orders newer than this date will be retrieved|
|_[Optional]_ endTime|Orders older than this date will be retrieved|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ fromId|TradeId to fetch from. Default gets most recent trades|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceMultipleOrdersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#place-multiple-orders-trade](https://binance-docs.github.io/apidocs/delivery/en/#place-multiple-orders-trade)  
<p>

*Place multiple orders in one call*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.PlaceMultipleOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>> PlaceMultipleOrdersAsync(BinanceFuturesBatchOrder[] orders, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orders|The orders to place|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#new-order-trade](https://binance-docs.github.io/apidocs/delivery/en/#new-order-trade)  
<p>

*Places a new order*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesPlacedOrder>> PlaceOrderAsync(string symbol, OrderSide side, FuturesOrderType type, decimal? quantity, decimal? price = default, PositionSide? positionSide = default, TimeInForce? timeInForce = default, bool? reduceOnly = default, string? newClientOrderId = default, decimal? stopPrice = default, decimal? activationPrice = default, decimal? callbackRate = default, WorkingType? workingType = default, bool? closePosition = default, OrderResponseType? orderResponseType = default, bool? priceProtect = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|type|The order type|
|quantity|The quantity of the base symbol|
|_[Optional]_ price|The price to use|
|_[Optional]_ positionSide|The position side|
|_[Optional]_ timeInForce|Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)|
|_[Optional]_ reduceOnly|Specify as true if the order is intended to only reduce the position|
|_[Optional]_ newClientOrderId|Unique id for order|
|_[Optional]_ stopPrice|Used for stop orders|
|_[Optional]_ activationPrice|Used with TRAILING_STOP_MARKET orders, default as the latest price（supporting different workingType)|
|_[Optional]_ callbackRate|Used with TRAILING_STOP_MARKET orders|
|_[Optional]_ workingType|stopPrice triggered by: "MARK_PRICE", "CONTRACT_PRICE"|
|_[Optional]_ closePosition|Close-All，used with STOP_MARKET or TAKE_PROFIT_MARKET.|
|_[Optional]_ orderResponseType|The response type. Default Acknowledge|
|_[Optional]_ priceProtect|If true when price reaches stopPrice, difference between "MARK_PRICE" and "CONTRACT_PRICE" cannot be larger than "triggerProtect" of the symbol.|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
