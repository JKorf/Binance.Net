---
title: Socket API documentation
has_children: true
---
*[generated documentation]*  
### BinanceSocketClient  
*Client for accessing the Binance websocket API*
  
***
*Coin futures streams*  
**[IBinanceSocketClientCoinFuturesStreams](CoinFuturesApi/IBinanceSocketClientCoinFuturesStreams.html) CoinFuturesStreams { get; }**  
***
*Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.*  
**void SetApiCredentials(BinanceApiCredentials credentials);**  
***
*Spot streams*  
**[IBinanceSocketClientSpotStreams](SpotApi/IBinanceSocketClientSpotStreams.html) SpotStreams { get; }**  
***
*Usd futures streams*  
**[IBinanceSocketClientUsdFuturesStreams](UsdFuturesApi/IBinanceSocketClientUsdFuturesStreams.html) UsdFuturesStreams { get; }**  
