# ![Icon](https://github.com/JKorf/Binance.Net/blob/master/Resources/binance-coin.png?raw=true) Binance.Net 

![Build status](https://travis-ci.org/JKorf/Binance.Net.svg?branch=master)

A .Net wrapper for the Binance API as described on [Binance](https://www.binance.com/restapipub.html), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Binance.Net/issues)**

---
Also check out my other exchange API wrappers:
<table>
<tr>
<td><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Resources/icon.png?raw=true">
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Resources/icon.png?raw=true">
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a></td>
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

Most API methods are available in two flavors, sync and async:
````C#
public void NonAsyncMethod()
{
    using(var client = new BinanceClient())
    {
        var result = client.Ping();
    }
}

public async Task AsyncMethod()
{
    using(var client = new BinanceClient())
    {
        var result2 = await client.PingAsync();
    }
}
````

## Examples
Examples can be found in the Examples folder.


## Response handling
All API requests will respond with an CallResult object. This object contains whether the call was successful, the data returned from the call and an error if the call wasn't successful. As such, one should always check the Success flag when processing a response.
For example:
```C#
using(var client = new BinanceClient())
{
	var result = client.GetServerTime();
	if (result.Success)
		Console.WriteLine($"Server time: {result.Data}");
	else
		Console.WriteLine($"Error: {result.Error.Message}");
}
```
## Options & Authentication
The default behavior of the clients can be changed by providing options to the constructor, or using the `SetDefaultOptions` before creating a new client. Api credentials can be provided in the options.

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

**Handling socket events**

Subscribing to a socket stream returns a BinanceStreamSubscription object. This object can be used to be notified when a socket closes or an error occures:
````C#
var sub = client.SubscribeToAllSymbolTicker(data =>
{
	Console.WriteLine("Reveived list update");
});

sub.Data.Closed += () =>
{
	Console.WriteLine("Socket closed");
};

sub.Data.Error += (e) =>
{
	Console.WriteLine("Socket error " + e.Message);
};
````

**Unsubscribing from socket endpoints:**

Sockets streams can be unsubscribed by using the `client.UnsubscribeFromStream` method in combination with the stream subscription received from subscribing:
```C#
using(var client = new BinanceSocketClient())
{
	var successDepth = client.SubscribeToDepthStream("bnbbtc", (data) =>
	{
		// handle data
	});

	client.UnsubscribeFromStream(successDepth.Data);
}
```

Additionaly, all sockets can be closed with the `UnsubscribeAllStreams` method. Beware that when a client is disposed the sockets are automatically disposed. This means that if the code is no longer in the using statement the eventhandler won't fire anymore. To prevent this from happening make sure the code doesn't leave the using statement or don't use the socket client in a using statement:
```C#
// Doesn't leave the using block
using(var client = new BinanceSocketClient())
{
	var successDepth = client.SubscribeToDepthStream("bnbbtc", (data) =>
	{
		// handle data
	});

	Console.ReadLine();
}

// Without using block
var client = new BinanceSocketClient();
client.SubscribeToDepthStream("bnbbtc", (data) =>
{
	// handle data
});
```

When no longer listening to private endpoints the `client.StopUserStream` method in `BinanceClient` should be used to signal the Binance server the stream can be closed.


## Release notes
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