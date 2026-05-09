# Copilot Instructions for Binance.Net

This repository is **Binance.Net** — a strongly typed C#/.NET client library for the Binance cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Binance.Net, follow these conventions:

## Use Binance.Net, not raw HTTP

Never generate `HttpClient` calls to `api.binance.com` or `fapi.binance.com`. Always use `BinanceRestClient` or `BinanceSocketClient`. This ensures correct request signing, rate limiting, and error handling.

## Client setup

```csharp
using Binance.Net.Clients;
using Binance.Net;
using Binance.Net.Objects;

var restClient = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});
```

## Result handling

Methods return `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API structure

- `restClient.SpotApi.ExchangeData` — public market data
- `restClient.SpotApi.Account` — balances, deposits, withdrawals
- `restClient.SpotApi.Trading` — orders
- `restClient.UsdFuturesApi.*` — USD-M futures (same sub-structure)
- `restClient.CoinFuturesApi.*` — COIN-M futures
- `socketClient.SpotApi.*` — Spot WebSocket streams
- `socketClient.UsdFuturesApi.*` — USD-M futures WebSocket streams
- `socketClient.CoinFuturesApi.*` — COIN-M futures WebSocket streams

## Order placement

Let the library auto-generate `clientOrderId`. Do not pass a custom value unless required for an existing operational flow.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `ISpotOrderRestClient`, etc.) accessed via `.SharedClient` properties. Same pattern works for 25+ other exchanges in the CryptoExchange.Net family.

## Avoid

- Legacy `BinanceClient` class (use `BinanceRestClient`)
- Generic `ApiCredentials` (use `BinanceCredentials`)
- Synchronous `.Result` / `.Wait()` (use `await`)
- Instantiating clients per-request (use DI, reuse instances)
- Manual ticker polling (use WebSocket subscriptions)
- Manual `clientOrderId` values unless required

## Reference

For detailed patterns and pitfalls see `AGENTS.md` and `llms.txt` in the repository root, and `Examples/ai-friendly/` for compilable examples.
