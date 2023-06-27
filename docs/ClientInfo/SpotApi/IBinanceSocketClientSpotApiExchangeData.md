---
title: IBinanceSocketClientSpotApiExchangeData
has_children: false
parent: IBinanceSocketClientSpotApi
grand_parent: Socket API documentation
---
*[generated documentation]*  
`BinanceSocketClient > SpotApi > ExchangeData`  
*Binance Spot Exchange Data socket requests and subscriptions*
  

***

## GetAggregatedTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#aggregate-trades](https://binance-docs.github.io/apidocs/websocket_api/en/#aggregate-trades)  
<p>

*Gets compressed, aggregate trades. Trades that fill at the same time, from the same order, with the same price will have the quantity aggregated.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceStreamAggregatedTrade>>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ fromId|Filter by from trade id|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## GetBookTickersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/websocket_api/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book for a symbol.*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetBookTickersAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceBookPrice>>>> GetBookTickersAsync(IEnumerable<string>? symbols = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols|Filter by symbols|

</p>

***

## GetCurrentAvgPriceAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#current-average-price](https://binance-docs.github.io/apidocs/websocket_api/en/#current-average-price)  
<p>

*Gets current average price for a symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceAveragePrice>>> GetCurrentAvgPriceAsync(string symbol);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#exchange-information](https://binance-docs.github.io/apidocs/websocket_api/en/#exchange-information)  
<p>

*Gets information about the exchange including rate limits and symbol list*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceExchangeInfo>>> GetExchangeInfoAsync(IEnumerable<string>? symbols = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols||

</p>

***

## GetKlinesAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#klines](https://binance-docs.github.io/apidocs/websocket_api/en/#klines)  
<p>

*Get candlestick data for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceSpotKline>>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|interval|Kline interval|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## GetOrderBookAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#order-book](https://binance-docs.github.io/apidocs/websocket_api/en/#order-book)  
<p>

*Gets the order book for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<BinanceOrderBook>>> GetOrderBookAsync(string symbol, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ limit|Number of entries|

</p>

***

## GetRecentTradesAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#recent-trades](https://binance-docs.github.io/apidocs/websocket_api/en/#recent-trades)  
<p>

*Gets the recent trades for a symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceRecentTradeQuote>>>> GetRecentTradesAsync(string symbol, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ limit|Max results|

</p>

***

## GetRollingWindowTickersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#rolling-window-price-change-statistics](https://binance-docs.github.io/apidocs/websocket_api/en/#rolling-window-price-change-statistics)  
<p>

*Get data based on the last x time, specified as windowSize*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetRollingWindowTickersAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceRollingWindowTick>>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols);  
```  

|Parameter|Description|
|---|---|
|symbols|Filter by symbols|

</p>

***

## GetServerTimeAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#check-server-time](https://binance-docs.github.io/apidocs/websocket_api/en/#check-server-time)  
<p>

*Get the server time*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<DateTime>>> GetServerTimeAsync();  
```  

|Parameter|Description|
|---|---|

</p>

***

## GetTickersAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/websocket_api/en/#symbol-price-ticker)  
<p>

*Get data regarding the last 24 hours*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<Binance24HPrice>>>> GetTickersAsync(IEnumerable<string>? symbols = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbols|Filter by symbols|

</p>

***

## GetTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#historical-trades-market_data](https://binance-docs.github.io/apidocs/websocket_api/en/#historical-trades-market_data)  
<p>

*Gets the historical trades for a symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceRecentTradeQuote>>>> GetTradeHistoryAsync(string symbol, long? fromId = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ fromId|Filter by from trade id|
|_[Optional]_ limit|Max results|

</p>

***

## GetUIKlinesAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#ui-klines](https://binance-docs.github.io/apidocs/websocket_api/en/#ui-klines)  
<p>

*Get candlestick data for the provided symbol. Returns modified kline data, optimized for the presentation of candlestick charts*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.GetUIKlinesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<BinanceResponse<IEnumerable<BinanceSpotKline>>>> GetUIKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|interval|Kline interval|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max results|

</p>

***

## PingAsync  

[https://binance-docs.github.io/apidocs/websocket_api/en/#test-connectivity](https://binance-docs.github.io/apidocs/websocket_api/en/#test-connectivity)  
<p>

*Ping to test connection*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.PingAsync();  
```  

```csharp  
Task<CallResult<BinanceResponse<object>>> PingAsync();  
```  

|Parameter|Description|
|---|---|

</p>

***

## SubscribeToAggregatedTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
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

## SubscribeToAggregatedTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams](https://binance-docs.github.io/apidocs/spot/en/#aggregate-trade-streams)  
<p>

*Subscribes to the aggregated trades update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(/* parameters */);  
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

## SubscribeToAllMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-market-mini-tickers-stream](https://binance-docs.github.io/apidocs/spot/en/#all-market-mini-tickers-stream)  
<p>

*Subscribes to mini ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToAllMiniTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToAllRollingWindowTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-market-rolling-window-statistics-streams](https://binance-docs.github.io/apidocs/spot/en/#all-market-rolling-window-statistics-streams)  
<p>

*Subscribe to rolling window ticker updates stream for all symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToAllRollingWindowTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan windowSize, Action<DataEvent<IEnumerable<BinanceStreamRollingWindowTick>>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|windowSize|Window size, either 1 hour or 4 hours|
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
var result = await client.SpotApi.ExchangeData.SubscribeToAllTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToBlvtInfoUpdatesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#blvt-info-streams](https://binance-docs.github.io/apidocs/futures/en/#blvt-info-streams)  
<p>

*Subscribes to leveraged token info updates*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToBlvtInfoUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokens|The tokens to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBlvtInfoUpdatesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#blvt-info-streams](https://binance-docs.github.io/apidocs/futures/en/#blvt-info-streams)  
<p>

*Subscribes to leveraged token info updates*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToBlvtInfoUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|token|The token to subscribe to|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBlvtKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams](https://binance-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams)  
<p>

*Subscribes to leveraged token kline updates*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToBlvtKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tokens|The tokens to subscribe to|
|interval|The kline interval|
|onMessage|The event handler for the received data|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBlvtKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams](https://binance-docs.github.io/apidocs/futures/en/#blvt-nav-kline-candlestick-streams)  
<p>

*Subscribes to leveraged token kline updates*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToBlvtKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|token|The token to subscribe to|
|interval|The kline interval|
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
var result = await client.SpotApi.ExchangeData.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToBookTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-book-ticker-streams)  
<p>

*Subscribes to the book ticker update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToBookTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

## SubscribeToKlineUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-streams)  
<p>

*Subscribes to the candlestick update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

*Subscribes to the candlestick update stream for the provided symbol and intervals*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

*Subscribes to the candlestick update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(/* parameters */);  
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

## SubscribeToMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a list of symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToMiniTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-mini-ticker-stream)  
<p>

*Subscribes to mini ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToMiniTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream](https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream)  
<p>

*Subscribes to the depth update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
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

## SubscribeToOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream](https://binance-docs.github.io/apidocs/spot/en/#diff-depth-stream)  
<p>

*Subscribes to the order book updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
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

## SubscribeToPartialOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
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

## SubscribeToPartialOrderBookUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams](https://binance-docs.github.io/apidocs/spot/en/#partial-book-depth-streams)  
<p>

*Subscribes to the depth updates for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(/* parameters */);  
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

## SubscribeToRollingWindowTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-rolling-window-statistics-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-rolling-window-statistics-streams)  
<p>

*Subscribe to rolling window ticker updates stream for a symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToRollingWindowTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToRollingWindowTickerUpdatesAsync(string symbol, TimeSpan windowSize, Action<DataEvent<BinanceStreamRollingWindowTick>> onMessage, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|windowSize|Window size, either 1 hour or 4 hours|
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
var result = await client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToTickerUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams](https://binance-docs.github.io/apidocs/spot/en/#individual-symbol-ticker-streams)  
<p>

*Subscribes to ticker updates stream for a specific symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(/* parameters */);  
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

## SubscribeToTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#trade-streams](https://binance-docs.github.io/apidocs/spot/en/#trade-streams)  
<p>

*Subscribes to the trades update stream for the provided symbols*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(/* parameters */);  
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

## SubscribeToTradeUpdatesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#trade-streams](https://binance-docs.github.io/apidocs/spot/en/#trade-streams)  
<p>

*Subscribes to the trades update stream for the provided symbol*  

```csharp  
var client = new BinanceSocketClient();  
var result = await client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(/* parameters */);  
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
