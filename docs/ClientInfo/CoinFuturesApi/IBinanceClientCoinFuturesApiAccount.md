---
title: IBinanceClientCoinFuturesApiAccount
has_children: false
parent: IBinanceClientCoinFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > CoinFuturesApi > Account`  
*Binance COIN-M futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## ChangeInitialLeverageAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#change-initial-leverage-trade](https://binance-docs.github.io/apidocs/delivery/en/#change-initial-leverage-trade)  
<p>

*Requests to change the initial leverage of the given symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.ChangeInitialLeverageAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#change-margin-type-trade](https://binance-docs.github.io/apidocs/delivery/en/#change-margin-type-trade)  
<p>

*Change the margin type for an open position*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.ChangeMarginTypeAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#account-information-user_data](https://binance-docs.github.io/apidocs/delivery/en/#account-information-user_data)  
<p>

*Gets account information, including balances*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceFuturesCoinAccountInfo>> GetAccountInfoAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBalancesAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#futures-account-balance-user_data](https://binance-docs.github.io/apidocs/delivery/en/#futures-account-balance-user_data)  
<p>

*Gets account balances*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetBalancesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceCoinFuturesAccountBalance>>> GetBalancesAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBracketsAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#notional-bracket-for-pair-user_data](https://binance-docs.github.io/apidocs/delivery/en/#notional-bracket-for-pair-user_data)  
<p>

*Gets Notional and Leverage Brackets.*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetBracketsAsync();  
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

## GetIncomeHistoryAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#get-income-history-user_data](https://binance-docs.github.io/apidocs/delivery/en/#get-income-history-user_data)  
<p>

*Gets the income history for the futures account*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetIncomeHistoryAsync();  
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

[https://binance-docs.github.io/apidocs/delivery/en/#get-position-margin-change-history-trade](https://binance-docs.github.io/apidocs/delivery/en/#get-position-margin-change-history-trade)  
<p>

*Requests the margin change history for a specific symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetMarginChangeHistoryAsync(/* parameters */);  
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

## GetPositionAdlQuantileEstimationAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#position-adl-quantile-estimation-user_data](https://binance-docs.github.io/apidocs/delivery/en/#position-adl-quantile-estimation-user_data)  
<p>

*Get position ADL quantile estimations*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetPositionAdlQuantileEstimationAsync();  
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

[https://binance-docs.github.io/apidocs/delivery/en/#position-information-user_data](https://binance-docs.github.io/apidocs/delivery/en/#position-information-user_data)  
<p>

*Gets account position information*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetPositionInformationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePositionDetailsCoin>>> GetPositionInformationAsync(string? marginAsset = default, string? pair = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ marginAsset|Filter by margin asset|
|_[Optional]_ pair|Filter by pair|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPositionModeAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#get-current-position-mode-user_data](https://binance-docs.github.io/apidocs/delivery/en/#get-current-position-mode-user_data)  
<p>

*Get user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetPositionModeAsync();  
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

## GetUserCommissionRateAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#user-commission-rate-user_data](https://binance-docs.github.io/apidocs/delivery/en/#user-commission-rate-user_data)  
<p>

*Gets account commission rates*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.GetUserCommissionRateAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#keepalive-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/delivery/en/#keepalive-user-data-stream-user_stream)  
<p>

*Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.KeepAliveUserStreamAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#modify-isolated-position-margin-trade](https://binance-docs.github.io/apidocs/delivery/en/#modify-isolated-position-margin-trade)  
<p>

*Change the margin on an open position*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.ModifyPositionMarginAsync(/* parameters */);  
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

[https://binance-docs.github.io/apidocs/delivery/en/#change-position-mode-trade](https://binance-docs.github.io/apidocs/delivery/en/#change-position-mode-trade)  
<p>

*Change user's position mode (Hedge Mode or One-way Mode ) on EVERY symbol*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.ModifyPositionModeAsync(/* parameters */);  
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

## StartUserStreamAsync  

[https://binance-docs.github.io/apidocs/delivery/en/#start-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/delivery/en/#start-user-data-stream-user_stream)  
<p>

*Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.StartUserStreamAsync();  
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

[https://binance-docs.github.io/apidocs/delivery/en/#close-user-data-stream-user_stream](https://binance-docs.github.io/apidocs/delivery/en/#close-user-data-stream-user_stream)  
<p>

*Stop the user stream, no updates will be send anymore*  

```csharp  
var client = new BinanceClient();  
var result = await client.CoinFuturesApi.Account.StopUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to stop|
|_[Optional]_ ct|Cancellation token|

</p>
