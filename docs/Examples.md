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
var subAccountBalances = await binanceRestClient.GeneralApi.SubAccount.GetSubAccountsAsync();

// Transfer 0.001m BTC from master account to a subaccount
var transferResult = await binanceRestClient.GeneralApi.SubAccount.TransferSubAccountAsync(
                    TransferAccountType.Spot, 
                    TransferAccountType.UsdtFuture, 
                    "BTC",
                    0.001m,
                    toEmail: "test@test.com");
```

### Futures collateral
```csharp
// Add collateral
var result = await binanceRestClient.GeneralApi.Futures.AdjustCrossCollateralLoanToValueAsync("BTC", "BUSD", 1, AdjustRateDirection.Additional);

// Get transfer history
var transferHistory = await binanceRestClient.GeneralApi.Futures.GetFuturesTransferHistoryAsync("BTC", DateTime.UtcNow.AddDays(-10));
```

### Mining
```csharp
// Get mining info
var result = await binanceRestClient.GeneralApi.Mining.GetMiningCoinListAsync();
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
var spotSymbolData = await binanceRestClient.SpotApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var spotTickerData = await binanceRestClient.SpotApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var spotOrderBookData = await binanceRestClient.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT");

// Getting recent trades of a symbol
var spotTradeHistoryData = await binanceRestClient.SpotApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
```

### Requesting balances
Account info includes a list of balances
```csharp
var spotAccountInfo = await binanceRestClient.SpotApi.Account.GetAccountInfoAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await binanceRestClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Limit,
                0.001m,
                50000,
                timeInForce: TimeInForce.GoodTillCanceled);
                
// Placing a market buy order for 50USDT                
var orderData = await binanceRestClient.SpotApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                SpotOrderType.Market,
                quoteQuantity: 50);
                                        
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData = await binanceRestClient.SpotApi.Trading.PlaceOrderAsync(
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
var orderData = await binanceRestClient.SpotApi.Trading.GetOrderAsync("BTCUSDT", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceRestClient.SpotApi.Trading.GetOrdersAsync("BTCUSDT");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceRestClient.SpotApi.Trading.CancelOrderAsync("BTCUSDT", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceRestClient.SpotApi.Trading.GetUserTradesAsync("BTCUSDT");
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.SpotApi.SubscribeToAllTickerUpdatesAsync(data =>
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceRestClient.SpotApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
    // Handler failure
    return;
}
var sub = await binanceSocketClient.SpotApi.SubscribeToUserDataUpdatesAsync(listenKey.Data, 
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
var usdFuturesSymbolData = await binanceRestClient.UsdFuturesApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var usdFuturesTickerData = await binanceRestClient.UsdFuturesApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var usdFuturesOrderBookData = await binanceRestClient.UsdFuturesApi.ExchangeData.GetOrderBookAsync("BTCUSDT");

// Getting recent trades of a symbol
var usdFuturesTradeHistoryData = await binanceRestClient.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
```

### Requesting balances
```csharp
var balanceData = await binanceRestClient.UsdFuturesApi.Account.GetBalancesAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                FuturesOrderType.Limit,
                0.001m,
                50000,
                timeInForce: TimeInForce.GoodTillCanceled);
                                                            
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData =  await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                "BTCUSDT",
                OrderSide.Buy,
                FuturesOrderType.Stop,
                0.001m,
                39000,
                timeInForce: Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                stopPrice: 40000);
                
// Place a buy market order and set TakeProfit/StopLoss for the position ( result checking omitted )
var openPositionResult = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Buy, FuturesOrderType.Market, 0.001m);
var stopLossResult = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Sell, FuturesOrderType.StopMarket, quantity: null, closePosition: true, stopPrice: 40000);
var takeProfitResult = await binanceRestClient.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", OrderSide.Sell, FuturesOrderType.TakeProfitMarket, quantity: null, closePosition: true, stopPrice: 43000);
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await binanceRestClient.UsdFuturesApi.Trading.GetOrderAsync("BTCUSDT", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceRestClient.UsdFuturesApi.Trading.GetOrdersAsync("BTCUSDT");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceRestClient.UsdFuturesApi.Trading.CancelOrderAsync("BTCUSDT", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceRestClient.UsdFuturesApi.Trading.GetUserTradesAsync("BTCUSDT");
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.UsdFuturesApi.SubscribeToAllTickerUpdatesAsync(data => 
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceRestClient.UsdFuturesApi.Account.StartUserStreamAsync();
if (!listenKey.Success)
{
    // Handler failure
    return;
}
var sub = await binanceSocketClient.UsdFuturesApi.SubscribeToUserDataUpdatesAsync(listenKey.Data,
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
var coinFuturesSymbolData = await binanceRestClient.CoinFuturesApi.ExchangeData.GetExchangeInfoAsync();

// Getting ticker
var coinFuturesTickerData = await binanceRestClient.CoinFuturesApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var coinFuturesOrderBookData = await binanceRestClient.CoinFuturesApi.ExchangeData.GetOrderBookAsync("BTCUSD_PERP");

// Getting recent trades of a symbol
var coinFuturesTradeHistoryData = await binanceRestClient.CoinFuturesApi.ExchangeData.GetTradeHistoryAsync("BTCUSD_PERP");
```

### Requesting balances
```csharp
var balanceData = await binanceRestClient.CoinFuturesApi.Account.GetBalancesAsync();
```
### Placing order
```csharp
// Placing a buy limit order for 100 contracts at a price of 50000USDT each
var orderData = await binanceRestClient.CoinFuturesApi.Trading.PlaceOrderAsync(
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
var orderData = await binanceRestClient.CoinFuturesApi.Trading.GetOrderAsync("BTCUSD_PERP", 1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await binanceRestClient.CoinFuturesApi.Trading.GetOrdersAsync("BTCUSD_PERP");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await binanceRestClient.CoinFuturesApi.Trading.CancelOrderAsync("BTCUSD_PERP", 1234);
```

### Get user trades
```csharp
var userTradesResult = await binanceRestClient.CoinFuturesApi.Trading.GetUserTradesAsync();
```

### Subscribing to market data updates
```csharp
var subscribeResult = await binanceSocketClient.CoinFuturesApi.SubscribeToAllTickerUpdatesAsync(data =>
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
var listenKey = await binanceRestClient.CoinFuturesApi.Account.StartUserStreamAsync();
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
