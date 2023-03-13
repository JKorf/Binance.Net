---
title: IBinanceSocketClientCoinFuturesStreams
has_children: false
parent: Socket API documentation
---
*[generated documentation]*  
`BinanceSocketClient > CoinFuturesStreams`  
*Binance Coin futures streams*
  

***

## SubscribeToAggregatedTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#all-book-tickers-stream](https://binance-docs.github.io/apidocs/delivery/en/#all-book-tickers-stream)  
<p>

*Subscribes to all book ticker update streams*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAllBookTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToAllLiquidationUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#all-market-liquidation-order-streams](https://binance-docs.github.io/apidocs/delivery/en/#all-market-liquidation-order-streams)  
<p>

*Subscribes to all forced liquidations stream*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAllLiquidationUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToAllMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#all-market-mini-tickers-stream](https://binance-docs.github.io/apidocs/delivery/en/#all-market-mini-tickers-stream)  
<p>

*Subscribes to mini ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAllMiniTickerUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#all-market-tickers-streams](https://binance-docs.github.io/apidocs/delivery/en/#all-market-tickers-streams)  
<p>

*Subscribes to ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToAllTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinance24HPrice>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams)  
<p>

*Subscribes to the book ticker update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams)  
<p>

*Subscribes to the book ticker update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToContinuousContractKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams)  
<p>

*Subscribes to the continuous contract candlestick update stream for the provided pair*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToContinuousContractKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The pair|
|contractType|The contract type|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToContinuousContractKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams)  
<p>

*Subscribes to the continuous contract candlestick update stream for the provided pairs*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToContinuousContractKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pairs|The pairs|
|contractType|The contract type|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams)  
<p>

*Subscribes to the index candlestick update stream for the provided pair*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToIndexKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The pair|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams)  
<p>

*Subscribes to the index candlestick update stream for the provided pairs*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToIndexKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pairs|The pairs|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexPriceUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream](https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream)  
<p>

*Subscribes to the Index price update stream for a single pair*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToIndexPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The symbol|
|updateInterval|Update interval in milliseconds, either 1000 or 3000. Defaults to 3000|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToIndexPriceUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream](https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream)  
<p>

*Subscribes to the Index price update stream for a list of pairs*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToIndexPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pairs|The pairs|
|updateInterval|Update interval in milliseconds, either 1000 or 3000. Defaults to 3000|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbol and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

## SubscribeToLiquidationUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams](https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams)  
<p>

*Subscribes to specific symbol forced liquidations stream*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToLiquidationUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToLiquidationUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams](https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams)  
<p>

*Subscribes to list of symbol forced liquidations stream*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToLiquidationUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams)  
<p>

*Subscribes to the mark price candlestick update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMarkPriceKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams](https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams)  
<p>

*Subscribes to the mark price candlestick update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMarkPriceKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|interval|The interval of the candlesticks|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream](https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream)  
<p>

*Subscribes to the Mark price update stream for a single symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMarkPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|updateInterval|Update interval in milliseconds, either 1000 or 3000. Defaults to 3000|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMarkPriceUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream](https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream)  
<p>

*Subscribes to the Mark price update stream for a list of symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMarkPriceUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|updateInterval|Update interval in milliseconds, either 1000 or 3000. Defaults to 3000|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a list of symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams](https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams)  
<p>

*Subscribes to the order book updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|updateInterval|Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams](https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams)  
<p>

*Subscribes to the depth update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols|
|updateInterval|Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe on|
|levels|The amount of entries to be returned in the update of each symbol|
|updateInterval|Update interval in milliseconds, either 100 or 500. Defaults to 250|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams)  
<p>

*Subscribes to ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams](https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams)  
<p>

*Subscribes to ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToUserDataUpdatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#user-data-streams](https://binance-docs.github.io/apidocs/delivery/en/#user-data-streams)  
<p>

*Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.CoinFuturesStreams.SubscribeToUserDataUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(string listenKey, Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onLeverageUpdate, Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate, Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate, Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate, Action<DataEvent<BinanceStreamEvent>> onListenKeyExpired, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|Listen key retrieved by the StartUserStream method|
|onLeverageUpdate|The event handler for leverage changed update|
|onMarginUpdate|The event handler for whenever a margin has changed|
|onAccountUpdate|The event handler for whenever an account update is received|
|onOrderUpdate|The event handler for whenever an order status update is received|
|onListenKeyExpired|Responds when the listen key for the stream has expired. Initiate a new instance of the stream here|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
