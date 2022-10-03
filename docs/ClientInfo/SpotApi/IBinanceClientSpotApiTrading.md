---
title: IBinanceClientSpotApiTrading
has_children: false
parent: IBinanceClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi > Trading`  
*Binance Spot trading endpoints, placing and mananging orders.*
  

***

## AddToLiquidityPoolAsync  

[https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-trade](https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-trade)  
<p>

*Add liquidity to a pool*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.AddToLiquidityPoolAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapOperationResult>> AddToLiquidityPoolAsync(int poolId, string asset, decimal quantity, LiquidityType? type = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|poolId|The pool|
|asset|The asset|
|quantity|Quantity to add|
|_[Optional]_ type|Add type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## AddToLiquidityPoolPreviewAsync  

[https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-preview-user_data](https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-preview-user_data)  
<p>

*Calculate expected share quantity for adding liquidity in single or dual token.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.AddToLiquidityPoolPreviewAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapPreviewResult>> AddToLiquidityPoolPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|poolId|The pool|
|asset|The asset|
|quantity|Quantity to add|
|type|Add type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllMarginOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-all-open-orders-on-a-symbol-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-all-open-orders-on-a-symbol-trade)  
<p>

*Cancel all active orders for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelAllMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the to cancel orders for|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cancel-all-open-orders-on-a-symbol-trade](https://binance-docs.github.io/apidocs/spot/en/#cancel-all-open-orders-on-a-symbol-trade)  
<p>

*Cancels all open orders on a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelAllOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllOrdersAsync(string symbol, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelMarginOcoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-oco-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-oco-trade)  
<p>

*Cancels a pending margin oco order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelMarginOcoOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = default, long? orderListId = default, string? listClientOrderId = default, string? newClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ orderListId|The id of the order list to cancel|
|_[Optional]_ listClientOrderId|The client order id of the order list to cancel|
|_[Optional]_ newClientOrderId|The new client order list id for the order list|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelMarginOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-order-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-order-trade)  
<p>

*Cancel an active order for margin account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, string? newClientOrderId = default, bool? isIsolated = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ newClientOrderId|Unique identifier for this cancel|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOcoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cancel-oco-trade](https://binance-docs.github.io/apidocs/spot/en/#cancel-oco-trade)  
<p>

*Cancels a pending oco order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelOcoOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = default, string? listClientOrderId = default, string? newClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderListId|The id of the order list to cancel|
|_[Optional]_ listClientOrderId|The client order id of the order list to cancel|
|_[Optional]_ newClientOrderId|The new client order list id for the order list|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cancel-order-trade](https://binance-docs.github.io/apidocs/spot/en/#cancel-order-trade)  
<p>

*Cancels a pending order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrderBase>> CancelOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, string? newClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ newClientOrderId|Unique identifier for this cancel|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ConvertTransferAsync  

<p>

*Convert between BUSD and stablecoins*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.ConvertTransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceConvertTransferResult>> ConvertTransferAsync(string clientTransferId, string asset, decimal quantity, string targetAsset, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientTransferId|Transfer id, should be unique value|
|asset|Current asset|
|quantity|Quantity|
|targetAsset|Target asset|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetC2CTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-c2c-trade-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-c2c-trade-history-user_data)  
<p>

*Get Customer to Customer trade history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetC2CTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceC2CUserTrade>>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|side|Trade side|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|The page|
|_[Optional]_ pageSize|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetConvertTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-convert-trade-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-convert-trade-history-user_data)  
<p>

*Get convert trade history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetConvertTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceListResult<BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|startTime|Filter by start time|
|endTime|Filter by end time|
|_[Optional]_ limit|Max amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetConvertTransferHistoryAsync  

<p>

*Get convert transfer history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetConvertTransferHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceConvertTransferRecord>>> GetConvertTransferHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = default, string? asset = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|startTime|Filter by start time|
|endTime|Filter by end time|
|_[Optional]_ transferId|Filter by transfer id|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokensRedemptionRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-redemption-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-redemption-record-user_data)  
<p>

*Get redemption records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLeveragedTokensRedemptionRecordsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBlvtRedemption>>> GetLeveragedTokensRedemptionRecordsAsync(string? tokenName = default, long? id = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ tokenName|Filter by token|
|_[Optional]_ id|Filter by id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokensSubscriptionRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-subscription-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-subscription-record-user_data)  
<p>

*Get subscription records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLeveragedTokensSubscriptionRecordsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBlvtSubscription>>> GetLeveragedTokensSubscriptionRecordsAsync(string? tokenName = default, long? id = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ tokenName|Filter by token|
|_[Optional]_ id|Filter by id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-information-of-a-pool-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-information-of-a-pool-user_data)  
<p>

*Get liquidity info for a pool*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLiquidityPoolInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBSwapPoolLiquidity>>> GetLiquidityPoolInfoAsync(int? poolId = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ poolId|Get a specific pool|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolOperationRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-operation-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-operation-record-user_data)  
<p>

*Get liquidity operation records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLiquidityPoolOperationRecordsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBSwapOperation>>> GetLiquidityPoolOperationRecordsAsync(long? operationId = default, int? poolId = default, BSwapOperation? operation = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ operationId|Filter by operationId|
|_[Optional]_ poolId|Filter by poolId|
|_[Optional]_ operation|Filter by operation|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolSwapHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-swap-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-swap-history-user_data)  
<p>

*Get swap history records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLiquidityPoolSwapHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBSwapRecord>>> GetLiquidityPoolSwapHistoryAsync(long? swapId = default, BSwapStatus? status = default, string? quoteAsset = default, string? baseAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ swapId|Filter by swapId|
|_[Optional]_ status|Filter by status|
|_[Optional]_ quoteAsset|Filter by quote asset|
|_[Optional]_ baseAsset|Filter by base asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolSwapQuoteAsync  

[https://binance-docs.github.io/apidocs/spot/en/#request-quote-user_data](https://binance-docs.github.io/apidocs/spot/en/#request-quote-user_data)  
<p>

*Request a quote for swap quote asset (selling asset) for base asset (buying asset), essentially price/exchange rates. quoteQty is quantity of quote asset(to sell).*  
*Please be noted the quote is for reference only, the actual price will change as the liquidity changes, it's recommended to swap immediate after request a quote for slippage prevention.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetLiquidityPoolSwapQuoteAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapQuote>> GetLiquidityPoolSwapQuoteAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteAsset|Quote asset|
|baseAsset|Base asset|
|quoteQuantity|Quote quantity|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginOcoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-oco-user_data)  
<p>

*Retrieves data for a specific margin oco order. Either orderListId or listClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginOcoOrderAsync();  
```  

```csharp  
Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = default, bool? isIsolated = default, long? orderListId = default, string? origClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Mandatory for isolated margin, not supported for cross margin|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ orderListId|The list order id of the order|
|_[Optional]_ origClientOrderId|Either orderListId or listClientOrderId must be provided|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-oco-user_data)  
<p>

*Retrieves a list of margin oco orders matching the parameters*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginOcoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = default, bool? isIsolated = default, long? fromId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Mandatory for isolated margin, not supported for cross margin|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ fromId|Only return oco orders with id higher than this|
|_[Optional]_ startTime|Only return oco orders placed later than this. Only valid if fromId isn't provided|
|_[Optional]_ endTime|Only return oco orders placed before this. Only valid if fromId isn't provided|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginOpenOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-oco-user_data)  
<p>

*Retrieves a list of open margin oco orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginOpenOcoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOpenOcoOrdersAsync(string? symbol = default, bool? isIsolated = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Mandatory for isolated margin, not supported for cross margin|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-order-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-order-user_data)  
<p>

*Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, bool? isIsolated = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|_[Optional]_ orderId|The order id of the order|
|_[Optional]_ origClientOrderId|The client order id of the order|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-orders-user_data)  
<p>

*Gets all margin account orders for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginOrdersAsync(string symbol, long? orderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, bool? isIsolated = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get orders for|
|_[Optional]_ orderId|If set, only orders with an order id higher than the provided will be returned|
|_[Optional]_ startTime|If set, only orders placed after this time will be returned|
|_[Optional]_ endTime|If set, only orders placed before this time will be returned|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginUserTradesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-trade-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-trade-list-user_data)  
<p>

*Gets all user margin account trades for provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetMarginUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginUserTradesAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? fromId = default, bool? isIsolated = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|_[Optional]_ startTime|Orders newer than this date will be retrieved|
|_[Optional]_ endTime|Orders older than this date will be retrieved|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ fromId|TradeId to fetch from. Default gets most recent trades|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOcoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-oco-user_data)  
<p>

*Retrieves data for a specific oco order. Either orderListId or listClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOcoOrderAsync();  
```  

```csharp  
Task<WebCallResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = default, string? listClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderListId|The list order id of the order|
|_[Optional]_ listClientOrderId|The client order id of the list order|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-all-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-all-oco-user_data)  
<p>

*Retrieves a list of oco orders matching the parameters*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOcoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOcoOrdersAsync(long? fromId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromId|Only return oco orders with id higher than this|
|_[Optional]_ startTime|Only return oco orders placed later than this. Only valid if fromId isn't provided|
|_[Optional]_ endTime|Only return oco orders placed before this. Only valid if fromId isn't provided|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenMarginOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-orders-user_data)  
<p>

*Gets a list of open margin account orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOpenMarginOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenMarginOrdersAsync(string? symbol = default, bool? isIsolated = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get open orders for|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOcoOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-open-oco-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-open-oco-user_data)  
<p>

*Retrieves a list of open oco orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOpenOcoOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOpenOcoOrdersAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#current-open-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#current-open-orders-user_data)  
<p>

*Gets a list of open orders*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOpenOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenOrdersAsync(string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get open orders for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-order-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-order-user_data)  
<p>

*Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = default, string? origClientOrderId = default, long? receiveWindow = default, CancellationToken ct = default);  
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

[https://binance-docs.github.io/apidocs/spot/en/#all-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#all-orders-user_data)  
<p>

*Gets all orders for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOrdersAsync(string symbol, long? orderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
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

## GetPayTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-pay-trade-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-pay-trade-history-user_data)  
<p>

*Get pay trade history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetPayTradeHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePayTrade>>> GetPayTradeHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-history-user_data)  
<p>

*Get staking history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetStakingHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceStakingHistory>>> GetStakingHistoryAsync(StakingProductType product, StakingTransactionType transactionType, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|transactionType|Transaction type|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingPositionsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-position-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-position-user_data)  
<p>

*Get staking positions*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetStakingPositionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceStakingPosition>>> GetStakingPositionsAsync(StakingProductType product, string? productId = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|_[Optional]_ productId|Product id|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#account-trade-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#account-trade-list-user_data)  
<p>

*Gets all user trades for provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.GetUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceTrade>>> GetUserTradesAsync(string symbol, long? orderId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? fromId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get trades for|
|_[Optional]_ orderId|Get trades for this order id|
|_[Optional]_ startTime|Orders newer than this date will be retrieved|
|_[Optional]_ endTime|Orders older than this date will be retrieved|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ fromId|TradeId to fetch from. Default gets most recent trades|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## LiquidityPoolSwapAsync  

[https://binance-docs.github.io/apidocs/spot/en/#swap-trade](https://binance-docs.github.io/apidocs/spot/en/#swap-trade)  
<p>

*Swap quote asset for base asset*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.LiquidityPoolSwapAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapResult>> LiquidityPoolSwapAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quoteAsset|Quote asset|
|baseAsset|Base asset|
|quoteQuantity|Quote quantity|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceMarginOCOOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-oco-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-oco-trade)  
<p>

*Places a new margin OCO(One cancels other) order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceMarginOCOOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol, OrderSide side, decimal price, decimal stopPrice, decimal quantity, decimal? stopLimitPrice = default, TimeInForce? stopLimitTimeInForce = default, decimal? stopIcebergQuantity = default, decimal? limitIcebergQuantity = default, SideEffectType? sideEffectType = default, bool? isIsolated = default, string? listClientOrderId = default, string? limitClientOrderId = default, string? stopClientOrderId = default, OrderResponseType? orderResponseType = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol the order is for|
|side|The order side (buy/sell)|
|price|The price to use|
|stopPrice|The stop price|
|quantity|The quantity of the symbol|
|_[Optional]_ stopLimitPrice|The price for the stop limit order|
|_[Optional]_ stopLimitTimeInForce|Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)|
|_[Optional]_ stopIcebergQuantity|Iceberg quantity for the stop order|
|_[Optional]_ limitIcebergQuantity|Iceberg quantity for the limit order|
|_[Optional]_ sideEffectType|Side effect type|
|_[Optional]_ isIsolated|Is isolated|
|_[Optional]_ listClientOrderId|Client id for the order list|
|_[Optional]_ limitClientOrderId|Client id for the limit order|
|_[Optional]_ stopClientOrderId|Client id for the stop order|
|_[Optional]_ orderResponseType|Order response type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceMarginOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-order-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-order-trade)  
<p>

*Margin account new order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = default, decimal? quoteQuantity = default, string? newClientOrderId = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQuantity = default, SideEffectType? sideEffectType = default, bool? isIsolated = default, OrderResponseType? orderResponseType = default, int? receiveWindow = default, CancellationToken ct = default);  
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
|_[Optional]_ icebergQuantity|Used for iceberg orders|
|_[Optional]_ sideEffectType|Side effect type for this order|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ orderResponseType|Used for the response JSON|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOcoOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#new-oco-trade](https://binance-docs.github.io/apidocs/spot/en/#new-oco-trade)  
<p>

*Places a new OCO(One cancels other) order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceOcoOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, decimal stopPrice, decimal? stopLimitPrice = default, string? listClientOrderId = default, string? limitClientOrderId = default, string? stopClientOrderId = default, decimal? limitIcebergQuantity = default, decimal? stopIcebergQuantity = default, TimeInForce? stopLimitTimeInForce = default, int? trailingDelta = default, int? receiveWindow = default, CancellationToken ct = default);  
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
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#new-order-trade](https://binance-docs.github.io/apidocs/spot/en/#new-order-trade)  
<p>

*Places a new order*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = default, decimal? quoteQuantity = default, string? newClientOrderId = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, OrderResponseType? orderResponseType = default, int? trailingDelta = default, int? receiveWindow = default, CancellationToken ct = default);  
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
|_[Optional]_ orderResponseType|Used for the response JSON|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceTestOrderAsync  

[https://binance-docs.github.io/apidocs/spot/en/#test-new-order-trade](https://binance-docs.github.io/apidocs/spot/en/#test-new-order-trade)  
<p>

*Places a new test order. Test orders are not actually being executed and just test the functionality.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PlaceTestOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = default, decimal? quoteQuantity = default, string? newClientOrderId = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, OrderResponseType? orderResponseType = default, int? trailingDelta = default, int? receiveWindow = default, CancellationToken ct = default);  
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
|_[Optional]_ orderResponseType|Used for the response JSON|
|_[Optional]_ trailingDelta|Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PurchaseStakingProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#purchase-staking-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#purchase-staking-product-user_data)  
<p>

*Purchase a staking product*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.PurchaseStakingProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingPositionResult>> PurchaseStakingProductAsync(StakingProductType product, string productId, decimal quantity, bool? renewable = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|productId|Product id|
|quantity|Quantity to purchase|
|_[Optional]_ renewable|Renewable|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemLeveragedTokenAsync  

[https://binance-docs.github.io/apidocs/spot/en/#redeem-blvt-user_data](https://binance-docs.github.io/apidocs/spot/en/#redeem-blvt-user_data)  
<p>

*Redeem a token*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.RedeemLeveragedTokenAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBlvtRedeemResult>> RedeemLeveragedTokenAsync(string tokenName, decimal quantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokenName|Name of the token to redeem|
|quantity|Quantity to redeem|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemStakingProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#redeem-staking-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#redeem-staking-product-user_data)  
<p>

*Redeem a staking product*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.RedeemStakingProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> RedeemStakingProductAsync(StakingProductType product, string productId, string? positionId = default, decimal? quantity = default, bool? renewable = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|productId|Product id|
|_[Optional]_ positionId|Position id, required for Staking or LockedDefi types|
|_[Optional]_ quantity|Quantity to purchase|
|_[Optional]_ renewable|Renewable|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RemoveFromLiquidityPoolAsync  

[https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-trade](https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-trade)  
<p>

*Remove liquidity from a pool*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.RemoveFromLiquidityPoolAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapOperationResult>> RemoveFromLiquidityPoolAsync(int poolId, string asset, LiquidityType type, decimal shareQuantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|poolId|The pool|
|asset|The asset|
|type|Remove type|
|shareQuantity|Quantity to remove|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RemoveFromLiquidityPoolPreviewAsync  

[https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-preview-user_data](https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-preview-user_data)  
<p>

*Calculate expected share quantity for removing liquidity in single or dual token.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.RemoveFromLiquidityPoolPreviewAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBSwapPreviewResult>> RemoveFromLiquidityPoolPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|poolId|The pool|
|asset|The asset|
|quantity|Quantity to add|
|type|Add type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ReplaceOrderAsync  

<p>

*Cancel an existing order and place a new order on the same symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.ReplaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceReplaceOrderResult>> ReplaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, CancelReplaceMode cancelReplaceMode, long? cancelOrderId = default, string? cancelClientOrderId = default, string? newCancelClientOrderId = default, string? newClientOrderId = default, decimal? quantity = default, decimal? quoteQuantity = default, decimal? price = default, TimeInForce? timeInForce = default, decimal? stopPrice = default, decimal? icebergQty = default, OrderResponseType? orderResponseType = default, int? trailingDelta = default, int? strategyId = default, int? strategyType = default, int? receiveWindow = default, CancellationToken ct = default);  
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
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeLeveragedTokenAsync  

[https://binance-docs.github.io/apidocs/spot/en/#subscribe-blvt-user_data](https://binance-docs.github.io/apidocs/spot/en/#subscribe-blvt-user_data)  
<p>

*Subscribe to a token*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Trading.SubscribeLeveragedTokenAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBlvtSubscribeResult>> SubscribeLeveragedTokenAsync(string tokenName, decimal cost, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokenName|Name of the token to subscribe to|
|cost|Cost of the subscription|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
