---
title: IBinanceRestClientGeneralApiLoans
has_children: false
parent: IBinanceRestClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > GeneralApi > Loans`  
*Binance Spot Crypto loans endpoints*
  

***

## AdjustLTVAsync  

[https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-crypto-loan-adjust-ltv-trade](https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-crypto-loan-adjust-ltv-trade)  
<p>

*Adjust LTV for a loan*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.AdjustLTVAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCryptoLoanLtvAdjust>> AdjustLTVAsync(long orderId, decimal quantity, bool addOrRmove, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Order id|
|quantity|Adjustment quantity|
|addOrRmove|True for add, false to reduce|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## BorrowAsync  

[https://binance-docs.github.io/apidocs/spot/en/#borrow-crypto-loan-borrow-trade](https://binance-docs.github.io/apidocs/spot/en/#borrow-crypto-loan-borrow-trade)  
<p>

*Take a crypto loan*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.BorrowAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCryptoLoanBorrow>> BorrowAsync(string loanAsset, string collateralAsset, int loanTerm, decimal? loanQuantity = default, decimal? collateralQuantity = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|loanAsset|Asset to loan|
|collateralAsset|Collateral asset|
|loanTerm|Loan term in days, 7/14/30/90/180|
|_[Optional]_ loanQuantity|Quantity to loan in loan asset|
|_[Optional]_ collateralQuantity|Quantity to loan in collateral asset|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CustomizeMarginCallAsync  

<p>

*Customize margin call for ongoing orders only.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.CustomizeMarginCallAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanMarginCallResult>>> CustomizeMarginCallAsync(decimal marginCall, string? orderId = default, string? collateralAsset = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|marginCall|Margin call value|
|_[Optional]_ orderId|Order id. Required if collateralAsset is not send|
|_[Optional]_ collateralAsset|Collateral asset. Required if order id is not send|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBorrowHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-borrow-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-borrow-history-user_data)  
<p>

*Get borrow order history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetBorrowHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanBorrowRecord>>> GetBorrowHistoryAsync(long? orderId = default, string? loanAsset = default, string? collateralAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ loanAsset|Filter by loan asset|
|_[Optional]_ collateralAsset|Filter by collateral asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page number|
|_[Optional]_ limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCollateralAssetsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-collateral-assets-data-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-collateral-assets-data-user_data)  
<p>

*Get LTV information and collateral limit of collateral assets. The collateral limit is shown in USD value.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetCollateralAssetsAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanCollateralAsset>>> GetCollateralAssetsAsync(string? collateralAsset = default, int? vipLevel = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ collateralAsset|Filter by collateral asset|
|_[Optional]_ vipLevel|Vip level|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCollateralRepayRateAsync  

[https://binance-docs.github.io/apidocs/spot/en/#check-collateral-repay-rate-user_data](https://binance-docs.github.io/apidocs/spot/en/#check-collateral-repay-rate-user_data)  
<p>

*Get the the rate of collateral coin / loan coin when using collateral repay, the rate will be valid within 8 second.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetCollateralRepayRateAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCryptoLoanRepayRate>> GetCollateralRepayRateAsync(string loanAsset, string collateralAsset, decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|loanAsset|Loan asset|
|collateralAsset|Collateral asset|
|quantity|Quantity|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIncomeHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data)  
<p>

*Get income history from crypto loans*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetIncomeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceCryptoLoanIncome>>> GetIncomeHistoryAsync(string asset, LoanIncomeType? type = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
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

## GetLoanableAssetsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-loanable-assets-data-user_data-2](https://binance-docs.github.io/apidocs/spot/en/#get-loanable-assets-data-user_data-2)  
<p>

*Get interest rate and borrow limit of loanable assets. The borrow limit is shown in USD value.*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetLoanableAssetsAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanAsset>>> GetLoanableAssetsAsync(string? loanAsset = default, int? vipLevel = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ loanAsset|Filter by loan asset|
|_[Optional]_ vipLevel|Vip level|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLtvAdjustHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-get-loan-ltv-adjustment-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-get-loan-ltv-adjustment-history-user_data)  
<p>

*Get LTV adjustment history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetLtvAdjustHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanLtvAdjustRecord>>> GetLtvAdjustHistoryAsync(long? orderId = default, string? loanAsset = default, string? collateralAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ loanAsset|Filter by loan asset|
|_[Optional]_ collateralAsset|Filter by collateral asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page number|
|_[Optional]_ limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenBorrowOrdersAsync  

[https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-ongoing-orders-user_data](https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-ongoing-orders-user_data)  
<p>

*Get ongoing loan orders*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetOpenBorrowOrdersAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanOpenBorrowOrder>>> GetOpenBorrowOrdersAsync(long? orderId = default, string? loanAsset = default, string? collateralAsset = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ loanAsset|Filter by loan asset|
|_[Optional]_ collateralAsset|Filter by collateral asset|
|_[Optional]_ page|Page number|
|_[Optional]_ limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRepayHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#repay-get-loan-repayment-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#repay-get-loan-repayment-history-user_data)  
<p>

*Get loan repayment history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.GetRepayHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanRepayRecord>>> GetRepayHistoryAsync(long? orderId = default, string? loanAsset = default, string? collateralAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|Filter by order id|
|_[Optional]_ loanAsset|Filter by loan asset|
|_[Optional]_ collateralAsset|Filter by collateral asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page number|
|_[Optional]_ limit|Page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayAsync  

[https://binance-docs.github.io/apidocs/spot/en/#repay-crypto-loan-repay-trade](https://binance-docs.github.io/apidocs/spot/en/#repay-crypto-loan-repay-trade)  
<p>

*Repay a loan*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Loans.RepayAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCryptoLoanRepay>> RepayAsync(long orderId, decimal quantity, bool? repayWithBorrowedAsset = default, bool? collateralReturn = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|Order id to repay|
|quantity|Quantity to repay|
|_[Optional]_ repayWithBorrowedAsset|True to repay with the borrowed asset, false to repay with collateral asset|
|_[Optional]_ collateralReturn|Return extra colalteral to spot account|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
