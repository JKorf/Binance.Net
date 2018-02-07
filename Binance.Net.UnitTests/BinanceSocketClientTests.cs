using System;
using System.Collections.Generic;
using Binance.Net.Events;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;
using WebSocket4Net;

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
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            var subscibtion = client.SubscribeToDepthStream("test", (test) => result = test);

            var data = new BinanceStreamDepth()
            {
                Event = "TestDepthStream",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                FirstUpdateId = 1,
                LastUpdateId = 2,
                Asks = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry(){ Price = 1.1m, Quantity = 2.2m},
                    new BinanceOrderBookEntry(){ Price = 3.3m, Quantity = 4.4m}
                },
                Bids = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry(){ Price = 5.5m, Quantity = 6.6m},
                    new BinanceOrderBookEntry(){ Price = 7.7m, Quantity = 8.8m}
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsTrue(subscibtion.Success);
            Assert.IsTrue(subscibtion.Data != null);
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
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToKlineStream("test", KlineInterval.OneMinute, (test) => result = test);

            var data = new BinanceStreamKline()
            {
                Event = "TestKlineStream",
                EventTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                Data = new BinanceStreamKlineInner()
                {
                    ActiveBuyVolume = 0.1m,
                    Close = 0.2m,
                    EndTime = new DateTime(2017, 1, 2),
                    Final = true,
                    FirstTrade = 10000000000,
                    High = 0.3m,
                    Interval = KlineInterval.OneMinute,
                    LastTrade = 2000000000000,
                    Low = 0.4m,
                    Open = 0.5m,
                    QuoteActiveBuyVolume = 0.6m,
                    QuoteVolume = 0.7m,
                    StartTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TradeCount = 10,
                    Volume = 0.8m
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Data"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Data, result.Data));
        }

        [TestCase()]
        public void SubscribingToPartialBookDepthStream_Should_TriggerWhenPartialBookStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceOrderBook result = null;
            var client = new BinanceSocketClient { SocketFactory = factory.Object };
            client.SubscribeToPartialBookDepthStream("test", 10, (test) => result = test);

            var data = new BinanceOrderBook()
            {
                Asks = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.1m,
                        Quantity = 0.2m
                    },
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.3m,
                        Quantity = 0.4m
                    }
                },
                LastUpdateId = 1,
                Bids = new List<BinanceOrderBookEntry>()
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Asks", "Bids"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Asks[0], result.Asks[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Asks[1], result.Asks[1]));
        }

        [TestCase()]
        public void SubscribingToSymbolTicker_Should_TriggerWhenSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamTick result = null;
            var client = new BinanceSocketClient { SocketFactory = factory.Object };
            client.SubscribeToSymbolTicker("test", (test) => result = test);

            var data = new BinanceStreamTick()
            {
                BestAskPrice = 0.1m,
                BestAskQuantity = 0.2m,
                BestBidPrice = 0.3m,
                BestBidQuantity = 0.4m,
                CloseTradesQuantity = 0.5m,
                CurrentDayClosePrice = 0.6m,
                FirstTradeId = 1,
                HighPrice = 0.7m,
                LastTradeId = 2,
                LowPrice = 0.8m,
                OpenPrice = 0.9m,
                PrevDayClosePrice = 1.0m,
                PriceChange = 1.1m,
                PriceChangePercentage = 1.2m,
                StatisticsCloseTime = new DateTime(2017, 1, 2),
                StatisticsOpenTime = new DateTime(2017, 1, 1),
                Symbol = "test",
                TotalTradedBaseAssetVolume = 1.3m,
                TotalTradedQuoteAssetVolume = 1.4m,
                TotalTrades = 3,
                WeightedAverage = 1.5m
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result));
        }

        [TestCase()]
        public void SubscribingToAllSymbolTicker_Should_TriggerWhenAllSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamTick[] result = null;
            var client = new BinanceSocketClient { SocketFactory = factory.Object };
            client.SubscribeToAllSymbolTicker((test) => result = test);

            var data = new[] 
            {
                new BinanceStreamTick()
                {
                    BestAskPrice = 0.1m,
                    BestAskQuantity = 0.2m,
                    BestBidPrice = 0.3m,
                    BestBidQuantity = 0.4m,
                    CloseTradesQuantity = 0.5m,
                    CurrentDayClosePrice = 0.6m,
                    FirstTradeId = 1,
                    HighPrice = 0.7m,
                    LastTradeId = 2,
                    LowPrice = 0.8m,
                    OpenPrice = 0.9m,
                    PrevDayClosePrice = 1.0m,
                    PriceChange = 1.1m,
                    PriceChangePercentage = 1.2m,
                    StatisticsCloseTime = new DateTime(2017, 1, 2),
                    StatisticsOpenTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TotalTradedBaseAssetVolume = 1.3m,
                    TotalTradedQuoteAssetVolume = 1.4m,
                    TotalTrades = 3,
                    WeightedAverage = 1.5m
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data[0], result[0]));
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
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
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
                Price = 1.1m,
                Quantity = 2.2m,
                TradeTime = new DateTime(2017, 1, 1)
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamAccountInfo result = null;
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToUserStream("test", (test) => result = test, null);

            var data = new BinanceStreamAccountInfo()
            {
                Event = "outboundAccountInfo",
                EventTime = new DateTime(2017, 1, 1),
                BuyerCommission = 1.1m,
                CanDeposit = true,
                CanTrade = true,
                CanWithdraw = false,
                MakerCommission = 2.2m,
                SellerCommission = 3.3m,
                TakerCommission = 4.4m,
                Balances = new List<BinanceStreamBalance>()
                {
                    new BinanceStreamBalance(){ Asset = "test1", Free = 1.1m, Locked = 2.2m},
                    new BinanceStreamBalance(){ Asset = "test2", Free = 3.3m, Locked = 4.4m},
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Balances"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Balances[0], result.Balances[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Balances[1], result.Balances[1]));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenOrderUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamOrderUpdate result = null;
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToUserStream("test", null, (test) => result = test);

            var data = new BinanceStreamOrderUpdate()
            {
                Event = "executionReport",
                EventTime = new DateTime(2017, 1, 1),
                AccumulatedQuantityOfFilledTrades = 1.1m,
                BuyerIsMaker = true,
                C = "",
                Commission = 2.2m,
                CommissionAsset = "test",
                ExecutionType = ExecutionType.Trade,
                F = 3.3m,
                g = 4.4m,
                I = 100000000000,
                NewClientOrderId = "test",
                OrderId = 100000000000,
                P = 5.5m,
                Price = 6.6m,
                PriceLastFilledTrade = 7.7m,
                Quantity = 8.8m,
                QuantityOfLastFilledTrade = 9.9m,
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
            socket.Raise(r => r.OnMessage += null, new MessageReceivedEventArgs(JsonConvert.SerializeObject(data)));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data, result, "Balances"));
        }

        [TestCase()]
        public void UnsubscribingStream_Should_CloseTheSocket()
        {
            // arrange
            bool closed = false;
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close()).Raises(s => s.OnClose += null, new Events.ClosedEventArgs(0, "", true));
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));
            socket.Object.OnClose += (sender, args) =>
            {
                closed = true;
            };

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);
            
            var client = new BinanceSocketClient { SocketFactory = factory.Object };
            var subscription = client.SubscribeToTradesStream("test", (data) => { });

            // act
            client.UnsubscribeFromStream(subscription.Data);

            // assert
            Assert.IsTrue(closed);
        }
        
        [TestCase()]
        public void UnsubscribingAll_Should_CloseAllSockets()
        {
            // arrange
            int closed = 0;
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close()).Raises(s => s.OnClose += null, new Events.ClosedEventArgs(0, "", true));
            socket.Setup(s => s.Connect());
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));
            socket.Object.OnClose += (sender, args) =>
            {
                closed++;
            };

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            var client = new BinanceSocketClient { SocketFactory = factory.Object };
            client.SubscribeToTradesStream("test", (data) => { });
            client.SubscribeToDepthStream("test", (data) => { });

            // act
            client.UnsubscribeAllStreams();

            // assert
            Assert.IsTrue(closed == 2);
        }

        [TestCase()]
        public void WhenSocketConnectionFailsIt_Should_ReturnAnError()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close()).Raises(s => s.OnClose += null, new Events.ClosedEventArgs(0, "", true));
            socket.Setup(s => s.Connect()).Throws(new Exception("Can't connect"));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<string>())).Returns(socket.Object);

            var client = new BinanceSocketClient { SocketFactory = factory.Object };

            // act
            var result = client.SubscribeToTradesStream("test", (data) => { });

            // assert
            Assert.IsFalse(result.Success);
        }
    }
}
