# Binance.Net ![Icon](https://github.com/JKorf/Binance.Net/blob/master/Resources/binance-coin.png?raw=true)
![Nuget version](https://img.shields.io/nuget/v/binance.net.svg)

A .Net wrapper for the Binance API as described on [Binance](https://www.binance.com/restapipub.html), including all features.
## Installation
Binance.Net is available on [Nuget](https://www.nuget.org/packages/Binance.Net/).
```
pm> Install-Package Binance.Net
```

## Examples
Two examples have been provided, a console application providing the basis interaction with the API wrapper, and a WPF application showing some more advanced use casus. Both can be found in the Examples folder.

## Usage
Start using the API by including `using Binance.Net;` in your usings.
Binance.Net provides two clients to interact with the Binance API. The `BinanceClient` provides all rest API calls. The `BinanceSocketClient` provides functions to interact with the Websockets provided by the Binance API. Both clients are disposable and as such can be used in a `using` statement:
```C#
using(var client = new BinanceClient())
{
}

using(var client = new BinanceSocketClient())
{
}
```

For most API methods Binance.Net provides two versions, synchronized and async calls. 

### Setting API credentials
For private endpoints (trading, order history, account info etc) an API key and secret has to be provided. For this the `SetApiCredentials` method can be used in both clients, or the credentials can be provided as arguments:
```C#
using(var client = new BinanceClient("APIKEY", "APISECRET"))
{
	client.SetApiCredentials("APIKEY", "APISECRET");
}
```
Alternatively the credentials can be set as default in BinanceDefaults to provide them to all new clients.
```C#
BinanceDefaults.SetDefaultApiCredentials("APIKEY", "APISECRET");
```
API credentials can be managed at https://www.binance.com/userCenter/createApi.html. Make sure to enable the required permission for the right API calls.

### Response handling
All API requests will respond with an ApiResult object. This object contains whether the call was successful, the data returned from the call and an error message if the call wasn't successful. As such, one should always check the Success flag when processing a response.
For example:
```C#
using(var client = new BinanceClient())
{
	var allPrices = client.GetAllPrices();
	if (allPrices.Success)
	{
		foreach (var price in allPrices.Data)
			Console.WriteLine($"{price.Symbol}: {price.Price}");
	}
	else
		Console.WriteLine($"Error: {allPrices.Error.Message}");
}
```

### Requests
Public requests:
```C#
using(var client = new BinanceClient())
{
	// Pings the API to check the connection
	var ping = client.Ping();
	// Gets the server time
	var serverTime = client.GetServerTime();
	// Gets the order book for specified symbol
	var orderBook = client.GetOrderBook("BNBBTC", 10);
	// Gets a compresed view of trades for specified symbol
	var aggTrades = client.GetAggregatedTrades("BNBBTC", startTime: DateTime.UtcNow.AddMinutes(-2), endTime: DateTime.UtcNow, limit: 10);
	// Gets klines data for the specified symbol
	var klines = client.GetKlines("BNBBTC", KlineInterval.OneHour, startTime: DateTime.UtcNow.AddHours(-10), endTime: DateTime.UtcNow, limit: 10);
	// Gets prices and changes in the last 24 hours for specified symbol
	var prices24h = client.Get24HPrices("BNBBTC");
	// Gets all symbols and latest prices
	var allPrices = client.GetAllPrices();
	// Gets book prices (asks/bids) for all symbols
	var allBookPrices = client.GetAllBookPrices();
}
```

Private requests:
```C#
using(var client = new BinanceClient())
{
	// Gets all open orders for specified symbol
	var openOrders = client.GetOpenOrders("BNBBTC");
	// Gets all orders for specified symbol
	var allOrders = client.GetAllOrders("BNBBTC");
	// Places a test order to test the API functionality. No order will actually be placed
	var testOrderResult = client.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancel, 1, 1);
	// Request information about an order
	var queryOrder = client.QueryOrder("BNBBTC", allOrders.Data[0].OrderId);
	// Places an order
	var orderResult = client.PlaceOrder("BNBBTC", OrderSide.Sell, OrderType.Limit, TimeInForce.GoodTillCancel, 10, 0.0002);
	// Cancels an existing order
	var cancelResult = client.CancelOrder("BNBBTC", orderResult.Data.OrderId);
	// Gets information about your account
	var accountInfo = client.GetAccountInfo();
	// Gets all trades for specified symbol
	var myTrades = client.GetMyTrades("BNBBTC");
	// Gets your deposit history
	var depositHistory = client.GetDepositHistory();
	// Gets your withdraw history
	var withdrawalHistory = client.GetWithdrawHistory();
	// Requests a withdraw
	var withdraw = client.Withdraw("TEST", "Address", 1, "TestWithdraw");
}
```

### Websockets
The Binance.Net socket client provides several socket endpoint to which can be subsribed.

Public socket endpoints:
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
}
```

Private socket endpoints:

For the private endpoints a user stream has to be started on the Binance server. This can be done using the `client.StartUserStream()` command in the `BinanceClient`. This command will return a listen key which can then be provided to the private socket subscriptions:
```C#
using(var client = new BinanceSocketClient())
{
	var successAccount = client.SubscribeToAccountUpdateStream(listenKey, (data) =>
	{
		// handle data
	});
	var successOrder = client.SubscribeToOrderUpdateStream(listenKey, (data) =>
	{
		// handle data
	});
}
```

Unsubscribing from socket endpoints:
Public socket endpoints can be unsubscribed by using the `client.UnsubscribeFromStream` method in combination with the stream ID received from subscribing:
```C#
using(var client = new BinanceSocketClient())
{
	var successDepth = client.SubscribeToDepthStream("bnbbtc", (data) =>
	{
		// handle data
	});

	client.UnsubscribeFromStream(successDepth.StreamId);
}
```

Private socket endpoints can be unsubscribed using the specific methods `client.UnsubscribeFromAccountUpdateStream` and `client.UnsubscribeFromOrderUpdateStream`.

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

### AutoTimestamp
For some private calls a timestamp has to be send to the Binance server. This timestamp in combination with the recvWindow parameter in the request will determine how long the request will be valid. If more than the recvWindow in miliseconds has passed since the provided timestamp the request will be rejected.

While testing I found that my local computer time was offset to the Binance server time, which made it reject all my requests. I added a fix for this in the Binance.Net client which will automatically calibrate the timestamp to the Binance server time. This behaviour is turned off by default and can be turned on using the `client.AutoTimeStamp` property. 


### Logging
Binance.Net will by default log warning and error messages. To change the verbosity `SetLogVerbosity` can be called on a client. The default log verbosity for all new clients can also be set using the `SetDefaultLogVerbosity` in `BinanceDefaults`.

Binance.Net logging will default to logging to the Trace (Trace.WriteLine). This can be changed with the `SetLogOutput` method on clients. Alternatively a default output can be set in the `BinanceDefaults` using the `SetDefaultLogOutput` method:
```C#
BinanceDefaults.SetDefaultLogOutput(Console.Out);
BinanceDefaults.SetDefaultLogVerbosity(LogVerbosity.Debug);
```


## Release notes
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