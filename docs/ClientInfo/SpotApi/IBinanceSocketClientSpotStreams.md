---
title: IBinanceSocketClientSpotStreams
has_children: true
parent: Socket API documentation
---
*[generated documentation]*  
`BinanceSocketClient > SpotStreams`  
*Binance Spot streams*
  

***

## SubscribeToAggregatedTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAggregatedTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAllBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-book-tickers-stream](https://binance-docs.github.io/apidocs/spot/en/#all-book-tickers-stream)  
<p>

*Subscribes to all book ticker update streams*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToAllBookTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAllMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-market-mini-tickers-stream](https://binance-docs.github.io/apidocs/spot/en/#all-market-mini-tickers-stream)  
<p>

*Subscribes to mini ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToAllMiniTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAllTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-market-tickers-stream](https://binance-docs.github.io/apidocs/spot/en/#all-market-tickers-stream)  
<p>

*Subscribes to ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToAllTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams)  
<p>

*Subscribes to the book ticker update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams)  
<p>

*Subscribes to the book ticker update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbol and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|intervals|The intervals of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|intervals|The intervals of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a list of symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream](https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream)  
<p>

*Subscribes to the order book updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|updateInterval|Update interval in milliseconds|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream](https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream)  
<p>

*Subscribes to the depth update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|updateInterval|Update interval in milliseconds|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe on|
|levels|The amount of entries to be returned in the update|
|updateInterval|Update interval in milliseconds|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe on|
|levels|The amount of entries to be returned in the update of each symbol|
|updateInterval|Update interval in milliseconds|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams)  
<p>

*Subscribes to ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams)  
<p>

*Subscribes to ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#trade-streams](https://binance-docs.github.io/apidocs/spot/en/#trade-streams)  
<p>

*Subscribes to the trades update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#trade-streams](https://binance-docs.github.io/apidocs/spot/en/#trade-streams)  
<p>

*Subscribes to the trades update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToUserDataUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#user-data-streams](https://binance-docs.github.io/apidocs/spot/en/#user-data-streams)  
<p>

*Subscribes to the account update stream. Prior to using this, the BinanceClient.Spot.UserStreams.StartUserStream method should be called.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotStreams.SubscribeToUserDataUpdatesAsync(/* parameters */);  
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
