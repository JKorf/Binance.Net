using System;
using System.Linq;
using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using System.Threading.Tasks;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Socket;
using Microsoft.Extensions.Logging;

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
            client.SpotStreams.SubscribeToKlineUpdatesAsync("ETHBTC", KlineInterval.OneMinute, (test) => result = test.Data);

            var data = new BinanceCombinedStream<BinanceStreamKlineData>()
            {
                Stream = "ethbtc@kline_1m",
                Data = new BinanceStreamKlineData()
                {
                    Event = "TestKlineStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "ETHBTC",
                    Data = new BinanceStreamKline()
                    {
                        TakerBuyBaseVolume = 0.1m,
                        ClosePrice = 0.2m,
                        CloseTime = new DateTime(2017, 1, 2),
                        Final = true,
                        FirstTrade = 10000000000,
                        HighPrice = 0.3m,
                        Interval = KlineInterval.OneMinute,
                        LastTrade = 2000000000000,
                        LowPrice = 0.4m,
                        OpenPrice = 0.5m,
                        TakerBuyQuoteVolume = 0.6m,
                        QuoteVolume = 0.7m,
                        OpenTime = new DateTime(2017, 1, 1),
                        Symbol = "test",
                        TradeCount = 10,
                        Volume = 0.8m
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
        public async Task SubscribingToSymbolTicker_Should_TriggerWhenSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket, new BinanceSocketClientOptions()
            {
                LogLevel = LogLevel.Debug
            });

            IBinanceTick result = null;
            await client.SpotStreams.SubscribeToTickerUpdatesAsync("ETHBTC", (test) => result = test.Data);

            var data = new BinanceCombinedStream<BinanceStreamTick>()
            {
                Stream = "ethbtc@ticker",
                Data = new BinanceStreamTick() { 
                    FirstTradeId = 1,
                    HighPrice = 0.7m,
                    LastTradeId = 2,
                    LowPrice = 0.8m,
                    OpenPrice = 0.9m,
                    PrevDayClosePrice = 1.0m,
                    PriceChange = 1.1m,
                    Symbol = "test",
                    Volume = 1.3m,
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
        public async Task SubscribingToAllSymbolTicker_Should_TriggerWhenAllSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            IBinanceTick[] result = null;
            await client.SpotStreams.SubscribeToAllTickerUpdatesAsync((test) => result = test.Data.ToArray());

            var data = new BinanceCombinedStream<BinanceStreamTick[]>
            {
                Data = new[]
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
                        Volume = 1.3m,
                        QuoteVolume = 1.4m,
                        TotalTrades = 3
                    }
                },
                Stream = "!ticker@arr"
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data[0], result[0]));
        }

        [TestCase()]
        public async Task SubscribingToTradeStream_Should_TriggerWhenTradeStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamTrade result = null;
            await client.SpotStreams.SubscribeToTradeUpdatesAsync("ETHBTC", (test) => result = test.Data);

            var data = new BinanceCombinedStream<BinanceStreamTrade>()
            {
                Stream = "ethbtc@trade",
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
        public async Task SubscribingToUserStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamBalanceUpdate result = null;
            await client.SpotStreams.SubscribeToUserDataUpdatesAsync("test", null, null, null, (test) => result = test.Data);

            var data = new BinanceCombinedStream<BinanceStreamBalanceUpdate>
            {
                Stream = "test",
                Data = new BinanceStreamBalanceUpdate()
                {
                    Event = "balanceUpdate",
                    EventTime = new DateTime(2017, 1, 1),
                    Asset = "BTC",
                    BalanceDelta = 1,
                    ClearTime = new DateTime(2018, 1, 1),
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOcoOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket, new BinanceSocketClientOptions(){ LogLevel = LogLevel.Debug });

            BinanceStreamOrderList result = null;
            client.SpotStreams.SubscribeToUserDataUpdatesAsync("test", null, (test) => result = test.Data, null, null);

            var data = new BinanceCombinedStream<BinanceStreamOrderList>
            {
                Stream = "test",
                Data = new BinanceStreamOrderList()
                {
                    Event = "listStatus",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "BNBUSDT",
                    ContingencyType = "OCO",
                    ListStatusType = ListStatusType.Done,
                    ListOrderStatus = ListOrderStatus.Done,
                    Id = 1,
                    ListClientOrderId = "2",
                    TransactionTime = new DateTime(2018, 1, 1),
                    Orders = new[]
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
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result, "Orders"));
            Assert.IsTrue(TestHelpers.AreEqual(data.Data.Orders.ToList()[0], result.Orders.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(data.Data.Orders.ToList()[1], result.Orders.ToList()[1]));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new TestSocket();
            var client = TestHelpers.CreateSocketClient(socket);

            BinanceStreamOrderUpdate result = null;
            client.SpotStreams.SubscribeToUserDataUpdatesAsync("test", (test) => result = test.Data, null, null, null);

            var data = new BinanceCombinedStream<BinanceStreamOrderUpdate>
            {
                Stream = "test",
                Data = new BinanceStreamOrderUpdate()
                {
                    Event = "executionReport",
                    EventTime = new DateTime(2017, 1, 1),
                    BuyerIsMaker = true,
                    Fee = 2.2m,
                    FeeAsset = "test",
                    ExecutionType = ExecutionType.Trade,
                    I = 100000000000,
                    Id = 100000000000,
                    Price = 6.6m,
                    Quantity = 8.8m,
                    RejectReason = OrderRejectReason.AccountCannotSettle,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Filled,
                    Symbol = "test",
                    TimeInForce = TimeInForce.GoodTillCanceled,
                    TradeId = 10000000000000,
                    Type = SpotOrderType.Limit,
                    ClientOrderId = "123",
                    IcebergQuantity = 9.9m,
                    IsWorking = true,
                    OriginalClientOrderId = "456",
                    StopPrice = 10.10m
                }
            };

            // act
            socket.InvokeMessage(data);

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestHelpers.AreEqual(data.Data, result, "Balances"));
        }
    }
}
