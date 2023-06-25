---
title: IBinanceRestClientCoinFuturesApi
has_children: true
parent: IBinanceClientCoinFuturesApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`BinanceClient > CoinFuturesApi > IBinanceRestClient`  
*Binance Coin futures API endpoints*
  
***
*Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.*  
**IFuturesClient CommonFuturesClient { get; }**  
***
*Endpoints related to account settings, info or actions*  
**IBinanceRestClientCoinFuturesApiAccount Account { get; }**  
***
*Endpoints related to retrieving market data*  
**IBinanceRestClientCoinFuturesApiExchangeData ExchangeData { get; }**  
***
*Endpoints related to orders and trades*  
**IBinanceRestClientCoinFuturesApiTrading Trading { get; }**  
