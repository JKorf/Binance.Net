---
title: IBinanceRestClientUsdFuturesApiExchangeData
has_children: false
parent: IBinanceRestClientUsdFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > UsdFuturesApi > ExchangeData`  
*Binance USD-M futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAggregatedTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/futures/en/#compressed-aggregate-trades-list](https://binance-docs.github.io/apidocs/futures/en/#compressed-aggregate-trades-list)  
<p>

*Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the trades for|
|_[Optional]_ fromId|ID to get aggregate trades from INCLUSIVE.|
|_[Optional]_ startTime|Time to start getting trades from|
|_[Optional]_ endTime|Time to stop getting trades from|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetIndexAsync  

[https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index](https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index)  
<p>

*Get asset index for Multi-Assets mode for a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetAssetIndexAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesAssetIndex>> GetAssetIndexAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetIndexesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index](https://binance-docs.github.io/apidocs/futures/en/#multi-assets-mode-asset-index)  
<p>

*Get asset indexex for Multi-Assets mode for all symbols*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetAssetIndexesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesAssetIndex>>> GetAssetIndexesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBookPriceAsync  

[https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book for a symbol.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetBookPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get book price for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBookPricesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/futures/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetBookPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCompositeIndexInfoAsync  

[https://binance-docs.github.io/apidocs/futures/en/#composite-index-symbol-information](https://binance-docs.github.io/apidocs/futures/en/#composite-index-symbol-information)  
<p>

*Gets composite index info*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetCompositeIndexInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCompositeIndexInfo>>> GetCompositeIndexInfoAsync(string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetContinuousContractKlinesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#continuous-contract-kline-candlestick-data](https://binance-docs.github.io/apidocs/futures/en/#continuous-contract-kline-candlestick-data)  
<p>

*Get candlestick data for the provided pair*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetContinuousContractKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceKline>>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The symbol to get the data for|
|contractType|The contract type|
|interval|The candlestick timespan|
|_[Optional]_ startTime|Start time to get candlestick data|
|_[Optional]_ endTime|End time to get candlestick data|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/futures/en/#exchange-information](https://binance-docs.github.io/apidocs/futures/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and symbol list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingRatesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#get-funding-rate-history](https://binance-docs.github.io/apidocs/futures/en/#get-funding-rate-history)  
<p>

*Get funding rate history for the provided symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetFundingRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesFundingRateHistory>>> GetFundingRatesAsync(string symbol, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ startTime|Start time to get funding rate history|
|_[Optional]_ endTime|End time to get funding rate history|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetGlobalLongShortAccountRatioAsync  

[https://binance-docs.github.io/apidocs/futures/en/#long-short-ratio](https://binance-docs.github.io/apidocs/futures/en/#long-short-ratio)  
<p>

*Gets Global Long/Short Ratio (Accounts)*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetGlobalLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period timespan|
|limit|Max number of results|
|startTime|Start time to get global long/short ratio (accounts)|
|endTime|End time to get global long/short ratio (accounts)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIndexPriceKlinesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#index-price-kline-candlestick-data](https://binance-docs.github.io/apidocs/futures/en/#index-price-kline-candlestick-data)  
<p>

*Get Kline/candlestick data for the index price of a pair.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetIndexPriceKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceKline>>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The symbol to get the data for|
|interval|The candlestick timespan|
|_[Optional]_ startTime|Start time to get candlestick data|
|_[Optional]_ endTime|End time to get candlestick data|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#kline-candlestick-data](https://binance-docs.github.io/apidocs/futures/en/#kline-candlestick-data)  
<p>

*Get klines for a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|interval|The kline interval|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarkPriceAsync  

[https://binance-docs.github.io/apidocs/futures/en/#mark-price](https://binance-docs.github.io/apidocs/futures/en/#mark-price)  
<p>

*Get Mark Price and Funding Rate for the provided symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetMarkPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarkPriceKlinesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#mark-price-kline-candlestick-data](https://binance-docs.github.io/apidocs/futures/en/#mark-price-kline-candlestick-data)  
<p>

*Kline/candlestick bars for the mark price of a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetMarkPriceKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol get the data for|
|interval|The interval of the klines|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time|
|_[Optional]_ endTime|End time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarkPricesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#mark-price](https://binance-docs.github.io/apidocs/futures/en/#mark-price)  
<p>

*Get Mark Price and Funding Rate for all symbols*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetMarkPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesMarkPrice>>> GetMarkPricesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenInterestAsync  

[https://binance-docs.github.io/apidocs/futures/en/#open-interest](https://binance-docs.github.io/apidocs/futures/en/#open-interest)  
<p>

*Get present open interest of a specific symbol.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenInterestHistoryAsync  

[https://binance-docs.github.io/apidocs/futures/en/#open-interest-statistics](https://binance-docs.github.io/apidocs/futures/en/#open-interest-statistics)  
<p>

*Gets Open Interest History*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesOpenInterestHistory>>> GetOpenInterestHistoryAsync(string symbol, PeriodInterval period, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period timespan|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time to get open interest history|
|_[Optional]_ endTime|End time to get open interest history|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://binance-docs.github.io/apidocs/futures/en/#order-book](https://binance-docs.github.io/apidocs/futures/en/#order-book)  
<p>

*Gets the order book for the provided symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the order book for|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPriceAsync  

[https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker)  
<p>

*Gets the price of a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the price for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPricesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/futures/en/#symbol-price-ticker)  
<p>

*Get a list of the prices of all symbols*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentTradesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#recent-trades-list](https://binance-docs.github.io/apidocs/futures/en/#recent-trades-list)  
<p>

*Get the most recent trades for a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get trades for|
|_[Optional]_ limit|Max amount of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#check-server-time](https://binance-docs.github.io/apidocs/futures/en/#check-server-time)  
<p>

*Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetServerTimeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|resetAutoTimestamp|Whether the response should be used for a new auto timestamp calculation|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTakerBuySellVolumeRatioAsync  

[https://binance-docs.github.io/apidocs/futures/en/#taker-buy-sell-volume](https://binance-docs.github.io/apidocs/futures/en/#taker-buy-sell-volume)  
<p>

*Gets Taker Buy/Sell Volume Ratio*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string symbol, PeriodInterval period, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period timespan|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time to get taker buy/sell volume ratio|
|_[Optional]_ endTime|End time to get taker buy/sell volume ratio|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickerAsync  

[https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours change*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IBinance24HPrice>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/futures/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours change*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTopLongShortAccountRatioAsync  

[https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-accounts](https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-accounts)  
<p>

*Gets Top Trader Long/Short Ratio (Accounts)*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortAccountRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period timespan|
|limit|Max number of results|
|startTime|Start time to get top trader long/short ratio (accounts)|
|endTime|End time to get top trader long/short ratio (accounts)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTopLongShortPositionRatioAsync  

[https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-positions](https://binance-docs.github.io/apidocs/futures/en/#top-trader-long-short-ratio-positions)  
<p>

*Gets Top Trader Long/Short Ratio (Positions)*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortPositionRatioAsync(string symbol, PeriodInterval period, int? limit, DateTime? startTime, DateTime? endTime, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period timespan|
|limit|Max number of results|
|startTime|Start time to get top trader long/short ratio (positions)|
|endTime|End time to get top trader long/short ratio (positions)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/futures/en/#old-trades-lookup-market_data](https://binance-docs.github.io/apidocs/futures/en/#old-trades-lookup-market_data)  
<p>

*Get trade history for a symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = default, long? fromId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get trades for|
|_[Optional]_ limit|The max amount of results|
|_[Optional]_ fromId|Retrun trades after this trade id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PingAsync  

[https://binance-docs.github.io/apidocs/futures/en/#test-connectivity](https://binance-docs.github.io/apidocs/futures/en/#test-connectivity)  
<p>

*Pings the Binance Futures API*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.ExchangeData.PingAsync();  
```  

```csharp  
Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>
