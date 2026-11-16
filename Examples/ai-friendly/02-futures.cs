// 02-futures.cs
//
// Demonstrates: USD-M futures — set leverage, place market order,
// retrieve open position, close position.
//
// Setup: dotnet add package Binance.Net
// Substitute API_KEY / API_SECRET. The API key must have Futures trading enabled.

using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;

var client = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETHUSDT";

// This opening/closing recipe uses one-way mode. Do not change the account's
// position mode automatically just to run an example.
var mode = await client.UsdFuturesApi.Account.GetPositionModeAsync();
if (!mode.Success)
{
    Console.WriteLine($"Failed to get position mode: {mode.Error}");
    return;
}
if (mode.Data.IsHedgeMode)
{
    Console.WriteLine("This example requires one-way mode; adapt both order legs for hedge mode.");
    return;
}

// ---- 1. SET LEVERAGE ----
// Leverage is per-symbol, persists across orders. Max varies by symbol and account tier.
var leverage = await client.UsdFuturesApi.Account.ChangeInitialLeverageAsync(symbol, 5);
if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}
Console.WriteLine($"Leverage set to {leverage.Data.Leverage}x for {symbol}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
// Market order — fills immediately at best available price.
// Hedge-mode opening and closing orders require a different parameter combination.
var openOrder = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: FuturesOrderType.Market,
    quantity: 0.01m);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}
Console.WriteLine($"Opened position via order {openOrder.Data.Id}");

// ---- 3. GET CURRENT POSITION ----
// PositionsAsync returns all positions — filter by symbol and non-zero quantity.
var positions = await client.UsdFuturesApi.Account.GetPositionInformationAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

var position = positions.Data.FirstOrDefault(p => p.Quantity > 0);
if (position == null)
{
    Console.WriteLine("No open position found (may not have filled yet).");
    return;
}

Console.WriteLine($"Position: {position.Quantity} {symbol} at avg {position.EntryPrice}");
Console.WriteLine($"Unrealized PnL: {position.UnrealizedPnl} USDT");
Console.WriteLine($"Liquidation price: {position.LiquidationPrice}");

// ---- 4. CLOSE THE POSITION ----
// Opposite side, same quantity, reduceOnly=true to ensure no accidental position flip.
var closeOrder = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Sell,
    type: FuturesOrderType.Market,
    quantity: Math.Abs(position.Quantity),
    reduceOnly: true);

if (closeOrder.Success)
{
    Console.WriteLine($"Submitted close order {closeOrder.Data.Id}");
}
else
{
    Console.WriteLine($"Failed to close position: {closeOrder.Error}");
}

// Common variations:
//   Limit order:        type: FuturesOrderType.Limit, add price + timeInForce
//   Stop-market:        type: FuturesOrderType.StopMarket, add stopPrice
//   Take-profit:        type: FuturesOrderType.TakeProfitMarket, add stopPrice
//   COIN-M futures:     use client.CoinFuturesApi.* (same API surface)
//   Hedge mode:         adapt both order legs and close parameters; this sample exits if enabled
//   Margin type:        client.UsdFuturesApi.Account.ChangeMarginTypeAsync(symbol, FuturesMarginType.Isolated)
