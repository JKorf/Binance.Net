---
title: IBinanceRestClientUsdFuturesApiAccount
has_children: false
parent: IBinanceRestClientUsdFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > UsdFuturesApi > Account`  
*Binance USD-M futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## ChangeInitialLeverageAsync  

[https://binance-docs.github.io/apidocs/futures/en/#change-initial-leverage-trade](https://binance-docs.github.io/apidocs/futures/en/#change-initial-leverage-trade)  
<p>

*Requests to change the initial leverage of the given symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.ChangeInitialLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to change the initial leverage for|
|leverage|The amount of initial leverage to change to|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeMarginTypeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#change-margin-type-trade](https://binance-docs.github.io/apidocs/futures/en/#change-margin-type-trade)  
<p>

*Change the margin type for an open position*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.ChangeMarginTypeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to change the position type for|
|marginType|The type of margin to use|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountInfoAsync  

[https://binance-docs.github.io/apidocs/futures/en/#account-information-v2-user_data](https://binance-docs.github.io/apidocs/futures/en/#account-information-v2-user_data)  
<p>

*Gets account information, including balances*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBalancesAsync  

[https://binance-docs.github.io/apidocs/futures/en/#futures-account-balance-v2-user_data](https://binance-docs.github.io/apidocs/futures/en/#futures-account-balance-v2-user_data)  
<p>

*Gets account balances*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceUsdFuturesAccountBalance>>> GetBalancesAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBracketsAsync  

[https://binance-docs.github.io/apidocs/futures/en/#notional-and-leverage-brackets-user_data](https://binance-docs.github.io/apidocs/futures/en/#notional-and-leverage-brackets-user_data)  
<p>

*Gets Notional and Leverage Brackets.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetBracketsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesSymbolBracket>>> GetBracketsAsync(string? symbolOrPair = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbolOrPair|The symbol or pair to get the data for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDownloadIdForTransactionHistoryAsync  

<p>

*Get download id for downloading transaction history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetDownloadIdForTransactionHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTransactionHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|startTime|Start time of the data to download|
|endTime|End time of the data to download|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDownloadLinkForTransactionHistoryAsync  

<p>

*Get the download link for transaction history by download id*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetDownloadLinkForTransactionHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTransactionHistoryAsync(string downloadId, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|downloadId|The download id as requested by <see cref="GetDownloadIdForTransactionHistoryAsync" />|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIncomeHistoryAsync  

[https://binance-docs.github.io/apidocs/futures/en/#get-income-history-user_data](https://binance-docs.github.io/apidocs/futures/en/#get-income-history-user_data)  
<p>

*Gets the income history for the futures account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetIncomeHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesIncomeHistory>>> GetIncomeHistoryAsync(string? symbol = default, string? incomeType = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get income history from|
|_[Optional]_ incomeType|The income type filter to apply to the request|
|_[Optional]_ startTime|Time to start getting income history from|
|_[Optional]_ endTime|Time to stop getting income history from|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginChangeHistoryAsync  

[https://binance-docs.github.io/apidocs/futures/en/#get-position-margin-change-history-trade](https://binance-docs.github.io/apidocs/futures/en/#get-position-margin-change-history-trade)  
<p>

*Requests the margin change history for a specific symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetMarginChangeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesMarginChangeHistoryResult>>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to get margin history for|
|_[Optional]_ type|Filter the history by the direction of margin change|
|_[Optional]_ startTime|Margin changes newer than this date will be retrieved|
|_[Optional]_ endTime|Margin changes older than this date will be retrieved|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMultiAssetsModeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#get-current-multi-assets-mode-user_data](https://binance-docs.github.io/apidocs/futures/en/#get-current-multi-assets-mode-user_data)  
<p>

*Get user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetMultiAssetsModeAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPositionAdlQuantileEstimationAsync  

[https://binance-docs.github.io/apidocs/futures/en/#position-adl-quantile-estimation-user_data](https://binance-docs.github.io/apidocs/futures/en/#position-adl-quantile-estimation-user_data)  
<p>

*Get position ADL quantile estimations*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetPositionAdlQuantileEstimationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesQuantileEstimation>>> GetPositionAdlQuantileEstimationAsync(string? symbol = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Only get for this symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPositionInformationAsync  

[https://binance-docs.github.io/apidocs/futures/en/#position-information-v2-user_data](https://binance-docs.github.io/apidocs/futures/en/#position-information-v2-user_data)  
<p>

*Gets account information*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetPositionInformationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePositionDetailsUsdt>>> GetPositionInformationAsync(string? symbol = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPositionModeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#get-current-position-mode-user_data](https://binance-docs.github.io/apidocs/futures/en/#get-current-position-mode-user_data)  
<p>

*Get user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetPositionModeAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradingStatusAsync  

[https://binance-docs.github.io/apidocs/futures/en/#user-api-trading-quantitative-rules-indicators-user_data](https://binance-docs.github.io/apidocs/futures/en/#user-api-trading-quantitative-rules-indicators-user_data)  
<p>

*Gets the current status of the trading rules for the account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetTradingStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserCommissionRateAsync  

[https://binance-docs.github.io/apidocs/futures/en/#user-commission-rate-user_data](https://binance-docs.github.io/apidocs/futures/en/#user-commission-rate-user_data)  
<p>

*Gets account commission rates*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.GetUserCommissionRateAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## KeepAliveUserStreamAsync  

[https://binance-docs.github.io/apidocs/futures/en/#keepalive-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/futures/en/#keepalive-user-data-stream-user_stream)  
<p>

*Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.KeepAliveUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyPositionMarginAsync  

[https://binance-docs.github.io/apidocs/futures/en/#modify-isolated-position-margin-trade](https://binance-docs.github.io/apidocs/futures/en/#modify-isolated-position-margin-trade)  
<p>

*Change the margin on an open position*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.ModifyPositionMarginAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal amount, FuturesMarginChangeDirectionType type, PositionSide? positionSide = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to adjust the position margin for|
|amount|The amount of margin to be used|
|type|Whether to reduce or add margin to the position|
|_[Optional]_ positionSide|Default BOTH for One-way Mode ; LONG or SHORT for Hedge Mode. It must be sent with Hedge Mode.|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ModifyPositionModeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#change-position-mode-trade](https://binance-docs.github.io/apidocs/futures/en/#change-position-mode-trade)  
<p>

*Change user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.ModifyPositionModeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|dualPositionSide|User position mode|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SetMultiAssetsModeAsync  

[https://binance-docs.github.io/apidocs/futures/en/#change-multi-assets-mode-trade](https://binance-docs.github.io/apidocs/futures/en/#change-multi-assets-mode-trade)  
<p>

*Set user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.SetMultiAssetsModeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|enabled|Enabled or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StartUserStreamAsync  

[https://binance-docs.github.io/apidocs/futures/en/#start-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/futures/en/#start-user-data-stream-user_stream)  
<p>

*Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.StartUserStreamAsync();  
```  

```csharp  
Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StopUserStreamAsync  

[https://binance-docs.github.io/apidocs/futures/en/#close-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/futures/en/#close-user-data-stream-user_stream)  
<p>

*Stop the user stream, no updates will be send anymore*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.UsdFuturesApi.Account.StopUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to stop|
|_[Optional]_ ct|Cancellation token|

</p>
