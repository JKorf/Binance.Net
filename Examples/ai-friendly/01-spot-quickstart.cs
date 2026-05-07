// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated balance,
// limit order placement, order status check.
//
// Setup:
//   dotnet new console -n SpotQuickstart && cd SpotQuickstart
//   dotnet add package Binance.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below
//   dotnet run

using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application — do not create per-request.
var publicClient = new BinanceRestClient();

var ticker = await publicClient.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
if (!ticker.Success)
{
    // .Error contains Code, Message, and may include exchange-specific data
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"BTC/USDT last price: {ticker.Data.LastPrice}");
Console.WriteLine($"24h volume: {ticker.Data.Volume} BTC");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});

// Get all balances — empty .Free / .Locked balances are filtered out by Binance
var account = await tradingClient.SpotApi.Account.GetAccountInfoAsync();
if (!account.Success)
{
    Console.WriteLine($"Failed to get account: {account.Error}");
    return;
}

foreach (var balance in account.Data.Balances.Where(b => b.Total > 0))
{
    Console.WriteLine($"{balance.Asset}: {balance.Available} free, {balance.Locked} locked");
}

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Limit, Buy, 0.001 BTC at a price 5% below current — likely will not fill immediately.
// Note: the library auto-generates a clientOrderId for you. Don't pass one manually
// unless you have a specific operational need — auto-generated IDs are well-formed
// and avoid format-related order rejections.
var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);

var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: SpotOrderType.Limit,
    quantity: 0.001m,
    price: safePrice,
    timeInForce: TimeInForce.GoodTillCanceled);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed order {order.Data.Id} at {safePrice}, status: {order.Data.Status}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync("BTCUSDT", order.Data.Id);
if (status.Success)
{
    Console.WriteLine($"Order status: {status.Data.Status}, filled: {status.Data.QuantityFilled}");
}

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync("BTCUSDT", order.Data.Id);
if (cancel.Success)
{
    Console.WriteLine($"Cancelled order {order.Data.Id}");
}

// Common variations:
//   Market order:   type: SpotOrderType.Market, omit price and timeInForce
//   Stop-loss:      type: SpotOrderType.StopLoss, add stopPrice parameter
//   Quote-currency quantity: use quoteQuantity parameter instead of quantity (market orders)
//   OCO (One-Cancels-Other): use tradingClient.SpotApi.Trading.PlaceOcoOrderAsync(...)
