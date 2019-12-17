using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;

namespace BinanceAPI.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });

            using (var client = new BinanceClient())
            {
                // Public
                var ping = client.Ping();
                var exchangeInfo = client.GetExchangeInfo();
                var serverTime = client.GetServerTime();
                var orderBook = client.GetOrderBook("BNBBTC", 10);
                var aggTrades = client.GetAggregatedTrades("BNBBTC", startTime: DateTime.UtcNow.AddMinutes(-2), endTime: DateTime.UtcNow, limit: 10);
                var klines = client.GetKlines("BNBBTC", KlineInterval.OneHour, startTime: DateTime.UtcNow.AddHours(-10), endTime: DateTime.UtcNow, limit: 10);
                var price = client.GetPrice("BNBBTC");
                var prices24h = client.Get24HPrice("BNBBTC");
                var allPrices = client.GetAllPrices();
                var allBookPrices = client.GetAllBookPrices();
                var historicalTrades = client.GetHistoricalSymbolTrades("BNBBTC");

                // Private
                var openOrders = client.GetOpenOrders("BNBBTC");
                var allOrders = client.GetAllOrders("BNBBTC");
                var testOrderResult = client.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, 1, price: 1, timeInForce: TimeInForce.GoodTillCancel);
                var queryOrder = client.GetOrder("BNBBTC", allOrders.Data.First().OrderId);
                var orderResult = client.PlaceOrder("BNBBTC", OrderSide.Sell, OrderType.Limit, 10, price: 0.0002m, timeInForce: TimeInForce.GoodTillCancel);
                var cancelResult = client.CancelOrder("BNBBTC", orderResult.Data.OrderId);
                var accountInfo = client.GetAccountInfo();
                var myTrades = client.GetMyTrades("BNBBTC");

                // Withdrawal/deposit
                var withdrawalHistory = client.GetWithdrawalHistory();
                var depositHistory = client.GetDepositHistory();
                var withdraw = client.Withdraw("ASSET", "ADDRESS", 0);
            }

            var socketClient = new BinanceSocketClient();
            // Streams
            var successDepth = socketClient.SubscribeToOrderBookUpdates("bnbbtc", 1000, (data) =>
            {
                // handle data
            });
            var successTrades = socketClient.SubscribeToTradeUpdates("bnbbtc", (data) =>
            {
                // handle data
            });
            var successKline = socketClient.SubscribeToKlineUpdates("bnbbtc", KlineInterval.OneMinute, (data) =>
            {
                // handle data
            });
            var successTicker = socketClient.SubscribeToAllSymbolTickerUpdates((data) =>
            {
                // handle data
            });
            var successSingleTicker = socketClient.SubscribeToSymbolTickerUpdates("bnbbtc", (data) =>
            {
                // handle data
            });

            string listenKey;
            using (var client = new BinanceClient())
                listenKey = client.StartUserStream().Data;

            var successAccount = socketClient.SubscribeToUserDataUpdates(listenKey, data =>
                {
                    // Handle account info data
                },
                data =>
                {
                    // Handle order update info data
                },
                null, // Handler for OCO updates
                null, // Handler for position updates
                null); // Handler for account balance updates (withdrawals/deposits)
            socketClient.UnsubscribeAll();

            Console.ReadLine();
        }
    }
}
