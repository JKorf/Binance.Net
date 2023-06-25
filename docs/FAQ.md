---
title: FAQ
nav_order: 4
---

See also [CryptoExchange.Net FAQ](https://jkorf.github.io/CryptoExchange.Net/FAQ.html)

### The user data stream stops sending updates after x time 
You're probably not calling the KeepAliveUserStreamAsync periodically to keep the user stream alive.

### Does this library support the testnet / Binance.us / any other variant  

Yes, as long as the API endpoints are the same. Switch by changing the environment in the client options

### Timestamp for this request was 1000ms ahead of the server's time / Timestamp for this request is outside of the recvWindow.  

See [Timestamping](https://jkorf.github.io/CryptoExchange.Net/Timestamping.html).

