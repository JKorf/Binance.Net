# Binance.Net usage

These snippets use the imports below. Account and trading snippets assume an authenticated `client`. See [API surfaces](api-surfaces.md) for method routing and [operational considerations](safety.md) for trading and retry decisions.

```csharp
using System;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
```

## Authentication

Public calls can use `new BinanceRestClient()`. Private REST calls can use HMAC credentials:

```csharp
using var client = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});
```

The strings are placeholders. `BinanceCredentials` also supports `WithHMAC`, `WithRSAXml`, `WithRSAPem` on compatible targets, and `WithEd25519` on .NET 8 or later. Select a key format supported by the endpoint and target framework; see [credentials](safety.md#credentials).

## REST results

```csharp
using var client = new BinanceRestClient();
var ticker = await client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    Console.WriteLine(ticker.Error);
    return;
}

Console.WriteLine(ticker.Data.LastPrice);
```

On failure, diagnostic fields include `ErrorCode`, `Message`, `ErrorType`, and `IsTransient`. See [retry decisions](safety.md#retry-decisions) before adding retries.

## Account and spot orders

Read balances with `client.SpotApi.Account.GetAccountInfoAsync()`; its result has `Data.Balances` containing `Asset`, `Available`, and `Locked`.

```csharp
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: SpotOrderType.Limit,
    quantity: 0.001m,
    price: 50000m,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.Id);
```

Query or cancel with `GetOrderAsync(symbol, orderId: ...)` or `CancelOrderAsync(symbol, orderId: ...)`. Inspect each result. The [API map](api-surfaces.md#spot-rest) lists related operations.

## USD-M futures

```csharp
const string symbol = "ETHUSDT";
var leverage = await client.UsdFuturesApi.Account.ChangeInitialLeverageAsync(symbol, 5);
if (!leverage.Success)
{
    Console.WriteLine(leverage.Error);
    return;
}

var order = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: FuturesOrderType.Market,
    quantity: 0.01m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

Console.WriteLine(order.Data.Id);
```

The example assumes one-way mode. In hedge mode specify `PositionSide.Long` or `PositionSide.Short` and use endpoint-supported close parameters. Margin configuration uses `ChangeMarginTypeAsync(symbol, FuturesMarginType.Isolated)`. For COIN-M select `CoinFuturesApi` and verify contract quantity units. Check the account's position mode before constructing a close; see [trading and account changes](safety.md#trading-and-account-changes).

## WebSocket lifecycle

```csharp
using var socket = new BinanceSocketClient();
var subscription = await socket.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}

try
{
    Console.ReadLine(); // Replace with the application's lifetime.
}
finally
{
    await socket.UnsubscribeAsync(subscription.Data);
}
```

For klines, `SubscribeToKlineUpdatesAsync(symbol, interval, handler)` delivers the candle at `update.Data.Data`; `Final` indicates a closed candle. Use `ExchangeData` for native market-data subscriptions. See [futures socket routing](api-surfaces.md#futures-websocket) and [private streams](api-surfaces.md#private-streams) for account subscriptions.

## Shared API V2

```csharp
using var client = new BinanceRestClient();
IGetTickerRest ticker = client.SpotApi.SharedApi;
var result = await ticker.GetTickerAsync(
    new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "BTC", "USDT")));

if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

For aggregate properties and runtime capability lookup, see [Shared APIs](api-surfaces.md#shared-api-v2).

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddBinance();
```

For private calls, configure `options.ApiCredentials` in the `AddBinance(options => ...)` callback with the credentials shown in [authentication](#authentication). Inject `IBinanceRestClient`, `IBinanceSocketClient`, or `IBinanceSharedApiClient`.

## Complete examples in a source checkout

These links require the repository checkout; the snippets above are available in the packaged skill itself.

- [Spot quickstart](../../../../Examples/ai-friendly/01-spot-quickstart.cs)
- [Futures](../../../../Examples/ai-friendly/02-futures.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Multi-exchange](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
