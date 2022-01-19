---
title: IBinanceClientCoinFuturesApiExchangeData
has_children: false
parent: IBinanceClientCoinFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > CoinFuturesApi > ExchangeData`  
*Binance COIN-M futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAggregatedTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#compressed-aggregate-trades-list](https://binance-docs.github.io/apidocs/delivery/en/#compressed-aggregate-trades-list)  
<p>

*Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync(/* parameters */);  
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

## GetBasisAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#basis](https://binance-docs.github.io/apidocs/delivery/en/#basis)  
<p>

*Gets basis*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetBasisAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesBasis>>> GetBasisAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The pair to get the data for|
|contractType|The contract type|
|period|The period timespan|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time|
|_[Optional]_ endTime|End time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBookPricesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/delivery/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book for a symbol.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetBookPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesBookPrice>>> GetBookPricesAsync(string? symbol = default, string? pair = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol to get book price for|
|_[Optional]_ pair|Filter by pair|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetContinuousContractKlinesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-data](https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-data)  
<p>

*Get candlestick data for the provided pair*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetContinuousContractKlinesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#exchange-information](https://binance-docs.github.io/apidocs/delivery/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and symbol list*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingRatesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#index-price-and-mark-price](https://binance-docs.github.io/apidocs/delivery/en/#index-price-and-mark-price)  
<p>

*Get funding rate history for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetFundingRatesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#long-short-ratio](https://binance-docs.github.io/apidocs/delivery/en/#long-short-ratio)  
<p>

*Gets Global Long/Short Ratio (Accounts)*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetGlobalLongShortAccountRatioAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#index-price-kline-candlestick-data](https://binance-docs.github.io/apidocs/delivery/en/#index-price-kline-candlestick-data)  
<p>

*Get candlestick data for the provided pair*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetIndexPriceKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-data](https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-data)  
<p>

*Get candlestick data for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|interval|The candlestick timespan|
|_[Optional]_ startTime|Start time to get candlestick data|
|_[Optional]_ endTime|End time to get candlestick data|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarkPriceKlinesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-data](https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-data)  
<p>

*Kline/candlestick bars for the mark price of a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetMarkPriceKlinesAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#index-price-and-mark-price](https://binance-docs.github.io/apidocs/delivery/en/#index-price-and-mark-price)  
<p>

*Get Mark Price and Funding Rate for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetMarkPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCoinMarkPrice>>> GetMarkPricesAsync(string? symbol = default, string? pair = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get the data for|
|_[Optional]_ pair|Filter by pair|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenInterestAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#open-interest](https://binance-docs.github.io/apidocs/delivery/en/#open-interest)  
<p>

*Get present open interest of a specific symbol.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetOpenInterestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCoinOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenInterestHistoryAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#open-interest-statistics](https://binance-docs.github.io/apidocs/delivery/en/#open-interest-statistics)  
<p>

*Gets Open Interest History*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCoinOpenInterestHistory>>> GetOpenInterestHistoryAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The pair to get the data for|
|contractType|The contract type|
|period|The period timespan|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time to get open interest history|
|_[Optional]_ endTime|End time to get open interest history|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#order-book](https://binance-docs.github.io/apidocs/delivery/en/#order-book)  
<p>

*Gets the order book for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
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

## GetPricesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/delivery/en/#symbol-price-ticker)  
<p>

*Get a list of the prices of all symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCoinPrice>>> GetPricesAsync(string? symbol = default, string? pair = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Retrieve for a symbol|
|_[Optional]_ pair|Retrieve prices for a specific pair|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentTradesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#recent-trades-list](https://binance-docs.github.io/apidocs/delivery/en/#recent-trades-list)  
<p>

*Gets the recent trades for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get recent trades for|
|_[Optional]_ limit|Result limit|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#check-server-time](https://binance-docs.github.io/apidocs/delivery/en/#check-server-time)  
<p>

*Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetServerTimeAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#taker-buy-sell-volume](https://binance-docs.github.io/apidocs/delivery/en/#taker-buy-sell-volume)  
<p>

*Gets Taker Buy/Sell Volume Ratio*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetTakerBuySellVolumeRatioAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = default, DateTime? startTime = default, DateTime? endTime = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|pair|The pair to get the data for|
|contractType|The contract type|
|period|The period timespan|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ startTime|Start time to get taker buy/sell volume ratio|
|_[Optional]_ endTime|End time to get taker buy/sell volume ratio|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/delivery/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours change*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(string? symbol = default, string? pair = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get the data for|
|_[Optional]_ pair|Filter by pair|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTopLongShortAccountRatioAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#top-trader-long-short-ratio-accounts](https://binance-docs.github.io/apidocs/delivery/en/#top-trader-long-short-ratio-accounts)  
<p>

*Gets Top Trader Long/Short Ratio (Accounts)*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetTopLongShortAccountRatioAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#top-trader-long-short-ratio-positions](https://binance-docs.github.io/apidocs/delivery/en/#top-trader-long-short-ratio-positions)  
<p>

*Gets Top Trader Long/Short Ratio (Positions)*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetTopLongShortPositionRatioAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#old-trades-lookup-market_data](https://binance-docs.github.io/apidocs/delivery/en/#old-trades-lookup-market_data)  
<p>

*Gets the historical  trades for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = default, long? fromId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get recent trades for|
|_[Optional]_ limit|Result limit|
|_[Optional]_ fromId|From which trade id on results should be retrieved|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PingAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#test-connectivity](https://binance-docs.github.io/apidocs/delivery/en/#test-connectivity)  
<p>

*Pings the Binance Futures API*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.ExchangeData.PingAsync();  
```  

```csharp  
Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>
