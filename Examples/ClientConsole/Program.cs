using System;
using Binance.Net;
using Binance.Net.Objects;

namespace BinanceApi.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BinanceClient.SetAPICredentials("ISNA3LdjrlwdnCXycl8oJU47MnrYK592LiH8g7fG46J1GhQwE2p3cHU07SWnlz3p", "mQjUqedFRVrBPcn8cTnBdMZPdE13ZcJ2Ac96rOERUq7YIM6vsfSLDBkJ9kZ2mtmp");

            // Public
            var ping = BinanceClient.Ping();
            var serverTime = BinanceClient.GetServerTime();
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
            var successStart = BinanceClient.StartUserStream();
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
            var successAccount = BinanceClient.SubscribeToAccountUpdateStream((data) =>
            {
                // handle data
            });
            var successOrder = BinanceClient.SubscribeToOrderUpdateStream((data) =>
            {
                // handle data
            });            

            BinanceClient.UnsubscribeFromStream(successDepth.StreamId);
            BinanceClient.UnsubscribeFromStream(successTrades.StreamId);
            BinanceClient.UnsubscribeFromStream(successKline.StreamId);
            BinanceClient.UnsubscribeFromAccountUpdateStream();
            BinanceClient.UnsubscribeFromOrderUpdateStream();

            Console.ReadLine();
        }
    }
}
