using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;
using CryptoExchange.Net.Logging;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceNetTest
    {
        [TestCase()]
        public void SubscribingToKlineStream_Should_TriggerWhenKlineStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamKlineData result = null;
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToKlineStream("test", KlineInterval.OneMinute, (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamKlineData>()
            {
                Stream = "test",
                Data = new BinanceStreamKlineData()
                {
                    Event = "TestKlineStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    Data = new BinanceStreamKline()
                    {
                        TakerBuyBaseAssetVolume = 0.1m,
                        Close = 0.2m,
                        CloseTime = new DateTime(2017, 1, 2),
                        Final = true,
                        FirstTrade = 10000000000,
                        High = 0.3m,
                        Interval = KlineInterval.OneMinute,
                        LastTrade = 2000000000000,
                        Low = 0.4m,
                        Open = 0.5m,
                        TakerBuyQuoteAssetVolume = 0.6m,
                        QuoteAssetVolume = 0.7m,
                        OpenTime = new DateTime(2017, 1, 1),
                        Symbol = "test",
                        TradeCount = 10,
                        Volume = 0.8m
                    }
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Data, result, "Data"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Data.Data, result.Data));
        }

        [TestCase()]
        public void SubscribingToSymbolTicker_Should_TriggerWhenSymbolTickerStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

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
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

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
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

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
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

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
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamTrade result = null;
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToTradesStream("test", (test) => result = test);

            var data = new BinanceCombinedStream<BinanceStreamTrade>()
            {
                Stream = "test",
                Data = new BinanceStreamTrade()
                {
                    Event = "TestTradeStream",
                    EventTime = new DateTime(2017, 1, 1),
                    Symbol = "test",
                    TradeId = 1000000000000,
                    BuyerIsMaker = true,
                    BuyerOrderId = 10000000000000,
                    SellerOrderId = 2000000000000,
                    Price = 1.1m,
                    Quantity = 2.2m,
                    TradeTime = new DateTime(2017, 1, 1)
                }
            };

            // act
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

            // assert
            Assert.IsNotNull(result);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(data.Data, result));
        }

        [TestCase()]
        public void SubscribingToUserStream_Should_TriggerWhenAccountUpdateStreamMessageIsReceived()
        {
            // arrange
            var socket = new Mock<IWebsocket>();
            socket.Setup(s => s.Close());
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

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
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

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
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

            BinanceStreamOrderUpdate result = null;
            var client = new BinanceSocketClient {SocketFactory = factory.Object};
            client.SubscribeToUserStream("test", null, (test) => result = test);

            var data = new BinanceStreamOrderUpdate()
            {
                Event = "executionReport",
                EventTime = new DateTime(2017, 1, 1),
                AccumulatedQuantityOfFilledTrades = 1.1m,
                BuyerIsMaker = true,
                Commission = 2.2m,
                CommissionAsset = "test",
                ExecutionType = ExecutionType.Trade,
                I = 100000000000,
                OrderId = 100000000000,
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
                Type = OrderType.Limit,
                ClientOrderId = "123",
                IcebergQuantity = 9.9m,
                IsWorking = true,
                OriginalClientOrderId = "456",
                StopPrice = 10.10m
            };

            // act
            socket.Raise(r => r.OnMessage += null, JsonConvert.SerializeObject(data));

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
            socket.Setup(s => s.Close()).Returns(Task.Run(() => Thread.Sleep(1))).Raises(s => s.OnClose += null);
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));
            socket.Object.OnClose += () =>
            {
                closed = true;
            };

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);
            
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
            socket.Setup(s => s.Close()).Returns(Task.Run(() => Thread.Sleep(1))).Raises(s => s.OnClose += null);
            socket.Setup(s => s.Connect()).Returns(Task.FromResult(true));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));
            socket.Object.OnClose += () =>
            {
                closed++;
            };

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

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
            socket.Setup(s => s.Close()).Returns(Task.Run(() => Thread.Sleep(1))).Raises(s => s.OnClose += null);
            socket.Setup(s => s.Connect()).Throws(new Exception("Can't connect"));
            socket.Setup(s => s.SetEnabledSslProtocols(It.IsAny<System.Security.Authentication.SslProtocols>()));

            var factory = new Mock<IWebsocketFactory>();
            factory.Setup(s => s.CreateWebsocket(It.IsAny<Log>(), It.IsAny<string>())).Returns(socket.Object);

            var client = new BinanceSocketClient { SocketFactory = factory.Object };

            // act
            var result = client.SubscribeToTradesStream("test", (data) => { });

            // assert
            Assert.IsFalse(result.Success);
        }
    }
}
