---
title: IBinanceRestClientGeneralApiFutures
has_children: false
parent: IBinanceClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > GeneralApi > IBinanceRestClientFutures`  
*Binance futures interaction endpoints*
  

***

## AdjustCrossCollateralLoanToValueAsync  

[https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-v2-trade](https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-v2-trade)  
<p>

*Adjust cross collateral LTV*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.AdjustCrossCollateralLoanToValueAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralAdjustLtvResult>> AdjustCrossCollateralLoanToValueAsync(string collateralAsset, string loanAsset, decimal quantity, AdjustRateDirection direction, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|collateralAsset|The collateral asset|
|loanAsset|The loan asset|
|quantity|The quantity|
|direction|The direction|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## BorrowForCrossCollateralAsync  

[https://binance-docs.github.io/apidocs/spot/en/#borrow-for-cross-collateral-trade](https://binance-docs.github.io/apidocs/spot/en/#borrow-for-cross-collateral-trade)  
<p>

*Borrow for cross-collateral*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.BorrowForCrossCollateralAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralBorrowResult>> BorrowForCrossCollateralAsync(string asset, string collateralAsset, decimal? quantity = default, decimal? collateralQuantity = default, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset to borrow|
|collateralAsset|The asset to use as collateral|
|_[Optional]_ quantity|The quantity to borrow|
|_[Optional]_ collateralQuantity|The quantity of collateral asset to use|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAdjustCrossCollateralLoanToValueHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#adjust-cross-collateral-ltv-history-user_data)  
<p>

*Get cross collateral LTV adjustment history*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetAdjustCrossCollateralLoanToValueHistoryAsync();  
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
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetCrossCollateralBorrowHistoryAsync();  
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

## GetCrossCollateralInformationAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-information-v2-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-information-v2-user_data)  
<p>

*Get cross-collateral info*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetCrossCollateralInformationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceCrossCollateralInformation>>> GetCrossCollateralInformationAsync(long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossCollateralLiquidationHistoryAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-liquidation-history-user_data](https://binance-docs.github.io/apidocs/spot/en/#cross-collateral-liquidation-history-user_data)  
<p>

*Get cross collateral liquidation history*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetCrossCollateralLiquidationHistoryAsync();  
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
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetCrossCollateralRepayHistoryAsync();  
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
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetCrossCollateralWalletAsync();  
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
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetFuturesTransferHistoryAsync(/* parameters */);  
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

## GetMaxAmountForAdjustCrossCollateralLoanToValueAsync  

[https://binance-docs.github.io/apidocs/spot/en/#get-max-amount-for-adjust-cross-collateral-ltv-v2-user_data](https://binance-docs.github.io/apidocs/spot/en/#get-max-amount-for-adjust-cross-collateral-ltv-v2-user_data)  
<p>

*Get max quantity for adjust cross-collateral LTV*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetMaxAmountForAdjustCrossCollateralLoanToValueAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralAdjustMaxAmounts>> GetMaxAmountForAdjustCrossCollateralLoanToValueAsync(string collateralAsset, string loanAsset, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|collateralAsset|The collateral asset|
|loanAsset|The loan asset|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRateAfterAdjustLoanToValueAsync  

[https://binance-docs.github.io/apidocs/spot/en/#calculate-rate-after-adjust-cross-collateral-ltv-v2-user_data](https://binance-docs.github.io/apidocs/spot/en/#calculate-rate-after-adjust-cross-collateral-ltv-v2-user_data)  
<p>

*Calculate rate after adjust cross-collateral loan to value*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.GetRateAfterAdjustLoanToValueAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralAfterAdjust>> GetRateAfterAdjustLoanToValueAsync(string collateralAsset, string loanAsset, decimal quantity, AdjustRateDirection direction, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|collateralAsset|The collateral asset|
|loanAsset|The loan asset|
|quantity|The quantity|
|direction|The direction|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## RepayForCrossCollateralAsync  

[https://binance-docs.github.io/apidocs/spot/en/#repay-for-cross-collateral-trade](https://binance-docs.github.io/apidocs/spot/en/#repay-for-cross-collateral-trade)  
<p>

*Repay for cross-collateral*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.RepayForCrossCollateralAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceCrossCollateralRepayResult>> RepayForCrossCollateralAsync(string asset, string collateralAsset, decimal quantity, long? receiveWindow = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|asset|The asset|
|collateralAsset|The collateral asset to repay|
|quantity|The quantity to repay|
|_[Optional]_ receiveWindow|The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request|
|_[Optional]_ ct|Cancellation token|

</p>

***

## TransferFuturesAccountAsync  

[https://binance-docs.github.io/apidocs/spot/en/#new-future-account-transfer-user_data](https://binance-docs.github.io/apidocs/spot/en/#new-future-account-transfer-user_data)  
<p>

*Execute a transfer between the spot account and a futures account*  

```csharp  
var client = new BinanceClient();  
var result = await client.GeneralApi.IBinanceRestClientFutures.TransferFuturesAccountAsync(/* parameters */);  
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
