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
using System.Reflection;

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
            var expected = new Binance24HPrice()
            {
                AskPrice = 0.123m,
                BidPrice = 0.456m,
                CloseTime = new DateTime(2017, 01, 02),
                FirstId = 10000000000,
                HighPrice = 0.789m,
                LastId = 20000000000,
                LastPrice = 1.123m,
                LowPrice = 1.456m,
                OpenPrice = 1.789m,
                OpenTime = new DateTime(2017, 01, 01),
                PreviousClosePrice = 2.123m,
                PriceChange = 2.456m,
                PriceChangePercent = 2.789m,
                Trades = 123,
                Volume = 3.123m,
                AskQuantity = 3.456m,
                BidQuantity = 3.789m,
                QuoteVolume = 4.123m,
                Symbol = "BNBBTC",
                WeightedAveragePrice = 3.456m
            };

            var client = PrepareClient(JsonConvert.SerializeObject(expected));

            // act
            var result = client.Get24HPrice("BNBBTC");

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
                        Price = 0.1m,
                        Quantity = 1.1m
                    },
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.2m,
                        Quantity = 2.2m
                    }
                },
                Bids = new List<BinanceOrderBookEntry>()
                {
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.3m,
                        Quantity = 3.3m
                    },
                    new BinanceOrderBookEntry()
                    {
                        Price = 0.4m,
                        Quantity = 4.4m
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
                BuyerCommission = 0.1m,
                CanDeposit = true,
                CanTrade = false,
                CanWithdraw = true,
                MakerCommission = 0.2m,
                SellerCommission = 0.3m,
                TakerCommission = 0.4m,
                Balances = new List<BinanceBalance>()
                {
                    new BinanceBalance()
                    {
                        Asset = "bnb", 
                        Free = 0.1m,
                        Locked = 0.2m
                    },
                    new BinanceBalance()
                    {
                        Asset = "btc",
                        Free = 0.3m,
                        Locked = 0.4m
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
                    Price = 1.1m,
                    Quantity = 2.2m,
                    Timestamp = new DateTime(2017, 1, 1),
                    WasBestPriceMatch = true
                },
                new BinanceAggregatedTrades()
                {
                    AggregateTradeId = 2,
                    BuyerWasMaker = false,
                    FirstTradeId = 30000000000,
                    LastTradeId = 400000000000,
                    Price = 3.3m,
                    Quantity = 4.4m,
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
                    AskPrice = 0.1m,
                    AskQuantity = 0.2m,
                    BidPrice = 0.3m,
                    BidQuantity = 0.4m,
                    Symbol = "BNBBTC"
                },
                new BinanceBookPrice()
                {
                    AskPrice = 0.5m,
                    AskQuantity = 0.6m,
                    BidPrice = 0.7m,
                    BidQuantity = 0.8m,
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
                    ExecutedQuantity = 0.1m,
                    IcebergQuantity = 0.2m,
                    OrderId = 100000000000,
                    OriginalQuantity = 0.3m,
                    Price = 0.4m,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5m,
                    Symbol = "BNBBTC",
                    Time = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    ExecutedQuantity = 0.6m,
                    IcebergQuantity = 0.7m,
                    OrderId = 200000000000,
                    OriginalQuantity = 0.8m,
                    Price = 0.9m,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0m,
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
                    Price = 1.1m,
                    Symbol = "BNBBTC"
                },
                new BinancePrice()
                {
                    Price = 2.2m,
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
                        Amount = 1.1m,
                        Asset = "BNB",
                        InsertTime = new DateTime(2017, 1, 1),
                        Status = DepositStatus.Pending
                    },
                    new BinanceDeposit()
                    {
                        Amount = 2.2m,
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
                    AssetVolume = 0.1m,
                    Close = 0.2m,
                    CloseTime = new DateTime(2017, 1, 1),
                    High = 0.3m,
                    Low = 0.4m,
                    Open = 0.5m,
                    OpenTime = new DateTime(2016, 1, 1),
                    TakerBuyBaseAssetVolume = 0.6m,
                    TakerBuyQuoteAssetVolume = 0.7m,
                    Trades = 10,
                    Volume = 0.8m
               },
               new BinanceKline()
               {
                    AssetVolume = 0.9m,
                    Close = 1.0m,
                    CloseTime = new DateTime(2015, 1, 1),
                    High = 1.1m,
                    Low = 1.2m,
                    Open = 1.3m,
                    OpenTime = new DateTime(2014, 1, 1),
                    TakerBuyBaseAssetVolume = 1.4m,
                    TakerBuyQuoteAssetVolume = 1.5m,
                    Trades = 20,
                    Volume = 1.6m
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
                    Commission = 0.1m,
                    CommissionAsset = "bnb",
                    Id = 10000000000,
                    IsBestMatch = true,
                    IsBuyer = false,
                    IsMaker= true,
                    Price = 0.3m,
                    Quantity = 0.4m,
                    Time =  new DateTime(2017, 1, 1)
                },
                new BinanceTrade()
                {
                    Commission = 0.5m,
                    CommissionAsset = "eth",
                    Id = 10000000000,
                    IsBestMatch = false,
                    IsBuyer = true,
                    IsMaker= false,
                    Price = 0.6m,
                    Quantity = 0.7m,
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
                    ExecutedQuantity = 0.1m,
                    IcebergQuantity = 0.2m,
                    OrderId = 100000000000,
                    OriginalQuantity = 0.3m,
                    Price = 0.4m,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5m,
                    Symbol = "BNBBTC",
                    Time = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    ExecutedQuantity = 0.6m,
                    IcebergQuantity = 0.7m,
                    OrderId = 200000000000,
                    OriginalQuantity = 0.8m,
                    Price = 0.9m,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0m,
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
                        Amount = 0.1m,
                        ApplyTime = new DateTime(2017, 1, 1),
                        Asset = "BNB",
                        Status = WithdrawalStatus.AwaitingApproval,
                        Id = "123",
                        TransactionId = "1"
                    },
                    new BinanceWithdrawal()
                    {
                        Address = "test2",
                        Amount = 0.2m,
                        ApplyTime = new DateTime(2017, 1, 1),
                        Asset = "ETH",
                        Status = WithdrawalStatus.Completed,
                        Id = "123",
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
            var canceled = new BinanceCanceledOrder()
            {
                ClientOrderId = "test",
                OrderId = 100000000000,
                Symbol = "BNBBTC",
                OriginalClientOrderId = "test2"
            };

            var client = PrepareClient(JsonConvert.SerializeObject(canceled));

            // act
            var result = client.CancelOrder("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(canceled, result.Data));
        }

        [TestCase]
        public void PlaceTestOrder_Should_RespondWithPlacedTestOrder()
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
            var result = client.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce:TimeInForce.GoodTillCancel, quantity:1, price:2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(placed, result.Data));
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
            var result = client.PlaceOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

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
                ExecutedQuantity = 0.6m,
                IcebergQuantity = 0.7m,
                OrderId = 200000000000,
                OriginalQuantity = 0.8m,
                Price = 0.9m,
                Side = OrderSide.Sell,
                Status = OrderStatus.PartiallyFilled,
                StopPrice = 1.0m,
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
                Success = true, 
                Message = "Test"
            };

            var client = PrepareClient(JsonConvert.SerializeObject(order));

            // act
            var result = client.Withdraw("BNBBTC", "test", 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(order, result.Data));
        }

        [TestCase]
        public void StartUserStream_Should_RespondWithListenKey()
        {
            // arrange
            var key = new BinanceListenKey()
            {
                ListenKey = "123"
            };

            var client = PrepareClient(JsonConvert.SerializeObject(key));

            // act
            var result = client.StartUserStream();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Compare.PublicInstancePropertiesEqual(key, result.Data));
        }

        [TestCase]
        public void KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var client = PrepareClient("{}");

            // act
            var result = client.KeepAliveUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void StopUserStream_Should_Respond()
        {
            // arrange
            var client = PrepareClient("{}");

            // act
            var result = client.StopUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
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
            Assert.IsNotNull(accountInfo.Error);
            Assert.IsNotNull(accountInfo.Error.Message);
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
            client.SetApiCredentials("test", "test");

            // act
            client.GetAllOrders("BNBBTC");

            // assert
            factory.Verify(x => x.Create(It.Is<string>(s => s.Contains("time"))));            
        }

        [TestCase()]
        public void ReceivingBinanceError_Should_ReturnBinanceErrorAndNotSuccess()
        {
            // arrange
            var client = PrepareExceptionClient(JsonConvert.SerializeObject(new BinanceError(){ Code = 1, Message = "TestMessage"}), "504 error", 504);

            // act
            var result = client.Ping();
            
            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 1);
            Assert.IsTrue(result.Error.Message == "TestMessage");
        }

        [TestCase()]
        public void ReceivingBinanceErrorWithBelow400StatusCode_Should_NotReturnBinanceErrorAndNotSucceed()
        {
            // arrange
            var client = PrepareExceptionClient(JsonConvert.SerializeObject(new BinanceError() { Code = 1, Message = "TestMessage" }), "InvalidStatusCodeResponse", 203);

            // act
            var result = client.Ping();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code != 1);
            Assert.IsTrue(result.Error.Message.Contains("InvalidStatusCodeResponse"));
        }

        [TestCase()]
        public void ReceivingErrorStatusWithoutBinanceError_Should_ReturnError()
        {
            // arrange
            var client = PrepareExceptionClient("error", "404ErrorWithoutErrorObject", 404);

            // act
            var result = client.Ping();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Message.Contains("404ErrorWithoutErrorObject"));
        }

        [TestCase()]
        public void ReceivingInvalidData_Should_ReturnError()
        {
            // arrange
            var client = PrepareClient("TestErrorNotValidJson");

            // act
            var result = client.Ping();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Message.Contains("TestErrorNotValidJson"));
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

        private BinanceClient PrepareExceptionClient(string responseData, string exceptionMessage, int statusCode, bool credentials = true)
        {
            var expectedBytes = Encoding.UTF8.GetBytes(responseData);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var webresponse = Activator.CreateInstance<HttpWebResponse>();
            typeof(HttpWebResponse).GetField("m_StatusCode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(webresponse, (HttpStatusCode)statusCode);
            typeof(HttpWebResponse).GetField("m_ConnectStream", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(webresponse, responseStream);

            var we = new WebException();
            typeof(WebException).GetField("_message", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(we, exceptionMessage);
            typeof(WebException).GetField("m_Response", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(we, webresponse);
            
            var response = new Mock<IResponse>();
            response.Setup(c => c.GetResponseStream()).Throws(we);

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
