---
title: Rest API documentation
has_children: true
---
*[generated documentation]*  
### BinanceRestClient  
*Client for accessing the Binance Rest API.*
  
***
*Coin futures API endpoints*  
**[IBinanceRestClientCoinFuturesApi](CoinFuturesApi/IBinanceRestClientCoinFuturesApi.html) CoinFuturesApi { get; }**  
***
*General API endpoints*  
**[IBinanceRestClientGeneralApi](GeneralApi/IBinanceRestClientGeneralApi.html) GeneralApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(ApiCredentials credentials);**  
***
*Spot API endpoints*  
**[IBinanceRestClientSpotApi](SpotApi/IBinanceRestClientSpotApi.html) SpotApi { get; }**  
***
*Usd futures API endpoints*  
**[IBinanceRestClientUsdFuturesApi](UsdFuturesApi/IBinanceRestClientUsdFuturesApi.html) UsdFuturesApi { get; }**  
