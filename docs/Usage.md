---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Binance API, the `BinanceClient` and `BinanceSocketClient`.

*Create a new rest client*
```csharp
var binanceClient = new BinanceClient(new BinanceClientOptions()
{
	// Set options here for this client
});
```

*Create a new socket client*
```csharp
var binanceSocketClient = new BinanceSocketClient(new BinanceSocketClientOptions()
{
	// Set options here for this client
});
```

Different options are available to set on the clients, see this example
```csharp
var binanceClient = new BinanceClient(new BinanceClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	SpotApiOptions = new BinanceApiClientOptions
	{
		BaseAddress = "ADDRESS",
		RateLimitingBehaviour = RateLimitingBehaviour.Fail
	},
	UsdFuturesApiOptions = new BinanceApiClientOptions
	{
		ApiCredentials = new ApiCredentials("OTHER-API-KEY-FOR-FUTURES", "OTHER-API-SECRET-FOR-FUTURES")
	}
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions`:
```csharp
BinanceClient.SetDefaultOptions(new BinanceClientOptions{
	// Set options here for all new clients
});
var binanceClient = new BinanceClient();
```
More info on the specific options can be found in the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#dependency-injection)
