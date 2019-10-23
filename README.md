# ![Icon](https://github.com/JKorf/Binance.Net/blob/master/Binance.Net/Icon/icon.png?raw=true) Binance.Net 

![Build status](https://travis-ci.org/JKorf/Binance.Net.svg?branch=master)

A .Net wrapper for the Binance API as described on [Binance](https://www.binance.com/restapipub.html), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Binance.Net/issues)**

## CryptoExchange.Net
Implementation is build upon the CryptoExchange.Net library, make sure to also check out the documentation on that: [docs](https://github.com/JKorf/CryptoExchange.Net)

Other CryptoExchange.Net implementations:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Huobi.Net"><img src="https://github.com/JKorf/Huobi.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Huobi.Net">Huobi</a>
</td>
<td><a href="https://github.com/JKorf/Kucoin.Net"><img src="https://github.com/JKorf/Kucoin.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kucoin.Net">Kucoin</a>
</td>
<td><a href="https://github.com/JKorf/Kraken.Net"><img src="https://github.com/JKorf/Kraken.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kraken.Net">Kraken</a>
</td>
</tr>
</table>
Implementations from third parties:
<table>
<tr>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
</tr>
</table>

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  


## Installation
![Nuget version](https://img.shields.io/nuget/v/binance.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Binance.Net.svg)
Available on [Nuget](https://www.nuget.org/packages/Binance.Net/).
```
pm> Install-Package Binance.Net
```
To get started with Binance.Net first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/Binance.Net/). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'Binance.Net' and hit enter. The Binance.Net package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Binance.Net in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Binance.Net will be installed in. After selecting the correct project type  `Install-Package Binance.Net`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Binance.Net.
## Getting started
After installing it's time to actually use it. To get started we have to add the Binance.Net namespace:  `using Binance.Net;`.

Binance.Net provides two clients to interact with the Binance API. The  `BinanceClient`  provides all rest API calls. The  `BinanceSocketClient`  provides functions to interact with the websocket provided by the Binance API. Both clients are disposable and as such can be used in a  `using`statement.

## Examples
Examples can be found in the Examples folder.

## Timestamping
Requests made to Binance are checked for a correct timestamp. When requests are send a timestamp is added to the message. When Binance processes the message the timestamp is checked to be > the current time and < the current time + 5000ms (default). If the timestamp is outside these limits the following errors will be returned:
`timestamps 1000ms ahead of server time` or `Timestamp for this request is outside of the recvWindow`
The recvWindow is default 5000ms and can be changed using the `ReceiveWindow` configuration option. All times are communicated in UTC so there won't be any timezone issues. However, because of clock drifting it can be that the client UTC time is not the same as the server UTC time. It is therefor recommended clients use the `SP TimeSync` program to resync the client UTC time more often than windows does by default (every 10 minutes or less is recommended).

## Websockets
The Binance.Net socket client provides several socket endpoint to which can be subscribed.

**Public socket endpoints:**
```C#
using(var client = new BinanceSocketClient())
{
	var successDepth = client.SubscribeToDepthStream("bnbbtc", (data) =>
	{
		// handle data
	});
	var successTrades = client.SubscribeToTradesStream("bnbbtc", (data) =>
	{
		// handle data
	});
	var successKline = client.SubscribeToKlineStream("bnbbtc", KlineInterval.OneMinute, (data) =>
	{
		// handle data
	});
	var successSymbol = client.SubscribeToSymbolTicker("bnbbtc", (data) =>
	{
		// handle data
	});
	var successSymbols = client.SubscribeToAllSymbolTicker((data) =>
	{
		// handle data
	});
	var successOrderBook = client.SubscribeToPartialBookDepthStream("bnbbtc", 10, (data) =>
	{
		// handle data
	});
}
```

**Private socket endpoints:**

For the private endpoint a user stream has to be started on the Binance server. This can be done using the `StartUserStream()` method in the `BinanceClient`. This command will return a listen key which can then be provided to the private socket subscription:
```C#
using(var client = new BinanceSocketClient())
{
	var successOrderBook = client.SubscribeToUserStream(listenKey, 
	(accountInfoUpdate) =>
	{
		// handle account info update
	},
	(orderInfoUpdate) =>
	{
		// handle order info update
	});
}
```

When no longer listening to private endpoints the `client.StopUserStream` method in `BinanceClient` should be used to signal the Binance server the stream can be closed.


## Release notes
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
