using System;
using System.Collections.Generic;
using Binance.Net.Events;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceNetTest
    {
        [TestCase()]
        public void SubscribingToDepthStream_Should_TriggerWhenDepthStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));
            
            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamDepth result = null;
            var client = new BinanceSocketClient();
            client.SocketFactory = factory.Object;
            client.SubscribeToDepthStream("test", (test) => result = test);

            var data = new BinanceStreamDepth()
            {
                Event = "TestDepthStream",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                UpdateId = 1,
                Asks = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry(){ Price = 1.1, Quantity = 2.2},
                    new BinanceOrderBookEntry(){ Price = 3.3, Quantity = 4.4}
                },
                Bids = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry(){ Price = 5.5, Quantity = 6.6},
                    new BinanceOrderBookEntry(){ Price = 7.7, Quantity = 8.8}
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessagedEventArgs(JsonConvert.SerializeObject(data), false, false, true, new byte[2]));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Bids", "Asks"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Asks[0], result.Asks[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Asks[1], result.Asks[1]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Bids[0], result.Bids[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Bids[1], result.Bids[1]));
        }

        [TestCase()]
        public void SubscribingToKlineStream_Should_TriggerWhenKlineStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamKline result = null;
            var client = new BinanceSocketClient();
            client.SocketFactory = factory.Object;
            client.SubscribeToKlineStream("test", KlineInterval.OneMinute, (test) => result = test);

            var data = new BinanceStreamKline()
            {
                Event = "TestKlineStream",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                Data = new BinanceStreamKlineInner()
                {
                    ActiveBuyVolume = 0.1,
                    Close = 0.2,
                    EndTime = new DateTime(2017, 1, 2),
                    Final = true,
                    FirstTrade = 10000000000,
                    High = 0.3,
                    Interval = KlineInterval.OneMinute,
                    LastTrade = 2000000000000,
                    Low = 0.4,
                    Open = 0.5,
                    QuoteActiveBuyVolume = 0.6,
                    QuoteVolume = 0.7,
                    StartTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TradeCount = 10,
                    Volume = 0.8
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessagedEventArgs(JsonConvert.SerializeObject(data), false, false, true, new byte[2]));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Data"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Data, result.Data));
        }

        [TestCase()]
        public void SubscribingToTradeStream_Should_TriggerWhenTradeStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamTrade result = null;
            var client = new BinanceSocketClient();
            client.SocketFactory = factory.Object;
            client.SubscribeToTradesStream("test", (test) => result = test);

            var data = new BinanceStreamTrade()
            {
                Event = "TestTradeStream",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                AggregatedTradeId = 1000000000000,
                BuyerIsMaker = true,
                FirstTradeId = 10000000000000,
                LastTradeId = 2000000000000,
                Price = 1.1,
                Quantity = 2.2,
                TradeTime = new DateTime(2017, 1, 1)
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessagedEventArgs(JsonConvert.SerializeObject(data), false, false, true, new byte[2]));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result));
        }

        [TestCase()]
        public void SubscribingToAccountUpdateStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamAccountInfo result = null;
            var client = new BinanceSocketClient();
            client.SocketFactory = factory.Object;
            client.SubscribeToAccountUpdateStream("test", (test) => result = test);

            var data = new BinanceStreamAccountInfo()
            {
                Event = "outboundAccountInfo",
                EventTime = new DateTime(2017, 1, 1),
                BuyerCommission = 1.1,
                CanDeposit = true,
                CanTrade = true,
                CanWithdraw = false,
                MakerCommission = 2.2,
                SellerCommission = 3.3,
                TakerCommission = 4.4,
                Balances = new List<BinanceStreamBalance>()
                {
                    new BinanceStreamBalance(){ Asset = "test1", Free = 1.1, Locked = 2.2},
                    new BinanceStreamBalance(){ Asset = "test2", Free = 3.3, Locked = 4.4},
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessagedEventArgs(JsonConvert.SerializeObject(data), false, false, true, new byte[2]));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Balances"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Balances[0], result.Balances[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Balances[1], result.Balances[1]));
        }

        [TestCase()]
        public void SubscribingToOrderUpdateStream_Should_TriggerWhenOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamOrderUpdate result = null;
            var client = new BinanceSocketClient();
            client.SocketFactory = factory.Object;
            client.SubscribeToOrderUpdateStream("test", (test) => result = test);

            var data = new BinanceStreamOrderUpdate()
            {
                Event = "executionReport",
                EventTime = new DateTime(2017, 1, 1),
                AccumulatedQuantityOfFilledTrades = 1.1,
                BuyerIsMaker = true,
                C = "",
                Commission = 2.2,
                CommissionAsset = "test",
                ExecutionType = ExecutionType.Trade,
                F = 3.3,
                g = 4.4,
                I = 100000000000,
                NewClientOrderId = "test",
                OrderId = 100000000000,
                P = 5.5,
                Price = 6.6,
                PriceLastFilledTrade = 7.7,
                Quantity = 8.8,
                QuantityOfLastFilledTrade = 9.9,
                RejectReason = OrderRejectReason.AccountCannotSettle,
                Side = OrderSide.Buy,
                Status = OrderStatus.Filled,
                Symbol = "test",
                Time = new DateTime(2017, 1, 1),
                TimeInForce = TimeInForce.GoodTillCancel,
                TradeId = 10000000000000,
                Type = OrderType.Limit
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessagedEventArgs(JsonConvert.SerializeObject(data), false, false, true, new byte[2]));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Balances"));
        }
    }   

}
