---
title: IBinanceRestClientGeneralApiBrokerage
has_children: false
parent: IBinanceRestClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > GeneralApi > Brokerage`  
*Binance brokerage endpoints.*
  

***

## AddIpRestrictionForSubAccountApiKeyAsync  

<p>

*Add IP Restriction for Sub Account Api Key*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.AddIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageAddIpRestrictionResult>> AddIpRestrictionForSubAccountApiKeyAsync(string subAccountId, string apiKey, string ipAddress, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey|Api key|
|ipAddress|IP address|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeBnbBurnForSubAccountMarginInterestAsync  

<p>

*Enable Or Disable BNB Burn for Sub Account Margin Interest*  
*<para>Sub account must be enabled margin before using this switch</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeBnbBurnForSubAccountMarginInterestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageChangeBnbBurnMarginInterestResult>> ChangeBnbBurnForSubAccountMarginInterestAsync(string subAccountId, bool interestBnbBurn, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|interestBnbBurn|"true" or "false", margin loan whether uses BNB to pay for margin interest or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeBnbBurnForSubAccountSpotAndMarginAsync  

<p>

*Enable Or Disable BNB Burn for Sub Account SPOT and MARGIN*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeBnbBurnForSubAccountSpotAndMarginAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageChangeBnbBurnSpotAndMarginResult>> ChangeBnbBurnForSubAccountSpotAndMarginAsync(string subAccountId, bool spotBnbBurn, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|spotBnbBurn|"true" or "false", spot and margin whether use BNB to pay for transaction fees or not|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeIpRestrictionForSubAccountApiKeyAsync  

<p>

*Enable or Disable IP Restriction for Sub Account Api Key*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageIpRestriction>> ChangeIpRestrictionForSubAccountApiKeyAsync(string subAccountId, string apiKey, bool ipRestrict, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey|Api key|
|ipRestrict|IP restrict|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeSubAccountApiKeyPermissionAsync  

<p>

*Change Sub Account Api Permission*  
*<para>This request will change the api permission for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  
*<para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>*  
*<para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeSubAccountApiKeyPermissionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> ChangeSubAccountApiKeyPermissionAsync(string subAccountId, string apiKey, bool isSpotTradingEnabled, bool isMarginTradingEnabled, bool isFuturesTradingEnabled, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey|Api key|
|isSpotTradingEnabled|Is spot trading enabled|
|isMarginTradingEnabled|Is margin trading enabled|
|isFuturesTradingEnabled|Is futures trading enabled|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeSubAccountCoinFuturesCommissionAdjustmentAsync  

<p>

*Change Sub Account COIN-Ⓜ Futures Commission Adjustment*  
*<para>This request will change the COIN-Ⓜ futures commission for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  
*<para>The sub-account's COIN-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>*  
*<para>If futures disabled, it is not allowed to set subaccount's COIN-Ⓜ futures commission adjustment on any symbol</para>*  
*<para>Different symbols have the same commission for the same pair</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeSubAccountCoinFuturesCommissionAdjustmentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountCoinFuturesCommission>> ChangeSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId, string pair, int makerAdjustment, int takerAdjustment, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|pair|Pair|
|makerAdjustment|Maker adjustment (100 for 0.01%)|
|takerAdjustment|Taker adjustment (100 for 0.01%)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeSubAccountCommissionAsync  

<p>

*Change Sub Account Commission*  
*<para>This request will change the commission for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  
*<para>If margin disabled, it is not allowed to send marginMakerCommission or marginTakerCommission</para>*  
*<para>If margin enabled, marginMakerCommission or marginTakerCommission has default value as spotMakerCommission or spotTakerCommission</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeSubAccountCommissionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountCommission>> ChangeSubAccountCommissionAsync(string subAccountId, decimal makerCommission, decimal takerCommission, decimal? marginMakerCommission = default, decimal? marginTakerCommission = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|makerCommission|Maker commission|
|takerCommission|Taker commission|
|_[Optional]_ marginMakerCommission|Margin maker commission|
|_[Optional]_ marginTakerCommission|Margin taker commission|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeSubAccountFuturesCommissionAdjustmentAsync  

<p>

*Change Sub Account USDT-Ⓜ Futures Commission Adjustment*  
*<para>This request will change the USDT-Ⓜ futures commission for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  
*<para>The sub-account's USDT-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>*  
*<para>If futures disabled, it is not allowed to set subaccount's USDT-Ⓜ futures commission adjustment on any symbol</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.ChangeSubAccountFuturesCommissionAdjustmentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountFuturesCommission>> ChangeSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string symbol, int makerAdjustment, int takerAdjustment, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|symbol|Symbol|
|makerAdjustment|Maker adjustment (100 for 0.01%)|
|takerAdjustment|Taker adjustment (100 for 0.01%)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateApiKeyForSubAccountAsync  

<p>

*Create Api Key for Sub Account*  
*<para>This request will generate a api key for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  
*<para>Sub account should be enable margin before its api-key's marginTrade being enabled</para>*  
*<para>Sub account should be enable futures before its api-key's futuresTrade being enabled</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.CreateApiKeyForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageApiKeyCreateResult>> CreateApiKeyForSubAccountAsync(string subAccountId, bool isSpotTradingEnabled, bool? isMarginTradingEnabled = default, bool? isFuturesTradingEnabled = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|isSpotTradingEnabled|Is spot trading enabled|
|_[Optional]_ isMarginTradingEnabled|Is margin trading enabled|
|_[Optional]_ isFuturesTradingEnabled|Is futures trading enabled|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CreateSubAccountAsync  

<p>

*Create a Sub Account*  
*<para>This request will generate a sub account under your brokerage master account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.CreateSubAccountAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountCreateResult>> CreateSubAccountAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DeleteIpRestrictionForSubAccountApiKeyAsync  

<p>

*Delete IP Restriction for Sub Account Api Key*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.DeleteIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageIpRestrictionBase>> DeleteIpRestrictionForSubAccountApiKeyAsync(string subAccountId, string apiKey, string ipAddress, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey|Api key|
|ipAddress|IP address|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## DeleteSubAccountApiKeyAsync  

<p>

*Delete Sub Account Api Key*  
*<para>This request will delete a api key for a sub account</para>*  
*<para>You need to enable "trade" option for the api key which requests this endpoint</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.DeleteSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<object> DeleteSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey||
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableFuturesForSubAccountAsync  

<p>

*Enable Futures for Sub Account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.EnableFuturesForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageEnableFuturesResult>> EnableFuturesForSubAccountAsync(string subAccountId, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableLeverageTokenForSubAccountAsync  

<p>

*Enable Leverage Token for Sub Account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.EnableLeverageTokenForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageEnableLeverageTokenResult>> EnableLeverageTokenForSubAccountAsync(string subAccountId, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## EnableMarginForSubAccountAsync  

<p>

*Enable Margin for Sub Account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.EnableMarginForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageEnableMarginResult>> EnableMarginForSubAccountAsync(string subAccountId, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBnbBurnStatusForSubAccountAsync  

<p>

*Get BNB Burn Status for Sub Account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetBnbBurnStatusForSubAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageBnbBurnStatus>> GetBnbBurnStatusForSubAccountAsync(string subAccountId, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBrokerAccountInfoAsync  

<p>

*Broker Account Information*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetBrokerAccountInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageAccountInfo>> GetBrokerAccountInfoAsync(int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBrokerCommissionRebatesRecentAsync  

<p>

*Query Broker Commission Rebate Recent Record (Spot)*  
*<para>Only get the latest history of past 7 days</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetBrokerCommissionRebatesRecentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageRebate>>> GetBrokerCommissionRebatesRecentAsync(string subAccountId, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ startDate|From date|
|_[Optional]_ endDate|To date|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 500, max 500)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBrokerFuturesCommissionRebatesHistoryAsync  

<p>

*Query Broker Futures Commission Rebate Record*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetBrokerFuturesCommissionRebatesHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageFuturesRebate>>> GetBrokerFuturesCommissionRebatesHistoryAsync(BinanceBrokerageFuturesType futuresType, DateTime startDate, DateTime endDate, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|futuresType|Futures type|
|startDate|Start date|
|endDate|End date|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 10, max 100)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIpRestrictionForSubAccountApiKeyAsync  

<p>

*Get IP Restriction for Sub Account Api Key*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetIpRestrictionForSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageIpRestriction>> GetIpRestrictionForSubAccountApiKeyAsync(string subAccountId, string apiKey, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|apiKey|Api key|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountApiKeyAsync  

<p>

*Query Sub Account Api Key*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountApiKeyAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSubAccountApiKey>> GetSubAccountApiKeyAsync(string subAccountId, string? apiKey = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ apiKey|Api key|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 500, max 500)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountCoinFuturesCommissionAdjustmentAsync  

<p>

*Query Sub Account COIN-Ⓜ Futures Commission Adjustment*  
*<para>The sub-account's COIN-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>*  
*<para>If pair not sent, commission adjustment of all symbols will be returned</para>*  
*<para>If futures disabled, it is not allowed to set subaccount's COIN-Ⓜ futures commission adjustment on any symbol</para>*  
*<para>Different symbols have the same commission for the same pair</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountCoinFuturesCommissionAdjustmentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountCoinFuturesCommissionAdjustmentAsync(string subAccountId, string? pair = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ pair|Pair|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountDepositHistoryAsync  

<p>

*Get Sub Account Deposit History*  
*<para>Please notice the default startDate and endDate to make sure that time interval is within 0-7 days</para>*  
*<para>If both startDate and endDate are sent, time between startDate and endDate must be less than 7 days</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountDepositHistoryAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountDepositTransaction>>> GetSubAccountDepositHistoryAsync(string? subAccountId = default, string? coin = default, BinanceBrokerageSubAccountDepositStatus? status = default, DateTime? startDate = default, DateTime? endDate = default, int? limit = default, int? offset = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subAccountId|Sub account id|
|_[Optional]_ coin|Coin|
|_[Optional]_ status|Status|
|_[Optional]_ startDate|From date (default 7 days from current timestamp)|
|_[Optional]_ endDate|To date (default present timestamp)|
|_[Optional]_ limit|Limit (default 500)|
|_[Optional]_ offset|Offset (default 0)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountFuturesAssetInfoAsync  

<p>

*Query Sub Account Futures Asset info*  
*<para>If subAccountId is not sent, the size must be sent</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountFuturesAssetInfoAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageFuturesAssetInfo>> GetSubAccountFuturesAssetInfoAsync(BinanceBrokerageFuturesType futuresType, string? subAccountId = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|futuresType|Futures type|
|_[Optional]_ subAccountId|Sub account id|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 10, max 20)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountFuturesCommissionAdjustmentAsync  

<p>

*Query Sub Account USDT-Ⓜ Futures Commission Adjustment*  
*<para>The sub-account's USDT-Ⓜ futures commission of a symbol equals to the base commission of the symbol on the sub-account's fee tier plus the commission adjustment</para>*  
*<para>If symbol not sent, commission adjustment of all symbols will be returned</para>*  
*<para>If futures disabled, it is not allowed to set subaccount's USDT-Ⓜ futures commission adjustment on any symbol</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountFuturesCommissionAdjustmentAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccountFuturesCommission>>> GetSubAccountFuturesCommissionAdjustmentAsync(string subAccountId, string? symbol = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountMarginAssetInfoAsync  

<p>

*Query Sub Account Margin Asset info*  
*<para>If subAccountId is not sent, the size must be sent</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountMarginAssetInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageMarginAssetInfo>> GetSubAccountMarginAssetInfoAsync(string? subAccountId = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subAccountId|Sub account id|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 10, max 20)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountsAsync  

<p>

*Query Sub Account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageSubAccount>>> GetSubAccountsAsync(string? subAccountId = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subAccountId|Sub account id|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 500)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSubAccountSpotAssetInfoAsync  

<p>

*Query Sub Account Spot Asset info*  
*<para>If subAccountId is not sent, the size must be sent</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetSubAccountSpotAssetInfoAsync();  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageSpotAssetInfo>> GetSubAccountSpotAssetInfoAsync(string? subAccountId = default, int? page = default, int? size = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ subAccountId|Sub account id|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ size|Size (default 10, max 20)|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTransferFuturesHistoryAsync  

<p>

*Query Sub Account Transfer History (Futures)*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetTransferFuturesHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageTransferFuturesTransactions>> GetTransferFuturesHistoryAsync(string subAccountId, BinanceBrokerageFuturesType futuresType, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? limit = default, string? clientTransferId = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|subAccountId|Sub account id|
|futuresType|Futures type|
|_[Optional]_ startDate|From date (default 30 days records)|
|_[Optional]_ endDate|To date (default 30 days records)|
|_[Optional]_ page|Page (default 1)|
|_[Optional]_ limit|Limit (default 50, max 500)|
|_[Optional]_ clientTransferId|Client transfer id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTransferHistoryAsync  

<p>

*Query Sub Account Transfer History (Spot)*  
*<para>If showAllStatus is true, the status in response will show four types: INIT,PROCESS,SUCCESS,FAILURE</para>*  
*<para>If showAllStatus is false, the status in response will show three types: INIT,PROCESS,SUCCESS</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetTransferHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransaction>>> GetTransferHistoryAsync(string? fromId = default, string? toId = default, string? clientTransferId = default, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? limit = default, bool showAllStatus, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromId|From id|
|_[Optional]_ toId|To id|
|_[Optional]_ clientTransferId|Client transfer id|
|_[Optional]_ startDate|From date|
|_[Optional]_ endDate|To date|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Limit (default 500, max 500)|
|showAllStatus|Show all status|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTransferHistoryUniversalAsync  

<p>

*Query Sub Account Transfer History Universal*  
*<para>Either fromId or toId must be sent. Return fromId equal master account by default</para>*  
*<para>Only get the latest history of past 30 days</para>*  
*<para>If showAllStatus is true, the status in response will show four types: INIT,PROCESS,SUCCESS,FAILURE</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.GetTransferHistoryUniversalAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceBrokerageTransferTransactionUniversal>>> GetTransferHistoryUniversalAsync(string? fromId = default, string? toId = default, string? clientTransferId = default, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? limit = default, bool showAllStatus, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ fromId|From id|
|_[Optional]_ toId|To id|
|_[Optional]_ clientTransferId|Client transfer id|
|_[Optional]_ startDate|From date|
|_[Optional]_ endDate|To date|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Limit (default 500, max 500)|
|showAllStatus|Show all status|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferAsync  

<p>

*Sub Account Transfer (Spot)*  
*<para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>*  
*<para>Transfer from master account if fromId not sent</para>*  
*<para>Transfer to master account if toId not sent</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.TransferAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageTransferResult>> TransferAsync(string asset, decimal quantity, string? fromId, string? toId, string? clientTransferId = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity|
|fromId|From id|
|toId|To id|
|_[Optional]_ clientTransferId|Client transfer id, must be unique|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferFuturesAsync  

<p>

*Sub Account Transfer (Futures)*  
*<para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>*  
*<para>Transfer from master account if fromId not sent</para>*  
*<para>Transfer to master account if toId not sent</para>*  
*<para>Each master account could transfer 5000 times/min</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.TransferFuturesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageTransferFuturesResult>> TransferFuturesAsync(string asset, decimal quantity, BinanceBrokerageFuturesType futuresType, string? fromId, string? toId, string? clientTransferId = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity|
|futuresType|Futures type|
|fromId|From id|
|toId|To id|
|_[Optional]_ clientTransferId|Client transfer id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferUniversalAsync  

<p>

*Sub Account Transfer Universal*  
*<para>You need to enable "internal transfer" option for the api key which requests this endpoint</para>*  
*<para>Transfer from master account if fromId not sent</para>*  
*<para>Transfer to master account if toId not sent</para>*  
*<para>Transfer between futures account is not supported</para>*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Brokerage.TransferUniversalAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceBrokerageTransferResult>> TransferUniversalAsync(string asset, decimal quantity, string? fromId, BrokerageAccountType fromAccountType, string? toId, BrokerageAccountType toAccountType, string? clientTransferId = default, int? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|quantity|Quantity|
|fromId|From id|
|fromAccountType|From type|
|toId|To id|
|toAccountType|To type|
|_[Optional]_ clientTransferId|Client transfer id, must be unique|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
