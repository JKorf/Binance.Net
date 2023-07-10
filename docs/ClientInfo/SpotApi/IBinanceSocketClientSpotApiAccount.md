---
title: IBinanceSocketClientSpotApiAccount
has_children: false
parent: IBinanceSocketClientSpotApi
grand_parent: Socket API documentation
---
*[generated documentation]*  
`BinanceSocketClient > SpotApi > Account`  
*Binance Spot Account socket requests and subscriptions*
  

***

## GetAccountInfoAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-information-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-information-user_data)  
<p>

*Gets account information, including balances*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.GetAccountInfoAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceAccountInfo>>> GetAccountInfoAsync(IEnumerable<string>? symbols = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols||

</p>

***

## GetOrderRateLimitsAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-rate-limits-user_data](https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-rate-limits-user_data)  
<p>

*Get order rate limit status*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.GetOrderRateLimitsAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceCurrentRateLimit>>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols||

</p>

***

## KeepAliveUserStreamAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#ping-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/websocket_api/en/#ping-user-data-stream-user_stream)  
<p>

*Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.KeepAliveUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<object>>> KeepAliveUserStreamAsync(string listenKey);  
```  

|Parameter|Description|
|---|---|
|listenKey||

</p>

***

## StartUserStreamAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#start-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/websocket_api/en/#start-user-data-stream-user_stream)  
<p>

*Starts a user stream by requesting a listen key. This listen key can be used in a subsequent request to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.StartUserStreamAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<string>>> StartUserStreamAsync();  
```  

|Parameter|Description|
|---|---|

</p>

***

## StopUserStreamAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#stop-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/websocket_api/en/#stop-user-data-stream-user_stream)  
<p>

*Stops the current user stream*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.StopUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<object>>> StopUserStreamAsync(string listenKey);  
```  

|Parameter|Description|
|---|---|
|listenKey||

</p>

***

## SubscribeToUserDataUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#user-data-streams](https://binance-docs.github.io/apidocs/spot/en/#user-data-streams)  
<p>

*Subscribes to the account update stream. Prior to using this, the BinanceClient.Spot.UserStreams.StartUserStream method should be called.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.Account.SubscribeToUserDataUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(string listenKey, Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage, Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage, Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage, Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|Listen key retrieved by the StartUserStream method|
|onOrderUpdateMessage|The event handler for whenever an order status update is received|
|onOcoOrderUpdateMessage|The event handler for whenever an oco order status update is received|
|onAccountPositionMessage|The event handler for whenever an account position update is received. Account position updates are a list of changed funds|
|onAccountBalanceUpdate|The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
