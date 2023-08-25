---
title: IBinanceRestClientGeneralApiMining
has_children: false
parent: IBinanceRestClientGeneralApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceRestClient > GeneralApi > Mining`  
*Binance Spot Mining endpoints*
  

***

## CancelHashrateResaleRequestAsync  

[https://binance-docs.github.io/apidocs/spot/en/#cancel-hashrate-resale-configuration-user_data](https://binance-docs.github.io/apidocs/spot/en/#cancel-hashrate-resale-configuration-user_data)  
<p>

*Cancel Hashrate Resale Configuration*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.CancelHashrateResaleRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<bool>> CancelHashrateResaleRequestAsync(int configId, string userName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|configId|Mining id|
|userName|Mining account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHashrateResaleDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-detail-user_data](https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-detail-user_data)  
<p>

*Gets hash rate resale details*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetHashrateResaleDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceHashrateResaleDetails>> GetHashrateResaleDetailsAsync(int configId, string userName, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|configId|The mining id|
|userName|Mining account|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHashrateResaleListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-list-user_data)  
<p>

*Gets hash rate resale list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetHashrateResaleListAsync();  
```  

```csharp  
Task<WebCallResult<BinanceHashrateResaleList>> GetHashrateResaleListAsync(int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMinerDetailsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#request-for-detail-miner-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#request-for-detail-miner-list-user_data)  
<p>

*Gets miner details*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMinerDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMinerDetails>>> GetMinerDetailsAsync(string algorithm, string userName, string workerName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|Mining account|
|workerName|Miners name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMinerListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#request-for-miner-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#request-for-miner-list-user_data)  
<p>

*Gets miner list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMinerListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMinerList>> GetMinerListAsync(string algorithm, string userName, int? page = default, bool? sortAscending = default, string? sortColumn = default, MinerStatus? workerStatus = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|Mining account|
|_[Optional]_ page|Result page|
|_[Optional]_ sortAscending|Sort in ascending order|
|_[Optional]_ sortColumn|Column to sort by|
|_[Optional]_ workerStatus|Filter by status|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningAccountEarningsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#mining-account-earning-user_data](https://binance-docs.github.io/apidocs/spot/en/#mining-account-earning-user_data)  
<p>

*Get mining account earnings*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningAccountEarningsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMiningEarnings>> GetMiningAccountEarningsAsync(string algo, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algo|Algorithm|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningAccountListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#account-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#account-list-user_data)  
<p>

*Gets mining account list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningAccountListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMiningAccount>>> GetMiningAccountListAsync(string algorithm, string userName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|Mining account user name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningAlgorithmListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#acquiring-algorithm-market_data](https://binance-docs.github.io/apidocs/spot/en/#acquiring-algorithm-market_data)  
<p>

*Gets mining algorithms info*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningAlgorithmListAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMiningAlgorithm>>> GetMiningAlgorithmListAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningCoinListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#acquiring-coinname-market_data](https://binance-docs.github.io/apidocs/spot/en/#acquiring-coinname-market_data)  
<p>

*Gets mining coins info*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningCoinListAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<BinanceMiningCoin>>> GetMiningCoinListAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningOtherRevenueListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#extra-bonus-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#extra-bonus-list-user_data)  
<p>

*Get other revenue list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningOtherRevenueListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceOtherRevenueList>> GetMiningOtherRevenueListAsync(string algorithm, string userName, string? coin = default, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|Mining account|
|_[Optional]_ coin|Coin|
|_[Optional]_ startDate|Start date|
|_[Optional]_ endDate|End date|
|_[Optional]_ page|Result page|
|_[Optional]_ pageSize|Results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningRevenueListAsync  

[https://binance-docs.github.io/apidocs/spot/en/#earnings-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#earnings-list-user_data)  
<p>

*Gets revenue list*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningRevenueListAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceRevenueList>> GetMiningRevenueListAsync(string algorithm, string userName, string? coin = default, DateTime? startDate = default, DateTime? endDate = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|Mining account|
|_[Optional]_ coin|Coin|
|_[Optional]_ startDate|Start date|
|_[Optional]_ endDate|End date|
|_[Optional]_ page|Result page|
|_[Optional]_ pageSize|Results per page|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMiningStatisticsAsync  

[https://binance-docs.github.io/apidocs/spot/en/#statistic-list-user_data](https://binance-docs.github.io/apidocs/spot/en/#statistic-list-user_data)  
<p>

*Get mining statistics*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.GetMiningStatisticsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<BinanceMiningStatistic>> GetMiningStatisticsAsync(string algorithm, string userName, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|algorithm|Algorithm|
|userName|User name|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceHashrateResaleRequestAsync  

[https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-request-user_data](https://binance-docs.github.io/apidocs/spot/en/#hashrate-resale-request-user_data)  
<p>

*Hashrate resale request*  

```csharp  
var client = new BinanceRestClient();  
var result = await client.GeneralApi.Mining.PlaceHashrateResaleRequestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<int>> PlaceHashrateResaleRequestAsync(string userName, string algorithm, DateTime startDate, DateTime endDate, string toUser, decimal hashRate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|userName|Mining account|
|algorithm|Transfer algorithm|
|startDate|Resale start time|
|endDate|Resale end time|
|toUser|To mining account|
|hashRate|Resale hashrate h/s must be transferred (BTC is greater than 500000000000 ETH is greater than 500000)|
|_[Optional]_ ct|Cancellation token|

</p>
