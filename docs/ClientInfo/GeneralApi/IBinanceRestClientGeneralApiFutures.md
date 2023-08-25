---
title: IBinanceRestClientGeneralApiFutures
has_children: false
parent: IBinanceRestClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > GeneralApi > Futures`  
*Binance futures interaction endpoints*
  

***

## GetAdjustCrossCollateralLoanToValueHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-history-user_data)  
<p>

*Get cross collateral LTV adjustment history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetAdjustCrossCollateralLoanToValueHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>>> GetAdjustCrossCollateralLoanToValueHistoryAsync(string? collateralAsset = default, string? loanAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ collateralAsset|The collateral asset|
|_[Optional]_ loanAsset|The loan asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralBorrowHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-borrow-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-borrow-history-user_data)  
<p>

*Get cross collateral borrow history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetCrossCollateralBorrowHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>>> GetCrossCollateralBorrowHistoryAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|The asset to get history for|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralInterestHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-interest-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-interest-history-user_data)  
<p>

*Get cross collateral interest history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetCrossCollateralInterestHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralInterestHistory>>> GetCrossCollateralInterestHistoryAsync(string? collateralAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ collateralAsset|The collateral asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralLiquidationHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-liquidation-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-liquidation-history-user_data)  
<p>

*Get cross collateral liquidation history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetCrossCollateralLiquidationHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>>> GetCrossCollateralLiquidationHistoryAsync(string? collateralAsset = default, string? loanAsset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ collateralAsset|The collateral asset|
|_[Optional]_ loanAsset|The loan asset|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralRepayHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-repayment-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-repayment-history-user_data)  
<p>

*Get cross collateral borrow history*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetCrossCollateralRepayHistoryAsync();  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>>> GetCrossCollateralRepayHistoryAsync(string? asset = default, DateTime? startTime = default, DateTime? endTime = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|The asset to get history for|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralWalletAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-wallet-v2-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-wallet-v2-user_data)  
<p>

*Get cross-collateral wallet info*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetCrossCollateralWalletAsync();  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralWallet>> GetCrossCollateralWalletAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFuturesTransferHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-future-account-transaction-history-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-future-account-transaction-history-list-user_data)  
<p>

*Get history of transfers between spot and futures account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.GetFuturesTransferHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = default, int? page = default, int? limit = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to get history for|
|startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|The page to return|
|_[Optional]_ limit|The page size|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferFuturesAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#new-future-account-transfer-user_data](https://binance-docs.github.io/apidocs/spot/en/#new-future-account-transfer-user_data)  
<p>

*Execute a transfer between the spot account and a futures account*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Futures.TransferFuturesAccountAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceTransaction>> TransferFuturesAccountAsync(string asset, decimal quantity, FuturesTransferType transferType, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to transfer|
|quantity|Quantity to transfer|
|transferType|The transfer direction|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>
