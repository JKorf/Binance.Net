---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Binance API, the `BinanceRestClient` and `BinanceSocketClient`. They can be created manually on the fly or be added to the dotnet DI using the `AddBinance` extension method.

*Manually create a new client*
```csharp
var binanceRestClient = new BinanceRestClient(options =>
{
	// Set options here for this client
});

var binanceSocketClient = new BinanceSocketClient(options =>
{
	// Set options here for this client
});
```

*Using dotnet dependency inject*
```csharp
services.AddBinance(
	restOptions => {
		// set options for the rest client
	},
	socketClientOptions => {
		// set options for the socket client
	});	
	
// IBinanceRestClient, IBinanceSocketClient and IBinanceOrderBookFactory are now available for injecting
```

Different options are available to set on the clients:  
```csharp
var binanceRestClient = new BinanceRestClient(options =>
{
	options.ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET");
	options.Environment = BinanceEnvironment.Testnet;
	options.UsdFuturesOptions.ApiCredentials = new ApiCredentials("OTHER-API-KEY", "OTHER-API-SECRET"); // Override the credentials for the USD futures API
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions` or during the registration in the DI container:  
```csharp
BinanceRestClient.SetDefaultOptions(options =>
{
	// Set options here for all new clients
});
var binanceClient = new BinanceRestClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Dependency%20Injection.html)
