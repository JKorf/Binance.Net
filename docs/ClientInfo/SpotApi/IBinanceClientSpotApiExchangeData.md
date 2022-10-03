---
title: IBinanceClientSpotApiExchangeData
has_children: false
parent: IBinanceClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi > ExchangeData`  
*Binance Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAggregatedTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#compressed-aggregate-trades-list](https://binance-docs.github.io/apidocs/spot/en/#compressed-aggregate-trades-list)  
<p>

*Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync(/* parameters */);  
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

## GetAssetDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#asset-detail-user_data](https://binance-docs.github.io/apidocs/spot/en/#asset-detail-user_data)  
<p>

*Gets the withdraw/deposit details for an asset*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetAssetDetailsAsync();  
```  

```csharp  
Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBookPriceAsync  

[https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics)  
<p>

*Gets the best price/quantity on the order book for a symbol.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetBookPriceAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/spot/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/spot/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book for a symbol.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetBookPricesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(IEnumerable<string> symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get book price for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBookPricesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#symbol-order-book-ticker](https://binance-docs.github.io/apidocs/spot/en/#symbol-order-book-ticker)  
<p>

*Gets the best price/quantity on the order book for all symbols.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetBookPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCurrentAvgPriceAsync  

[https://binance-docs.github.io/apidocs/spot/en/#current-average-price](https://binance-docs.github.io/apidocs/spot/en/#current-average-price)  
<p>

*Gets current average price for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetCurrentAvgPriceAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#exchange-information](https://binance-docs.github.io/apidocs/spot/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and symbol list*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#exchange-information](https://binance-docs.github.io/apidocs/spot/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and information on the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get data for token|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#exchange-information](https://binance-docs.github.io/apidocs/spot/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and information on the provided symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|Symbols to get data for token|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#exchange-information](https://binance-docs.github.io/apidocs/spot/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and information on the provided symbol based on an account permission*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(AccountType permission, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|permission|account type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetExchangeInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#exchange-information](https://binance-docs.github.io/apidocs/spot/en/#exchange-information)  
<p>

*Get's information about the exchange including rate limits and information on the provided symbols based on account permissions*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetExchangeInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(AccountType[] permissions, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|permissions|account type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginSymbolAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-isolated-margin-symbol-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-isolated-margin-symbol-user_data)  
<p>

*Isolated margin symbol info*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetIsolatedMarginSymbolAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbolAsync(string symbol, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginSymbolsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-all-isolated-margin-symbol-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-all-isolated-margin-symbol-user_data)  
<p>

*Isolated margin symbol info*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetIsolatedMarginSymbolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-data](https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-data)  
<p>

*Get candlestick data for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetKlinesAsync(/* parameters */);  
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

## GetLeveragedTokenInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-blvt-info-market_data](https://binance-docs.github.io/apidocs/spot/en/#get-blvt-info-market_data)  
<p>

*Get blvt info*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetLeveragedTokenInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBlvtInfo>>> GetLeveragedTokenInfoAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeveragedTokensHistoricalKlinesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#historical-blvt-nav-kline-candlestick](https://binance-docs.github.io/apidocs/futures/en/#historical-blvt-nav-kline-candlestick)  
<p>

*Get's historical klines*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetLeveragedTokensHistoricalKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBlvtKline>>> GetLeveragedTokensHistoricalKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The token|
|interval|Kline interval|
|_[Optional]_ startTime|Filter by startTime|
|_[Optional]_ endTime|Filter by endTime|
|_[Optional]_ limit|Number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolConfigurationAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-pool-configure-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-pool-configure-user_data)  
<p>

*Get pool config*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetLiquidityPoolConfigurationAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBSwapPoolConfig>>> GetLiquidityPoolConfigurationAsync(int poolId, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|poolId|Id of the pool|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidityPoolsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#list-all-swap-pools-market_data](https://binance-docs.github.io/apidocs/spot/en/#list-all-swap-pools-market_data)  
<p>

*Get all swap pools*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetLiquidityPoolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBSwapPool>>> GetLiquidityPoolsAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginAssetAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-asset-market_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-asset-market_data)  
<p>

*Get a margin asset*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetMarginAssetAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginAsset>> GetMarginAssetAsync(string asset, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The symbol to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginAssetsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-all-margin-assets-market_data](https://binance-docs.github.io/apidocs/spot/en/#get-all-margin-assets-market_data)  
<p>

*Get all assets available for margin trading*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetMarginAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginPriceIndexAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-priceindex-market_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-priceindex-market_data)  
<p>

*Get margin price index*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetMarginPriceIndexAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginSymbolAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-cross-margin-pair-market_data](https://binance-docs.github.io/apidocs/spot/en/#query-cross-margin-pair-market_data)  
<p>

*Get a margin pair*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetMarginSymbolAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginPair>> GetMarginSymbolAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginSymbolsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-all-cross-margin-pairs-market_data](https://binance-docs.github.io/apidocs/spot/en/#get-all-cross-margin-pairs-market_data)  
<p>

*Get all asset pairs available for margin trading*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetMarginSymbolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://binance-docs.github.io/apidocs/spot/en/#order-book](https://binance-docs.github.io/apidocs/spot/en/#order-book)  
<p>

*Gets the order book for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the order book for|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPriceAsync  

[https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker)  
<p>

*Gets the price of a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetPriceAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker)  
<p>

*Gets the prices of symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetPricesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to get the price for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPricesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker](https://binance-docs.github.io/apidocs/spot/en/#symbol-price-ticker)  
<p>

*Get a list of the prices of all symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetPricesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetProductsAsync  

<p>

*Get general data for the products available on Binance*  
*NOTE: This is not an official endpoint and might be changed or removed at any point by Binance*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetProductsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentTradesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#recent-trades-list](https://binance-docs.github.io/apidocs/spot/en/#recent-trades-list)  
<p>

*Gets the recent trades for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
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

## GetRollingWindowTickerAsync  

[https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics)  
<p>

*Get data based on the last x time, specified as windowSize*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetRollingWindowTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get data for|
|_[Optional]_ windowSize|The window size to use|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRollingWindowTickersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#rolling-window-price-change-statistics)  
<p>

*Get data based on the last x time, specified as windowSize*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetRollingWindowTickersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to get data for|
|_[Optional]_ windowSize|The window size to use|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://binance-docs.github.io/apidocs/spot/en/#check-server-time](https://binance-docs.github.io/apidocs/spot/en/#check-server-time)  
<p>

*Requests the server for the local time*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingProductsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-list-user_data)  
<p>

*Get avaialble staking products list*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetStakingProductsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceStakingProduct>>> GetStakingProductsAsync(StakingProductType product, string? asset = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|_[Optional]_ asset|Filter for asset|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max items per page|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSystemStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#system-status-system](https://binance-docs.github.io/apidocs/spot/en/#system-status-system)  
<p>

*Gets the status of the Binance platform*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetSystemStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickerAsync  

[https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours for the provided symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours for the provided symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetTickersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbols|The symbols to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics](https://binance-docs.github.io/apidocs/spot/en/#24hr-ticker-price-change-statistics)  
<p>

*Get data regarding the last 24 hours for all symbols*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeFeeAsync  

[https://binance-docs.github.io/apidocs/spot/en/#trade-fee-user_data](https://binance-docs.github.io/apidocs/spot/en/#trade-fee-user_data)  
<p>

*Gets the trade fee for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetTradeFeeAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol to get withdrawal fee for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#old-trade-lookup-market_data](https://binance-docs.github.io/apidocs/spot/en/#old-trade-lookup-market_data)  
<p>

*Gets the historical  trades for a symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
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

## GetUiKlinesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#uiklines](https://binance-docs.github.io/apidocs/spot/en/#uiklines)  
<p>

*Get candlestick data for the provided symbol. Returns modified kline data, optimized for the presentation of candlestick charts*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.GetUiKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<IBinanceKline>>> GetUiKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, CancellationToken ct = default);  
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

## PingAsync  

[https://binance-docs.github.io/apidocs/spot/en/#test-connectivity](https://binance-docs.github.io/apidocs/spot/en/#test-connectivity)  
<p>

*Pings the Binance API*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.ExchangeData.PingAsync();  
```  

```csharp  
Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>
