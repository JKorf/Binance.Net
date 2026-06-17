// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against Binance, OKX, Bybit, Kraken, and 25+ other exchanges
// from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package Binance.Net
//   dotnet add package JK.OKX.Net    // optional, for the OKX example
//   dotnet add package Bybit.Net     // optional, for the Bybit example

using Binance.Net.Clients;
using CryptoExchange.Net.SharedApis;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on its API surfaces.
// SharedClient implements interfaces like ISpotTickerRestClient, ISpotOrderRestClient,
// IBalanceRestClient, etc. — a common abstraction across all exchanges.
// Call SharedClient.Discover() before routing optional shared features.

var binanceRest = new BinanceRestClient();
ISpotTickerRestClient binanceShared = binanceRest.SpotApi.SharedClient;

var sharedInfo = binanceRest.SpotApi.SharedClient.Discover();
var supportedFeatures = sharedInfo.Features
    .Where(x => x.Supported)
    .Select(x => x.EndpointName);
Console.WriteLine($"{sharedInfo.Exchange} {sharedInfo.TypeName}: {string.Join(", ", supportedFeatures)}");

// To add OKX or Bybit, install the package and:
//   ISpotTickerRestClient okxShared    = new OKXRestClient().UnifiedApi.SharedClient;
//   ISpotTickerRestClient bybitShared  = new BybitRestClient().V5Api.SharedClient;

// Common symbol type — handles formatting differences between exchanges automatically.
// Binance uses "BTCUSDT", OKX uses "BTC-USDT", others may differ. SharedSymbol normalizes.
var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

// Get ticker — same call signature across exchanges.
await PrintTicker(binanceShared, btcusdt);
// await PrintTicker(okxShared, btcusdt);
// await PrintTicker(bybitShared, btcusdt);

// ---- AGNOSTIC METHOD — works against any exchange ----
async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- WHY THIS MATTERS ----
// You can build:
//   - Multi-exchange arbitrage scanners
//   - Best-execution routers
//   - Unified portfolio dashboards
//   - Exchange comparison tools
// without writing per-exchange branches everywhere.

// ---- AVAILABLE SHARED INTERFACES ----
// REST:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   ISpotOrderClientIdRestClient, ISpotTriggerOrderRestClient
//   IFuturesOrderRestClient, IFuturesSymbolRestClient, IFuturesTpSlRestClient
//   IBalanceRestClient, IPositionRestClient, IFeeRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, IKlineRestClient
//   IDepositRestClient, IWithdrawalRestClient, ITransferRestClient
//   IBookTickerRestClient
// WebSocket:
//   ITickerSocketClient, IBookTickerSocketClient
//   IOrderBookSocketClient, ITradeSocketClient, IKlineSocketClient
//   IUserTradeSocketClient, IBalanceSocketClient, ISpotOrderSocketClient,
//   IFuturesOrderSocketClient

// ---- WEBSOCKET EXAMPLE — SHARED SUBSCRIPTION ----
var binanceSocket = new BinanceSocketClient();
ITickerSocketClient binanceTickerSocket = binanceSocket.SpotApi.SharedClient;

var sub = await binanceTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{binanceTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await binanceSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Multi-exchange arbitrage:  loop over List<ISpotTickerRestClient>, find max bid / min ask
//   Cross-exchange orderbook:  IOrderBookSocketClient on each exchange, merge into composite book
//   Best execution:            ISpotOrderRestClient on N exchanges, route by liquidity
//   Note on aliases:           SharedSymbol normalizes most assets; for exotic ones see AssetAliases
