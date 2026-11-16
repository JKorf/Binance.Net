---
name: binance-net
description: Build C#/.NET integrations using Binance.Net for Binance spot, margin, USD-M and COIN-M futures, REST, WebSockets, authentication, account operations, trading, and Shared APIs. Use when writing or updating code that consumes the library.
---

# Binance.Net

Use `BinanceRestClient` and `BinanceSocketClient` for Binance exchange access. They provide signing, rate limiting, result handling, and subscription management.

## Setup

```sh
dotnet add package Binance.Net
```

Use the API provided by the installed package. Choose `SpotApi`, `UsdFuturesApi`, or `CoinFuturesApi` for trading and `GeneralApi` for additional REST products. Margin endpoints live under `SpotApi.Account` and `SpotApi.Trading`.

## Consumer conventions

- Public market data needs no credentials. Private calls use `BinanceCredentials`; see [authentication](references/usage.md#authentication) for HMAC and other supported key formats.
- Spot and USD-M symbols commonly use compact names such as `BTCUSDT`. Read exchange info for COIN-M contract names and quantities.
- Spot orders use `SpotOrderType`; futures use `FuturesOrderType`. Position mode, quantity units, and conditional-order endpoints differ by product.
- REST calls return `HttpResult<T>` or `HttpResult`. Subscriptions return `WebSocketResult<UpdateSubscription>`; native socket API queries return `QueryResult<T>`, often with a `BinanceResponse<T>` wrapper. Shared non-I/O helpers can return `ExchangeCallResult<T>`. Check `Success` before accessing `Data` and inspect the declared payload type.
- Reuse clients, await asynchronous methods, and pass cancellation tokens through the workflow.
- Keep socket callbacks short and unsubscribe through the concrete socket client on shutdown. Account-stream setup differs between spot, margin, and futures; see [private streams](references/api-surfaces.md#private-streams).
- Use exchange filters to choose valid quantities and prices. Let the library generate client order IDs unless external correlation is needed.
- For portable multi-exchange code, use a surface's `SharedApi` and narrow capability interfaces. Use `IBinanceSharedApiClient` for multiple surfaces or dynamic capability lookup.

## Task references

Read only the references needed for the task:

- [API surfaces](references/api-surfaces.md): Client roots, endpoint routing, environments, Shared API capabilities, and symbol catalogs.
- [Usage](references/usage.md): Authentication, results, account reads, trading, subscriptions, and dependency injection examples.
- [Operational considerations](references/safety.md): Key permissions, filters, position modes, ambiguous order outcomes, and retry decisions.
