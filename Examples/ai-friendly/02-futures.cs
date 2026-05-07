// 02-futures.cs
//
// Demonstrates: USD-M futures — set leverage, place market order,
// retrieve open position, close position.
//
// Setup: dotnet add package Binance.Net
// Substitute API_KEY / API_SECRET. The API key must have Futures trading enabled.

using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;

var client = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETHUSDT";

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
// PositionSide.Long required if account is in Hedge mode; ignore if One-way mode.
var openOrder = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: FuturesOrderType.Market,
    quantity: 0.01m,
    positionSide: PositionSide.Long);

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

var position = positions.Data.FirstOrDefault(p => p.Quantity != 0);
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
    positionSide: PositionSide.Long,
    reduceOnly: true);

if (closeOrder.Success)
{
    Console.WriteLine($"Closed position via order {closeOrder.Data.Id}");
}

// Common variations:
//   Limit order:        type: FuturesOrderType.Limit, add price + timeInForce
//   Stop-market:        type: FuturesOrderType.StopMarket, add stopPrice
//   Take-profit:        type: FuturesOrderType.TakeProfitMarket, add stopPrice
//   COIN-M futures:     use client.CoinFuturesApi.* (same API surface)
//   Hedge vs One-way:   client.UsdFuturesApi.Account.ModifyPositionModeAsync(...)
//   Margin type:        client.UsdFuturesApi.Account.ChangeMarginTypeAsync(symbol, FuturesMarginType.Isolated)
