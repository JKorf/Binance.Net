---
title: IBinanceClientSpotApiAccount
has_children: false
parent: IBinanceClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi > Account`  
*Binance Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings*
  

***

## CloseIsolatedMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin)  
<p>

*Close the user stream for margin account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.CloseIsolatedMarginUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> CloseIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The isolated symbol|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CrossMarginTransferAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-trade)  
<p>

*Execute transfer between spot account and margin account.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.CrossMarginTransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> CrossMarginTransferAsync(string asset, decimal quantity, TransferDirectionType type, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset being transferred, e.g., BTC|
|quantity|The quantity to be transferred|
|type|TransferDirection (MainToMargin/MarginToMain)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DisableFastWithdrawSwitchAsync  

[https://binance-docs.github.io/apidocs/spot/en/#disable-fast-withdraw-switch-user_data](https://binance-docs.github.io/apidocs/spot/en/#disable-fast-withdraw-switch-user_data)  
<p>

*This request will disable fastwithdraw switch under your account.*  
*You need to enable "trade" option for the api key which requests this endpoint.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.DisableFastWithdrawSwitchAsync();  
```  

```csharp  
Task<WebCallResult<object>> DisableFastWithdrawSwitchAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DisableIsolatedMarginAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#disable-isolated-margin-account-trade](https://binance-docs.github.io/apidocs/spot/en/#disable-isolated-margin-account-trade)  
<p>

*Disabled an isolated margin account info*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.DisableIsolatedMarginAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<CreateIsolatedMarginAccountResult>> DisableIsolatedMarginAccountAsync(string symbol, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to enable isoldated margin account for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DustTransferAsync  

[https://binance-docs.github.io/apidocs/spot/en/#dust-transfer-user_data](https://binance-docs.github.io/apidocs/spot/en/#dust-transfer-user_data)  
<p>

*Converts dust (small amounts of) assets to BNB*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.DustTransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|assets|The assets to convert to BNB|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableFastWithdrawSwitchAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-fast-withdraw-switch-user_data](https://binance-docs.github.io/apidocs/spot/en/#enable-fast-withdraw-switch-user_data)  
<p>

*This request will enable fastwithdraw switch under your account.*  
*You need to enable "trade" option for the api key which requests this endpoint.*  
**  
*When Fast Withdraw Switch is on, transferring funds to a Binance account will be done instantly.*  
*There is no on-chain transaction, no transaction ID and no withdrawal fee.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.EnableFastWithdrawSwitchAsync();  
```  

```csharp  
Task<WebCallResult<object>> EnableFastWithdrawSwitchAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableIsolatedMarginAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-isolated-margin-account-trade](https://binance-docs.github.io/apidocs/spot/en/#enable-isolated-margin-account-trade)  
<p>

*Enable an isolated margin account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.EnableIsolatedMarginAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<CreateIsolatedMarginAccountResult>> EnableIsolatedMarginAccountAsync(string symbol, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to enable isoldated margin account for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#account-information-user_data](https://binance-docs.github.io/apidocs/spot/en/#account-information-user_data)  
<p>

*Gets account information, including balances*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAccountStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#account-status-user_data](https://binance-docs.github.io/apidocs/spot/en/#account-status-user_data)  
<p>

*Gets the status of the account associated with the api key/secret*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetAccountStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAPIKeyPermissionsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-api-key-permission-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-api-key-permission-user_data)  
<p>

*Get permission info for the current API key*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetAPIKeyPermissionsAsync();  
```  

```csharp  
Task<WebCallResult<BinanceAPIKeyPermissions>> GetAPIKeyPermissionsAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetDividendRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#asset-dividend-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#asset-dividend-record-user_data)  
<p>

*Get asset dividend records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetAssetDividendRecordsAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceDividendRecord>>> GetAssetDividendRecordsAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ startTime|Filter by start time from|
|_[Optional]_ endTime|Filter by end time till|
|_[Optional]_ limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetsForDustTransferAsync  

<p>

*Get assets that can be converted to BNB*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetAssetsForDustTransferAsync();  
```  

```csharp  
Task<WebCallResult<BinanceElligableDusts>> GetAssetsForDustTransferAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBnbBurnStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-bnb-burn-status-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-bnb-burn-status-user_data)  
<p>

*Gets the status of the BNB burn switch for spot trading and margin interest*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetBnbBurnStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBnbBurnStatus>> GetBnbBurnStatusAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginTransferHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-all-open-orders-on-a-symbol-trade](https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-all-open-orders-on-a-symbol-trade)  
<p>

*Get history of transfers*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetCrossMarginTransferHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetCrossMarginTransferHistoryAsync(TransferDirection direction, int? page = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|direction|The direction of the the transfers to retrieve|
|_[Optional]_ page|Results page|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|_[Optional]_ limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDailyFutureAccountSnapshotAsync  

[https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data](https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data)  
<p>

*Get a daily account snapshot (assets and positions)*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDailyFutureAccountSnapshotAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFuturesAccountSnapshot>>> GetDailyFutureAccountSnapshotAsync(DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|The start time|
|_[Optional]_ endTime|The end time|
|_[Optional]_ limit|The amount of days to retrieve|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDailyMarginAccountSnapshotAsync  

[https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data](https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data)  
<p>

*Get a daily account snapshot (assets)*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDailyMarginAccountSnapshotAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMarginAccountSnapshot>>> GetDailyMarginAccountSnapshotAsync(DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|The start time|
|_[Optional]_ endTime|The end time|
|_[Optional]_ limit|The amount of days to retrieve|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDailySpotAccountSnapshotAsync  

[https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data](https://binance-docs.github.io/apidocs/spot/en/#daily-account-snapshot-user_data)  
<p>

*Get a daily account snapshot (balances)*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDailySpotAccountSnapshotAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSpotAccountSnapshot>>> GetDailySpotAccountSnapshotAsync(DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|The start time|
|_[Optional]_ endTime|The end time|
|_[Optional]_ limit|The amount of days to retrieve|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositAddressAsync  

[https://binance-docs.github.io/apidocs/spot/en/#deposit-address-supporting-network-user_data](https://binance-docs.github.io/apidocs/spot/en/#deposit-address-supporting-network-user_data)  
<p>

*Gets the deposit address for an asset*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDepositAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, string? network = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset to get address for|
|_[Optional]_ network|Network|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDepositHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#deposit-history-supporting-network-user_data](https://binance-docs.github.io/apidocs/spot/en/#deposit-history-supporting-network-user_data)  
<p>

*Gets the deposit history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDepositHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceDeposit>>> GetDepositHistoryAsync(string? asset = default, DepositStatus? status = default, DateTime? startTime = default, DateTime? endTime = default, int? offset = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ status|Filter by status|
|_[Optional]_ startTime|Filter start time from|
|_[Optional]_ endTime|Filter end time till|
|_[Optional]_ offset|Offset the results|
|_[Optional]_ limit|Amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetDustLogAsync  

[https://binance-docs.github.io/apidocs/spot/en/#dustlog-user_data](https://binance-docs.github.io/apidocs/spot/en/#dustlog-user_data)  
<p>

*Gets the history of dust conversions*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetDustLogAsync();  
```  

```csharp  
Task<WebCallResult<BinanceDustLogList>> GetDustLogAsync(DateTime? startTime = default, DateTime? endTime = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|The start time|
|_[Optional]_ endTime|The end time|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEnabledIsolatedMarginAccountLimitAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-enabled-isolated-margin-account-limit-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-enabled-isolated-margin-account-limit-user_data)  
<p>

*Get max number of enabled isolated margin accounts*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetEnabledIsolatedMarginAccountLimitAsync();  
```  

```csharp  
Task<WebCallResult<IsolatedMarginAccountLimit>> GetEnabledIsolatedMarginAccountLimitAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFiatDepositWithdrawHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#fiat-endpoints](https://binance-docs.github.io/apidocs/spot/en/#fiat-endpoints)  
<p>

*Get Fiat deposit/withdrawal history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetFiatDepositWithdrawHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFiatWithdrawDeposit>>> GetFiatDepositWithdrawHistoryAsync(TransactionType side, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|side|Filter by side|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Return a specific page|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFiatPaymentHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-fiat-payments-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-fiat-payments-history-user_data)  
<p>

*Get Fiat payment history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetFiatPaymentHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFiatPayment>>> GetFiatPaymentHistoryAsync(OrderSide side, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|side|Filter by side|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Return a specific page|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingWalletAsync  

[https://binance-docs.github.io/apidocs/spot/en/#funding-wallet-user_data](https://binance-docs.github.io/apidocs/spot/en/#funding-wallet-user_data)  
<p>

*Get funding wallet assets*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetFundingWalletAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFundingAsset>>> GetFundingWalletAsync(string? asset = default, bool? needBtcValuation = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ needBtcValuation|Return BTC valuation|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-isolated-margin-account-info-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-isolated-margin-account-info-user_data)  
<p>

*Isolated margin account info*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginAccountAsync();  
```  

```csharp  
Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAccountTransferHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-isolated-margin-transfer-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-isolated-margin-transfer-history-user_data)  
<p>

*Get history of transfer to and from the isolated margin account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetIsolatedMarginAccountTransferHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceIsolatedMarginTransfer>>> GetIsolatedMarginAccountTransferHistoryAsync(string symbol, string? asset = default, IsolatedMarginTransferDirection? from = default, IsolatedMarginTransferDirection? to = default, DateTime? startTime = default, DateTime? endTime = default, int? current, int? limit, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ from|Filter by direction|
|_[Optional]_ to|Filter by direction|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|current|Current page|
|limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginAccountInfoAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-cross-margin-account-details-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-cross-margin-account-details-user_data)  
<p>

*Query margin account details*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginForcedLiquidationHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-force-liquidation-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-force-liquidation-record-user_data)  
<p>

*Get history of forced liquidations*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginForcedLiquidationHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetMarginForcedLiquidationHistoryAsync(int? page = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, string? isolatedSymbol = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ page|Results page|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|_[Optional]_ limit|Limit of the amount of results|
|_[Optional]_ isolatedSymbol|Filter by isolated symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginInterestHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-interest-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-interest-history-user_data)  
<p>

*Get history of interest*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginInterestHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetMarginInterestHistoryAsync(string? asset = default, int? page = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, string? isolatedSymbol = default, bool? archived = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ page|Results page|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|_[Optional]_ limit|Limit of the amount of results|
|_[Optional]_ isolatedSymbol|Filter by isolated symbol|
|_[Optional]_ archived|Set to true for archived data from 6 months ago|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginInterestRateHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-margin-interest-rate-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-margin-interest-rate-history-user_data)  
<p>

*Get history of interest rate*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginInterestRateHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceInterestRateHistory>>> GetMarginInterestRateHistoryAsync(string asset, string? vipLevel = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Filter by asset|
|_[Optional]_ vipLevel|Vip level|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|_[Optional]_ limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginLoansAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-loan-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-loan-record-user_data)  
<p>

*Query loan records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginLoansAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetMarginLoansAsync(string asset, long? transactionId = default, DateTime? startTime = default, DateTime? endTime = default, int? current, int? limit, string? isolatedSymbol = default, bool? archived = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The records asset|
|_[Optional]_ transactionId|The id of loan transaction|
|_[Optional]_ startTime|Time to start getting records from|
|_[Optional]_ endTime|Time to stop getting records to|
|current|Number of page records|
|limit|The records count size need show|
|_[Optional]_ isolatedSymbol|Filter by isolated symbol|
|_[Optional]_ archived|Set to true for archived data from 6 months ago|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginMaxBorrowAmountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-max-borrow-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-max-borrow-user_data)  
<p>

*Query max borrow quantity*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginMaxBorrowAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMarginAmount>> GetMarginMaxBorrowAmountAsync(string asset, string? isolatedSymbol = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The records asset|
|_[Optional]_ isolatedSymbol|The isolated symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginMaxTransferAmountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-max-transfer-out-amount-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-max-transfer-out-amount-user_data)  
<p>

*Query max transfer-out quantity*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginMaxTransferAmountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<decimal>> GetMarginMaxTransferAmountAsync(string asset, string? isolatedSymbol = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The records asset|
|_[Optional]_ isolatedSymbol|The isolated symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarginRepaysAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-repay-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-repay-record-user_data)  
<p>

*Query repay records*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetMarginRepaysAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetMarginRepaysAsync(string asset, long? transactionId = default, DateTime? startTime = default, DateTime? endTime = default, int? current = default, int? size = default, string? isolatedSymbol = default, bool? archived = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The records asset|
|_[Optional]_ transactionId|The id of repay transaction|
|_[Optional]_ startTime|Time to start getting records from|
|_[Optional]_ endTime|Time to stop getting records to|
|_[Optional]_ current|Filter by number|
|_[Optional]_ size|The records count size need show|
|_[Optional]_ isolatedSymbol|Filter by isolated symbol|
|_[Optional]_ archived|Set to true for archived data from 6 months ago|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderRateLimitStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-current-order-count-usage-trade](https://binance-docs.github.io/apidocs/spot/en/#query-current-order-count-usage-trade)  
<p>

*Get the current used order rate limits*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetOrderRateLimitStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceOrderRateLimit>>> GetOrderRateLimitStatusAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRebateHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-spot-rebate-history-records-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-spot-rebate-history-records-user_data)  
<p>

*Get rebate history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetRebateHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceRebateWrapper>> GetRebateHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Results per page|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradingStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#account-api-trading-status-user_data](https://binance-docs.github.io/apidocs/spot/en/#account-api-trading-status-user_data)  
<p>

*Gets the trading status for the current account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetTradingStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTransfersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-user-universal-transfer-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#query-user-universal-transfer-history-user_data)  
<p>

*Get transfer history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetTransfersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceTransfer>>> GetTransfersAsync(UniversalTransferType type, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|type|The type of transfer|
|_[Optional]_ startTime|Filter by startTime|
|_[Optional]_ endTime|Filter by endTime|
|_[Optional]_ page|The page|
|_[Optional]_ pageSize|Results per page|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserAssetsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#all-coins-39-information-user_data](https://binance-docs.github.io/apidocs/spot/en/#all-coins-39-information-user_data)  
<p>

*Gets information of assets for a user*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetUserAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceUserAsset>>> GetUserAssetsAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetWithdrawalHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#withdraw-history-supporting-network-user_data](https://binance-docs.github.io/apidocs/spot/en/#withdraw-history-supporting-network-user_data)  
<p>

*Gets the withdrawal history*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.GetWithdrawalHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = default, string? withdrawOrderId = default, WithdrawalStatus? status = default, DateTime? startTime = default, DateTime? endTime = default, int? receiveWindow = default, int? limit = default, int? offset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ withdrawOrderId|Filter by withdraw order id|
|_[Optional]_ status|Filter by status|
|_[Optional]_ startTime|Filter start time from|
|_[Optional]_ endTime|Filter end time till|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ limit|Add limit. Default: 1000, Max: 1000|
|_[Optional]_ offset|Add offset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## IsolatedMarginAccountTransferAsync  

[https://binance-docs.github.io/apidocs/spot/en/#isolated-margin-account-transfer-margin](https://binance-docs.github.io/apidocs/spot/en/#isolated-margin-account-transfer-margin)  
<p>

*Transfer from or to isolated margin account*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.IsolatedMarginAccountTransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> IsolatedMarginAccountTransferAsync(string asset, string symbol, IsolatedMarginTransferDirection from, IsolatedMarginTransferDirection to, decimal quantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset|
|symbol|Isolated symbol|
|from|From|
|to|To|
|quantity|Quantity to transfer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## KeepAliveIsolatedMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin)  
<p>

*Sends a keep alive for the current user stream for margin account listen key to keep the stream from closing.*  
*Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.KeepAliveIsolatedMarginUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> KeepAliveIsolatedMarginUserStreamAsync(string symbol, string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The isolated symbol|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## KeepAliveMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin)  
<p>

*Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.KeepAliveMarginUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> KeepAliveMarginUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## KeepAliveUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot](https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot)  
<p>

*Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.KeepAliveUserStreamAsync(/* parameters */);  
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

## MarginBorrowAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-borrow-margin](https://binance-docs.github.io/apidocs/spot/en/#margin-account-borrow-margin)  
<p>

*Borrow. Apply for a loan.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.MarginBorrowAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> MarginBorrowAsync(string asset, decimal quantity, bool? isIsolated = default, string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset being borrow, e.g., BTC|
|quantity|The quantity to be borrow|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ symbol|The isolated symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## MarginRepayAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-account-repay-margin](https://binance-docs.github.io/apidocs/spot/en/#margin-account-repay-margin)  
<p>

*Repay loan for margin account.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.MarginRepayAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> MarginRepayAsync(string asset, decimal quantity, bool? isIsolated = default, string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset being repay, e.g., BTC|
|quantity|The quantity to be borrow|
|_[Optional]_ isIsolated|For isolated margin or not|
|_[Optional]_ symbol|The isolated symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SetBnbBurnStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#toggle-bnb-burn-on-spot-trade-and-margin-interest-user_data](https://binance-docs.github.io/apidocs/spot/en/#toggle-bnb-burn-on-spot-trade-and-margin-interest-user_data)  
<p>

*Sets the status of the BNB burn switch for spot trading and margin interest*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.SetBnbBurnStatusAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBnbBurnStatus>> SetBnbBurnStatusAsync(bool? spotTrading = default, bool? marginInterest = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ spotTrading|If BNB burning should be enabled for spot trading|
|_[Optional]_ marginInterest|If BNB burning should be enabled for margin interest|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StartIsolatedMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-isolated-margin)  
<p>

*Starts a user stream  for margin account by requesting a listen key.*  
*This listen key can be used in subsequent requests to  BinanceSocketClient.Spot.SubscribeToUserDataUpdates*  
*The stream will close after 60 minutes unless a keep alive is send.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.StartIsolatedMarginUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<string>> StartIsolatedMarginUserStreamAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The isolated symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StartMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin)  
<p>

*Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to BinanceSocketClient.Futures.SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.StartMarginUserStreamAsync();  
```  

```csharp  
Task<WebCallResult<string>> StartMarginUserStreamAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StartUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot](https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot)  
<p>

*Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to BinanceSocketClient.Futures.SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.StartUserStreamAsync();  
```  

```csharp  
Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StopMarginUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin](https://binance-docs.github.io/apidocs/spot/en/#listen-key-margin)  
<p>

*Stops the current user stream*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.StopMarginUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> StopMarginUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## StopUserStreamAsync  

[https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot](https://binance-docs.github.io/apidocs/spot/en/#listen-key-spot)  
<p>

*Stops the current user stream*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.StopUserStreamAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> StopUserStreamAsync(string listenKey, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|listenKey|The listen key to keep alive|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferAsync  

[https://binance-docs.github.io/apidocs/spot/en/#user-universal-transfer-user_data](https://binance-docs.github.io/apidocs/spot/en/#user-universal-transfer-user_data)  
<p>

*Transfers between accounts*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.TransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> TransferAsync(UniversalTransferType type, string asset, decimal quantity, string? fromSymbol = default, string? toSymbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|type|The type of transfer|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|_[Optional]_ fromSymbol|From symbol when transfering from/to isolated margin|
|_[Optional]_ toSymbol|To symbol when transfering from/to isolated margin|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## WithdrawAsync  

[https://binance-docs.github.io/apidocs/spot/en/#withdraw-user_data](https://binance-docs.github.io/apidocs/spot/en/#withdraw-user_data)  
<p>

*Withdraw assets from Binance to an address*  

```csharp  
var client = new BinanceClient();  
var result = await client.SpotApi.Account.WithdrawAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal quantity, string? withdrawOrderId = default, string? network = default, string? addressTag = default, string? name = default, bool? transactionFeeFlag = default, WalletType? walletType = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to withdraw|
|address|The address to send the funds to|
|quantity|The quantity to withdraw|
|_[Optional]_ withdrawOrderId|Custom client order id|
|_[Optional]_ network|The network to use|
|_[Optional]_ addressTag|Secondary address identifier for assets like XRP,XMR etc.|
|_[Optional]_ name|Description of the address|
|_[Optional]_ transactionFeeFlag|When making internal transfer, true for returning the fee to the destination account; false for returning the fee back to the departure account. Default false.|
|_[Optional]_ walletType|The wallet type for withdraw|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
