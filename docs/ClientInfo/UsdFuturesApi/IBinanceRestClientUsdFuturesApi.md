---
title: IBinanceRestClientUsdFuturesApi
has_children: true
parent: IBinanceClientUsdFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > UsdFuturesApi > IBinanceRestClient`  
*Binance USD futures API endpoints*
  
***
*Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**IBinanceRestClientUsdFuturesApiAccount Account { get; }**  
***
*Endpoints related to retrieving market data*  
**IBinanceRestClientUsdFuturesApiExchangeData ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**IBinanceRestClientUsdFuturesApiTrading Trading { get; }**  
