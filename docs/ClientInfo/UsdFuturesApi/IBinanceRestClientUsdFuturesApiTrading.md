---
title: IBinanceRestClientUsdFuturesApiTrading
has_children: false
parent: IBinanceClientUsdFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > UsdFuturesApi > IBinanceRestClientTrading`  
*Binance USD-M futures trading endpoints, placing and mananging orders.*
  

***

## CancelAlgoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cancel-algo-order-trade](https://binance-docs.github.io/apidocs/spot/en/#cancel-algo-order-trade)  
<p>

*Cancel an algo order*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.CancelAlgoOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algoOrderId|Algo id to cancel|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllOrdersAfterTimeoutAsync  

[https://binance-docs.github.io/apidocs/futures/en/#auto-cancel-all-open-orders-trade](https://binance-docs.github.io/apidocs/futures/en/#auto-cancel-all-open-orders-trade)  
<p>

*Cancel all open orders of the specified symbol at the end of the specified countdown. This rest endpoint means to ensure your open orders are canceled in case of an outage. The endpoint should be called repeatedly as heartbeats*  
*so that the existing countdown time can be canceled and replaced by a new one.*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.CancelAllOrdersAfterTimeoutAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#cancel-all-open-orders-trade](https://binance-docs.github.io/apidocs/futures/en/#cancel-all-open-orders-trade)  
<p>

*Cancels all open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.CancelAllOrdersAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#cancel-multiple-orders-trade](https://binance-docs.github.io/apidocs/futures/en/#cancel-multiple-orders-trade)  
<p>

*Cancels muliple orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.CancelMultipleOrdersAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#cancel-order-trade](https://binance-docs.github.io/apidocs/futures/en/#cancel-order-trade)  
<p>

*Cancels a pending order*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.CancelOrderAsync(/* parameters */);  
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

## GetAlgoSubOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-sub-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-sub-orders-user_data)  
<p>

*Get algo sub orders overview*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetAlgoSubOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algoId|Algo id|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedAlgoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-historical-algo-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-historical-algo-orders-user_data)  
<p>

*Get list of closed algo orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetClosedAlgoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = default, OrderSide? side = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbol|
|_[Optional]_ side|Filter by side|
|_[Optional]_ startTime|Fitler by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetForcedOrdersAsync  

[https://binance-docs.github.io/apidocs/futures/en/#user-39-s-force-orders-user_data](https://binance-docs.github.io/apidocs/futures/en/#user-39-s-force-orders-user_data)  
<p>

*Gets a list of users forced orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetForcedOrdersAsync();  
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

## GetOpenAlgoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-current-algo-open-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-current-algo-open-orders-user_data)  
<p>

*Get list of open algo orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetOpenAlgoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrderAsync  

[https://binance-docs.github.io/apidocs/futures/en/#query-current-open-order-user_data](https://binance-docs.github.io/apidocs/futures/en/#query-current-open-order-user_data)  
<p>

*Retrieves data for a specific open order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetOpenOrderAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#current-all-open-orders-user_data](https://binance-docs.github.io/apidocs/futures/en/#current-all-open-orders-user_data)  
<p>

*Gets a list of open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetOpenOrdersAsync();  
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

[https://binance-docs.github.io/apidocs/futures/en/#query-order-user_data](https://binance-docs.github.io/apidocs/futures/en/#query-order-user_data)  
<p>

*Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetOrderAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#all-orders-user_data](https://binance-docs.github.io/apidocs/futures/en/#all-orders-user_data)  
<p>

*Gets all orders for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOrdersAsync(string symbol, long? orderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get orders for|
|_[Optional]_ orderId|If set, only orders with an order id higher than the provided will be returned|
|_[Optional]_ startTime|If set, only orders placed after this time will be returned|
|_[Optional]_ endTime|If set, only orders placed before this time will be returned|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#account-trade-list-user_data](https://binance-docs.github.io/apidocs/futures/en/#account-trade-list-user_data)  
<p>

*Gets all user trades for provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.GetUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesUsdtTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? fromId = default, long? orderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|_[Optional]_ startTime|Orders newer than this date will be retrieved|
|_[Optional]_ endTime|Orders older than this date will be retrieved|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ fromId|TradeId to fetch from. Default gets most recent trades|
|_[Optional]_ orderId|Get the trades for a specific order|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceMultipleOrdersAsync  

[https://binance-docs.github.io/apidocs/futures/en/#place-multiple-orders-trade](https://binance-docs.github.io/apidocs/futures/en/#place-multiple-orders-trade)  
<p>

*Place multiple orders in one call*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.PlaceMultipleOrdersAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/futures/en/#new-order-trade](https://binance-docs.github.io/apidocs/futures/en/#new-order-trade)  
<p>

*Places a new order*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.PlaceOrderAsync(/* parameters */);  
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

***

## PlaceTimeWeightedAveragePriceOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#time-weighted-average-price-twap-new-order-trade](https://binance-docs.github.io/apidocs/spot/en/#time-weighted-average-price-twap-new-order-trade)  
<p>

*Place a new Time Weighted Average Price order*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.PlaceTimeWeightedAveragePriceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(string symbol, OrderSide side, decimal quantity, int duration, string? clientOrderId = default, bool? reduceOnly = default, decimal? limitPrice = default, PositionSide? positionSide = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|side|Order side|
|quantity|Order quantity|
|duration|Duration in seconds. Less than 5 minutes will be defaulted to 5 minutes, more than 24 hours will be defaulted to 24 hours.|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ limitPrice|Limit price of the order. If null will use market price|
|_[Optional]_ positionSide|Position side|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceVolumeParticipationOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#volume-participation-vp-new-order-trade](https://binance-docs.github.io/apidocs/spot/en/#volume-participation-vp-new-order-trade)  
<p>

*Place a new Volume Participation order*  

```csharp  
var client = new BinanceClient();  
var result = await client.UsdFuturesApi.IBinanceRestClientTrading.PlaceVolumeParticipationOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceAlgoOrderResult>> PlaceVolumeParticipationOrderAsync(string symbol, OrderSide side, decimal quantity, OrderUrgency urgency, string? clientOrderId = default, bool? reduceOnly = default, decimal? limitPrice = default, PositionSide? positionSide = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|side|Order side|
|quantity|Order quantity|
|urgency|Represent the relative speed of the current execution|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ limitPrice|Limit price of the order. If null will use market price|
|_[Optional]_ positionSide|Position side|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
