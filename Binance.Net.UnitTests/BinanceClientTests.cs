using NUnit.Framework;
using Moq;
using System.IO;
using System.Net;
using System.Text;
using System;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Requests;
using System.Linq;

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
            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(time));

            // act
            var result = objects.Client.GetServerTime();

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

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(expected));

            // act
            var result = objects.Client.Get24HPrice("BNBBTC");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(expected, result.Data));
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

            var objects = TestHelpers.PrepareClient(() => Construct(), "{\"lastUpdateId\":123,\"asks\": [[0.1, 1.1], [0.2, 2.2]], \"bids\": [[0.3,3.3], [0.4,4.4]]}");

            // act
            var result = objects.Client.GetOrderBook("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orderBook, result.Data, "Asks", "Bids"));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orderBook.Asks[0], result.Data.Asks[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orderBook.Asks[1], result.Data.Asks[1]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orderBook.Bids[0], result.Data.Bids[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orderBook.Bids[1], result.Data.Bids[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(accountInfo));

            // act
            var result = objects.Client.GetAccountInfo();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(accountInfo, result.Data, "Balances"));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(accountInfo.Balances[0], result.Data.Balances[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(accountInfo.Balances[1], result.Data.Balances[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(trades));

            // act
            var result = objects.Client.GetAggregatedTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(trades.Length, result.Data.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(trades[0], result.Data[0]));            
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(trades[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(prices));

            // act
            var result = objects.Client.GetAllBookPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(prices.Length, result.Data.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(prices[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(prices[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(orders));

            // act
            var result = objects.Client.GetAllOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orders[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orders[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(prices));

            // act
            var result = objects.Client.GetAllPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, prices.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(prices[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(prices[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(history));

            // act
            var result = objects.Client.GetDepositHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Success);
            Assert.AreEqual(result.Data.List.Count, history.List.Count);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(history.List[0], result.Data.List[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(history.List[1], result.Data.List[1]));
        }

        [TestCase]
        public void GetKlines_Should_RespondWithKlines()
        {
            // arrange
            var klines = new [] 
            {
               new BinanceKline()
               {
                    QuoteAssetVolume = 0.1m,
                    Close = 0.2m,
                    CloseTime = new DateTime(1970, 1, 1),
                    High = 0.3m,
                    Low = 0.4m,
                    Open = 0.5m,
                    OpenTime = new DateTime(1970, 1, 1),
                    TakerBuyBaseAssetVolume = 0.6m,
                    TakerBuyQuoteAssetVolume = 0.7m,
                    TradeCount = 10,
                    Volume = 0.8m
               },
               new BinanceKline()
               {
                   QuoteAssetVolume = 0.9m,
                    Close = 1.0m,
                    CloseTime = new DateTime(1970, 1, 1),
                    High = 1.1m,
                    Low = 1.2m,
                    Open = 1.3m,
                    OpenTime = new DateTime(1970, 1, 1),
                    TakerBuyBaseAssetVolume = 1.4m,
                    TakerBuyQuoteAssetVolume = 1.5m,
                   TradeCount = 20,
                    Volume = 1.6m
               }
            };

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(new object[]
            {
                new object[] { 0, 0.5m, 0.3m, 0.4m, 0.2m, 0.8m, 0, 0.1m, 10, 0.6m, 0.7m},
                new object[] { 0, 1.3m, 1.1m, 1.2m, 1.0m, 1.6m, 0, 0.9m, 20, 1.4m, 1.5m }
            }));

            // act
            var result = objects.Client.GetKlines("BNBBTC", KlineInterval.OneMinute);

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, klines.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(klines[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(klines[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(trades));

            // act
            var result = objects.Client.GetMyTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Length, trades.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(trades[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(trades[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(orders));

            // act
            var result = objects.Client.GetOpenOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Length);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orders[0], result.Data[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(orders[1], result.Data[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(history));

            // act
            var result = objects.Client.GetWithdrawHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Success);
            Assert.AreEqual(result.Data.List.Count, history.List.Count);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(history.List[0], result.Data.List[0]));
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(history.List[1], result.Data.List[1]));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(canceled));

            // act
            var result = objects.Client.CancelOrder("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(canceled, result.Data));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(placed));

            // act
            var result = objects.Client.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce:TimeInForce.GoodTillCancel, quantity:1, price:2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(placed, result.Data));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(placed));

            // act
            var result = objects.Client.PlaceOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(placed, result.Data));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(order));

            // act
            var result = objects.Client.QueryOrder("BNBBTC", orderId: 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(order, result.Data));
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

            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test")
            }), JsonConvert.SerializeObject(order));

            // act
            var result = objects.Client.Withdraw("BNBBTC", "test", 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(order, result.Data));
        }

        [TestCase]
        public void StartUserStream_Should_RespondWithListenKey()
        {
            // arrange
            var key = new BinanceListenKey()
            {
                ListenKey = "123"
            };

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(key));

            // act
            var result = objects.Client.StartUserStream();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.PublicInstancePropertiesEqual(key, result.Data));
        }

        [TestCase]
        public void KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var objects = TestHelpers.PrepareClient(() => Construct(), "{}");

            // act
            var result = objects.Client.KeepAliveUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void StopUserStream_Should_Respond()
        {
            // arrange
            var objects = TestHelpers.PrepareClient(() => Construct(), "{}");

            // act
            var result = objects.Client.StopUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void Ping_Should_RespondWithSuccess()
        {
            // arrange
            var pingResult = new BinancePing();

            var objects = TestHelpers.PrepareClient(() => Construct(), JsonConvert.SerializeObject(pingResult));

            // act
            var result = objects.Client.Ping();

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase()]
        public void EnablingAutoTimestamp_Should_CallServerTime()
        {
            // arrange
            var objects = TestHelpers.PrepareClient(() => Construct(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("test", "test"),
                AutoTimestamp = true
            }), JsonConvert.SerializeObject("{}"));

            // act
            objects.Client.GetOpenOrders();

            // assert
            objects.RequestFactory.Verify(f => f.Create(It.Is<string>((msg) => msg.Contains("/time"))), Times.Once);
        }

        [TestCase()]
        public void ReceivingBinanceError_Should_ReturnBinanceErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.PrepareExceptionClient<BinanceClient>(JsonConvert.SerializeObject(new ArgumentError("TestMessage")), "504 error", 504);

            // act
            var result = client.Ping();
            
            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Message.Contains("504 error"));
        }

        [Test]
        public void ProvidingApiCredentials_Should_SaveApiCredentials()
        {
            // arrange
            // act
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));

            // assert
            Assert.AreEqual(authProvider.Credentials.Key.GetString(), "TestKey");
            Assert.AreEqual(authProvider.Credentials.Secret.GetString(), "TestSecret");
        }

        [Test]
        [TestCase("", "D0F0F055B496CBD9FD1C8CA6719D0B2253F54C667753F70AEF13F394D9161A8B")]
        public void AddingAuthToUriString_Should_GiveCorrectSignature(string parameters, string signature)
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));
            string uri = $"https://test.test-api.com{parameters}";

            // act
            var sign = authProvider.AddAuthenticationToParameters(uri, "POST", new Dictionary<string, object>(), true);

            // assert
            Assert.IsTrue((string)sign.Last().Value == signature);
        }

        [Test]
        public void AddingAuthToRequest_Should_AddApiKeyHeader()
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));
            var request = new Request(WebRequest.CreateHttp("https://test.test-api.com"));

            // act
            var sign = authProvider.AddAuthenticationToHeaders(request.Uri.ToString(), "GET", new Dictionary<string, object>(), true);

            // assert
            Assert.IsTrue(sign.First().Key == "X-MBX-APIKEY" && sign.First().Value == "TestKey");
        }

        private BinanceClient Construct(BinanceClientOptions options = null)
        {
            if (options != null)
                return new BinanceClient(options);
            return new BinanceClient();            
        }        
    }
}
