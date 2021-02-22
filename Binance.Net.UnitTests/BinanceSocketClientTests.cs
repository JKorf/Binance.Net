using System;
using System.Collections.Generic;
using System.Linq;
using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using CryptoExchange.Net.Logging;
using Binance.Net.Objects.Spot.UserStream;
using Binance.Net.Objects.Spot.MarketStream;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceNetTest
    {
        [TestCase()]
        public void SubscribingToKlineStream_Should_TriggerWhenKlineStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            IBinanceStreamKlineData result = null;
            client.Spot.SubscribeToKlineUpdatesAsync("ETHBTC", KlineInterval.OneMinute, (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamKlineData>()
            {
                Stream = "test",
                Data = new BinanceStreamKlineData()
                {
                    Event = "TestKlineStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "ETHBTC",
                    Data = new BinanceStreamKline()
                    {
                        TakerBuyBaseVolume = 0.1m,
                        Close = 0.2m,
                        CloseTime = new DateTime(2017, 1, 2),
                        Final = true,
                        FirstTrade = 10000000000,
                        High = 0.3m,
                        Interval = KlineInterval.OneMinute,
                        LastTrade = 2000000000000,
                        Low = 0.4m,
                        Open = 0.5m,
                        TakerBuyQuoteVolume = 0.6m,
                        QuoteVolume = 0.7m,
                        OpenTime = new DateTime(2017, 1, 1),
                        Symbol = "test",
                        TradeCount = 10,
                        BaseVolume = 0.8m
                    }
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result, "Data"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Data.Data, result.Data));
        }

        [TestCase()]
        public void SubscribingToSymbolTicker_Should_TriggerWhenSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket, new BinanceSocketClientOptions()
            {
                LogVerbosity = LogVerbosity.Debug
            });

            IBinanceTick result = null;
            client.Spot.SubscribeToSymbolTickerUpdates("ETHBTC", (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamTick>()
            {
                Stream = "test",
                Data = new BinanceStreamTick() { 
                    FirstTradeId = 1,
                    HighPrice = 0.7m,
                    LastTradeId = 2,
                    LowPrice = 0.8m,
                    OpenPrice = 0.9m,
                    PrevDayClosePrice = 1.0m,
                    PriceChange = 1.1m,
                    Symbol = "test",
                    BaseVolume = 1.3m,
                    QuoteVolume = 1.4m,
                    TotalTrades = 3
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result));
        }

        [TestCase()]
        public void SubscribingToAllSymbolTicker_Should_TriggerWhenAllSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            IBinanceTick[] result = null;
            client.Spot.SubscribeToAllSymbolTickerUpdates((test) => result = test.ToArray());

            var data = new[]
            {
                new BinanceStreamTick()
                {
                    FirstTradeId = 1,
                    HighPrice = 0.7m,
                    LastTradeId = 2,
                    LowPrice = 0.8m,
                    OpenPrice = 0.9m,
                    PrevDayClosePrice = 1.0m,
                    PriceChange = 1.1m,
                    Symbol = "test",
                    BaseVolume = 1.3m,
                    QuoteVolume = 1.4m,
                    TotalTrades = 3
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data[0], result[0]));
        }

        [TestCase()]
        public void SubscribingToTradeStream_Should_TriggerWhenTradeStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamTrade result = null;
            client.Spot.SubscribeToTradeUpdates("ETHBTC", (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamTrade>()
            {
                Stream = "test",
                Data = new BinanceStreamTrade()
                {
                    Event = "TestTradeStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "ETHBTC",
                    BuyerIsMaker = true,
                    BuyerOrderId = 10000000000000,
                    SellerOrderId = 2000000000000,
                    Price = 1.1m,
                    Quantity = 2.2m,
                    TradeTime = new DateTime(2017, 1, 1)
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamBalanceUpdate result = null;
            client.Spot.SubscribeToUserDataUpdates("test", null, null, null, (test) => result = test);

            var data = new BinanceStreamBalanceUpdate()
            {
                Event = "balanceUpdate",
                EventTime = new DateTime(2017, 1, 1),
                Asset = "BTC",
                BalanceDelta = 1,
                ClearTime = new DateTime(2018, 1, 1),
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOcoOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket, new BinanceSocketClientOptions(){ LogVerbosity = LogVerbosity.Debug });

            BinanceStreamOrderList result = null;
            client.Spot.SubscribeToUserDataUpdatesAsync("test", null, (test) => result = test, null, null);

            var data = new BinanceStreamOrderList()
            {
                Event = "listStatus",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "BNBUSDT",
                ContingencyType = "OCO",
                ListStatusType = ListStatusType.Done,
                ListOrderStatus = ListOrderStatus.Done,
                OrderListId = 1,
                ListClientOrderId = "2",
                TransactionTime = new DateTime(2018, 1, 1),
                Orders = new []
                {
                    new BinanceStreamOrderId()
                    {
                        Symbol = "BNBUSDT",
                        OrderId = 2,
                        ClientOrderId = "3"
                    },
                    new BinanceStreamOrderId()
                    {
                        Symbol = "BNBUSDT",
                        OrderId = 3,
                        ClientOrderId = "4"
                    }
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result, "Orders"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Orders.ToList()[0], result.Orders.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(data.Orders.ToList()[1], result.Orders.ToList()[1]));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamOrderUpdate result = null;
            client.Spot.SubscribeToUserDataUpdatesAsync("test", (test) => result = test, null, null, null);

            var data = new BinanceStreamOrderUpdate()
            {
                Event = "executionReport",
                EventTime = new DateTime(2017, 1, 1),
                BuyerIsMaker = true,
                Commission = 2.2m,
                CommissionAsset = "test",
                ExecutionType = ExecutionType.Trade,
                I = 100000000000,
                OrderId = 100000000000,
                Price = 6.6m,
                Quantity = 8.8m,
                RejectReason = OrderRejectReason.AccountCannotSettle,
                Side = OrderSide.Buy,
                Status = OrderStatus.Filled,
                Symbol = "test",
                TimeInForce = TimeInForce.GoodTillCancel,
                TradeId = 10000000000000,
                Type = OrderType.Limit,
                ClientOrderId = "123",
                IcebergQuantity = 9.9m,
                IsWorking = true,
                OriginalClientOrderId = "456",
                StopPrice = 10.10m
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data, result, "Balances"));
        }
    }
}
