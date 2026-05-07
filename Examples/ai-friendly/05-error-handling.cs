// 05-error-handling.cs
//
// Demonstrates: WebCallResult patterns, retry logic, common error scenarios.
//
// Setup: dotnet add package Binance.Net

using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects;

var client = new BinanceRestClient(options =>
{
    options.ApiCredentials = new BinanceCredentials("API_KEY", "API_SECRET");
});

// ---- 1. THE BASIC PATTERN ----
// Every method returns WebCallResult<T> (REST) or CallResult<T> (WebSocket).
// .Success is true/false. .Data is the payload (only valid when .Success).
// .Error contains structured error info when .Success is false.
// .Error.IsTransient hints if a retry might succeed (rate limit, network, 5xx).

var result = await client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.LastPrice}");
}
else
{
    Console.WriteLine($"Code:    {result.Error.Code}");
    Console.WriteLine($"Message: {result.Error.Message}");
    Console.WriteLine($"Type:    {result.Error.ErrorType}");        // typed enum
    Console.WriteLine($"Transient: {result.Error.IsTransient}");    // retry hint
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors (rate limit, network blip, server overload).
// Do not retry on validation errors or insufficient balance — they will repeat.

async Task<WebCallResult<T>> WithRetry<T>(
    Func<Task<WebCallResult<T>>> call,
    int maxAttempts = 3)
{
    WebCallResult<T> last = default!;
    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success) return last;
        if (!last.Error!.IsTransient) return last;

        // Exponential backoff: 0.5s, 1s, 2s
        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }
    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT"));

// ---- 3. COMMON BINANCE ERROR SCENARIOS ----
//
// Code -1003 ("Too many requests"):
//   Rate limit hit. Binance.Net has built-in client-side rate limiting,
//   but bursts during reconnection can still hit server-side limits.
//   IsTransient=true. Retry with backoff. Check the Retry-After header (handled internally).
//
// Code -1021 ("Timestamp for this request is outside of the recvWindow"):
//   Local clock drifted from Binance server. Solution: enable AutoTimestamp (default on).
//   options.AutoTimestamp = true; options.TimestampRecalculationInterval = TimeSpan.FromHours(1);
//
// Code -2010 ("Account has insufficient balance for requested action"):
//   Permanent — do not retry. Surface to caller.
//
// Code -1013 ("Filter failure: PRICE_FILTER" / "LOT_SIZE" / "MIN_NOTIONAL"):
//   Order parameters violate symbol filters. Use ExchangeHelpers.ApplyRules
//   to round quantity/price to allowed steps before placing the order:
//
//      var symbols = await client.SpotApi.ExchangeData.GetExchangeInfoAsync();
//      var symInfo = symbols.Data.Symbols.First(s => s.Name == "BTCUSDT");
//      var lotSize = symInfo.LotSizeFilter!;
//      var validQty = ExchangeHelpers.AdjustValueStep(
//          lotSize.MinQuantity, lotSize.MaxQuantity, lotSize.StepSize, RoundingType.Down, rawQty);
//
// Code -1022 ("Signature for this request is not valid"):
//   API key / secret mismatch, or trying to use Spot key for Futures endpoint.
//   Permanent — fix credentials.
//
// Code -2011 ("Unknown order"):
//   Order ID does not exist or already cancelled / filled. May be expected.

// ---- 4. ORDER PLACEMENT WITH FILTER VALIDATION ----
var exchangeInfo = await client.SpotApi.ExchangeData.GetExchangeInfoAsync("BTCUSDT");
if (!exchangeInfo.Success || exchangeInfo.Data.Symbols.Length == 0)
{
    Console.WriteLine("Cannot fetch symbol info — aborting order");
    return;
}

var symbol = exchangeInfo.Data.Symbols.First();
decimal rawQuantity = 0.00123456m;

// Round to allowed step size to avoid -1013 LOT_SIZE error
decimal validQuantity = symbol.LotSizeFilter != null
    ? CryptoExchange.Net.ExchangeHelpers.AdjustValueStep(
        symbol.LotSizeFilter.MinQuantity,
        symbol.LotSizeFilter.MaxQuantity,
        symbol.LotSizeFilter.StepSize,
        RoundingType.Down,
        rawQuantity)
    : rawQuantity;

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTCUSDT",
    side: OrderSide.Buy,
    type: SpotOrderType.Market,
    quantity: validQuantity);

if (!order.Success)
{
    // Distinguish error categories for the caller
    string category = order.Error!.IsTransient
        ? "Transient — should retry"
        : "Permanent — surface to user";

    Console.WriteLine($"{category}: {order.Error.Code} {order.Error.Message}");
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// Binance.Net returns errors via WebCallResult.Error, NOT via thrown exceptions.
// Exceptions are thrown only for:
//   - Misconfiguration (e.g., disposed client, missing credentials when required)
//   - OperationCanceledException when CancellationToken is triggered
//   - Programmer errors (null arguments, etc.)
// Network errors, rate limits, API errors → all on .Error.

// Common variations:
//   With CancellationToken:    pass `ct: cancellationToken` to any method
//   With timeout per request:  options.RequestTimeout = TimeSpan.FromSeconds(10);
//   Polly integration:         use IsTransient as the IsTransientPredicate
