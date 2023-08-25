---
title: IBinanceRestClientGeneralApiStaking
has_children: false
parent: IBinanceRestClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > GeneralApi > Staking`  
*Binance Staking endpoints*
  

***

## GetBethRateHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-beth-rate-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-beth-rate-history-user_data)  
<p>

*Get Beth rate history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetBethRateHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceBethRateHistory>>> GetBethRateHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBethUnwrapHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-unwrap-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-unwrap-history-user_data)  
<p>

*Get unwrap history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetBethUnwrapHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethUnwrapHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBethWrapHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-wrap-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-wbeth-wrap-history-user_data)  
<p>

*Get wrap history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetBethWrapHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceBethWrapHistory>>> GetBethWrapHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEthRedemptionHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-eth-redemption-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-eth-redemption-history-user_data)  
<p>

*Get ETH redemption history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetEthRedemptionHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceEthRedemptionHistory>>> GetEthRedemptionHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEthRewardsHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-eth-rewards-distribution-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-eth-rewards-distribution-history-user_data)  
<p>

*Get ETH rewards history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetEthRewardsHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceEthRewardsHistory>>> GetEthRewardsHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEthStakingAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#eth-staking-account-user_data](https://binance-docs.github.io/apidocs/spot/en/#eth-staking-account-user_data)  
<p>

*Get eth staking account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetEthStakingAccountAsync();  
```  

```csharp  
Task<WebCallResult<BinanceEthStakingAccount>> GetEthStakingAccountAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEthStakingHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-eth-staking-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-eth-staking-history-user_data)  
<p>

*Get ETH staking history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetEthStakingHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceEthStakingHistory>>> GetEthStakingHistoryAsync(DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEthStakingQuotaAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-current-eth-staking-quota-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-current-eth-staking-quota-user_data)  
<p>

*Get ETH staking quotas*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetEthStakingQuotaAsync();  
```  

```csharp  
Task<WebCallResult<BinanceEthStakingQuota>> GetEthStakingQuotaAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-history-user_data)  
<p>

*Get staking history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetStakingHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceStakingHistory>>> GetStakingHistoryAsync(StakingProductType product, StakingTransactionType transactionType, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|transactionType|Transaction type|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingPersonalQuotaAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-personal-left-quota-of-staking-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-personal-left-quota-of-staking-product-user_data)  
<p>

*Get personal staking quota*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetStakingPersonalQuotaAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingPersonalQuota>> GetStakingPersonalQuotaAsync(StakingProductType product, string productId, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|The staking product|
|productId|Product id|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingPositionsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-position-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-position-user_data)  
<p>

*Get staking positions*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetStakingPositionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceStakingPosition>>> GetStakingPositionsAsync(StakingProductType product, string? productId = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|_[Optional]_ productId|Product id|
|_[Optional]_ page|Page|
|_[Optional]_ limit|Max results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetStakingProductsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-staking-product-list-user_data)  
<p>

*Get avaialble staking products list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.GetStakingProductsAsync(/* parameters */);  
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

## PurchaseStakingProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#purchase-staking-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#purchase-staking-product-user_data)  
<p>

*Purchase a staking product*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.PurchaseStakingProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingPositionResult>> PurchaseStakingProductAsync(StakingProductType product, string productId, decimal quantity, bool? renewable = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|productId|Product id|
|quantity|Quantity to purchase|
|_[Optional]_ renewable|Renewable|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemEthStakingAsync  

[https://binance-docs.github.io/apidocs/spot/en/#redeem-eth-trade](https://binance-docs.github.io/apidocs/spot/en/#redeem-eth-trade)  
<p>

*Redeem from ETH staking*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.RedeemEthStakingAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> RedeemEthStakingAsync(decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quantity|Amount|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemStakingProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#redeem-staking-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#redeem-staking-product-user_data)  
<p>

*Redeem a staking product*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.RedeemStakingProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> RedeemStakingProductAsync(StakingProductType product, string productId, string? positionId = default, decimal? quantity = default, bool? renewable = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|Product type|
|productId|Product id|
|_[Optional]_ positionId|Position id, required for Staking or LockedDefi types|
|_[Optional]_ quantity|Quantity to purchase|
|_[Optional]_ renewable|Renewable|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SetAutoStakingAsync  

[https://binance-docs.github.io/apidocs/spot/en/#set-auto-staking-user_data](https://binance-docs.github.io/apidocs/spot/en/#set-auto-staking-user_data)  
<p>

*Set auto staking for a product*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.SetAutoStakingAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> SetAutoStakingAsync(StakingProductType product, string positionId, bool renewable, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|product|The staking product|
|positionId|The position|
|renewable|Renewable|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeEthStakingAsync  

[https://binance-docs.github.io/apidocs/spot/en/#subscribe-eth-staking-trade](https://binance-docs.github.io/apidocs/spot/en/#subscribe-eth-staking-trade)  
<p>

*Subscribe to ETH staking*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.SubscribeEthStakingAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> SubscribeEthStakingAsync(decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quantity|Amount|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## UnwrapBethAsync  

[https://binance-docs.github.io/apidocs/spot/en/#unwrap-wbeth-trade](https://binance-docs.github.io/apidocs/spot/en/#unwrap-wbeth-trade)  
<p>

*Unwarp Beth*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.UnwrapBethAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> UnwrapBethAsync(decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quantity|Quantity to unwrap|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## WrapBethAsync  

[https://binance-docs.github.io/apidocs/spot/en/#wrap-beth-trade](https://binance-docs.github.io/apidocs/spot/en/#wrap-beth-trade)  
<p>

*Wrap Beth*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Staking.WrapBethAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceStakingResult>> WrapBethAsync(decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quantity|Quantity to wrap|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
