---
title: IBinanceClientGeneralApiLending
has_children: false
parent: IBinanceClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > GeneralApi > Lending`  
*Binance Spot Lending/Savings endpoints*
  

***

## ChangeToDailyPositionAsync  

[https://binance-docs.github.io/apidocs/spot/en/#change-fixed-activity-position-to-daily-position-user_data](https://binance-docs.github.io/apidocs/spot/en/#change-fixed-activity-position-to-daily-position-user_data)  
<p>

*Changed fixed/activity position to daily position*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.ChangeToDailyPositionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceLendingChangeToDailyResult>> ChangeToDailyPositionAsync(string projectId, int lot, long? positionId = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|projectId|Id of the project|
|lot|The lot|
|_[Optional]_ positionId|For fixed position|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCryptoLoansIncomeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data)  
<p>

*Get income history from crypto loans*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetCryptoLoansIncomeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceCryptoLoanIncome>>> GetCryptoLoansIncomeHistoryAsync(string asset, LoanIncomeType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset|
|_[Optional]_ type|Filter by type of incoming|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|_[Optional]_ limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCustomizedFixedProjectPositionsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-fixed-activity-project-position-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-fixed-activity-project-position-user_data)  
<p>

*Get customized fixed project position*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetCustomizedFixedProjectPositionsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>>> GetCustomizedFixedProjectPositionsAsync(string asset, string? projectId = default, ProjectStatus? status = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|_[Optional]_ projectId|The project id|
|_[Optional]_ status|Filter by status|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFixedAndCustomizedFixedProjectListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-fixed-and-activity-project-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-fixed-and-activity-project-list-user_data)  
<p>

*Get fixed and customized fixed project list*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetFixedAndCustomizedFixedProjectListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceProject>>> GetFixedAndCustomizedFixedProjectListAsync(ProjectType type, string? asset = default, ProductStatus? status = default, bool? sortAscending = default, string? sortBy = default, int? currentPage = default, int? size = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|type|Type of project|
|_[Optional]_ asset|Asset|
|_[Optional]_ status|Filter by status|
|_[Optional]_ sortAscending|If should sort ascending|
|_[Optional]_ sortBy|Sort by. Valid values: "START_TIME", "LOT_SIZE", "INTEREST_RATE", "DURATION"; default "START_TIME"|
|_[Optional]_ currentPage|Result page|
|_[Optional]_ size|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFlexibleProductListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-list-user_data)  
<p>

*Get product list*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetFlexibleProductListAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceSavingsProduct>>> GetFlexibleProductListAsync(ProductStatus? status = default, bool? featured = default, int? page = default, int? pageSize = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ status|Filter by status|
|_[Optional]_ featured|Filter by featured|
|_[Optional]_ page|Page to retrieve|
|_[Optional]_ pageSize|Page size to return|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFlexibleProductPositionAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-position-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-position-user_data)  
<p>

*Get flexible product position*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetFlexibleProductPositionAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceFlexibleProductPosition>>> GetFlexibleProductPositionAsync(string asset, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|Asset|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeftDailyPurchaseQuotaOfFlexableProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-purchase-quota-of-flexible-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-purchase-quota-of-flexible-product-user_data)  
<p>

*Get the purchase quota left for a product*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetLeftDailyPurchaseQuotaOfFlexableProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinancePurchaseQuotaLeft>> GetLeftDailyPurchaseQuotaOfFlexableProductAsync(string productId, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|productId|Id of the product|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLeftDailyRedemptionQuotaOfFlexibleProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-redemption-quota-of-flexible-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-redemption-quota-of-flexible-product-user_data)  
<p>

*Get the redemption quota left for a product*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceRedemptionQuotaLeft>> GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(string productId, RedeemType type, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|productId|Id of the product|
|type|Type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#lending-account-user_data](https://binance-docs.github.io/apidocs/spot/en/#lending-account-user_data)  
<p>

*Get lending account info*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetLendingAccountAsync();  
```  

```csharp  
Task<WebCallResult<BinanceLendingAccount>> GetLendingAccountAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLendingInterestHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-interest-history-user_data-2](https://binance-docs.github.io/apidocs/spot/en/#get-interest-history-user_data-2)  
<p>

*Get interest history*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetLendingInterestHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceLendingInterestHistory>>> GetLendingInterestHistoryAsync(LendingType lendingType, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? page, int? limit, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|lendingType|Lending type|
|_[Optional]_ asset|Asset|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|page|Results page|
|limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPurchaseRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-purchase-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-purchase-record-user_data)  
<p>

*Get purchase records*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetPurchaseRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinancePurchaseRecord>>> GetPurchaseRecordsAsync(LendingType lendingType, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? page, int? limit, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|lendingType|Lending type|
|_[Optional]_ asset|Asset|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|page|Results page|
|limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRedemptionRecordsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-redemption-record-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-redemption-record-user_data)  
<p>

*Get redemption records*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.GetRedemptionRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceRedemptionRecord>>> GetRedemptionRecordsAsync(LendingType lendingType, string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? page, int? limit, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|lendingType|Lending type|
|_[Optional]_ asset|Asset|
|_[Optional]_ startTime|Filter by startTime from|
|_[Optional]_ endTime|Filter by endTime from|
|page|Results page|
|limit|Limit of the amount of results|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PurchaseCustomizedFixedProjectAsync  

[https://binance-docs.github.io/apidocs/spot/en/#purchase-fixed-activity-project-user_data](https://binance-docs.github.io/apidocs/spot/en/#purchase-fixed-activity-project-user_data)  
<p>

*Purchase customized fixed project*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.PurchaseCustomizedFixedProjectAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseCustomizedFixedProjectAsync(string projectId, int lot, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|projectId|Id of the project|
|lot|The lot|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PurchaseFlexibleProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#purchase-flexible-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#purchase-flexible-product-user_data)  
<p>

*Purchase flexible product*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.PurchaseFlexibleProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseFlexibleProductAsync(string productId, decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|productId|Id of the product|
|quantity|The quantity to purchase|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RedeemFlexibleProductAsync  

[https://binance-docs.github.io/apidocs/spot/en/#redeem-flexible-product-user_data](https://binance-docs.github.io/apidocs/spot/en/#redeem-flexible-product-user_data)  
<p>

*Redeem flexible product*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.Lending.RedeemFlexibleProductAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<object>> RedeemFlexibleProductAsync(string productId, decimal quantity, RedeemType type, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|productId|Id of the product|
|quantity|The quantity to redeem|
|type|Redeem type|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
