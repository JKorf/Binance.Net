using System;
using Binance.Net;
using Binance.Net.Objects;

namespace BinanceApi.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceClient.SetAPICredentials("aZF83FdCJy61YMvh2NpjWsx4v2jKz3zrT6DsbtRzn7LMdz6mrOskvrNZ0RGn98UD", "00P4jv9bb3DvxChMbNZwJf3dcezNjZiczcs712eUyb8ULh7z8UJ82uQtSp6jmOIa");

            // Public
            var ping = BinanceClient.Ping();
            var orderBook = BinanceClient.GetOrderBook("BNBBTC", 10);
            var aggTrades = BinanceClient.GetAggregatedTrades("BNBBTC", startTime: DateTime.UtcNow.AddMinutes(-2), endTime: DateTime.UtcNow, limit: 10);
            var klines = BinanceClient.GetKlines("BNBBTC", KlineInterval.OneHour, startTime: DateTime.UtcNow.AddHours(-10), endTime: DateTime.UtcNow, limit: 10);
            var prices24h = BinanceClient.Get24HPrices("BNBBTC");
            var allPrices = BinanceClient.GetAllPrices();
            var allBookPrices = BinanceClient.GetAllBookPrices();

            // Private
            var openOrders = BinanceClient.GetOpenOrders("BNBBTC");
            var allOrders = BinanceClient.GetAllOrders("BNBBTC");
            var testOrderResult = BinanceClient.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancel, 1, 1);
            var queryOrder = BinanceClient.QueryOrder("BNBBTC", allOrders.Data[0].OrderId);
            var orderResult = BinanceClient.PlaceOrder("BNBBTC", OrderSide.Sell, OrderType.Limit, TimeInForce.GoodTillCancel, 10, 0.0002);
            var cancelResult = BinanceClient.CancelOrder("BNBBTC", orderResult.Data.OrderId);
            var accountInfo = BinanceClient.GetAccountInfo();
            var myTrades = BinanceClient.GetMyTrades("BNBBTC");

            // Streams
            var successStart = BinanceClient.StartUserStreamAsync().Result;
            var successAccount = BinanceClient.SubscribeToAccountUpdateStream((data) =>
            {
                Console.WriteLine($"data received: {data}");
            });
            var successOrder = BinanceClient.SubscribeToOrderUpdateStream((data) =>
            {
                Console.WriteLine($"data received: {data}");
            });
            var successKline = BinanceClient.SubscribeToKlineStream("bnbbtc", KlineInterval.OneMinute, (data) =>
            {
                Console.WriteLine($"data received: {data}");
            });

            BinanceClient.UnsubscribeFromAccountUpdateStream();
            BinanceClient.UnsubscribeFromOrderUpdateStream();

            Console.ReadLine();
        }
    }
}
