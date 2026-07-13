# ![.Binance.Net](https://raw.githubusercontent.com/JKorf/Binance.Net/master/Binance.Net/Icon/icon.png) Binance.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Binance.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Binance.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Binance.Net?style=for-the-badge) ![Since](https://img.shields.io/badge/since-2017-brightgreen?style=for-the-badge
)

Binance.Net is a strongly typed client library for accessing the [Binance REST and Websocket API](https://binance-docs.github.io/apidocs/#change-log). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments (binance.com, binance.us, testnet)
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## For AI Coding Assistants

This library provides first-class support for AI coding assistants. The relevant skill files are in this repository:

- **Agents**: `AGENTS.md` (auto-detected at repo root)
- **Cursor**: `.cursor/rules/binance-net.mdc`
- **GitHub Copilot**: `.github/copilot-instructions.md`
- **Other tools** (Windsurf, Codex, Continue, Aider, etc.): `llms.txt` at repo root
- **API quick map**: `docs/ai-api-map.md`
- **Compilable examples**: `Examples/ai-friendly/`

See [cryptoexchange-skills-hub](https://github.com/JKorf/cryptoexchange-skills-hub) for installable skills.

**Quick prompt to verify your assistant is using these:**
> "Show me a minimal example of placing a limit buy order on Binance Spot using Binance.Net, including authentication setup."

The expected output should use `BinanceRestClient`, `BinanceCredentials`, and the `HttpResult` pattern.

## Benchmark
Performance is a core focus. For a benchmark comparing Binance.Net performance to CCXT and Binance.Api, see [docs/binance-net-benchmark.md](docs/binance-net-benchmark.md).

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/binance.net.svg?style=for-the-badge)](https://www.nuget.org/packages/Binance.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/Binance.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/Binance.Net)

	dotnet add package Binance.Net
	
### GitHub packages
Binance.Net is available on [GitHub packages](https://github.com/JKorf/Binance.Net/pkgs/nuget/Binance.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/Binance.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/Binance.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/Binance.Net/releases).

	
## How to use
*Basic request:*
```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new BinanceRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.LastPrice;
```

*Place order:*
```csharp
var restClient = new BinanceRestClient(opts => {
	opts.ApiCredentials = new BinanceCredentials("APIKEY", "APISECRET");
});

// Place Limit order to go long for 0.1 ETH at 2000
var orderResult = await restClient.UsdFuturesApi.Trading.PlaceOrderAsync(
    "ETHUSDT",
    OrderSide.Buy,
    FuturesOrderType.Limit,
    0.1m,
    2000,
    timeInForce: TimeInForce.GoodTillCanceled,
    positionSide: PositionSide.Long);
```

*WebSocket subscription:*
```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new BinanceSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("ETHUSDT", (update) => 
{
  var lastPrice = update.Data.LastPrice;
});
```

*Get started and request the last price of a symbol in 40 seconds*  

<img src="https://github.com/JKorf/Binance.Net/blob/f74f262151f21b123deecd9b39a717458a18f6ff/docs/Binance.gif" width="600" />

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev?library=Binance.Net), or have a look at the examples [here](https://github.com/JKorf/Binance.Net/tree/master/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Binance.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bitstamp|[JKorf/Bitstamp.Net](https://github.com/JKorf/Bitstamp.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitstamp.Net.svg?style=flat-square)](https://www.nuget.org/packages/Bitstamp.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Lighter|[JKorf/Lighter.Net](https://github.com/JKorf/Lighter.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Lighter.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Lighter.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Polymarket|[JKorf/Polymarket.Net](https://github.com/JKorf/Polymarket.Net)|[![Nuget version](https://img.shields.io/nuget/v/Polymarket.net.svg?style=flat-square)](https://www.nuget.org/packages/Polymarket.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|Weex|[JKorf/Weex.Net](https://github.com/JKorf/Weex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Weex.net.svg?style=flat-square)](https://www.nuget.org/packages/Weex.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

### Spot/Margin/Savings/Mining REST
|API|Supported|Location|
|--|--:|--|
|Wallet endpoints|✓|`restClient.SpotApi.Account`|
|SubAccount endpoints|✓|`restClient.GeneralApi.SubAccount`|
|Market data endpoints|✓|`restClient.SpotApi.ExchangeData`|
|Websocket Market Streams|✓|`socketClient.SpotApi.ExchangeData`|
|Spot Trading Endpoints|✓|`restClient.SpotApi.Trading`|
|Spot Account Endpoints|✓|`restClient.SpotApi.Account`|
|Margin Account/Trade|Partial|`restClient.SpotApi.Account` / `restClient.SpotApi.Trading`|
|User Data Streams|✓|`socketClient.SpotApi.Account`|
|Margin User Data Streams|X||
|Simple Earn Endpoints|✓|`restClient.GeneralApi.SimpleEarn`|
|Auto-Invest Endpoints|✓|`restClient.GeneralApi.AutoInvest`|
|Staking Endpoints|✓|`restClient.GeneralApi.Staking`|
|Mining Endpoints|✓|`restClient.GeneralApi.Mining`|
|Futures|✓|`restClient.GeneralApi.Futures`|
|Futures Algo Endpoints|✓|`restClient.UsdFuturesApi.Trading`|
|Spot Algo Endpoints|✓|`restClient.SpotApi.Trading`|
|Classic Portfolio Margin Endpoints|Partial|`restClient.SpotApi.Account`|
|BLVT Endpoints|Partial|`restClient.SpotApi.Account` / `restClient.SpotApi.ExchangeData` / `restClient.SpotApi.Trading`|
|Fiat Endpoints|✓|`restClient.SpotApi.Account`|
|C2C Endpoints|✓|`restClient.SpotApi.Trading`|
|VIP Loans Endpoints|X||
|Crypto Loans Endpoints|Partial|`restClient.GeneralApi.CryptoLoans`|
|Pay Endpoints|✓|`restClient.SpotApi.Trading`|
|Convert Endpoints|✓|`restClient.SpotApi.ExchangeData` / `restClient.SpotApi.Trading`|
|Rebate Endpoints|✓|`restClient.SpotApi.Account`|
|NFT Endpoints|X||
|Binance Gift Card Endpoints|X||

### USD-M Futures REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.UsdFuturesApi.ExchangeData`|
|Trade|✓|`restClient.UsdFuturesApi.Account` / `restClient.UsdFuturesApi.Trading`|
|Websocket Market Streams|✓|`socketClient.UsdFuturesApi`|
|User Data Streams|✓|`socketClient.UsdFuturesApi`|
|Account|✓|`restClient.UsdFuturesApi.Account` / `restClient.UsdFuturesApi.Trading`|
|Convert|✓|`restClient.UsdFuturesApi.ExchangeData` / `restClient.UsdFuturesApi.Trading`|
|Classic Portfolio Margin Endpoints|X||

### COIN-M Futures REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.CoinFuturesApi.ExchangeData`|
|Websocket Market Streams|✓|`socketClient.CoinFuturesApi`|
|Trade|✓|`restClient.CoinFuturesApi.Account` / `restClient.CoinFuturesApi.Trading`|
|User Data Streams|✓|`socketClient.CoinFuturesApi`|
|Account|✓|`restClient.CoinFuturesApi.Account` / `restClient.CoinFuturesApi.Trading`|
|Classic Portfolio Margin Endpoints|X||

### Spot Websocket API
|API|Supported|Location|
|--|--:|--|
|Market data requests|✓|`socketClient.SpotApi.ExchangeData`|
|Trading requests|✓|`socketClient.SpotApi.Trading`|
|Account requests|✓|`socketClient.SpotApi.Account`|

### USD-M Futures Websocket API
|API|Supported|Location|
|--|--:|--|
|*|✓|`socketClient.UsdFuturesApi.Account` / `socketClient.UsdFuturesApi.ExchangeData` / `socketClient.UsdFuturesApi.Trading`|

### European Options
|API|Supported|Location|
|--|--:|--|
|*|X||

### Portfolio Margin
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
Any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 13.1.1 - 13 Jul 2026
    * Combined request rate limit for coin and usdt futures
    * Updated Usdt futures GetFundingInfoAsync and GetBasisAsync endpoint weight from 0 to 1
    * Fixed exception during authentication when retrying requests

* Version 13.1.0 - 09 Jul 2026
    * Updated CryptoExchange.Net to v12.1.0
    * Added CancelOnly to SymbolStatus enum
    * Added CoinM conditional order endpoints
    * Fixed typo in Accepeted deserialization in BinanceTravelWithdrawalResponse model

* Version 13.0.1 - 01 Jul 2026
    * Updated CryptoExchange.Net to V12.0.2
    * Added lower case value mapping for AccountType and FuturesMarginType enums
    * Added WithdrawCollaterals, BorrowCollaterals to BinanceCrossMarginCollateralRatio model
    * Added TieredLiquidationRatio to BinancePortfolioMarginCollateralRate model
    * Added IsPublic to BinanceIsolatedMarginTierData
    * Updated GetFiatPaymentHistoryAsync to return empty array if no results
    * Fixed websocket request signing issue

* Version 13.0.0 - 29 Jun 2026
    * Result types:
      * (Web)CallResult types are replaced by HttpResult, WebSocketResult and QueryResult with the same logic
      * WebSocketResult and QueryResult now return additional info for websocket operations
      * Updated result types to record type
      * Removed implicit result type conversion to bool, `if (result)` no longer works, instead use `if (result.Success)`
      * Fixed result object nullability hinting, for example Data might be null if Success isn't checked for true
    * Clients:
      * Added ToString overrides on base API types
      * Added Exchange property on BaseApiClient
      * Added ApiCredentials property on Api clients
      * Updated ILogger source from client name to topic specific client name
      * Removed logging from client creation
      * Fixed issue in SocketApiClient.GetSocketConnection causing requests to always wait the full max 10 seconds when there was a reconnecting socket
    * Shared APIs:
      * Added missing dedicated option types
      * Added Discover method on ISharedClient interface, returning info on supported capabilities and operations
      * Added ResetStaticExchangeParameters method on ExchangeParameters
      * Added Status property to SharedWithdrawal model
      * Added TradingModes property to SharedBalance model
      * Updated Shared ExchangeParameters parameter names to be case insensitive
      * Updated code comments
      * Replaced ExchangeResult with ExchangeCallResult type
      * Removed TradingMode from the response model, only maintained on models where it makes sense
      * Removed IListenKey support, listen keys now rely on internal management
    * Added user subscription overloads without listenkey which manages the listen key internally
    * Added async streaming on UserDataTracker items with StreamUpdatesAsync
    * Added cancellation token support to UserDataTracker starting
    * Added SupportedEnvironments property to PlatformInfo
    * Added Clear() method on UserClientProvider to clear all cached clients
    * Added setter to BinanceExchange.RateLimiter to allow custom rate limit settings
    * Added UnderlyingType.KrEquity Enum value
    * Updated symbol parameter to optional for GetFundingRatesAsync endpoint
    * Updated some endpoint ratelimit weights
    * Renamed DepositStatus.Completed to DepositStatus.Credited
    * Various small performance improvements
    * Fixed websocket connection attempts counting towards rate limit even when server could not be reached
    * Fixed duplicate subscriptions not correctly handled user subscriptions
    * Fixed deserialization error in BinanceFuturesFundingInfo when UpdateTime was null
