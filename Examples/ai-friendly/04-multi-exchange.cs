// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against Binance and other exchanges from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package Binance.Net
//   dotnet add package Bybit.Net  // optional, for comparison

using Binance.Net.Clients;
using CryptoExchange.Net.SharedApis;

// SharedApi exposes capabilities for individual operations.
using var binanceRest = new BinanceRestClient();
IGetTickerRest binanceShared = binanceRest.SpotApi.SharedApi;

var sharedInfo = binanceRest.SpotApi.SharedApi.Discover();
var supportedFeatures = sharedInfo.Features
    .Where(x => x.Supported)
    .Select(x => x.EndpointName);
Console.WriteLine($"{sharedInfo.Exchange} {sharedInfo.TypeName}: {string.Join(", ", supportedFeatures)}");

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(binanceShared, btcusdt);

async Task PrintTicker(IGetTickerRest client, SharedSymbol symbol)
{
    var result = await client.GetTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// Other capabilities include IGetSpotSymbolsRest, IPlaceSpotOrderRest,
// IPlaceFuturesOrderRest, IGetBalancesRest, and IGetOrderBookRest.
// Use the exchange aggregate for runtime-selected capability lookup.

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
using var binanceSocket = new BinanceSocketClient();
ISubscribeTickerSocket binanceTickerSocket = binanceSocket.SpotApi.SharedApi;

var sub = await binanceTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{binanceTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

try
{
    Console.WriteLine("Press Enter to exit");
    Console.ReadLine();
}
finally
{
    await binanceSocket.UnsubscribeAsync(sub.Data);
}

// Note: shared socket interfaces do not expose UnsubscribeAsync.
// Keep the concrete socket client and call concreteClient.UnsubscribeAsync(sub.Data).
