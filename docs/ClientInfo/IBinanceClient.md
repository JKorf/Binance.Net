---
title: Rest API documentation
has_children: true
---
*[generated documentation]*  
### BinanceClient  
*Client for accessing the Binance Rest API.*
  
***
*Coin futures API endpoints*  
**[IBinanceClientCoinFuturesApi](CoinFuturesApi/IBinanceClientCoinFuturesApi.html) CoinFuturesApi { get; }**  
***
*General API endpoints*  
**[IBinanceClientGeneralApi](GeneralApi/IBinanceClientGeneralApi.html) GeneralApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(BinanceApiCredentials credentials);**  
***
*Spot API endpoints*  
**[IBinanceClientSpotApi](SpotApi/IBinanceClientSpotApi.html) SpotApi { get; }**  
***
*Usd futures API endpoints*  
**[IBinanceClientUsdFuturesApi](UsdFuturesApi/IBinanceClientUsdFuturesApi.html) UsdFuturesApi { get; }**  
