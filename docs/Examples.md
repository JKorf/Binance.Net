---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

<Details>
<Summary>
<b>General API</b>

</Summary>
<BlockQuote>

### Subaccounts management
```csharp
// Get sub account list
var subAccountBalances = await binanceClient.GeneralApi.SubAccount.GetSubAccountsAsync();

// Transfer 0.001m BTC from master account to a subaccount
var transferResult = await binanceClient.GeneralApi.SubAccount.TransferSubAccountAsync(
                    TransferAccountType.Spot, 
                    TransferAccountType.UsdtFuture, 
                    "BTC",
                    0.001m,
                    toEmail: "test@test.com");
```

### Futures collateral
```csharp
// Add collateral
var result = await binanceClient.GeneralApi.Futures.AdjustCrossCollateralLoanToValueAsync("BTC", "BUSD", 1, AdjustRateDirection.Additional);

// Get transfer history
var transferHistory = await binanceClient.GeneralApi.Futures.GetFuturesTransferHistoryAsync("BTC", DateTime.UtcNow.AddDays(-10));
```

### Mining
```csharp
// Get mining info
var result = await binanceClient.GeneralApi.Mining.GetMiningCoinListAsync();
```


</BlockQuote>
</Details>

<Details>
<Summary>
<b>Spot API</b>

</Summary>
<BlockQuote>

### Get market data
```csharp
// Getting info on all symbols
var spotSymbolData = await binanceClient.SpotApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var spotTickerData = await binanceClient.SpotApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var spotOrderBookData = await binanceClient.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT");

// Getting recent trades of a symbol
var spotTradeHistoryData = await binanceClient.SpotApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
```

### Requesting balances
Account info includes a list of balances
```csharp
var spotAccountInfo = await binanceClient.SpotApi.Account.GetAccountInfoAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await binanceClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Limit,
                0.001m,
                50000,
                timeInForce: TimeInForce.GoodTillCanceled);
				
// Placing a market buy order for 50USDT				
var orderData = await binanceClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: 50);
										
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData = await binanceClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.StopLossLimit,
                0.001m,
                39000,
                timeInForce: TimeInForce.GoodTillCancel,
                stopPrice: 40000);
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await binanceClient.SpotApi.Trading.GetOrderAsync("BTCUSDT", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceClient.SpotApi.Trading.GetOrdersAsync("BTCUSDT");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceClient.SpotApi.Trading.CancelOrderAsync("BTCUSDT", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceClient.SpotApi.Trading.GetUserTradesAsync("BTCUSDT");
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(data =>
{
	// Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceClient.SpotApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
	// Handler failure
	return;
}
var sub = await binanceSocketClient.SpotStreams.SubscribeToUserDataUpdatesAsync(listenKey.Data, 
	data =>
	{
		// Handle order update
	},
	data =>
	{
		// Handle oco order update
	},
	data =>
	{
		// Handle account balance update, caused by trading
	}, 
	data =>
	{
		// Handle account balance update, caused by withdrawal/deposit or transfers
	});
```

</BlockQuote>
</Details>

<Details>
<Summary>
<b>USD futures API</b>

</Summary>
<BlockQuote>

### Get market data
```csharp
// Getting info on all symbols
var usdFuturesSymbolData = await binanceClient.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var usdFuturesTickerData = await binanceClient.UsdFuturesApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var usdFuturesOrderBookData = await binanceClient.UsdFuturesApi.ExchangeData.GetOrderBookAsync("BTCUSDT");

// Getting recent trades of a symbol
var usdFuturesTradeHistoryData = await binanceClient.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
```

### Requesting balances
```csharp
var balanceData = await binanceClient.UsdFuturesApi.Account.GetBalancesAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                FuturesOrderType.Limit,
                0.001m,
                50000,
                timeInForce: TimeInForce.GoodTillCanceled);
															
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData =  await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                FuturesOrderType.Stop,
                0.001m,
                39000,
                timeInForce: Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                stopPrice: 40000);
				
// Place a buy market order and set TakeProfit/StopLoss for the position ( result checking omitted )
var openPositionResult = await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Buy, FuturesOrderType.Market, 0.001m);
var stopLossResult = await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Sell, FuturesOrderType.StopMarket, quantity: null, closePosition: true, stopPrice: 40000);
var takeProfitResult = await binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Sell, FuturesOrderType.TakeProfitMarket, quantity: null, closePosition: true, stopPrice: 43000);
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await binanceClient.UsdFuturesApi.Trading.GetOrderAsync("BTCUSDT", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceClient.UsdFuturesApi.Trading.GetOrdersAsync("BTCUSDT");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceClient.UsdFuturesApi.Trading.CancelOrderAsync("BTCUSDT", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceClient.UsdFuturesApi.Trading.GetUserTradesAsync("BTCUSDT");
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.UsdFuturesStreams.SubscribeToAllTickerUpdatesAsync(data => 
{
	// Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceClient.UsdFuturesApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
	// Handler failure
	return;
}
var sub = await binanceSocketClient.UsdFuturesStreams.SubscribeToUserDataUpdatesAsync(listenKey.Data,
	data =>
	{
		// Handle leverage update
	},
	data =>
	{
		// Handle margin update
	},
	data =>
	{
		// Handle account balance update, caused by trading
	},
	data =>
	{
		// Handle order update
	},
	data =>
	{
		// Handle listen key expired
	});
```

</BlockQuote>
</Details>

<Details>
<Summary>
<b>Coin futures API</b>

</Summary>
<BlockQuote>

### Get market data
```csharp
// Getting info on all symbols
var coinFuturesSymbolData = await binanceClient.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var coinFuturesTickerData = await binanceClient.CoinFuturesApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var coinFuturesOrderBookData = await binanceClient.CoinFuturesApi.ExchangeData.GetOrderBookAsync("BTCUSD_PERP");

// Getting recent trades of a symbol
var coinFuturesTradeHistoryData = await binanceClient.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync("BTCUSD_PERP");
```

### Requesting balances
```csharp
var balanceData = await binanceClient.CoinFuturesApi.Account.GetBalancesAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 100 contracts at a price of 50000USDT each
var orderData = await binanceClient.CoinFuturesApi.Trading.PlaceOrderAsync(
                "BTCUSD_200925",
                OrderSide.Buy,
                FuturesOrderType.Limit,
                100, 
                50000,
                timeInForce: TimeInForce.GoodTillCanceled);
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await binanceClient.CoinFuturesApi.Trading.GetOrderAsync("BTCUSD_PERP", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceClient.CoinFuturesApi.Trading.GetOrdersAsync("BTCUSD_PERP");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceClient.CoinFuturesApi.Trading.CancelOrderAsync("BTCUSD_PERP", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceClient.CoinFuturesApi.Trading.GetUserTradesAsync();
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.CoinFuturesStreams.SubscribeToAllTickerUpdatesAsync(data =>
{
	// Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceClient.CoinFuturesApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
	// Handler failure
	return;
}
var sub = await binanceSocketClient.CoinFuturesApi.SubscribeToUserDataUpdatesAsync(listenKey.Data,
	data =>
	{
		// Handle leverage update
	},
	data =>
	{
		// Handle margin update
	},
	data =>
	{
		// Handle account balance update, caused by trading
	},
	data =>
	{
		// Handle order update
	},
	data =>
	{
		// Handle listen key expired
	});
```

</BlockQuote>
</Details>
