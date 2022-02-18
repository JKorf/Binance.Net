---
title: IBinanceClientSpotApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi`  
*Binance Spot API endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**[IBinanceClientSpotApiAccount](IBinanceClientSpotApiAccount.html) Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**[IBinanceClientSpotApiExchangeData](IBinanceClientSpotApiExchangeData.html) ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**[IBinanceClientSpotApiTrading](IBinanceClientSpotApiTrading.html) Trading { get; }**  
