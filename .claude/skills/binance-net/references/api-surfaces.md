# Binance.Net API surfaces

Use this reference to route an intent to the relevant member. Table entries abbreviate overloads; confirm parameters in the installed package before use. See [usage](usage.md) for setup and results.

## Client roots

| Surface | Purpose |
| --- | --- |
| `BinanceRestClient.SpotApi.ExchangeData` | Spot exchange info, tickers, klines, trades, and order books |
| `BinanceRestClient.SpotApi.Account` | Spot and margin accounts, wallets, transfers, and portfolio margin |
| `BinanceRestClient.SpotApi.Trading` | Spot and margin orders, order lists, and convert |
| `BinanceRestClient.UsdFuturesApi` | USD-M `ExchangeData`, `Account`, and `Trading` |
| `BinanceRestClient.CoinFuturesApi` | COIN-M `ExchangeData`, `Account`, and `Trading` |
| `BinanceRestClient.GeneralApi` | Subaccounts, brokerage, earn, staking, loans, and other general REST products |
| `BinanceSocketClient.SpotApi` | Spot `ExchangeData`, `Account`, and `Trading` |
| `BinanceSocketClient.UsdFuturesApi` | USD-M `ExchangeData`, `Account`, and `Trading` |
| `BinanceSocketClient.CoinFuturesApi` | COIN-M `ExchangeData`, `Account`, and `Trading` |

The tables use `client` for a `BinanceRestClient` and `socketClient` for a `BinanceSocketClient`. There is no separate `SpotApi.Margin` or options client root in this library.

## Environments

Select `BinanceEnvironment.Live`, `Testnet`, `Demo`, or `Us` through client options. Match credentials and supported products to the selected environment; a test or regional environment does not imply coverage of every live endpoint.

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
| Place margin OCO order | `client.SpotApi.Trading.PlaceMarginOCOOrderAsync(...)` |

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

## Private streams

Spot user data uses `socketClient.SpotApi.Account.SubscribeToUserDataUpdatesAsync(...)` with socket credentials and callback arguments; it connects through the spot WebSocket API. Do not apply an old spot listen-key recipe to this overload.

USD-M and COIN-M account subscriptions have overloads that obtain and renew a listen key automatically when credentials are configured, and overloads accepting an externally managed key. Manage externally supplied keys through the corresponding REST account API's start/keep-alive/stop methods. Margin subscriptions use margin-specific methods and listen tokens; inspect the selected overload. Unsubscribe through the concrete socket client in all cases.

Native socket account/trading requests return `QueryResult<BinanceResponse<T>>` where declared. Check outer success before unwrapping `Data`; this is distinct from subscription results.

## Shared API V2

For portable code, use `.SharedApi` on the selected native REST or socket root. Keep native request/model types separate from Shared API types.

| Intent | Capability or member |
| --- | --- |
| Get a ticker | `IGetTickerRest.GetTickerAsync` |
| Get/filter spot symbols | `IGetSpotSymbolsRest.GetSpotSymbolsAsync` |
| Get/filter futures symbols | `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync` |
| Place a spot order | `IPlaceSpotOrderRest.PlaceSpotOrderAsync` |
| Place a futures order | `IPlaceFuturesOrderRest.PlaceFuturesOrderAsync` |
| Subscribe to tickers | `ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync` |
| Subscribe to trades | `ISubscribeTradesSocket.SubscribeToTradeUpdatesAsync` |
| Subscribe to order books | `ISubscribeOrderBookSocket.SubscribeToOrderBookUpdatesAsync` |

`IBinanceSharedApiClient` groups the six supported surfaces:

| Property | Interface |
| --- | --- |
| `SpotRest` | `IBinanceRestClientSpotSharedApi` |
| `UsdFuturesRest` | `IBinanceRestClientUsdFuturesSharedApi` |
| `CoinFuturesRest` | `IBinanceRestClientCoinFuturesSharedApi` |
| `SpotSocket` | `IBinanceSocketClientSpotSharedApi` |
| `UsdFuturesSocket` | `IBinanceSocketClientUsdFuturesSharedApi` |
| `CoinFuturesSocket` | `IBinanceSocketClientCoinFuturesSharedApi` |

Use a surface property when the operation is known. For dynamic selection, call `GetCapability` on an aggregate instance with a typed descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest`, and specify the intended trading mode when selecting between spot and futures. Check for a null match, then use its `Capability` and `Options`. Use `GetCapabilities` for all matches and `Discover` for summary metadata.

`AddBinance` registers the aggregate and its supported capabilities. Where several surfaces or exchanges implement an injected capability, select the intended implementation explicitly instead of relying on the last DI registration.

### Symbols and quantities

Call `GetSpotSymbolsAsync` or `GetFuturesSymbolsAsync` successfully before reading the corresponding `SpotSymbolCatalog` or `FuturesSymbolCatalog`. `GetSymbolsRequest` supports asset-type filters. Symbol responses populate display names, base/quote asset types, and subtypes, including crypto, stablecoins, fiat, and TradFi classifications. Keep USD-M and COIN-M futures catalogs associated with their respective surfaces.

Shared book ticker, position, and user-trade quantities use `SharedOrderQuantity`. Read `QuantityInBaseAsset`, `QuantityInQuoteAsset`, or `QuantityInContracts` as appropriate. Binance shared order books report `QuantityType` as `SharedQuantityType.BaseAsset`.

Shared subscriptions are closed through the concrete `BinanceSocketClient.UnsubscribeAsync(subscription.Data)`.

## Source navigation in a checkout

These links are available in a source checkout, not in the standalone NuGet skill:

- [Client interfaces](../../../../Binance.Net/Interfaces/Clients/): exact method signatures and endpoint documentation.
- [Client implementations](../../../../Binance.Net/Clients/): transport and Shared API mappings.
- [Compiled examples](../../../../Examples/ai-friendly/): complete programs.

For packaged usage patterns, see [usage](usage.md).
