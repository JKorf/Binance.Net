# Binance.Net AI API Quick Map

Use this file to route common user intents to the correct Binance.Net client member. If a method name or parameter is not listed here, inspect `Binance.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new BinanceRestClient()` |
| WebSocket streams and socket API requests | `new BinanceSocketClient()` |
| API key authentication | `options.ApiCredentials = new BinanceCredentials("key", "secret")` |
| Live environment | `BinanceEnvironment.Live` |
| Testnet environment | `BinanceEnvironment.Testnet` |
| Binance.US environment | `BinanceEnvironment.Us` |
| Dependency injection | `services.AddBinance(options => { ... })` |

## Spot REST

| User intent | Binance.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get spot exchange info | `client.SpotApi.ExchangeData.GetExchangeInfoAsync()` |
| Get info for one spot symbol | `client.SpotApi.ExchangeData.GetExchangeInfoAsync("BTCUSDT")` |
| Get latest spot ticker | `client.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot price | `client.SpotApi.ExchangeData.GetPriceAsync("BTCUSDT")` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT")` |
| Get recent trades | `client.SpotApi.ExchangeData.GetRecentTradesAsync("BTCUSDT")` |
| Get historical trades | `client.SpotApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT")` |
| Get aggregate trades | `client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("BTCUSDT")` |
| Get klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("BTCUSDT", KlineInterval.OneMinute)` |
| Get account info and balances | `client.SpotApi.Account.GetAccountInfoAsync()` |
| Get user assets | `client.SpotApi.Account.GetUserAssetsAsync()` |
| Get deposit history | `client.SpotApi.Account.GetDepositHistoryAsync()` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawalHistoryAsync()` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(...)` |
| Get trade fees | `client.SpotApi.Account.GetTradeFeeAsync(...)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Place spot test order | `client.SpotApi.Trading.PlaceTestOrderAsync(...)` |
| Query spot order | `client.SpotApi.Trading.GetOrderAsync(symbol, orderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(symbol)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol)` |
| Replace spot order | `client.SpotApi.Trading.ReplaceOrderAsync(...)` |
| Place OCO order list | `client.SpotApi.Trading.PlaceOcoOrderListAsync(...)` |
| Place OTO order list | `client.SpotApi.Trading.PlaceOtoOrderListAsync(...)` |
| Place OTOCO order list | `client.SpotApi.Trading.PlaceOtocoOrderListAsync(...)` |
| Convert quote | `client.SpotApi.Trading.ConvertQuoteRequestAsync(...)` |
| Accept convert quote | `client.SpotApi.Trading.ConvertAcceptQuoteAsync(...)` |
| Get convert order status | `client.SpotApi.Trading.GetConvertOrderStatusAsync(...)` |
| Get convert trade history | `client.SpotApi.Trading.GetConvertTradeHistoryAsync(...)` |
| Portfolio margin account info | `client.SpotApi.Account.GetPortfolioMarginAccountInfoAsync()` |
| Portfolio margin collateral rates | `client.SpotApi.Account.GetPortfolioMarginCollateralRateAsync()` |

## Margin REST

Margin endpoints are under `SpotApi.Account` and `SpotApi.Trading`, not under a separate `SpotApi.Margin` property.

| User intent | Binance.Net member |
|---|---|
| Get margin account info | `client.SpotApi.Account.GetMarginAccountInfoAsync()` |
| Get isolated margin account info | `client.SpotApi.Account.GetIsolatedMarginAccountAsync(...)` |
| Borrow margin asset | `client.SpotApi.Account.MarginBorrowAsync(...)` |
| Repay margin loan | `client.SpotApi.Account.MarginRepayAsync(...)` |
| Transfer between account types, including margin | `client.SpotApi.Account.TransferAsync(...)` |
| Place margin order | `client.SpotApi.Trading.PlaceMarginOrderAsync(...)` |
| Cancel margin order | `client.SpotApi.Trading.CancelMarginOrderAsync(...)` |
| Get margin order | `client.SpotApi.Trading.GetMarginOrderAsync(...)` |
| Place margin OCO order | `client.SpotApi.Trading.PlaceMarginOcoOrderAsync(...)` |

## USD-M Futures REST

| User intent | Binance.Net member |
|---|---|
| Get USD-M futures exchange info | `client.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync()` |
| Get USD-M futures server time | `client.UsdFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get USD-M futures ticker | `client.UsdFuturesApi.ExchangeData.GetTickerAsync("ETHUSDT")` |
| Get USD-M futures price | `client.UsdFuturesApi.ExchangeData.GetPriceAsync("ETHUSDT")` |
| Get USD-M futures order book | `client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT")` |
| Get USD-M futures klines | `client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get USD-M futures account info | `client.UsdFuturesApi.Account.GetAccountInfoV3Async()` |
| Get USD-M futures balances | `client.UsdFuturesApi.Account.GetBalancesAsync()` |
| Get USD-M futures positions | `client.UsdFuturesApi.Account.GetPositionInformationAsync("ETHUSDT")` |
| Set initial leverage | `client.UsdFuturesApi.Account.ChangeInitialLeverageAsync(symbol, leverage)` |
| Change margin type | `client.UsdFuturesApi.Account.ChangeMarginTypeAsync(symbol, FuturesMarginType.Isolated)` |
| Change position mode | `client.UsdFuturesApi.Account.ModifyPositionModeAsync(...)` |
| Place USD-M futures order | `client.UsdFuturesApi.Trading.PlaceOrderAsync(...)` |
| Query USD-M futures order | `client.UsdFuturesApi.Trading.GetOrderAsync(symbol, orderId)` |
| Cancel USD-M futures order | `client.UsdFuturesApi.Trading.CancelOrderAsync(symbol, orderId)` |
| Cancel all USD-M futures orders | `client.UsdFuturesApi.Trading.CancelAllOrdersAsync(symbol)` |
| Place USD-M conditional order | `client.UsdFuturesApi.Trading.PlaceConditionalOrderAsync(...)` |
| Get current USD-M v3 positions | `client.UsdFuturesApi.Trading.GetPositionsAsync(symbol)` |

## COIN-M Futures REST

| User intent | Binance.Net member |
|---|---|
| Get COIN-M futures exchange info | `client.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync()` |
| Get COIN-M futures server time | `client.CoinFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get COIN-M futures tickers | `client.CoinFuturesApi.ExchangeData.GetTickersAsync(...)` |
| Get COIN-M futures prices | `client.CoinFuturesApi.ExchangeData.GetPricesAsync(...)` |
| Get COIN-M futures order book | `client.CoinFuturesApi.ExchangeData.GetOrderBookAsync(...)` |
| Get COIN-M futures klines | `client.CoinFuturesApi.ExchangeData.GetKlinesAsync(...)` |
| Get COIN-M futures account info | `client.CoinFuturesApi.Account.GetAccountInfoAsync()` |
| Get COIN-M futures balances | `client.CoinFuturesApi.Account.GetBalancesAsync()` |
| Get COIN-M futures positions | `client.CoinFuturesApi.Account.GetPositionInformationAsync(...)` |
| Set COIN-M initial leverage | `client.CoinFuturesApi.Account.ChangeInitialLeverageAsync(symbol, leverage)` |
| Change COIN-M margin type | `client.CoinFuturesApi.Account.ChangeMarginTypeAsync(symbol, FuturesMarginType.Isolated)` |
| Place COIN-M futures order | `client.CoinFuturesApi.Trading.PlaceOrderAsync(...)` |
| Query COIN-M futures order | `client.CoinFuturesApi.Trading.GetOrderAsync(symbol, orderId)` |
| Cancel COIN-M futures order | `client.CoinFuturesApi.Trading.CancelOrderAsync(symbol, orderId)` |

## General REST

| User intent | Binance.Net member |
|---|---|
| Sub-account endpoints | `client.GeneralApi.SubAccount.*` |
| Brokerage endpoints | `client.GeneralApi.Brokerage.*` |
| Futures account transfer/history endpoints | `client.GeneralApi.Futures.*` |
| Crypto loan endpoints | `client.GeneralApi.CryptoLoans.*` |
| Auto Invest endpoints | `client.GeneralApi.AutoInvest.*` |
| Mining endpoints | `client.GeneralApi.Mining.*` |
| Staking endpoints | `client.GeneralApi.Staking.*` |
| Simple Earn endpoints | `client.GeneralApi.SimpleEarn.*` |
| Copy Trading endpoints | `client.GeneralApi.CopyTrading.*` |
| Gift Card endpoints | `client.GeneralApi.GiftCard.*` |
| NFT endpoints | `client.GeneralApi.Nft.*` |

## Spot WebSocket

| User intent | Binance.Net member |
|---|---|
| Subscribe spot ticker updates | `socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe many spot ticker updates | `socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe all spot mini ticker updates | `socketClient.SpotApi.ExchangeData.SubscribeToAllMiniTickerUpdatesAsync(handler)` |
| Subscribe spot mini ticker updates | `socketClient.SpotApi.ExchangeData.SubscribeToMiniTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot klines | `socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe spot order book snapshots | `socketClient.SpotApi.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(...)` |
| Subscribe spot book ticker | `socketClient.SpotApi.ExchangeData.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe spot trades | `socketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot aggregate trades | `socketClient.SpotApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(symbol, handler)` |
| Subscribe spot user data | `socketClient.SpotApi.Account.SubscribeToUserDataUpdatesAsync(...)` |
| Socket API place spot order | `socketClient.SpotApi.Trading.PlaceOrderAsync(...)` |
| Socket API cancel spot order | `socketClient.SpotApi.Trading.CancelOrderAsync(...)` |
| Socket API get spot account info | `socketClient.SpotApi.Account.GetAccountInfoAsync()` |

## Futures WebSocket

| User intent | Binance.Net member |
|---|---|
| Subscribe USD-M ticker updates | `socketClient.UsdFuturesApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe USD-M klines | `socketClient.UsdFuturesApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe USD-M book ticker | `socketClient.UsdFuturesApi.ExchangeData.SubscribeToBookTickerUpdatesAsync(symbol, handler)` |
| Subscribe USD-M trades | `socketClient.UsdFuturesApi.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(symbol, handler)` |
| Subscribe USD-M user data | `socketClient.UsdFuturesApi.Account.SubscribeToUserDataUpdatesAsync(...)` |
| Socket API place USD-M order | `socketClient.UsdFuturesApi.Trading.PlaceOrderAsync(...)` |
| Subscribe COIN-M ticker updates | `socketClient.CoinFuturesApi.ExchangeData.SubscribeToTickerUpdatesAsync(symbol, handler)` |
| Subscribe COIN-M klines | `socketClient.CoinFuturesApi.ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, handler)` |
| Subscribe COIN-M user data | `socketClient.CoinFuturesApi.Account.SubscribeToUserDataUpdatesAsync(...)` |
| Socket API place COIN-M order | `socketClient.CoinFuturesApi.Trading.PlaceOrderAsync(...)` |

## SharedApis

Use SharedApis for exchange-agnostic code across Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | Binance.Net member or interface |
|---|---|
| Shared spot REST client | `new BinanceRestClient().SpotApi.SharedClient` |
| Shared USD-M futures REST client | `new BinanceRestClient().UsdFuturesApi.SharedClient` |
| Shared COIN-M futures REST client | `new BinanceRestClient().CoinFuturesApi.SharedClient` |
| Shared spot socket client | `new BinanceSocketClient().SpotApi.SharedClient` |
| Shared USD-M futures socket client | `new BinanceSocketClient().UsdFuturesApi.SharedClient` |
| Shared COIN-M futures socket client | `new BinanceSocketClient().CoinFuturesApi.SharedClient` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

For shared socket subscriptions, unsubscribe with `await subscription.Data.UnsubscribeAsync()`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `BinanceClient` | `BinanceRestClient` |
| `ApiCredentials` | `BinanceCredentials` |
| `SpotApi.Margin` | `SpotApi.Account` / `SpotApi.Trading` margin methods |
| `SpotApi.PortfolioMargin` | `SpotApi.Account.GetPortfolioMargin*` methods |
| `SpotApi.SubAccount` | `GeneralApi.SubAccount` |
| `GeneralApi.Loans` | `GeneralApi.CryptoLoans` |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | `subscription.Data.UnsubscribeAsync()` |
| Custom `clientOrderId` by default | Let Binance.Net auto-generate it |
| `positionSide` in every futures order | Include only when hedge mode is intended |
