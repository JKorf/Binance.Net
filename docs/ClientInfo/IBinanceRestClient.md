---
title: IBinanceRestClient
has_children: true
---
*[generated documentation]*  
### BinanceClient  
*Client for accessing the Binance Rest API.*
  
***
*Coin futures API endpoints*  
**IBinanceRestClientCoinFuturesApi CoinFuturesApi { get; }**  
***
*General API endpoints*  
**IBinanceRestClientGeneralApi GeneralApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(ApiCredentials credentials);**  
***
*Spot API endpoints*  
**IBinanceRestClientSpotApi SpotApi { get; }**  
***
*Usd futures API endpoints*  
**IBinanceRestClientUsdFuturesApi UsdFuturesApi { get; }**  
