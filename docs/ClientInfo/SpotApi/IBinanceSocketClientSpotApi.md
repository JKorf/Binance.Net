---
title: IBinanceSocketClientSpotApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi`  
*Spot API socket subscriptions and requests*
  
***
*Account streams and queries*  
**[IBinanceSocketClientSpotApiAccount](IBinanceSocketClientSpotApiAccount.html) Account { get; }**  
***
*Exchange data streams and queries*  
**[IBinanceSocketClientSpotApiExchangeData](IBinanceSocketClientSpotApiExchangeData.html) ExchangeData { get; }**  
***
*Set the API credentials for this API*  
**void SetApiCredentials<T>(T credentials) where T : ApiCredentials;**  
***
*Factory for websockets*  
**IWebsocketFactory SocketFactory { get; set; }**  
***
*Trading data and queries*  
**[IBinanceSocketClientSpotApiTrading](IBinanceSocketClientSpotApiTrading.html) Trading { get; }**  
