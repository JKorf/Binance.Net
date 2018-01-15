using System;
using Binance.Net;
using Binance.Net.Objects;
using Binance.Net.Logging;

namespace BinanceApi.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceDefaults.SetDefaultApiCredentials("APIKEY", "APISECRET");
            BinanceDefaults.SetDefaultLogVerbosity(LogVerbosity.Debug);
            BinanceDefaults.SetDefaultLogOutput(Console.Out);

            using (var client = new BinanceClient())
            using (var socketClient = new BinanceSocketClient())
            {
                // Public
                var ping = client.Ping();
                var serverTime = client.GetServerTime();
                var orderBook = client.GetOrderBook("BNBBTC", 10);
                var aggTrades = client.GetAggregatedTrades("BNBBTC", startTime: DateTime.UtcNow.AddMinutes(-2), endTime: DateTime.UtcNow, limit: 10);
                var klines = client.GetKlines("BNBBTC", KlineInterval.OneHour, startTime: DateTime.UtcNow.AddHours(-10), endTime: DateTime.UtcNow, limit: 10);
                var prices24h = client.Get24HPrice("BNBBTC");
                var allPrices = client.GetAllPrices();
                var allBookPrices = client.GetAllBookPrices();

                // Private
                var openOrders = client.GetOpenOrders("BNBBTC");
                var allOrders = client.GetAllOrders("BNBBTC");
                var testOrderResult = client.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, 1, price: 1, timeInForce: TimeInForce.GoodTillCancel);
                var queryOrder = client.QueryOrder("BNBBTC", allOrders.Data[0].OrderId);
                var orderResult = client.PlaceOrder("BNBBTC", OrderSide.Sell, OrderType.Limit, 10, price: 0.0002m, timeInForce: TimeInForce.GoodTillCancel);
                var cancelResult = client.CancelOrder("BNBBTC", orderResult.Data.OrderId);
                var accountInfo = client.GetAccountInfo();
                var myTrades = client.GetMyTrades("BNBBTC");


                // Withdrawal/deposit
                var withdrawalHistory = client.GetWithdrawHistory();
                var depositHistory = client.GetDepositHistory();
                var withdraw = client.Withdraw("ASSET", "ADDRESS", 0);


                // Streams
                var successDepth = socketClient.SubscribeToDepthStream("bnbbtc", (data) =>
                {
                    // handle data
                });
                var successTrades = socketClient.SubscribeToTradesStream("bnbbtc", (data) =>
                {
                    // handle data
                });
                var successKline = socketClient.SubscribeToKlineStream("bnbbtc", KlineInterval.OneMinute, (data) =>
                {
                    // handle data
                });

                var successStart = client.StartUserStream();
                var successAccount = socketClient.SubscribeToAccountUpdateStream(successStart.Data.ListenKey, (data) =>
                {
                    // handle data
                });
                var successOrder = socketClient.SubscribeToOrderUpdateStream(successStart.Data.ListenKey, (data) =>
                {
                    // handle data
                });

                socketClient.UnsubscribeFromStream(successDepth.Data);
                socketClient.UnsubscribeFromAccountUpdateStream();
                socketClient.UnsubscribeAllStreams();
            }

            Console.ReadLine();
        }
    }
}
