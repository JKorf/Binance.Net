---
title: Socket API documentation
has_children: true
---
*[generated documentation]*  
### BinanceSocketClient  
*Client for accessing the Binance websocket API*
  
***
*Coin futures streams*  
**[IBinanceSocketClientCoinFuturesApi](CoinFuturesApi/IBinanceSocketClientCoinFuturesApi.html) CoinFuturesApi { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(ApiCredentials credentials);**  
***
*Spot streams and requests*  
**[IBinanceSocketClientSpotApi](SpotApi/IBinanceSocketClientSpotApi.html) SpotApi { get; }**  
***
*Usd futures streams*  
**[IBinanceSocketClientUsdFuturesApi](UsdFuturesApi/IBinanceSocketClientUsdFuturesApi.html) UsdFuturesApi { get; }**  
