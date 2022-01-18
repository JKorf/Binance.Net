There are a decent amount of breaking changes when moving from version 7.x.x to version 8.x.x. Although the interface has changed, the available endpoints/information have not, so there should be no need to completely rewrite your program.
Most endpoints are now available under a slightly different name or path, and most data models have remained the same, barring a few renames.
In this document most changes will be described. If you have any other questions or issues when updating, feel free to open an issue.

Changes related to `IExchangeClient`, options and client structure are also (partially) covered in the [CryptoExchange.Net Migration Guide](https://github.com/JKorf/CryptoExchange.Net/wiki/Migration-Guide)

### Namespaces
There are a few namespace changes:  
|Type|Old|New|
|----|---|---|
|Enums|`Binance.Net.Objects`|`Binance.Net.Enums`  |
|Clients|`Binance.Net`|`Binance.Net.Clients`  |
|Client interfaces|`Binance.Net.Interfaces`|`Binance.Net.Interfaces.Clients`  |
|Objects|`Binance.Net.Objects`|`Binance.Net.Objects.Models`  |
|SymbolOrderBook|`Binance.Net`|`Binance.Net.SymbolOrderBooks`|

### Client options
The `BaseAddress`, rate limiting, trade rules and timestamping options are now under the specific Api client options.  
*V7*
````C#
var binanceClient = new BinanceClient(new BinanceClientOptions
{
	LogLevel = LogLevel.Debug,
	ApiCredentials = new ApiCredentials("GENERAL-KEY", "GENERAL-SECRET"),
	BaseAddress = BinanceApiAddresses.Default.RestClientAddress,
	BaseAddressUsdtFutures = BinanceApiAddresses.Default.UsdtFuturesRestClientAddress,
	AutoTimestamp = true,
	TradeRulesBehaviour = TradeRulesBehaviour.ThrowError
});
````

*V8*
````C#
var binanceClient = new BinanceClient(new BinanceClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	SpotApiOptions = new BinanceApiClientOptions
	{
		BaseAddress = BinanceApiAddresses.Default.RestClientAddress,
		AutoTimestamp = false
	},
	UsdFuturesApiOptions = new BinanceApiClientOptions
	{
		TradeRulesBehaviour = TradeRulesBehaviour.ThrowError,
		BaseAddress = BinanceApiAddresses.Default.UsdFuturesRestClientAddress,
		AutoTimestamp = true
	}
});
````

### Client structure
Version 8 restructured the rest clients into 4 parts: `GeneralApi`, `SpotApi`, `UsdFuturesApi` and `CoinFuturesApi`. This structure is chosen to make it more clear where what part of the API is found. This new structure is in line with the client structures of other `CryptoExchange.Net` implemenetations. More info on this [here](https://github.com/Jkorf/CryptoExchange.Net/wiki/Clients).
The socket client is also split into `SpotStreams`, `UsdFuturesStreams` and `CoinFuturesStreams`. This restructuring means all library calls will have changed, though most will only need to change the path:

*V7*
````C#
var balances = await binanceClient.General.GetAccountInfoAsync();
var withdrawals = await binanceClient.WithdrawDeposit.GetWithdrawalHistoryAsync();

var subAccountBalances = await binanceClient.SubAccount.GetSubAccountAssetsAsync();

var positionInfoUsd = await binanceClient.FuturesUsdt.GetPositionInformationAsync();
var positionInfoCoin = await binanceClient.FuturesCoin.GetPositionInformationAsync();

var tickersSpot = await binanceClient.Spot.Market.GetTickersAsync();
var tickersUsd = await binanceClient.FuturesUsdt.Market.GetTickersAsync();
var tickersCoin = await binanceClient.FuturesCoin.Market.GetTickersAsync();
var exchangeInfoSpot = await binanceClient.Spot.System.GetExchangeInfoAsync();
var exchangeInfoUsd = await binanceClient.FuturesUsdt.System.GetExchangeInfoAsync();
var exchangeInfoCoin = await binanceClient.FuturesCoin.System.GetExchangeInfoAsync();

var orderSpot = await binanceClient.Spot.Order.PlaceOrderAsync();
var orderUsd = await binanceClient.FuturesUsdt.Order.PlaceOrderAsync();
var orderCoin = await binanceClient.FuturesCoin.Order.PlaceOrderAsync();
var tradesSpot = await binanceClient.Spot.Order.GetUserTradesAsync();
var tradesUsd = await binanceClient.FuturesUsdt.Order.GetUserTradesAsync();
var tradesCoin = await binanceClient.FuturesCoin.Order.GetUserTradesAsync();

var subSpot = binanceSocketClient.Spot.SubscribeToSymbolTickerUpdatesAsync("BTCUSDT", DataHandler);
var subUsd = binanceSocketClient.FuturesUsdt.SubscribeToSymbolTickerUpdatesAsync("BTCUSDT", DataHandler);
var subCoin = binanceSocketClient.FuturesCoin.SubscribeToSymbolTickerUpdatesAsync("BTCUSD_PERP", DataHandler);
````

*V8*  
````C#
var balances = await binanceClient.SpotApi.Account.GetAccountInfoAsync();
var withdrawals = await binanceClient.SpotApi.Account.GetWithdrawalHistoryAsync();

var subAccountBalances = await binanceClient.GeneralApi.SubAccount.GetSubAccountAssetsAsync();
 
var positionInfoUsd = await binanceClient.UsdFuturesApi.Account.GetPositionInformationAsync();
var positionInfoCoin = await binanceClient.CoinFuturesApi.Account.GetPositionInformationAsync();

var tickersSpot = await binanceClient.SpotApi.ExchangeData.GetTickersAsync();
var tickersUsd = await binanceClient.UsdFuturesApi.ExchangeData.GetTickersAsync();
var tickersCoin = await binanceClient.CoinFuturesApi.ExchangeData.GetTickersAsync();
var exchangeInfoSpot = await binanceClient.SpotApi.ExchangeData.GetExchangeInfoAsync();
var exchangeInfoUsd = await binanceClient.SpotApi.ExchangeData.GetExchangeInfoAsync();
var exchangeInfoCoin = await binanceClient.SpotApi.ExchangeData.GetExchangeInfoAsync();

var orderSpot = await binanceClient.SpotApi.Trading.PlaceOrderAsync();
var orderUsd = await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync();
var orderCoin = await binanceClient.CoinFuturesApi.Trading.PlaceOrderAsync();
var tradesSpot = await binanceClient.SpotApi.Trading.GetUserTradesAsync();
var tradesUsd = await binanceClient.UsdFuturesApi.Trading.GetUserTradesAsync();
var tradesCoin = await binanceClient.CoinFuturesApi.Trading.GetUserTradesAsync();

var subSpot = binanceSocketClient.SpotStreams.SubscribeToTickerUpdatesAsync("BTCUSDT", DataHandler);
var subUsd = binanceSocketClient.UsdFuturesStreams.SubscribeToTickerUpdatesAsync("BTCUSDT", DataHandler);
var subCoin = binanceSocketClient.CoinFuturesStreams.SubscribeToTickerUpdatesAsync("BTCUSD_PERP", DataHandler);
````

### Definitions
Some names have been changed to a common definition. This includes where the name is part of a bigger name  
|Old|New||
|----|---|---|
|`Coin`|`Asset`|`GetUserCoinsAsync()` -> `GetUserAssetsAsync()`|
|`Open`/`High`/`Low`/`Close`|`OpenPrice`/`HighPrice`/`LowPrice`/`ClosePrice`||
|`BidPrice`/`AskPrice`/ `BidQuantity`/`AskQuantity`|`BestBidPrice`/`BestAskPrice`/ `BestBidQuantity`/`BestAskQuantity`||
|`Commission`|`Fee`||
|`Amount`|`Quantity`||
|`GoodTillCancel`|`GoodTillCanceled`|||

Some names have slightly changed to be consistent across different libraries  
`balance.Free` -> `balance.Available`  
`order.OrderId` -> `order.Id`  



