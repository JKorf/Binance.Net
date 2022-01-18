---
title: IBinanceClientGeneralApiSubAccount
has_children: false
parent: IBinanceClientGeneralApi
grand_parent: IBinanceClient
---
*[generated documentation]*  
`BinanceClient > GeneralApi > SubAccount`  
*Binance Spot Subaccount endpoints*
  

***

## AddIpToWhitelistForApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#add-ip-list-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#add-ip-list-for-a-sub-account-api-key-for-master-account)  
<p>

*Add IP addresses to the ip whitelist for an API key*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.AddIpToWhitelistForApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> AddIpToWhitelistForApiKeyAsync(string apiKey, IEnumerable<string> ipAddresses, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|apiKey|The api key|
|ipAddresses|Addresses to whitelist|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateVirtualSubAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#create-a-virtual-sub-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#create-a-virtual-sub-account-for-master-account)  
<p>

*Create a virtual sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.CreateVirtualSubAccountAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.EnableBlvtForSubAccountAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.EnableFuturesForSubAccountAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.EnableMarginForSubAccountAsync(/* parameters */);  
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

## GetIpWhitelistForApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-ip-restriction-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-ip-restriction-for-a-sub-account-api-key-for-master-account)  
<p>

*Get the current whitelisted ip addresses for an API key*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.GetIpWhitelistForApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> GetIpWhitelistForApiKeyAsync(string apiKey, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|apiKey|The api key|
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
var result = await client.GeneralApi.SubAccount.GetSubAccountAssetsAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountBtcValuesAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountDepositAddressAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountDepositHistoryAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountFuturesDetailsAsync(/* parameters */);  
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

## GetSubAccountMarginDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-margin-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-detail-on-sub-account-39-s-margin-account-for-master-account)  
<p>

*Gets margin details for a sub account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.GetSubAccountMarginDetailsAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountsAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountsFuturesPositionRiskAsync(/* parameters */);  
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

## GetSubAccountsFuturesSummaryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-futures-account-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#get-summary-of-sub-account-39-s-futures-account-for-master-account)  
<p>

*Gets futures summary for sub accounts*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.GetSubAccountsFuturesSummaryAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountsMarginSummaryAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountStatusAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountTransferHistoryForMasterAsync();  
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
var result = await client.GeneralApi.SubAccount.GetSubAccountTransferHistoryForSubAccountAsync();  
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
var result = await client.GeneralApi.SubAccount.GetUniversalTransferHistoryAsync();  
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

## RemoveIpFromWhitelistForApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#delete-ip-list-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#delete-ip-list-for-a-sub-account-api-key-for-master-account)  
<p>

*Remove IP addresses from the ip whitelist for an API key*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.RemoveIpFromWhitelistForApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> RemoveIpFromWhitelistForApiKeyAsync(string apiKey, IEnumerable<string> ipAddresses, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|apiKey|The api key|
|ipAddresses|Addresses to remove from whitelist|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ToggleIpRestrictionForApiKeyAsync  

[https://binance-docs.github.io/apidocs/spot/en/#enable-or-disable-ip-restriction-for-a-sub-account-api-key-for-master-account](https://binance-docs.github.io/apidocs/spot/en/#enable-or-disable-ip-restriction-for-a-sub-account-api-key-for-master-account)  
<p>

*Toggle IP restriction for an API key*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.SubAccount.ToggleIpRestrictionForApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceIpRestriction>> ToggleIpRestrictionForApiKeyAsync(string apiKey, bool enable, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|apiKey|The api key|
|enable|Enable or disable|
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
var result = await client.GeneralApi.SubAccount.TransferSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> TransferSubAccountAsync(TransferAccountType fromAccountType, TransferAccountType toAccountType, string asset, decimal quantity, string? fromEmail = default, string? toEmail = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|fromAccountType|Account type to transfer from|
|toAccountType|Account type to transfer to|
|asset|The asset to transfer|
|quantity|The quantity to transfer|
|_[Optional]_ fromEmail|From which account to transfer|
|_[Optional]_ toEmail|To which account to transfer|
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
var result = await client.GeneralApi.SubAccount.TransferSubAccountFuturesAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.TransferSubAccountMarginAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.TransferSubAccountToMasterAsync(/* parameters */);  
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
var result = await client.GeneralApi.SubAccount.TransferSubAccountToSubAccountAsync(/* parameters */);  
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
