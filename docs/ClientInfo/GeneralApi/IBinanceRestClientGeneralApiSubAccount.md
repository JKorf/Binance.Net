---
title: IBinanceRestClientGeneralApiSubAccount
has_children: false
parent: IBinanceClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > GeneralApi > IBinanceRestClientSubAccount`  
*Binance Spot Subaccount endpoints*
  

***

## CreateVirtualSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#create-a-virtual-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#create-a-virtual-sub-account-for-master-account)  
<p>

*Create a virtual sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.CreateVirtualSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountEmail>> CreateVirtualSubAccountAsync(string subAccountString, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountString|String based with which a subaccount email will be generated. Should not contain special characters|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableBlvtForSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-leverage-token-for-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#enable-leverage-token-for-sub-account-for-master-account)  
<p>

*Enable or disable blvt*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.EnableBlvtForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountBlvt>> EnableBlvtForSubAccountAsync(string email, bool enable, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|Email of the sub account|
|enable|Enable or disable (only true for now)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableFuturesForSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-futures-for-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#enable-futures-for-sub-account-for-master-account)  
<p>

*Enables futures for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.EnableFuturesForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountFuturesEnabled>> EnableFuturesForSubAccountAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The sub account email to enable futures for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableMarginForSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-margin-for-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#enable-margin-for-sub-account-for-master-account)  
<p>

*Enables margin for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.EnableMarginForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountMarginEnabled>> EnableMarginForSubAccountAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The email of the account to enable margin for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIpRestrictionForSubAccountApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-ip-restriction-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-ip-restriction-for-a-sub-account-api-key-for-master-account)  
<p>

*Get the ip restriction for a sub-account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The sub account email|
|apiKey|The sub account api key|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountAssetsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-assets-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-assets-for-master-account)  
<p>

*Gets list of balances for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountAssetsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBalance>>> GetSubAccountAssetsAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|For which account to get the assets|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountBtcValuesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-assets-summary-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-assets-summary-for-master-account)  
<p>

*Get BTC valued asset summary of subaccounts.*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountBtcValuesAsync();  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountSpotAssetsSummary>> GetSubAccountBtcValuesAsync(string? email = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ email|Email of the sub account|
|_[Optional]_ page|The page|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountDepositAddressAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-address-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-address-for-master-account)  
<p>

*Gets the deposit address for an asset to a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountDepositAddressAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountDepositAddress>> GetSubAccountDepositAddressAsync(string email, string asset, string? network = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The email of the account to deposit to|
|asset|The asset of the deposit|
|_[Optional]_ network|The coin network|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountDepositHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-history-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-deposit-history-for-master-account)  
<p>

*Gets the deposit history for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountDepositHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountDeposit>>> GetSubAccountDepositHistoryAsync(string email, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? offset = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The email of the account to get history for|
|_[Optional]_ asset|Filter for an asset|
|_[Optional]_ startTime|Only return deposits placed later this|
|_[Optional]_ endTime|Only return deposits placed before this|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ offset|Offset results by this|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountFuturesDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-futures-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-futures-account-for-master-account)  
<p>

*Gets futures details for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountFuturesDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountFuturesDetails>> GetSubAccountFuturesDetailsAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The email of the account to get future details for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountFuturesDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-futures-account-v2-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-futures-account-v2-for-master-account)  
<p>

*Gets futures details for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountFuturesDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountFuturesDetailsV2>> GetSubAccountFuturesDetailsAsync(FuturesAccountType futuresType, string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|futuresType|The account type to get future details for|
|email|The email of the account to get future details for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountMarginDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-margin-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-margin-account-for-master-account)  
<p>

*Gets margin details for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountMarginDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountMarginDetails>> GetSubAccountMarginDetailsAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The email of the account to get margin details for|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-list-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-list-for-master-account)  
<p>

*Gets a list of sub accounts associated with this master account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccount>>> GetSubAccountsAsync(string? email = default, int? page = default, int? limit = default, int? receiveWindow = default, bool? isFreeze = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ email|Filter the list by email|
|_[Optional]_ page|The page of the results|
|_[Optional]_ limit|The max amount of results to return|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ isFreeze|Is freezed|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsFuturesPositionRiskAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-futures-position-risk-of-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-futures-position-risk-of-sub-account-for-master-account)  
<p>

*Gets futures position risk for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountsFuturesPositionRiskAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountFuturesPositionRisk>>> GetSubAccountsFuturesPositionRiskAsync(string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|Email of the sub account|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsFuturesPositionRiskAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-futures-position-risk-of-sub-account-v2-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-futures-position-risk-of-sub-account-v2-for-master-account)  
<p>

*Gets futures position risk for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountsFuturesPositionRiskAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountFuturesPositionRiskV2>> GetSubAccountsFuturesPositionRiskAsync(FuturesAccountType futuresType, string email, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|futuresType|The account type to get future details for|
|email|Email of the sub account|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsFuturesSummaryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-futures-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-futures-account-for-master-account)  
<p>

*Gets futures summary for sub accounts*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountsFuturesSummaryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountsFuturesSummary>> GetSubAccountsFuturesSummaryAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsMarginSummaryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-margin-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-margin-account-for-master-account)  
<p>

*Gets margin summary for sub accounts*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountsMarginSummaryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountsMarginSummary>> GetSubAccountsMarginSummaryAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountStatusAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-39-s-status-on-margin-futures-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-sub-account-39-s-status-on-margin-futures-for-master-account)  
<p>

*Get Sub-account's Status on Margin/Futures(For Master Account)*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountStatus>>> GetSubAccountStatusAsync(string? email = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ email|Filter the list by email|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountTransferHistoryForMasterAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-asset-transfer-history-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#query-sub-account-spot-asset-transfer-history-for-master-account)  
<p>

*Gets the transfer history of a sub account (from the master account)*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountTransferHistoryForMasterAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountTransfer>>> GetSubAccountTransferHistoryForMasterAsync(string? fromEmail = default, string? toEmail = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromEmail|Filter the history by from email|
|_[Optional]_ toEmail|Filter the history by to email|
|_[Optional]_ startTime|Filter the history by startTime|
|_[Optional]_ endTime|Filter the history by endTime|
|_[Optional]_ page|The page of the results|
|_[Optional]_ limit|The max amount of results to return|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountTransferHistoryForSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#sub-account-transfer-history-for-sub-account](https://binance-docs.github.io/apidocs/spot/en/#sub-account-transfer-history-for-sub-account)  
<p>

*Gets the transfer history of a sub account (from the sub account)*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetSubAccountTransferHistoryForSubAccountAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountTransferSubAccount>>> GetSubAccountTransferHistoryForSubAccountAsync(string? asset = default, SubAccountTransferSubAccountType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|The asset|
|_[Optional]_ type|Filter by type of transfer|
|_[Optional]_ startTime|Only return transfers later than this|
|_[Optional]_ endTime|Only return transfers before this|
|_[Optional]_ limit|Max number of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUniversalTransferHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#query-universal-transfer-history-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#query-universal-transfer-history-for-master-account)  
<p>

*Gets a list of universal transfers*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.GetUniversalTransferHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSubAccountUniversalTransferTransaction>>> GetUniversalTransferHistoryAsync(string? fromEmail = default, string? toEmail = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromEmail|Filter the list by from email (fromEmail and toEmail cannot be present at same time)|
|_[Optional]_ toEmail|Filter the list by to email (fromEmail and toEmail cannot be present at same time)|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|The page of the results|
|_[Optional]_ limit|The max amount of results to return (Default 500, max 500)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RemoveIpRestrictionForSubAccountApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#delete-ip-list-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#delete-ip-list-for-a-sub-account-api-key-for-master-account)  
<p>

*Remove the ip restriction for a sub-account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.RemoveIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> RemoveIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, IEnumerable<string>? ipAddresses = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The sub account email|
|apiKey|The sub account api key|
|_[Optional]_ ipAddresses|Addresses to remove from whitelist|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#sub-account-futures-asset-transfer-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#sub-account-futures-asset-transfer-for-master-account)  
<p>

*Transfers an asset form/to a sub account. If fromEmail or toEmail is not send it is interpreted as from/to the master account. Transfer between futures accounts is not supported*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.TransferSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> TransferSubAccountAsync(TransferAccountType fromAccountType, TransferAccountType toAccountType, string asset, decimal quantity, string? fromEmail = default, string? toEmail = default, string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|fromAccountType|Account type to transfer from|
|toAccountType|Account type to transfer to|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|_[Optional]_ fromEmail|From which account to transfer|
|_[Optional]_ toEmail|To which account to transfer|
|_[Optional]_ symbol|The sybol to transfer, only for isolated margin|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSubAccountFuturesAsync  

[https://binance-docs.github.io/apidocs/spot/en/#futures-transfer-for-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#futures-transfer-for-sub-account-for-master-account)  
<p>

*Transfers from or to a futures sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.TransferSubAccountFuturesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountFuturesAsync(string email, string asset, decimal quantity, SubAccountFuturesTransferType type, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|Email of the sub account|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|type|The type of the transfer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSubAccountMarginAsync  

[https://binance-docs.github.io/apidocs/spot/en/#margin-transfer-for-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#margin-transfer-for-sub-account-for-master-account)  
<p>

*Transfers from or to a margin sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.TransferSubAccountMarginAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountMarginAsync(string email, string asset, decimal quantity, SubAccountMarginTransferType type, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|Email of the sub account|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|type|The type of the transfer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSubAccountToMasterAsync  

[https://binance-docs.github.io/apidocs/spot/en/#transfer-to-master-for-sub-account](https://binance-docs.github.io/apidocs/spot/en/#transfer-to-master-for-sub-account)  
<p>

*Transfers to master account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.TransferSubAccountToMasterAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToMasterAsync(string asset, decimal quantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferSubAccountToSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#transfer-to-sub-account-of-same-master-for-sub-account](https://binance-docs.github.io/apidocs/spot/en/#transfer-to-sub-account-of-same-master-for-sub-account)  
<p>

*Transfers to another sub account of the same master*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.TransferSubAccountToSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceSubAccountTransaction>> TransferSubAccountToSubAccountAsync(string email, string asset, decimal quantity, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|Email of the sub account|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## UpdateIpRestrictionForSubAccountApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#update-ip-restriction-for-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#update-ip-restriction-for-sub-account-api-key-for-master-account)  
<p>

*Update the ip restriction for a sub-account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientSubAccount.UpdateIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> UpdateIpRestrictionForSubAccountApiKeyAsync(string email, string apiKey, bool ipRestrict, IEnumerable<string>? ipAddresses = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|email|The sub account email|
|apiKey|The sub account api key|
|ipRestrict|Enable or disable ip restrictions|
|_[Optional]_ ipAddresses|Addresses to whitelist|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
