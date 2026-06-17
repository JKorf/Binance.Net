// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions — public ticker, klines,
// authenticated user data stream. Includes proper teardown.
//
// Setup: dotnet add package Binance.Net

using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;

// ---- 1. PUBLIC SOCKET CLIENT — for market data streams ----
// Reuse a single client instance across all subscriptions.
// The client manages connection pooling (multiple subscriptions on one socket
// when possible, splits across sockets when symbol limits are hit).
var publicSocket = new BinanceSocketClient();

// Subscription methods return WebSocketResult<UpdateSubscription>.
// Subscribe to ticker updates — fires ~1/second for active symbols
var tickerSub = await publicSocket.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update =>
    {
        // The handler is called on a background thread.
        // Keep it fast — offload heavy work to a queue or channel.
        Console.WriteLine($"BTC: {update.Data.LastPrice} (24h vol {update.Data.Volume:F2})");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

// Subscribe to 1-minute klines (candlesticks) — fires on each update,
// final update for an interval has IsFinal == true
var klineSub = await publicSocket.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    KlineInterval.OneMinute,
    update =>
    {
        var k = update.Data.Data;
        if (k.Final)
        {
            Console.WriteLine($"ETH 1m closed: O={k.OpenPrice} H={k.HighPrice} L={k.LowPrice} C={k.ClosePrice}");
        }
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT — for user data ----
// User data stream pushes order updates, balance changes, position updates.
var authSocket = new BinanceSocketClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});

var userSub = await authSocket.SpotApi.Account.SubscribeToUserDataUpdatesAsync(
    onOrderUpdateMessage: update =>
    {
        var o = update.Data;
        Console.WriteLine($"Order {o.Id} {o.Symbol}: {o.Status} (filled {o.QuantityFilled}/{o.Quantity})");
    },
    onAccountPositionMessage: update =>
    {
        // Triggered when account asset balance changes
        foreach (var b in update.Data.Balances)
            Console.WriteLine($"Balance update {b.Asset}: free={b.Available} locked={b.Locked}");
    },
    onAccountBalanceUpdate: update =>
    {
        // Triggered specifically for deposits / withdrawals
        Console.WriteLine($"Asset {update.Data.Asset} delta: {update.Data.BalanceDelta}");
    });

if (!userSub.Success)
{
    Console.WriteLine($"Failed to subscribe user data: {userSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 3. TEARDOWN — IMPORTANT! ----
// Always unsubscribe on shutdown to release resources cleanly.
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await authSocket.UnsubscribeAsync(userSub.Data);

// Or unsubscribe everything at once on the client:
// await publicSocket.UnsubscribeAllAsync();
// await authSocket.UnsubscribeAllAsync();

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Multiple symbols at once:    SubscribeToTickerUpdatesAsync(new[] { "BTCUSDT", "ETHUSDT" }, handler)
//   Order book stream:           SubscribeToPartialOrderBookUpdatesAsync(symbol, levels, ...)
//   Aggregated trades:           SubscribeToAggregatedTradeUpdatesAsync(symbol, handler)
//   Futures user data:           authSocket.UsdFuturesApi.Account.SubscribeToUserDataUpdatesAsync(...)
//   Reconnection events:         tickerSub.Data.ConnectionLost / ConnectionRestored
