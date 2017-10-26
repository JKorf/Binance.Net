using NUnit.Framework;
using Moq;
using System.IO;
using System.Net;
using System.Text;
using System;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceClientTests
    {
        [TestCase(1508837063996)]
        [TestCase(1507156891385)]
        public void GetServerTime_Should_RespondWithServerTimeDateTime(long milisecondsTime)
        {
            // arrange
            DateTime expected = new DateTime(1970, 1, 1).AddMilliseconds(milisecondsTime);
            var time = new BinanceCheckTime() { ServerTime = expected };
            var client = PrepareClient(JsonConvert.SerializeObject(time));

            // act
            var result = client.GetServerTime();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(expected, result.Data);
        }

        [TestCase]
        public void GetPrices24H_Should_RespondWithPricesForSymbol()
        {
            // arrange
            var expected = new Binance24hPrice()
            {
                AskPrice = 0.123,
                BidPrice = 0.456,
                CloseTime = new DateTime(2017, 01, 02),
                FirstId = 10000000000,
                HighPrice = 0.789,
                LastId = 20000000000,
                LastPrice = 1.123,
                LowPrice = 1.456,
                OpenPrice = 1.789,
                OpenTime = new DateTime(2017, 01, 01),
                PreviousClosePrice = 2.123,
                PriceChange = 2.456,
                PriceChangePercent = 2.789,
                Trades = 123,
                Volume = 3.123,
                WeightedAveragePrice = 3.456
            };

            var client = PrepareClient(JsonConvert.SerializeObject(expected));

            // act
            var result = client.Get24HPrices("BNBBTC");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(expected, result.Data));
        }

        [TestCase]
        public void GetOrderBook_Should_RespondWithOrderBook()
        {
            // arrange
            var orderBook = new BinanceOrderBook()
            {
                LastUpdateId = 123,
                Asks = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.1,
                        Quantity = 1.1
                    },
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.2,
                        Quantity = 2.2
                    }
                },
                Bids = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.3,
                        Quantity = 3.3
                    },
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.4,
                        Quantity = 4.4
                    }
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(orderBook));

            // act
            var result = client.GetOrderBook("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orderBook, result.Data, "Asks", "Bids"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orderBook.Asks[0], result.Data.Asks[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orderBook.Asks[1], result.Data.Asks[1]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orderBook.Bids[0], result.Data.Bids[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orderBook.Bids[1], result.Data.Bids[1]));
        }

        [TestCase]
        public void GetAccountInfo_Should_RespondWithAccountInfo()
        {
            // arrange
            var accountInfo = new BinanceAccountInfo()
            {
                BuyerCommission = 0.1,
                CanDeposit = true,
                CanTrade = false,
                CanWithdraw = true,
                MakerCommission = 0.2,
                SellerCommission = 0.3,
                TakerCommission = 0.4,
                Balances = new List<BinanceBalance>()
                {
                    new BinanceBalance()
                    {
                        Asset = "bnb", 
                        Free = 0.1,
                        Locked = 0.2
                    },
                    new BinanceBalance()
                    {
                        Asset = "btc",
                        Free = 0.3,
                        Locked = 0.4
                    }
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(accountInfo));

            // act
            var result = client.GetAccountInfo();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(accountInfo, result.Data, "Balances"));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(accountInfo.Balances[0], result.Data.Balances[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(accountInfo.Balances[1], result.Data.Balances[1]));
        }

        [TestCase]
        public void GetAggregatedTrades_Should_RespondWithGetAggregatedTrades()
        {
            // arrange
            var trades = new []
            {
                new BinanceAggregatedTrades()
                {
                    AggregateTradeId = 1,
                    BuyerWasMaker = true,
                    FirstTradeId = 10000000000,
                    LastTradeId = 200000000000,
                    Price = 1.1,
                    Quantity = 2.2,
                    Timestamp = new DateTime(2017, 1, 1),
                    WasBestPriceMatch = true
                },
                new BinanceAggregatedTrades()
                {
                    AggregateTradeId = 2,
                    BuyerWasMaker = false,
                    FirstTradeId = 30000000000,
                    LastTradeId = 400000000000,
                    Price = 3.3,
                    Quantity = 4.4,
                    Timestamp = new DateTime(2016, 1, 1),
                    WasBestPriceMatch = false
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(trades));

            // act
            var result = client.GetAggregatedTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(trades.Length, result.Data.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(trades[0], result.Data[0]));            
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(trades[1], result.Data[1]));
        }

        [TestCase]
        public void GetAllBookPrices_Should_RespondWithAllBookPrices()
        {
            // arrange
            var prices = new []
            {
                new BinanceBookPrice()
                {
                    AskPrice = 0.1,
                    AskQuantity = 0.2,
                    BidPrice = 0.3,
                    BidQuantity = 0.4,
                    Symbol = "BNBBTC"
                },
                new BinanceBookPrice()
                {
                    AskPrice = 0.5,
                    AskQuantity = 0.6,
                    BidPrice = 0.7,
                    BidQuantity = 0.8,
                    Symbol = "ETHBTC"
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(prices));

            // act
            var result = client.GetAllBookPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(prices.Length, result.Data.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(prices[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(prices[1], result.Data[1]));
        }

        [TestCase]
        public void GetAllOrders_Should_RespondWithAllOrders()
        {
            // arrange
            var orders = new []
            {
                new BinanceOrder()
                {
                    ClientOrderId = "order1",
                    ExecutedQuantity = 0.1,
                    IcebergQuantity = 0.2,
                    OrderId = 100000000000,
                    OriginalQuantity = 0.3,
                    Price = 0.4,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5,
                    Symbol = "BNBBTC",
                    Time = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    ExecutedQuantity = 0.6,
                    IcebergQuantity = 0.7,
                    OrderId = 200000000000,
                    OriginalQuantity = 0.8,
                    Price = 0.9,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0,
                    Symbol = "ETHBTC",
                    Time = new DateTime(2017, 1, 10),
                    TimeInForce = TimeInForce.ImmediateOrCancel,
                    Type = OrderType.Market
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(orders));

            // act
            var result = client.GetAllOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orders[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orders[1], result.Data[1]));
        }

        [TestCase]
        public void GetAllPrices_Should_RespondWithAllPrices()
        {
            // arrange
            var prices = new []
            {
                new BinancePrice()
                {
                    Price = 1.1,
                    Symbol = "BNBBTC"
                },
                new BinancePrice()
                {
                    Price = 2.2,
                    Symbol = "ETHBTC"
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(prices));

            // act
            var result = client.GetAllPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, prices.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(prices[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(prices[1], result.Data[1]));
        }

        [TestCase]
        public void GetDepositHistory_Should_RespondWithDepositHistory()
        {
            // arrange
            var history = new BinanceDepositList()
            {
                Success = true,
                List = new List<BinanceDeposit>()
                {
                    new BinanceDeposit()
                    {
                        Amount = 1.1,
                        Asset = "BNB",
                        InsertTime = new DateTime(2017, 1, 1),
                        Status = DepositStatus.Pending
                    },
                    new BinanceDeposit()
                    {
                        Amount = 2.2,
                        Asset = "BTC",
                        InsertTime = new DateTime(2016, 1, 1),
                        Status = DepositStatus.Success
                    }
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(history));

            // act
            var result = client.GetDepositHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Success);
            Assert.AreEqual(result.Data.List.Count, history.List.Count);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(history.List[0], result.Data.List[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(history.List[1], result.Data.List[1]));
        }

        [TestCase]
        public void GetKlines_Should_RespondWithKlines()
        {
            // arrange
            var klines = new [] 
            {
               new BinanceKline()
               {
                    AssetVolume = 0.1,
                    Close = 0.2,
                    CloseTime = new DateTime(2017, 1, 1),
                    High = 0.3,
                    Low = 0.4,
                    Open = 0.5,
                    OpenTime = new DateTime(2016, 1, 1),
                    TakerBuyBaseAssetVolume = 0.6,
                    TakerBuyQuoteAssetVolume = 0.7,
                    Trades = 10,
                    Volume = 0.8
               },
               new BinanceKline()
               {
                    AssetVolume = 0.9,
                    Close = 1.0,
                    CloseTime = new DateTime(2015, 1, 1),
                    High = 1.1,
                    Low = 1.2,
                    Open = 1.3,
                    OpenTime = new DateTime(2014, 1, 1),
                    TakerBuyBaseAssetVolume = 1.4,
                    TakerBuyQuoteAssetVolume = 1.5,
                    Trades = 20,
                    Volume = 1.6
               }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(klines));

            // act
            var result = client.GetKlines("BNBBTC", KlineInterval.OneMinute);

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, klines.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(klines[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(klines[1], result.Data[1]));
        }

        [TestCase]
        public void GetMyTrades_Should_RespondWithTrades()
        {
            // arrange
            var trades = new []
            {
                new BinanceTrade()
                {
                    Commission = 0.1,
                    CommissionAsset = "bnb",
                    Id = 10000000000,
                    IsBestMatch = true,
                    IsBuyer = false,
                    IsMaker= true,
                    Price = 0.3,
                    Quantity = 0.4,
                    Time =  new DateTime(2017, 1, 1)
                },
                new BinanceTrade()
                {
                    Commission = 0.5,
                    CommissionAsset = "eth",
                    Id = 10000000000,
                    IsBestMatch = false,
                    IsBuyer = true,
                    IsMaker= false,
                    Price = 0.6,
                    Quantity = 0.7,
                    Time =  new DateTime(2016, 1, 1)
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(trades));

            // act
            var result = client.GetMyTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, trades.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(trades[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(trades[1], result.Data[1]));
        }

        [TestCase]
        public void GetOpenOrders_Should_RespondWithOpenOrders()
        {
            // arrange
            var orders = new []
            {
                new BinanceOrder()
                {
                    ClientOrderId = "order1",
                    ExecutedQuantity = 0.1,
                    IcebergQuantity = 0.2,
                    OrderId = 100000000000,
                    OriginalQuantity = 0.3,
                    Price = 0.4,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5,
                    Symbol = "BNBBTC",
                    Time = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    ExecutedQuantity = 0.6,
                    IcebergQuantity = 0.7,
                    OrderId = 200000000000,
                    OriginalQuantity = 0.8,
                    Price = 0.9,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0,
                    Symbol = "ETHBTC",
                    Time = new DateTime(2017, 1, 10),
                    TimeInForce = TimeInForce.ImmediateOrCancel,
                    Type = OrderType.Market
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(orders));

            // act
            var result = client.GetOpenOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Length);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orders[0], result.Data[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(orders[1], result.Data[1]));
        }

        [TestCase]
        public void GetWithdrawHistory_Should_RespondWithWithdrawHistory()
        {
            // arrange
            var history = new BinanceWithdrawalList()
            {
                Success = true,
                List = new List<BinanceWithdrawal>()
                {
                    new BinanceWithdrawal()
                    {
                        Address = "test",
                        Amount = 0.1,
                        ApplyTime = new DateTime(2017, 1, 1),
                        Asset = "BNB",
                        Status = WithdrawalStatus.AwaitingApproval,
                        SuccessTime = new DateTime(2017, 1, 2),
                        TransactionId = "1"
                    },
                    new BinanceWithdrawal()
                    {
                        Address = "test2",
                        Amount = 0.2,
                        ApplyTime = new DateTime(2017, 1, 1),
                        Asset = "ETH",
                        Status = WithdrawalStatus.Completed,
                        SuccessTime = new DateTime(2017, 1, 2),
                        TransactionId = "2"
                    }
                }
            };

            var client = PrepareClient(JsonConvert.SerializeObject(history));

            // act
            var result = client.GetWithdrawHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Success);
            Assert.AreEqual(result.Data.List.Count, history.List.Count);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(history.List[0], result.Data.List[0]));
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(history.List[1], result.Data.List[1]));
        }

        [TestCase]
        public void CancelOrder_Should_RespondWithCanceledOrder()
        {
            // arrange
            var canceled = new BinancePlacedOrder()
            {
                ClientOrderId = "test",
                OrderId = 100000000000,
                Symbol = "BNBBTC",
                TransactTime = new DateTime(2017, 1, 1)
            };

            var client = PrepareClient(JsonConvert.SerializeObject(canceled));

            // act
            var result = client.CancelOrder("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(canceled, result.Data));
        }

        [TestCase]
        public void PlaceOrder_Should_RespondWithPlacedOrder()
        {
            // arrange
            var placed = new BinancePlacedOrder()
            {
                ClientOrderId = "test",
                OrderId = 100000000000,
                Symbol = "BNBBTC",
                TransactTime = new DateTime(2017, 1, 1)
            };

            var client = PrepareClient(JsonConvert.SerializeObject(placed));

            // act
            var result = client.PlaceOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCancel, 1, 2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(placed, result.Data));
        }

        [TestCase]
        public void QueryOrder_Should_RespondWithQueriedOrder()
        {
            // arrange
            var order = new BinanceOrder()
            {
                ClientOrderId = "order2",
                ExecutedQuantity = 0.6,
                IcebergQuantity = 0.7,
                OrderId = 200000000000,
                OriginalQuantity = 0.8,
                Price = 0.9,
                Side = OrderSide.Sell,
                Status = OrderStatus.PartiallyFilled,
                StopPrice = 1.0,
                Symbol = "ETHBTC",
                Time = new DateTime(2017, 1, 10),
                TimeInForce = TimeInForce.ImmediateOrCancel,
                Type = OrderType.Market
            };

            var client = PrepareClient(JsonConvert.SerializeObject(order));

            // act
            var result = client.QueryOrder("BNBBTC", orderId: 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(order, result.Data));
        }

        [TestCase]
        public void Withdraw_Should_RespondWithSuccess()
        {
            // arrange
            var order = new BinanceWithdrawalPlaced()
            {
                Success = true
            };

            var client = PrepareClient(JsonConvert.SerializeObject(order));

            // act
            var result = client.Withdraw("BNBBTC", "test", 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(order, result.Data));
        }

        [TestCase]
        public void Ping_Should_RespondWithSuccess()
        {
            // arrange
            var pingResult = new BinancePing();

            var client = PrepareClient(JsonConvert.SerializeObject(pingResult));

            // act
            var result = client.Ping();

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void RequestingPrivateInfo_Should_RequireAPIKeyAndSecret()
        {
            // arrange
            var client = PrepareClient("", false);

            // act
            var accountInfo = client.GetAccountInfo();

            // assert
            Assert.IsFalse(accountInfo.Success);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("test", null)]
        [TestCase("test", "")]
        [TestCase(null, "test")]
        [TestCase("", "test")]
        public void SettingEmptyValuesForAPICredentials_Should_ThrowException(string key, string secret)
        {
            // arrange
            var client = PrepareClient("");

            // act
            // assert
            Assert.Throws(typeof(ArgumentException), () => client.SetAPICredentials(key, secret));
        }

        [TestCase()]
        public void EnablingAutoTimestamp_Should_CallServerTime()
        {
            // arrange
            var expectedBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BinanceCheckTime() { ServerTime = DateTime.Now }));
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();            
            response.Setup(c => c.GetResponseStream()).Returns(responseStream);

            var request = new Mock<IRequest>();
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.GetResponse()).Returns(response.Object);

            var factory = new Mock<IRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);

            BinanceClient client = new BinanceClient
            {
                RequestFactory = factory.Object,
                AutoTimestamp = true
            };

            // act
            client.GetAllPrices();

            // assert
            factory.Verify(x => x.Create(It.Is<string>(s => s.Contains("time"))));            
        }

        private BinanceClient PrepareClient(string responseData, bool credentials = true)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var response = new Mock<IResponse>();
            response.Setup(c => c.GetResponseStream()).Returns(responseStream);

            var request = new Mock<IRequest>();
            request.Setup(c => c.Headers).Returns(new WebHeaderCollection());
            request.Setup(c => c.GetResponse()).Returns(response.Object);

            var factory = new Mock<IRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(request.Object);

            BinanceClient client = credentials ? new BinanceClient("Test", "Test") : new BinanceClient();
            client.RequestFactory = factory.Object;
            return client;
        }
    }   

}
