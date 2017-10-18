# Binance.Net
A .Net wrapper for the Binance API as described on [Binance](https://www.binance.com/restapipub.html)
## Installation
Binance.Net is available on [Nuget](https://www.nuget.org/packages/Binance.Net/).
Install using the package manager `pm> Install-Package Binance.Net`

## Examples
Two examples have been provided, a console application providing the basis interaction with the API wrapper, and a WPF application showing some more advanced use casus. Both can be found in the Examples folder.

## Usage
For most API methods Binance.Net provides two versions, synchronized and async calls. Start using the API by including `using Binance.Net;` in your usings.

### Response handling
All API requests will respond with an ApiResult object. This object contains wether the call was successful, the data returned from the call and an error message if the call wasn't successful. As such, one should always check the Success flag when processing a response.
For example:
```
var allPrices = BinanceClient.GetAllPrices();
if (allPrices.Success)
{
	foreach (var price in allPrices.Data)
		Console.WriteLine($"{price.Symbol}: {price.Price}");
}
else
	Console.WriteLine($"Error: {allPrices.Error.Message}");
```
### Setting API credentials
For private endpoints (trading, order history, account info etc) an API key and secret has to be provided. For this the SetAPICredentials method can be used:
```
BinanceClient.SetAPICredentials("APIKEY", "APISECRET");
```
API credentials can be managed at https://www.binance.com/userCenter/createApi.html

### Requests
Public requests:
```
var ping = BinanceClient.Ping();
var serverTime = BinanceClient.GetServerTime();
var orderBook = BinanceClient.GetOrderBook("BNBBTC", 10);
var aggTrades = BinanceClient.GetAggregatedTrades("BNBBTC", startTime: DateTime.UtcNow.AddMinutes(-2), endTime: DateTime.UtcNow, limit: 10);
var klines = BinanceClient.GetKlines("BNBBTC", KlineInterval.OneHour, startTime: DateTime.UtcNow.AddHours(-10), endTime: DateTime.UtcNow, limit: 10);
var prices24h = BinanceClient.Get24HPrices("BNBBTC");
var allPrices = BinanceClient.GetAllPrices();
var allBookPrices = BinanceClient.GetAllBookPrices();
```

Private requests:
```
var openOrders = BinanceClient.GetOpenOrders("BNBBTC");
var allOrders = BinanceClient.GetAllOrders("BNBBTC");
var testOrderResult = BinanceClient.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancel, 1, 1);
var queryOrder = BinanceClient.QueryOrder("BNBBTC", allOrders.Data[0].OrderId);
var orderResult = BinanceClient.PlaceOrder("BNBBTC", OrderSide.Sell, OrderType.Limit, TimeInForce.GoodTillCancel, 10, 0.0002);
var cancelResult = BinanceClient.CancelOrder("BNBBTC", orderResult.Data.OrderId);
var accountInfo = BinanceClient.GetAccountInfo();
var myTrades = BinanceClient.GetMyTrades("BNBBTC");
```

### Websockets
The Binance.Net client provides several socket endpoint to which can be subsribed.
Public socket endpoints:
```
var successDepth = BinanceClient.SubscribeToDepthStream("bnbbtc", (data) =>
{
	// handle data
});
var successTrades = BinanceClient.SubscribeToTradesStream("bnbbtc", (data) =>
{
	// handle data
});
var successKline = BinanceClient.SubscribeToKlineStream("bnbbtc", KlineInterval.OneMinute, (data) =>
{
	// handle data
});
```

Private socket endpoints:
For the private endpoints a user stream has to be started on the Binance server. This can be done using the `BinanceClient.StartUserStream()` command. This call should be made before subscribing to private socket endpoints.
```
var successAccount = BinanceClient.SubscribeToAccountUpdateStream((data) =>
{
	// handle data
});
var successOrder = BinanceClient.SubscribeToOrderUpdateStream((data) =>
{
	// handle data
});
```

Unsubscribing from socket endpoints:
Public socket endpoints can be unsubscribed by using the `BinanceClient.UnsubscribeFromStream` method in combination with the stream ID received from subscribing:
```
var successDepth = BinanceClient.SubscribeToDepthStream("bnbbtc", (data) =>
{
	// handle data
});

BinanceClient.UnsubscribeFromStream(successDepth.StreamId);
```

Private socket endpoints can be unsubscribed using the specific methods `BinanceClient.UnsubscribeFromAccountUpdateStream` and `BinanceClient.UnsubscribeFromOrderUpdateStream`.

When no longer listening the `BinanceClient.StopUserStream` method should be used to signal the Binance server the stream can be closed.

### AutoTimestamp
For some private calls a timestamp has to be send to the Binance server. This timestamp in combination with the recvWindow parameter in the request will determine how long the request will be valid. If the more than the recvWindow in miliseconds has passed since the provided timestamp the request will be rejected.

While testing I found that my local computer time was offset to the Binance server time, which made it reject all my requests. I added a fix for this in the Binance.Net client which will automatically calibrate the timestamp to the Binance server time. This behaviour is turned off by default and can be turned on using the `BinanceClient.AutoTimeStamp` property. 
