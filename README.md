# Binance.Net
[![.NET](https://github.com/JKorf/Binance.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/JKorf/Binance.Net/actions/workflows/dotnet.yml) [![Nuget version](https://img.shields.io/nuget/v/binance.net.svg)](https://www.nuget.org/packages/Binance.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/Binance.Net.svg)](https://www.nuget.org/packages/Binance.Net)

Binance.Net is a wrapper around the Binance API as described on [Binance](https://binance-docs.github.io/apidocs/spot/en/#change-log), including all features the API provides using clear and readable objects. The library support the spot, (isolated) margin and futures API's, both the REST and websocket API's.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Binance.Net/issues)**

[Documentation](https://jkorf.github.io/Binance.Net/)

## Support the project
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Referral link
Sign up using the following referral link to pay a small percentage of the trading fees you pay to support the project instead of paying them straight to Binance. This doesn't cost you a thing!
[Link](https://accounts.binance.com/en/register?ref=10153680)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1qz0jv0my7fc60rxeupr23e75x95qmlq6489n8gh  
**Eth**:  0x8E21C4d955975cB645589745ac0c46ECA8FAE504  

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Release notes
* Version 9.0.4 - 11 Jul 2023
    * Fixed GetUniversalTransferHistoryAsync deserialization

* Version 9.0.3 - 11 Jul 2023
    * Fixed GetUniversalTransferHistoryAsync deserialization

* Version 9.0.2 - 06 Jul 2023
    * Fixed socket client options copying

* Version 9.0.1 - 25 Jun 2023
    * Fix for invalid build

* Version 9.0.0 - 25 Jun 2023
    * Updated CryptoExchange.Net to version 6.0.0
    * Renamed BinanceClient to BinanceRestClient
    * Renamed UsdFuturesStreams and CoinFuturesStreams to UsdFuturesApi and CoinFuturesApi on the BinanceSocketClient
    * Updated endpoints to consistently use a base url without any path as basis to make switching environments/base urls clearer
    * Removed BinanceApiCredentials as the base ApiCredentials now supports RSA signing
    * Added IBinanceOrderBookFactory and implementation for creating order books
    * Updated dependency injection register method (AddBinance)

* Version 8.6.3 - 19 Jun 2023
    * Fixed trading rules check not actually applying to the parameters send when placing an order with the socket client

* Version 8.6.2 - 22 May 2023
    * Added orderId filter to futures GetUserTradesAsync endpoints
    * Fixed missing writers for BinanceSymbolFilter serialization

* Version 8.6.1 - 14 May 2023
    * Added TradeRules validation to websocket PlaceOrder endpoint
    * Corrected futures all book tickers stream response model
    * Added futures contractInfo streams

* Version 8.6.0 - 06 May 2023
    * Added Websocket API implementation

* Version 8.5.1 - 20 Apr 2023
    * Fixed deserialization error on trade update

* Version 8.5.0 - 17 Apr 2023
    * Added futures individual trade streams
    * Added coin futures all mark prices stream
    * Added Spot AutoConvertStableCoin endpoints
    * Added GetCrossMarginCollateralRatio endpoint
    * Added missing parameters to spot place order parameters
    * Added self trade prevention support
    * Fixed and updated various models

* Version 8.4.4 - 12 Apr 2023
    * Fixed deserialization issue SpotApi.GetExchangeInfoAsync()
    * Updated CryptoExchange.Net

* Version 8.4.3 - 18 Mar 2023
    * Fixed order deserialization when trailing delta timestamp is present
    * Updated subaccount ip restriction endpoints
    * Updated CryptoExchange.Net
	
* Version 8.4.2 - 16 Feb 2023
    * Fixed rate limiting issue with high UID weight requests

* Version 8.4.1 - 14 Feb 2023
    * Updated CryptoExchange.Net
    * Fixed Http Method on SpotApi.Trading.GetConvertTransferHistoryAsync request
    * Fixed request weight on SpotApi.Account.GetFiatDepositWithdrawHistoryAsync

* Version 8.4.0 - 09 Feb 2023
    * Added support for RSA request authentication

* Version 8.3.1 - 05 Feb 2023
    * Removed all book ticker stream subscription as it has been deprecated
    * Added Strategy and Grid update events to user stream subscription
    * Fixed missing IndexPrice on UsdFutures.SubscribeToAllMarkPriceUpdatesAsync
    * Updated AccountConfigUpdate event model

* Version 8.3.0 - 17 Nov 2022
    * Updated CryptoExchange.Net
    * Fixed BinanceConvertTransferRecord DeductedQuantity property
    * Fixed UsdFutures ContinuousContractKlines deserialization

* Version 8.2.0 - 03 Oct 2022
    * Added CryptoLoans endpoints
    * Added ConvertTransfer endpoints
    * Added GetMarginLevelInformationAsync endpoint
    * Added GetExchangeInfoAsync spot account type overloads
    * Renamed Lending endpoints to Savings endpoints
    * Added missing OrderRejectReason
    * Fixed incorrect AllowFailure enum serialization on SpotApi.ReplaceOrderAsync
    * Fixed ticker interface for futures streams

* Version 8.1.10 - 04 Sep 2022
    * Added missing (and future) account types, fixes warnings being generated in SpotApi.Trading.GetExchangeInfoAsync

* Version 8.1.9 - 24 Aug 2022
    * Added SpotApi.ExchangeData.GetUiKlinesAsync endpoint
    * Added 1 second kline interval support
    * Added new Notional filter for BinanceSymbols
    * Added Brokered to SpotApi.Account.GetAccountInfoAsync response model

* Version 8.1.8 - 13 Aug 2022
    * Added new endpoints:
        * SpotApi.Trading.ReplaceOrderAsync
        * SpotApi.ExchangeData.GetRollingWindowTickersAsync
        * SpotApi.Account.GetBalancesAsync
        * SpotApi.Account.GetMarginDustLogAsync
        * SpotApi.Acocunt.PortfolioMargin endpoints
    * Added rolling window ticker socket stream
    * Added ListenKey property to user stream updates
    * Fixed BinanceRebate quantity deserialization
    * Updated some request weights

* Version 8.1.7 - 18 Jul 2022
    * Updated CryptoExchange.Net

* Version 8.1.6 - 16 Jul 2022
    * Fixed MarginToIsolatedMargin UniversalTransferType serialization
    * Fixed max position filter serialization
    * Updated CryptoExchange.Net

* Version 8.1.5 - 12 Jul 2022
    * Fixed missing AccountType enum value causing error when deserializing GetExchangeInfoAsync
    * Fixed future deserialization failing when there's an unknown account type

* Version 8.1.4 - 10 Jul 2022
    * Added BalanceChange property in futures account update
    * Fixed GetPositionAdlQuantileEstimationAsync deserialization when symbol parameter is used
    * Updated CryptoExchange.Net

* Version 8.1.3 - 12 Jun 2022
    * Fixed `invalid signature` error on multiple subaccount endpoints
    * Fixed incorrect max limit on spot GetKlinesAsync
    * Fixed missing datetime converter on rebate UpdateTime property
    * Updated CryptoExchange.Net

* Version 8.1.2 - 24 May 2022
    * Fixed quoteQuantity not being passed on after checking trade rules
    * Fixed Blvt stream address and moved it to Spot
    * Updated CryptoExchange.Net

* Version 8.1.1 - 23 May 2022
    * Fixed parameter order trailing delta in PlaceOrderAsync

* Version 8.1.0 - 22 May 2022
    * Added LeveragedTokens user limit endpoint
    * Added Staking endpoints
    * Added USD futures transaction history download endpoints
    * Added USD futures algo orders endpoints
    * Added margin order rate limit endpoint
    * Added interest margin data endpoint
    * Added symbol collection overloads for GetPricesAsync, GetBookPricesAsync and GetTickersAsync
    * Added symbol parameter for subaccount transfer
    * Added margin account types for subaccount transfer
    * Added TrailingDelta support
    * Added quoteQuantity minimal notional value TradeRule validation
    * Renamed GetAllBookPricesAsync to GetBookPricesAsync

* Version 8.0.13 - 08 May 2022
    * Added TierAnnualInterestRate to BinanceFlexibleProductPosition
    * Changed GetFlexibleProductPositionAsync asset parameter to be optional
    * Updated CryptoExchange.Net

* Version 8.0.12 - 01 May 2022
    * Updated CryptoExchange.Net which fixed an timing related issue in the websocket reconnection logic
    * Fixed TrailingDelta symbol filter serialization
    * Added seconds representation to KlineInterval enum

* Version 8.0.11 - 20 Apr 2022
    * Added trailing delta symbol filter parsing

* Version 8.0.10 - 14 Apr 2022
    * Fixed request weight for order book requests
    * Updated CryptoExchange.Net

* Version 8.0.9 - 18 Mar 2022
    * Fixed Spot MiniTicker subscription having swapped base/quote volume properties
    * Fixed exception getting thrown when CancelOrderAsync on USD futures fails

* Version 8.0.8 - 10 Mar 2022
    * Changed MobileNumber type from long to string, fixing GetSubAccountStatusAsync deserialization when no phone number is defined
    * Updated CryptoExchange.Net

* Version 8.0.7 - 08 Mar 2022
    * Fixed inconsistent naming in BinanceLendingAccount model
    * Added AllowTrailingStop property on BinanceSymbol
    * Added new symbol filter PercentagePriceBySide
    * Updated spot GetOrderBookAsync to allow any limit under 5000
    * Updated CryptoExchange.Net

* Version 8.0.6 - 01 Mar 2022
    * Updated CryptoExchange.Net improving the websocket reconnection robustness

* Version 8.0.5 - 01 Mar 2022
    * Updated Limit checking on Usd futures GetMarkPriceKlinesAsync
    * Added time sync resetting when a timestamp error is received

* Version 8.0.4 - 27 Feb 2022
    * Updated CryptoExchange.Net to fix timestamping issue when request is ratelimiter
    * Updated USD futures kline requests max limit value check

* Version 8.0.3 - 25 Feb 2022
    * Fixed missing AccountType enum value breaking the spot GetExchangeInfoAsync call

* Version 8.0.2 - 24 Feb 2022
    * Fixed TradeRules not being applied
    * Updated CryptoExchange.Net

* Version 8.0.1 - 21 Feb 2022
    * Fixed ContractType being nullable
    * Fixed OrderType filename

* Version 8.0.0 - 18 Feb 2022
	* Added Github.io page for documentation: https://jkorf.github.io/Binance.Net/
	* Added unit tests for parsing the returned JSON for each endpoint and subscription
	* Added AddBinance extension method on IServiceCollection for easy dependency injection
	* Added URL reference to API endpoint documentation for each endpoint
	* Added default rate limiter

	* Refactored client structure to be consistent across exchange implementations
	* Renamed various properties to be consistent across exchange implementations

	* Cleaned up project structure
	* Fixed various models

	* Updated CryptoExchange.Net, see https://github.com/JKorf/CryptoExchange.Net#release-notes
	* See https://jkorf.github.io/Binance.Net/MigrationGuide.html for additional notes for updating from V7 to V8

* Version 7.2.5 - 08 Oct 2021
    * Updated CryptoExchange.Net to fix some socket issues

* Version 7.2.4 - 06 Oct 2021
    * Updated CryptoExchange.Net, fixing socket issue when calling from .Net Framework

* Version 7.2.3 - 05 Oct 2021
    * Added PriceProtect support

* Version 7.2.2 - 29 Sep 2021
    * Fix for BinanceSpotOrderBook
    * Updated CryptoExchange.Net

* Version 7.2.1 - 24 Sep 2021
    * Added GetEnabledIsolatedMarginAccountLimitAsync endpoint
    * Added EnableIsolatedMarginAccountAsync
    * Added Enabled property to IsolatedMarginAccount model
    * Added RemoveLiquidityPreviewAsync endpoint
    * Added AddLiquidityPreviewAsync endpoint
    * Added GetBSwapPoolConfigureAsync endpoint

* Version 7.2.0 - 20 Sep 2021
    * Updated stream Topic properties to reflect symbol where possible
    * Added DisableIsolatedMarginAccountAysnc endpoint
    * Updated CryptoExchange.Net

* Version 7.1.4 - 15 Sep 2021
    * Updated CryptoExchange.Net
    * Fixed missing interface CoinFutures system sub client

* Version 7.1.3 - 14 Sep 2021
    * Fixed CreateVirtualSubAccountAsync endpoint
    * Added missing FiatWithdrawDepositStatus entry
    * Updated testnet spot websocket url

* Version 7.1.2 - 02 Sep 2021
    * Fixed subaccount universal transfer result deserialization
    * Fix for disposing order book closing socket even if there are other connections

* Version 7.1.1 - 31 Aug 2021
    * Added optional start/endTime parameters to GetDusLogAsync
    * Fixed futures position deserialization

* Version 7.1.0 - 30 Aug 2021
    * Added Margin OCO endpoints
    * Fixed TransferSubAccountAsync parameters
    * Updated various models

* Version 7.0.5 - 26 Aug 2021
    * Updated CryptoExchange.Net, fixing reconnecting/resubscribing sockets with multiple subscriptions on a single connection

* Version 7.0.4 - 24 Aug 2021
    * Actually included fix for multiple symbols in GetExchangeInfoAsync

* Version 7.0.3 - 24 Aug 2021
    * Updated CryptoExchange.Net, improving websocket and SymbolOrderBook performance
    * Fix for GetExchangeInfoAsync filter by multiple symbols

* Version 7.0.2 - 16 Aug 2021
    * Added orderId parameter to GetUserTradesAsync
    * Added missing TransferAsync to client interface

* Version 7.0.1 - 13 Aug 2021
    * Fix for OperationCancelledException being thrown when closing a socket from a .net framework project

* Version 7.0.0 - 12 Aug 2021
	* Release version with new CryptoExchange.Net version 4.0.0
		* Multiple changes regarding logging and socket connection, see [CryptoExchange.Net release notes](https://github.com/JKorf/CryptoExchange.Net#release-notes)
	* Fixed deserialization of GetMiningStatisticsAsync

* Version 7.0.0-beta4 - 09 Aug 2021
    * Added Fiat endpoints
    * Renamed Get24HPriceAsync to GetTickerAsync
    * Renamed GetMyTradesAsync to GetUserTradesAsync
    * Renamed GetAllOrdersAsync to GetOrdersAsync
    * Renamed GetSymbolTradesAsync to GetRecentTradeHistoryAsync
    * Renamed GetHistoricalSymbolTradesAsync to GetTradeHistoryAsync
    * Renamed GetAggregatedTradesAsync to GetAggregatedTradeHistoryAsync
    * Renamed GetOpenMarginAccountOrdersAsync to GetMarginAccountOpenOrdersAsync
    * Renamed GetAllMarginAccountOrdersAsync to GetMarginAccountOrdersAsync
    * Renamed GetMyMarginAccountTradesAsync to GetMarginAccountUserTradesAsync
    * Renamed various PnL properties to Pnl

* Version 7.0.0-beta3 - 26 Jul 2021
    * Updated CryptoExchange.Net

* Version 7.0.0-beta2 - 22 Jul 2021
    * Added GetFundingWalletAsync endpoint
    * Added GetAPIKeyPermissionsAsync endpoint
    * Merged master

* Version 7.0.0-beta1 - 09 Jul 2021
    * Added symbol filter for GetExchangeInfoAsync
    * Added Async postfix for async methods
    * Updated CryptoExchange.Net

* Version 6.14.0-beta7 - 24 Jun 2021
    * Fixed/updated multiple models

* Version 6.14.0-beta6 - 19 Jun 2021
    * Fixed invalid symbol check on SubscribeToMarkPriceUpdatesAsync
    * Made TimeSync static to prevent re-doing when creating a new client

* Version 6.14.0-beta5 - 07 Jun 2021
    * Added BinanceApiAddresses class containing different api setups for easier
    * Updated CryptoExchange.Net

* Version 6.14.0-beta4 - 31 May 2021
    * Added GetTradingStatusAsync endpoint on USDT futures
    * Added GetMarkPriceKlinesAsync endpoint on futures
    * Added GetProductsAsync endpoint
    * Added GetContinuousContractKlinesAsync endpoint on USDT futures
    * Added PayToMain and MainToPay transfer types
    * Added MultiAssetMode endpoints and fields
    * Removed no longer supported GetLiquidationOrdersAsync futures endpoints
    * Added Assets property to USDT futures GetExchangeInfoAsync model
    * Added BalanceChange to futures account balance stream update

* Version 6.14.0-beta3 - 26 May 2021
    * Removed all non-async calls
    * Updated to CryptoExchange.Net changes

* Version 6.14.0-beta2 - 06 mei 2021
    * Updated CryptoExchange.Net

* Version 6.14.0-beta1 - 30 apr 2021
    * Updated to CryptoExchange.Net 4.0.0-beta1, new websocket implementation

* Version 6.13.6 - 01 Aug 2021
    * Added missing SubscribeToKlineUpdatesAsync overload

* Version 6.13.5 - 22 Jul 2021
    * Added support for multiple KlineIntervals in kline socket subscriptions
    * Updated from wapi to sapi endpoints

* Version 6.13.4 - 24 Jun 2021
    * Fixed/updated multiple models

* Version 6.13.3 - 28 apr 2021
    * Fixed some issues in the IExchangeClient interface
    * Updated ExchangeClient.Net
    * Fixed QueryOCO order id parameter

* Version 6.13.2 - 19 apr 2021
    * Updated CryptoExchange.Net

* Version 6.13.1 - 02 apr 2021
    * Fixed mining endpoints
    * Adjusted TransferSubAccountToSubAccount request
    * Removed invalid symbol check Usdt futures GetKlines

* Version 6.13.0 - 30 mrt 2021
    * Added CancelOpenMarginOrders endpoint
    * Added universal Transfer endpoints
    * Added new mining endpoints
    * Updated futures/loan endpoints to V2
    * Added new subAccount endpoints
    * Added margin GetInterestRateHistory endpoint
    * Updated multiple models
    * Fixed trade rules not applied to Oco orders
    * Fixed mining endpoints not signed
    * Fixed quoteQuantity parameter serialization on GetQuote liquid swap endpoint
    * Fixed GetAssetDetails throwing exception

* Version 6.12.0 - 10 mrt 2021
    * Fixed multiple models

* Version 6.11.1 - 01 mrt 2021
    * Added Nuget SymbolPackage

* Version 6.11.0 - 01 mrt 2021
    * Fixed position models
    * Fixed reduceOnly parameter on PlaceMultipleOrder method
    * Added transactionFeeFlag on Withdraw method
    * Added config for deterministic build
    * Updated CryptoExchange.Net

* Version 6.10.0 - 22 feb 2021
    * IndexPrice added to BinanceFurustMarkPrice
    * Removed deprecated accountInfo update from userDataStream
    * Updated spot Order models

* Version 6.9.1 - 18 feb 2021
    * Fixed inconsistent naming GetPrices

* Version 6.9.0 - 18 feb 2021
    * Updated withdraw models
    * Brokerage API updates
    * Updated GetPrice for coin futures
    * Fixed Swap quantity parameter
    * Added limit parameter to GetDividendRecords

* Version 6.8.2 - 11 feb 2021
    * Fixed more HttpMethods

* Version 6.8.1 - 11 feb 2021
    * Fixed GetOpenOrder endpoint on futures
    * Fixed HttpMethods in subaccount calls

* Version 6.8.0 - 09 feb 2021
    * Fixed stopPrice AutoComply trade rules behavior
    * Fixed position models

* Version 6.7.0 - 05 feb 2021
    * Fixed Mark stream update model
    * Added onLeverageUpdate to futures user stream subscription
    * Updated futures position info models

* Version 6.6.4 - 22 jan 2021
    * Fixed GetSubAccountsFuturesSummary http method
    * Moved FirstUpdateId in order book model
    * Updated for ICommonKline

* Version 6.6.3 - 11 jan 2021
    * Added missing enum values

* Version 6.6.2 - 11 jan 2021
    * Updated futures Symbol models for GetExchangeInfo
    * Updated CryptoExchange.Net
    * Added Pre-Settle symbol status to fix deserialization issue

* Version 6.6.1 - 05 jan 2021
    * Fixed contractType deserialization

* Version 6.6.0 - 05 jan 2021
    * Updated orderbook models
    * Fixed GetExchangeInfo deserialization for Futures

* Version 6.5.0 - 21 dec 2020
    * Updated Brokerage API
    * Fix for SubAccountToSubAccount email parameter serialization
    * Fix for GetSubAccountTransferHistoryForSubAccount using wrong Http method
    * Fix in TradeRules check
    * Updated CryptoExchange.Net, updated IExchangeClient

* Version 6.4.1 - 11 dec 2020
    * Updated CryptoExchange.Net
    * Added IExchangeClient implementation
    * Added missing properties Coin-M Symbol model
    * Added stopPrice checking in AutoComply trade rules behaviour

* Version 6.4.0 - 25 nov 2020
    * Fixed futures book ticker stream data parsing

* Version 6.3.7 - 24 nov 2020
    * Fix parsing of single bracket in GetBrackets
    * Added MaxPosition filter in GetExchangeInfo

* Version 6.3.6 - 19 nov 2020
    * Fixed reference in package

* Version 6.3.5 - 19 nov 2020
    * Added PendingTrade status to futures symbol status mapping
    * Added BNB burn toggle endpoints
    * Added Composite index endpoint to USDT futures
    * Added Composite index stream to USDT futures
    * Added archived parameter for margin loan/repay/interestHistory queries

* Version 6.3.4 - 16 nov 2020
    * Added missig TimeInForce mapping
    * Added missing orderResponseType parameter to futures PlaceOrder

* Version 6.3.3 - 09 nov 2020
    * Updated check for valid Binance symbol
    * Fixed ModifyPositionMode result symbol

* Version 6.3.2 - 23 okt 2020
    * Re-added locking when signing messages to prevent issues when multithreading

* Version 6.3.1 - 22 okt 2020
    * Added missing Interval property on stream kline updates

* Version 6.3.0 - 20 Oct 2020
    * Added BSwap endpoints
    * Added BLVT endpoints
    * Fixed incomeType parameter on GetIncomeHistory
    * Added BorrowLimit property to GetMaxBorrowAmount
    * Added Coin-M futures support for sub-account transfer
    * Added ids to lending borrow/repay results
    * Added ChangeToDailyPosition endpoint
    * Updated Regular to Activity in savings endpoints

* Version 6.2.0 - 08 Oct 2020
    * Added missing transaction timestamp on future user streams
    * Updated book price models
    * Update CryptoExchange.Net

* Version 6.1.0 - 06 Oct 2020
    * Fixed future trade timestamps
    * Fixed some decimal serialization culture issues
    * Updated future user streams to include timestamps
    * Fixed used weight parsing
    * Brokerage API update

* Version 6.0.2 - 17 Sep 2020
    * Fix for socket client receiving intermittent byte data
    * Updated market data interfaces to support inheritance

* Version 6.0.1 - 09 Sep 2020
    * Fixed missing properties in stream kline models

* Version 6.0.0 - 09 Sep 2020
    * Added future transfer endpoints
    * Added cross-collateral endpoints
    * Refactored volume properties to properly be named base/quote
    * Fixed isolated margin all symbols endpoint

* Version 6.0.0-beta.6 - 31 Aug 2020
    * Combined futures userstream Balance and Position update handlers, UpdateReason property added

* Version 6.0.0-beta.5 - 28 Aug 2020
    * Fixed futures order update wrong JsonConverter

* Version 6.0.0-beta.4 - 28 Aug 2020
    * Added support for Coin-M futures
    * Some refactoring

* Version 6.0.0-beta.3 - 17 Aug 2020
    * Fixed GetAccountInfo endpoint

* Version 6.0.0-beta.2 - 13 Aug 2020
    * Fixed ModifyPositionMargin futures call

* Version 6.0.0-beta.1 - 12 Aug 2020
    * Restructured BinanceClient and BinanceSocketClient to include the futures and brokerage API. Clients are now divided per topic
    * Added isolated margin endpoints
    * Fixed MinNotional checking in trade rules when also adjusting price
    * Added shared interfaces for Futures and Spot market data and market stream subscriptions

* Version 5.1.14 - 03 Aug 2020
    * Added check for MinNotional filter when using AutoComply trade rules behaviour
    * Adjusted bool parameter serialization

* Version 5.1.13 - 27 Jul 2020
	* Updated futures balance, account info, position endpoints to version 2
	* Added missing futures market data endpoints

* Version 5.1.12 - 21 Jul 2020
    * Updated order book models

* Version 5.1.11 - 20 Jul 2020
    * Fixes for future client

* Version 5.1.10 - 07 Jul 2020
    * Fixed datetime conversion for some objects

* Version 5.1.9 - 06 Jul 2020
    * Added CancelMultipleOrders
    * Added CancelAllOrders
    * Added EventTime to OrderBook stream
    * Fixed purchase record conversion

* Version 5.1.8 - 21 Jun 2020
    * Updated CryptoExchange

* Version 5.1.7 - 16 Jun 2020
    * Changed IncomeType to string, Update CryptoExchange.Net

* Version 5.1.6  - 11 Jun 2020
	* Fixed subscribe error on symbols with an `I` caused by unset culture info

* Version 5.1.5 - 07 Jun 2020
	* Fixed serialization/encryption bug

* Version 5.1.4 - 02 Jun 2020
	* Fixed empty request bug

* Version 5.1.3 - 02 Jun 2020
    * Added CancelAllOrders endpoint
    * Added PlaceMultipleOrders endpoint for futures
    * Added BinanceFuturesSymbolOrderBook
    * Added missing Expired order status mapping
    * Added GetBrackets to futures client

* Version 5.1.2 - 26 May 2020
    * Added CancelAllOrdersAfterTimeout futures endpoint
    * Added timestamp to various models
    * Added closePosition paramter for future orders

* Version 5.1.1 - 20 May 2020
    * Fixed ChangeInitialLeverage endpoint
    * Fixed ChangeMarginType endpoint
    * Fixed deserialization error on maxNotionalValue
    * Updated CryptoExchange.Net

* Version 5.1.0 - 20 may 2020
	* Bumped to release version
	* Added missing wallet endpoints
	* Added sub-account endpoints
	* Added savings endpoints

* Version 5.1.0-alpha10 - 08 May 2020
    * Added Brokerage client, various fixes, added some missing parameters

* Version 5.1.0-alpha9 - 01 May 2020
    * Fixed GetExchangeInfo call, merged master

* Version 5.1.0-alpha8 - 19 Mar 2020
    * Futures update

* Version 5.1.0-alpha7 - 16 Mar 2020
    * fixed reference

* Version 5.1.0-alpha6 - 16 Mar 2020
    * Fixed ticker stream

* Version 5.1.0-alpha5 - 16 Mar 2020
    * Futures update

* Version 5.1.0-alpha4 - 06 Mar 2020
    * Actual stream fixes

* Version 5.1.0-alpha3 - 06 Mar 2020
    * Futures stream fixes

* Version 5.1.0-alpha2 - 03 Mar 2020
    * Updated CryptoExchange version

* Version 5.1.0-alpha - 03 Mar 2020
    * First version Futures Api implementation

* Version 5.0.10 - 01 May 2020
    * Fixed filter parsing in GetExchangeInfo

* Version 5.0.9 - 03 Mar 2020
    * Fixed serialization issue on DustTransfer assets parameter

* Version 5.0.8 - 03 Mar 2020
    * Added SideEffectType and MarginBuyBorrow properties
    * Added trade rules check for margin orders

* Version 5.0.7 - 05 Feb 2020
    * Fixed incorrect Invalid symbol error

* Version 5.0.6 - 27 Jan 2020
    * Updated CryptoExchange.Net

* Version 5.0.5 - 23 Jan 2020
    * Added option for custom url

* Version 5.0.4 - 10 Dec 2019
    * Fix for BinanceSymbolOrderBook

* Version 5.0.3 - 13 Nov 2019
    * Updated for new API version
    * Added QuoteOrderQuantity parameter/property
    * Add stream balance update
    * Added precisions to ExchangeInfo symbols

* Version 5.0.1 - 23 Oct 2019
	* Fixed validation for 9 length symbols
	
* Version 5.0.1 - 23 Oct 2019
	* Fixed validation for 5 length symbols

* Version 5.0.0 - 23 Oct 2019
	* See CryptoExchange.Net 3.0 release notes
	* Added input validation
	* Added CancellationToken support to all requests
	* Now using IEnumerable<> for collections
	* Renamed various methods to be more in line with other exchanges
	* Renamed SubscribeToXXXStream to SubscribeToXXXUpdates

* Version 4.3.3 - 06 Oct 2019
    * Added serialization method for BinanceSymbolFilter

* Version 4.3.2 - 25 Sep 2019
    * Added missing AddressTag and TransactionFee properties in withdrawal object

* Version 4.3.1 - 03 Sep 2019
    * Added book ticker stream to socket client

* Version 4.3.0 - 02 Sep 2019
    * Added new Margin endpoints
    * Renamed Query- methods to Get- methods for consistency

* Version 4.2.3 - 29 Aug 2019
    * Added DustTransfer and GetDividendRecords endpoints
    * Added updateInterval parameter to depth streams

* Version 4.2.2 - 20 Aug 2019
    * Added missing margin endpoints
    * IndicatorType to enum

* Version 4.2.1 - 19 Aug 2019
    * Added current average price endpoint

* Version 4.2.0 - 15 Aug 2019
    * Implemented OCO orders
    * Adjustments for API update

* Version 4.1.3 - 12 Aug 2019
    * Fix margin order cancel

* Version 4.1.2 - 07 Aug 2019
    * Updated CryptoExchange.Net

* Version 4.1.1 - 05 Aug 2019
    * Added xml file for code docs

* Version 4.1.0 - 30 Jul 2019
    * Added margin API

* Version 4.0.17 - 09 jun 2019
	* Added TimestampOffset options
	* Update BinanceSymbolOrderBook

* Version 4.0.16 - 20 may 2019
	* Fixed AutoComply trade rules behavior

* Version 4.0.15 - 16 may 2019
	* Fixed order book limit implementation

* Version 4.0.14 - 14 may 2019
	* Added an order book implementation for easily keeping an updated order book
	* Added additional constructor to ApiCredentials to be able to read from file

* Version 4.0.13 - 01 may 2019
	* Updated to latest CryptoExchange.Net
		* Adds response header to REST call result
		* Added rate limiter per API key
		* Unified socket client workings

* Version 4.0.12 - 09 apr 2019
	* Fixed type in FifteenMinutes kline interval enum
	* Added update time to BinanceStreamAccountInfo
	* Added IsSpotTradingAllowed and IsMarginTradingAllowed fields to BinanceSymbol
	* Added IDisposable to client interfaces

* Version 4.0.11 - 02 apr 2019
	* Added Symbol field in BinanceTrade
	* Added deposit status Completed to deposits filter
	* Fixed Exception handler null reference if not set

* Version 4.0.10 - 18 mar 2019
	* Added AutoReconnect option
	* Fix for error parsing without code/message
	* Added QuoteQuantity to MyTrades result

* Version 4.0.9 - 07 mar 2019
	* Added start/end time parameters to GetAllOrders
	* Updated CryptoExchange.Net

* Version 4.0.8 - 27 feb 2019
	* Added sub account support
	* Added trading status call
	* Changed CallResult to WebCallResult for REST requests to expose the response status

* Version 4.0.7	- 01 feb 2019
	* Added exception event to subscriptions
	* General fixes

* Version 4.0.6 - 10 jan 2019
	* Fix for timestamp calculation

* Version 4.0.5 - 09 jan 2019
	* Adjusted AutoTimestamp calculation

* Version 4.0.4 - 28 dec 2018
	* Another fix for reconnecting

* Version 4.0.3 - 17 dec 2018
	* Fixed reconnecting sometimes throwing error

* Version 4.0.2 - 10 dec 2018
	* TradeRuleBehavior.AutoComply rounding fix

* Version 4.0.1 - 06 dec 2018
	* Fix for freezes if called from UI thread
	* Fixed AutoComply trade rules behavior
	* Fixed IDisposable interface

* Version 4.0.0 - 05 dec 2018
	* Updated to CryptoExchange.Net version 2
		* Libraries now use the same standard functionalities
		* Objects returned by socket subscriptions standardized across libraries
	* Added start/endtime parameters to GetMyTrades

* Version 3.3.0 - 15 nov 2018
	* Updated to support latest Binance API update, including:
		* Added RawRequest rate limit
		* Canceling an order now returns full order report
		* Added multiple symbol filters
		* Added LastQuoteTransactedQuantity to socket order update

* Version 3.2.12 - 15 nov 2018
	* Added event time to BinanceStreamTick

* Version 3.2.11 - 01 nov 2018
	* Exception handling in error response parsing

* Version 3.2.10 - 24 oct 2018
	* AutoTimestamp now enabled by default
	* BaseAssetPrecision and QuoteAssetPrecision type from string to int

* Version 3.2.9 - 18 oct 2018
	* Added default receiveWindow parameter to client options
	* Updated time calculation between server/client, should help people with unstable ping who got intermittent errors saying the local time was ahead of server time

* Version 3.2.8 - 04 oct 2018
	* Fixed subscriptions trying to reconnect if initial subscribe fails
	* Added accessors for symbol filters
	* Fix subscription reconnections

* Version 3.2.7 - 21 sep 2018
	* Updated CryptoExchange.Net

* Version 3.2.6 - 17 sep 2018
	* Combined PartialBookDepthStream data object with DepthStream data object
	* Fix reconnection bug

* Version 3.2.5 - 10 sep 2018
	* Added check for failed auto timestamp syncing
	* Added auto recalculation interval for auto timestamp

* Version 3.2.4 - 07 sep 2018
	* Fixed proxy setting on socket client

* Version 3.2.3 - 21 aug 2018
	* Fix for previous fix..

* Version 3.2.2 - 21 aug 2018
	* Fix for default api credentials getting disposed

* Version 3.2.1 - 20 aug 2018
	* Update CryptoExchange.Net for bugfix

* Version 3.2.0 - 16 aug 2018
	* Added socket client interface
	* Moved interface to interface namespace
	* Fixed some minor Resharper findings

* Version 3.1.18 - 13 aug 2018
	* Fix for userstream not connecting

* Version 3.1.17 - 13 aug 2018
	* Updated CryptoExchange.Net to fix bug

* Version 3.1.16 - 13 aug 2018
	* Updated CryptoExchange.Net
	* Fixed error response parsing

* Version 3.1.15 - 24 jul 2018
	* Fixed missing Symbol filter type

* Version 3.1.14 - 20 jul 2018
	* Added error parsing to code/message

* Version 3.1.13 - 19 jul 2018
	* Update to latest api update adding various properties/filters

* Version 3.1.12 - 17 jul 2018
	* Added GetAccountStatus endpoint
	* Added GetSystemStatus endpoint
	* Added GetDustLog endpoint

* Version 3.1.11 - 16 jul 2018
	* Fix for UI thread freezing when unsubscribing a stream

* Version 3.1.10 - 03 jul 2018
	* Small fix for socket event binding

* Version 3.1.9 - 25 jun 2018
	* Fix for Unsubscribe freezing if called from UI thread

* Version 3.1.8 - 08 jun 2018
	* Fix for DateTime parsing

* Version 3.1.7 - 08 jun 2018
	* Added missing TradeId field in PlaceOrder response

* Version 3.1.6 - 04 jun 2018
	* Fix for OrderUpdate mapping
	* Fix for BinanceSymbol mapping

* Version 3.1.5 - 07 may 2018
	* Added resetAutoTimestamp parameter to server time

* Version 3.1.4 - 03 may 2018
	* Additional debug logging

* Version 3.1.3 - 30 apr 2018
	* Refactored combined streams to be easier to use and reduce duplicate code
	* Fixed threadsafety issue in authenticator

* Version 3.1.2 - 19 apr 2018
	* Added combined streams

* Version 3.1.1 - 16 apr 2018
	* Added receiveWindow parameter to PlaceOrder

* Version 3.1.0 - 27 mar 2018
	* Added GetWithdrawalFee call
	* Refactored Klines some to have the stream and the rest data be more similair
	* Added code docs where missing

* Version 3.0.11 - 23 mar 2018
	* Updated closed/reconnect handling for sockets
	* Updated base

* Version 3.0.10 - 21 mar 2018
	* Now possible to add multiple log writers
	* Added automatic reconnecting after loss of internet
	* Fixed error when subsribing to a stream while passing Null as handler

* Version 3.0.9 - 13 mar 2018
	* Added trade stream next to aggregated trade stream
	* Fix for BuyerIsMaker field always being true

* Version 3.0.8 - 12 mar 2018
	* Fix for inconsistent int/long types
	* Fix for freezing when making calls from UI thread
	* Added auto reconnect functionality

* Version 3.0.7 - 08 mar 2018
	* Updated base

* Version 3.0.6 - 08 mar 2018
	* Fix for socket connecting in non dotnet core clients

* Version 3.0.5 - 07 mar 2018
	* Fix for deserialization error handling
	* Socket opening async, subscribe methods async

* Version 3.0.4 - 05 mar 2018
	* Added SetApiCredentials methods

* Version 3.0.2/3.0.3 - 05 mar 2018
	* SetDefaultOptions made static

* Version 3.0.1 - 05 mar 2018
	* Additional logging
	* Updated base verions

* Version 3.0.0 - 01 mar 2018
	* Updated to use a base package, which introduces some changes in syntax, but keeps functionality unchanged
	
* Version 2.3.4 - 12 feb 2018
	* Fix for AutoComply trading rules sending too much trailing zero's

* Version 2.3.3 - 10 feb 2018
	* Fix for stream order parsing

* Version 2.3.2 - 09 feb 2018
	* Changed base address from https://www.binance.com to https://api.binance.com to fix connection errors

* Version 2.3.1 - 08 feb 2018
	* Updated models to latest version
	* Cleaned code and code docs

* Version 2.3.0 - 07 feb 2018
	* Added missing fields to 24h prices
	* Changed subscription results from an id to an object with closed/error events
	* Changed how to subscribe to the user stream
	* Updated/fixed unit test project
	* Updated readme

* Version 2.2.5 - 24 jan 2018
	* Added optional automated checking of trading rules when placing an order
	* Added `BinanceHelpers` static class containing some basic helper functions
	* Fix for default logger not writing on a new line
	* Simplified internal defaults

* Version 2.2.4 - 23 jan 2018
	* Fix for RateLimit type in GetExchangeInfo
	* Split the BinanceSymbolFilter in 3 classes

* Version 2.2.3 - 15 jan 2018
	* Fix for calls freezing when made from UI thread

* Version 2.2.2 - 15 jan 2018
	* Fix in PlaceOrder using InvariantCulture
	* Fix for FirstId property in 24h price
	* Added symbol property to 24h price

* Version 2.2.1 - 12 jan 2018
	* Fix for parse error in StreamOrderUpdate

* Version 2.2.0 - 08 jan 2018
	* Updated according to latest documentation, adding various endpoints

* Version 2.1.3 - 9 nov 2017
	* Added automatic configurable retry on server errors
	* Refactor on error returns
	* Renamed ApiResult to BinanceApiResult
	
* Version 2.1.2 - 31 okt 2017
	* Added alot of code documentation
	* Small cleanups and fix

* Version 2.1.1 - 30 okt 2017
	* Fix for socket closing

* Version 2.1.0 - 30 okt 2017
	* Small rename/refactor, BinanceSocketClient also use ApiResult now

* Version 2.0.1 - 30 okt 2017
	* Improved error messages/handling in BinanceClient
	* Extra unit tests for failing requests

* Version 2.0.0 - 25 okt 2017
	* Changed from static class to object orriented, added IDisposable interface to be able to use `using` statements
	* Split websocket and restapi functionality in BinanceClient and BinanceSocketClient
	* Added method to set log output writer
	* Added abitlity to set defaults for new clients
	* Fixed unit tests for new setup
	* Updated documentation

* Version 1.1.2 - 25 okt 2017 
	* Added `UnsubscribeAllStreams` method

* Version 1.1.1 - 20 okt 2017 
	* Fix for withdrawal/deposit filter

* Version 1.1.0 - 20 okt 2017 
	* Updated withdrawal/deposit functionality according to API changes
	* Cleaned up BinanceClient a bit

* Version 1.0.9 - 19 okt 2017 
	* Added withdrawal/deposit functionality
