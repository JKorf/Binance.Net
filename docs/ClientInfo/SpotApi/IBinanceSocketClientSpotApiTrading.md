---
title: IBinanceSocketClientSpotApiTrading
has_children: false
parent: IBinanceClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi > Trading`  
*Binance Spot Trading socket requests*
  

***

## CancelAllOrdersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-open-orders-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-open-orders-trade)  
<p>

*Cancel all open orders for the symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelAllOrdersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> CancelAllOrdersAsync(string symbol);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|

</p>

***

## CancelOcoOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-oco-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-oco-trade)  
<p>

*Cancel an Oco order by either orderId or clientOrderId*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelOcoOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> CancelOcoOrderAsync(string symbol, long? orderId = default, string? clientOrderId = default, string? newClientOrderId = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ newClientOrderId|New client order id for the order|

</p>

***

## CancelOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-order-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-order-trade)  
<p>

*Cancel an order by either orderId or clientOrderId*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrder>>> CancelOrderAsync(string symbol, int? orderId = default, string? clientOrderId = default, string? newClientOrderId = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ newClientOrderId|New client order id for the order|

</p>

***

## GetOcoOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-oco-history-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-oco-history-user_data)  
<p>

*Get an oco order by either orderId or clientOrderId*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOcoOrderAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> GetOcoOrderAsync(long? orderId = default, string? clientOrderId = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|

</p>

***

## GetOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#query-oco-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#query-oco-user_data)  
<p>

*Get Oco order history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOcoOrdersAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceOrderOcoList>>>> GetOcoOrdersAsync(long? fromOrderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromOrderId|Filter from order id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## GetOpenOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-ocos-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-ocos-user_data)  
<p>

*Get open Oco orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOpenOcoOrdersAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceOrderOcoList>>>> GetOpenOcoOrdersAsync();  
```  

|Parameter|Description|
|---|---|

</p>

***

## GetOpenOrdersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-orders-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-orders-user_data)  
<p>

*Get open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOpenOrdersAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> GetOpenOrdersAsync(string? symbol = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Filter by symbols|

</p>

***

## GetOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#query-order-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#query-order-user_data)  
<p>

*Get order by either orderId or clientOrderId*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrder>>> GetOrderAsync(string symbol, int? orderId = default, string? clientOrderId = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|

</p>

***

## GetOrdersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-history-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-history-user_data)  
<p>

*Get order history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOrdersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> GetOrdersAsync(string symbol, long? fromOrderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ fromOrderId|Filter from order id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## GetPreventedTradesAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-prevented-matches-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-prevented-matches-user_data)  
<p>

*Get prevented trades because of self trade prevention*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetPreventedTradesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinancePreventedTrade>>>> GetPreventedTradesAsync(string symbol, long? preventedTradeId = default, long? orderId = default, long? fromPreventedTradeId = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ preventedTradeId|Filter by prevented trade id|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ fromPreventedTradeId|Filter from prevented id|
|_[Optional]_ limit|Max results|

</p>

***

## GetUserTradesAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-trade-history-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-trade-history-user_data)  
<p>

*Gets user trades for provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceTrade>>>> GetUserTradesAsync(string symbol, long? orderId = default, long? fromOrderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ fromOrderId|Filter from order id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## PlaceOcoOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-oco-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-oco-trade)  
<p>

*Places a new OCO(One cancels other) order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceOcoOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> PlaceOcoOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, decimal stopPrice, decimal? stopLimitPrice = default, string? listClientOrderId = default, string? limitClientOrderId = default, string? stopClientOrderId = default, decimal? limitIcebergQuantity = default, decimal? stopIcebergQuantity = default, TimeInForce? stopLimitTimeInForce = default, int? trailingDelta = default, int? limitStrategyId = default, int? limitStrategyType = default, decimal? limitIcebergQty = default, int? stopStrategyId = default, int? stopStrategyType = default, int? stopIcebergQty = default, SelfTradePreventionMode? selfTradePreventionMode = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|quantity|The quantity of the symbol|
|price|The price to use|
|stopPrice|The stop price|
|_[Optional]_ stopLimitPrice|The price for the stop limit order|
|_[Optional]_ listClientOrderId|Client id for the order list|
|_[Optional]_ limitClientOrderId|Client id for the limit order|
|_[Optional]_ stopClientOrderId|Client id for the stop order|
|_[Optional]_ limitIcebergQuantity|Iceberg quantity for the limit order|
|_[Optional]_ stopIcebergQuantity|Iceberg quantity for the stop order|
|_[Optional]_ stopLimitTimeInForce|Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ limitStrategyId|Strategy id of the limit order|
|_[Optional]_ limitStrategyType|Strategy type of the limit order|
|_[Optional]_ limitIcebergQty|Iceberg quantity of the limit order|
|_[Optional]_ stopStrategyId|Strategy id of the stop order|
|_[Optional]_ stopStrategyType|Strategy type of the stop order|
|_[Optional]_ stopIcebergQty|Iceberg quantity of the stop order|
|_[Optional]_ selfTradePreventionMode|Self trade prevention mode|

</p>

***

## PlaceOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-order-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-order-trade)  
<p>

*Places a new order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = default, decimal? quoteQuantity = default, string? newClientOrderId = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, int? trailingDelta = default, int? strategyId = default, int? strategyType = default, SelfTradePreventionMode? selfTradePreventionMode = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|type|The order type|
|_[Optional]_ quantity|The quantity of the symbol|
|_[Optional]_ quoteQuantity|The quantity of the quote symbol. Only valid for market orders|
|_[Optional]_ newClientOrderId|Unique id for order|
|_[Optional]_ price|The price to use|
|_[Optional]_ timeInForce|Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)|
|_[Optional]_ stopPrice|Used for stop orders|
|_[Optional]_ icebergQty|Used for iceberg orders|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ strategyId|Strategy id|
|_[Optional]_ strategyType|Strategy type|
|_[Optional]_ selfTradePreventionMode|Self trade prevention mode|

</p>

***

## PlaceTestOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#test-new-order-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#test-new-order-trade)  
<p>

*Places a new test order. Test orders are not actually being executed and just test the functionality.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceTestOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceTestOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = default, decimal? quoteQuantity = default, string? newClientOrderId = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, int? trailingDelta = default, int? strategyId = default, int? strategyType = default, SelfTradePreventionMode? selfTradePreventionMode = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|type|The order type (limit/market)|
|_[Optional]_ quantity|The quantity of the symbol|
|_[Optional]_ quoteQuantity|The quantity of the quote symbol. Only valid for market orders|
|_[Optional]_ newClientOrderId|Unique id for order|
|_[Optional]_ price|The price to use|
|_[Optional]_ timeInForce|Lifetime of the order (GoodTillCancel/ImmediateOrCancel)|
|_[Optional]_ stopPrice|Used for stop orders|
|_[Optional]_ icebergQty|User for iceberg orders|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ strategyId|Strategy id|
|_[Optional]_ strategyType|Strategy type|
|_[Optional]_ selfTradePreventionMode|Self trade prevention mode|

</p>

***

## ReplaceOrderAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-and-replace-order-trade](https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-and-replace-order-trade)  
<p>

*Cancel an existing order and place a new order on the same symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.ReplaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceReplaceOrderResult>>> ReplaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, CancelReplaceMode cancelReplaceMode, long? cancelOrderId = default, string? cancelClientOrderId = default, string? newCancelClientOrderId = default, string? newClientOrderId = default, decimal? quantity = default, decimal? quoteQuantity = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, OrderResponseType? orderResponseType = default, int? trailingDelta = default, int? strategyId = default, int? strategyType = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|type|The order type|
|cancelReplaceMode|Replacement behavior|
|_[Optional]_ cancelOrderId|The order id to cancel. Either this or cancelClientOrderId should be provided|
|_[Optional]_ cancelClientOrderId|The client order id to cancel. Either this or cancelOrderId should be provided|
|_[Optional]_ newCancelClientOrderId|New client order id for the canceled order|
|_[Optional]_ newClientOrderId|Unique id for order|
|_[Optional]_ quantity|The quantity of the symbol|
|_[Optional]_ quoteQuantity|The quantity of the quote symbol. Only valid for market orders|
|_[Optional]_ price|The price to use|
|_[Optional]_ timeInForce|Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)|
|_[Optional]_ stopPrice|Used for stop orders|
|_[Optional]_ icebergQty|Used for iceberg orders|
|_[Optional]_ orderResponseType|Used for the response JSON|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ strategyId|Strategy id|
|_[Optional]_ strategyType|Strategy type|

</p>
