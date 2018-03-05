using System;
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
                LogWriter = Console.Out
            });
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials("APIKEY", "APISECRET"),
                LogVerbosity = LogVerbosity.Debug,
                LogWriter = Console.Out
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
                var historicalTrades = client.GetHistoricalTrades("BNBBTC");

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
            }

            var socketClient = new BinanceSocketClient();
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
            var successTicker = socketClient.SubscribeToAllSymbolTicker((data) =>
            {
                // handle data
            });
            var successSingleTicker = socketClient.SubscribeToSymbolTicker("bnbbtc", (data) =>
            {
                // handle data
            });

            string listenKey;
            using (var client = new BinanceClient())
                listenKey = client.StartUserStream().Data.ListenKey;

            var successAccount = socketClient.SubscribeToUserStream(listenKey, data =>
                {
                    // Hanlde account info data
                },
                data =>
                {
                    // Hanlde order update info data
                });
            socketClient.UnsubscribeAllStreams();

            Console.ReadLine();
        }
    }
}
