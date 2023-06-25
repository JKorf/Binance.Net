---
title: IBinanceRestClientSpotApi
has_children: true
parent: IBinanceClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > SpotApi > IBinanceRestClient`  
*Binance Spot API endpoints*
  
***
*Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**ISpotClient CommonSpotClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**IBinanceRestClientSpotApiAccount Account { get; }**  
***
*Endpoints related to retrieving market and system data*  
**IBinanceRestClientSpotApiExchangeData ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**IBinanceRestClientSpotApiTrading Trading { get; }**  
