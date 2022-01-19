---
title: Timestamping
nav_order: 4
---

## Timestamping

Requests to private endpoints on the Binance API are required to have a timestamp parameter. Binance will check to see if the received timestamp of a request is within a certain timespan vs the Binance server time. The timespan of how much the timestamp parameter is allowed to  differ can be specified using the `recvWindow` parameter on requests to private endpoints.

Because not all computers have exactly the same time this mechanism can cause errors. If for example the client time is 2 minutes earlier than the server time the server will think the request was sent 2 minutes ago since (server time on receive - message timestamp = 2 minutes). This will make Binance reject the message. 

To fix this the `AutoTimestamp` client options was introduced, which requests the server time and compares it to the local client time. The offset this produces will be used to offset the timestamp which is sent to the server in authenticated requests. This works 90% of the time.

Another option is to sync the operating system time more often. For Windows users have reported success using the SP TimeSync tool.
