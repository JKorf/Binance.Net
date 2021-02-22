using Binance.Net.Objects;
using Binance.Net.UnitTests.TestImplementations;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Requests;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CryptoExchange.Net.Objects;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using Binance.Net.Objects.Spot.UserData;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot;
using Binance.Net.Enums;
using Binance.Net.Objects.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net.Logging;

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
            var client = TestHelpers.CreateResponseClient(JsonConvert.SerializeObject(time), new BinanceClientOptions() { AutoTimestamp = false });

            // act
            var result = client.Spot.System.GetServerTime();

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
                FirstTradeId = 10000000000,
                HighPrice = 0.789m,
                LastTradeId = 20000000000,
                PrevDayClosePrice = 1.123m,
                LowPrice = 1.456m,
                OpenPrice = 1.789m,
                OpenTime = new DateTime(2017, 01, 01),
                LastPrice = 2.123m,
                PriceChange = 2.456m,
                PriceChangePercent = 2.789m,
                TotalTrades = 123,
                BaseVolume = 3.123m,
                AskQuantity = 3.456m,
                BidQuantity = 3.789m,
                QuoteVolume = 4.123m,
                Symbol = "BNBBTC",
                WeightedAveragePrice = 3.456m
            };

            var client = TestHelpers.CreateResponseClient(JsonConvert.SerializeObject(expected));

            // act
            var result = client.Spot.Market.Get24HPrice("BNBBTC");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data));
        }

        [TestCase]
        public void GetOrderBook_Should_RespondWithOrderBook()
        {
            // arrange
            var orderBook = new BinanceOrderBook()
            {
                LastUpdateId = 123,
                Symbol = "BNBBTC",
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
            var client = TestHelpers.CreateResponseClient("{\"lastUpdateId\":123,\"asks\": [[0.1, 1.1], [0.2, 2.2]], \"bids\": [[0.3,3.3], [0.4,4.4]]}");

            // act
            var result = client.Spot.Market.GetOrderBook("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(orderBook, result.Data, "Asks", "Bids", "AsksStream", "BidsStream", "LastUpdateIdStream"));
            Assert.IsTrue(TestHelpers.AreEqual(orderBook.Asks.ToList()[0], result.Data.Asks.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(orderBook.Asks.ToList()[1], result.Data.Asks.ToList()[1]));
            Assert.IsTrue(TestHelpers.AreEqual(orderBook.Bids.ToList()[0], result.Data.Bids.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(orderBook.Bids.ToList()[1], result.Data.Bids.ToList()[1]));
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

            var client = TestHelpers.CreateResponseClient(accountInfo, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.General.GetAccountInfo();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo, result.Data, "Balances", "Permissions"));
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo.Balances.ToList()[0], result.Data.Balances.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo.Balances.ToList()[1], result.Data.Balances.ToList()[1]));
        }

        [TestCase]
        public void GetAggregatedTrades_Should_RespondWithGetAggregatedTrades()
        {
            // arrange
            var trades = new[]
            {
                new BinanceAggregatedTrade()
                {
                    AggregateTradeId = 1,
                    BuyerIsMaker = true,
                    FirstTradeId = 10000000000,
                    LastTradeId = 200000000000,
                    Price = 1.1m,
                    Quantity = 2.2m,
                    TradeTime = new DateTime(2017, 1, 1),
                    WasBestPriceMatch = true
                },
                new BinanceAggregatedTrade()
                {
                    AggregateTradeId = 2,
                    BuyerIsMaker = false,
                    FirstTradeId = 30000000000,
                    LastTradeId = 400000000000,
                    Price = 3.3m,
                    Quantity = 4.4m,
                    TradeTime = new DateTime(2016, 1, 1),
                    WasBestPriceMatch = false
                }
            };

            var client = TestHelpers.CreateResponseClient(trades);

            // act
            var result = client.Spot.Market.GetAggregatedTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            var resultData = result.Data.ToList();
            Assert.AreEqual(trades.Length, resultData.Count);
            Assert.IsTrue(TestHelpers.AreEqual(trades[0], resultData[0]));
            Assert.IsTrue(TestHelpers.AreEqual(trades[1], resultData[1]));
        }

        [TestCase]
        public void GetAllBookPrices_Should_RespondWithAllBookPrices()
        {
            // arrange
            var prices = new[]
            {
                new BinanceBookPrice()
                {
                    BestAskPrice = 0.1m,
                    BestAskQuantity = 0.2m,
                    BestBidPrice = 0.3m,
                    BestBidQuantity = 0.4m,
                    Symbol = "BNBBTC"
                },
                new BinanceBookPrice()
                {
                    BestAskPrice = 0.5m,
                    BestAskQuantity = 0.6m,
                    BestBidPrice = 0.7m,
                    BestBidQuantity = 0.8m,
                    Symbol = "ETHBTC"
                }
            };

            var client = TestHelpers.CreateResponseClient(prices);

            // act
            var result = client.Spot.Market.GetAllBookPrices();

            // assert
            var data = result.Data.ToList();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(prices.Length, data.Count);
            Assert.IsTrue(TestHelpers.AreEqual(prices[0], data[0]));
            Assert.IsTrue(TestHelpers.AreEqual(prices[1], data[1]));
        }

        [TestCase]
        public void GetAllOrders_Should_RespondWithAllOrders()
        {
            // arrange
            var orders = new[]
            {
                new BinanceOrder()
                {
                    ClientOrderId = "order1",
                    QuantityFilled = 0.1m,
                    IcebergQuantity = 0.2m,
                    OrderId = 100000000000,
                    Quantity = 0.3m,
                    Price = 0.4m,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5m,
                    Symbol = "BNBBTC",
                    CreateTime = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    QuantityFilled = 0.6m,
                    IcebergQuantity = 0.7m,
                    OrderId = 200000000000,
                    Quantity = 0.8m,
                    Price = 0.9m,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0m,
                    Symbol = "ETHBTC",
                    CreateTime = new DateTime(2017, 1, 10),
                    TimeInForce = TimeInForce.ImmediateOrCancel,
                    Type = OrderType.Market
                }
            };

            var client = TestHelpers.CreateResponseClient(orders, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.GetAllOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Count());
            Assert.IsTrue(TestHelpers.AreEqual(orders[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(orders[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetAllPrices_Should_RespondWithAllPrices()
        {
            // arrange
            var prices = new[]
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

            var client = TestHelpers.CreateResponseClient(prices);

            // act
            var result = client.Spot.Market.GetPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), prices.Length);
            Assert.IsTrue(TestHelpers.AreEqual(prices[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(prices[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetDepositHistory_Should_RespondWithDepositHistory()
        {
            // arrange
            var history = new List<BinanceDeposit>()
            {
                new BinanceDeposit()
                {
                    Amount = 1.1m,
                    Coin = "BNB",
                    InsertTime = new DateTime(2017, 1, 1),
                    Status = DepositStatus.Pending
                },
                new BinanceDeposit()
                {
                    Amount = 2.2m,
                    Coin = "BTC",
                    InsertTime = new DateTime(2016, 1, 1),
                    Status = DepositStatus.Success
                }
            };

            var client = TestHelpers.CreateResponseClient(history, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.WithdrawDeposit.GetDepositHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), history.Count());
            Assert.IsTrue(TestHelpers.AreEqual(history.ToList()[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(history.ToList()[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetKlines_Should_RespondWithKlines()
        {
            // arrange
            var klines = new[]
            {
               new BinanceSpotKline()
               {
                    BaseVolume = 0.1m,
                    Close = 0.2m,
                    CloseTime = new DateTime(1970, 1, 1),
                    High = 0.3m,
                    Low = 0.4m,
                    Open = 0.5m,
                    OpenTime = new DateTime(1970, 1, 1),
                    TakerBuyBaseVolume = 0.6m,
                    TakerBuyQuoteVolume = 0.7m,
                    TradeCount = 10,
                    QuoteVolume = 0.8m
               },
               new BinanceSpotKline()
               {
                    BaseVolume = 1.5m,
                    Close = 1.0m,
                    CloseTime = new DateTime(1970, 1, 1),
                    High = 1.1m,
                    Low = 1.2m,
                    Open = 1.3m,
                    OpenTime = new DateTime(1970, 1, 1),
                    TakerBuyBaseVolume = 1.4m,
                    TakerBuyQuoteVolume = 0.9m,
                    TradeCount = 20,
                    QuoteVolume = 1.6m
               }
            };

            var client = TestHelpers.CreateResponseClient(JsonConvert.SerializeObject(new object[]
            {
                new object[] { 0, 0.5m, 0.3m, 0.4m, 0.2m, 0.1m, 0, 0.8m, 10, 0.6m, 0.7m},
                new object[] { 0, 1.3m, 1.1m, 1.2m, 1.0m, 1.5m, 0, 1.6m, 20, 1.4m, 0.9m }
            }), new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Market.GetKlines("BNBBTC", KlineInterval.OneMinute);

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), klines.Length);
            Assert.IsTrue(TestHelpers.AreEqual(klines[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(klines[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetMyTrades_Should_RespondWithTrades()
        {
            // arrange
            var trades = new[]
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
                    Symbol = "BNBUSDT",
                    TradeTime =  new DateTime(2017, 1, 1)
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
                    Symbol = "ETHBTC",
                    TradeTime =  new DateTime(2016, 1, 1)
                }
            };

            var client = TestHelpers.CreateResponseClient(trades, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.GetMyTrades("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), trades.Length);
            Assert.IsTrue(TestHelpers.AreEqual(trades[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(trades[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetOpenOrders_Should_RespondWithOpenOrders()
        {
            // arrange
            var orders = new[]
            {
                new BinanceOrder()
                {
                    ClientOrderId = "order1",
                    QuantityFilled = 0.1m,
                    IcebergQuantity = 0.2m,
                    OrderId = 100000000000,
                    Quantity = 0.3m,
                    Price = 0.4m,
                    Side = OrderSide.Buy,
                    Status = OrderStatus.Canceled,
                    StopPrice = 0.5m,
                    Symbol = "BNBBTC",
                    CreateTime = new DateTime(2017, 1, 1),
                    TimeInForce = TimeInForce.GoodTillCancel,
                    Type = OrderType.Limit
                },
                new BinanceOrder()
                {
                    ClientOrderId = "order2",
                    QuantityFilled = 0.6m,
                    IcebergQuantity = 0.7m,
                    OrderId = 200000000000,
                    Quantity = 0.8m,
                    Price = 0.9m,
                    Side = OrderSide.Sell,
                    Status = OrderStatus.PartiallyFilled,
                    StopPrice = 1.0m,
                    Symbol = "ETHBTC",
                    CreateTime = new DateTime(2017, 1, 10),
                    TimeInForce = TimeInForce.ImmediateOrCancel,
                    Type = OrderType.Market
                }
            };

            var client = TestHelpers.CreateResponseClient(orders, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.GetOpenOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Count());
            Assert.IsTrue(TestHelpers.AreEqual(orders[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(orders[1], result.Data.ToList()[1]));
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
            var client = TestHelpers.CreateResponseClient(history, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.WithdrawDeposit.GetWithdrawalHistory();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), history.List.Count());
            Assert.IsTrue(TestHelpers.AreEqual(history.List.ToList()[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(history.List.ToList()[1], result.Data.ToList()[1]));
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

            var client = TestHelpers.CreateResponseClient(canceled, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.CancelOrder("BNBBTC",orderId:123);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(canceled, result.Data));
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
                CreateTime = new DateTime(2017, 1, 1)
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.PlaceTestOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
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
                CreateTime = new DateTime(2017, 1, 1)
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.PlaceOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
        }

        [TestCase]
        public void QueryOrder_Should_RespondWithQueriedOrder()
        {
            // arrange
            var order = new BinanceOrder()
            {
                ClientOrderId = "order2",
                QuantityFilled = 0.6m,
                IcebergQuantity = 0.7m,
                OrderId = 200000000000,
                Quantity = 0.8m,
                Price = 0.9m,
                Side = OrderSide.Sell,
                Status = OrderStatus.PartiallyFilled,
                StopPrice = 1.0m,
                Symbol = "ETHBTC",
                CreateTime = new DateTime(2017, 1, 10),
                TimeInForce = TimeInForce.ImmediateOrCancel,
                Type = OrderType.Market
            };

            var client = TestHelpers.CreateResponseClient(order, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.Order.GetOrder("BNBBTC", orderId: 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(order, result.Data));
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

            var client = TestHelpers.CreateResponseClient(order, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.WithdrawDeposit.Withdraw("BNBBTC", "test", 1, "x");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(order, result.Data));
        }

        [TestCase]
        public void PlaceMultipleOrders_Should_RespondWithResultList()
        {
            // arrange
            var response =
                "[\r\n    {\r\n        \"clientOrderId\": \"testOrder\",\r\n        \"cumQuote\": \"0\",\r\n        \"executedQty\": \"0\",\r\n        \"orderId\": 22542179,\r\n        \"avgPrice\": \"0.00000\",\r\n        \"origQty\": \"10\",\r\n        \"price\": \"0\",\r\n        \"reduceOnly\": false,\r\n        \"side\": \"BUY\",\r\n        \"positionSide\": \"SHORT\",\r\n        \"status\": \"NEW\",\r\n        \"stopPrice\": \"9300\",        // please ignore when order type is TRAILING_STOP_MARKET\r\n        \"symbol\": \"BTCUSDT\",\r\n        \"timeInForce\": \"GTC\",\r\n        \"type\": \"TRAILING_STOP_MARKET\",\r\n        \"activatePrice\": \"9020\",    // activation price, only return with TRAILING_STOP_MARKET order\r\n        \"priceRate\": \"0.3\",         // callback rate, only return with TRAILING_STOP_MARKET order\r\n        \"updateTime\": 1566818724722,\r\n        \"workingType\": \"CONTRACT_PRICE\"\r\n    },\r\n    {\r\n        \"code\": -2022, \r\n        \"msg\": \"ReduceOnly Order is rejected.\"\r\n    }\r\n]";

            var client = TestHelpers.CreateResponseClient(response, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false,
                LogVerbosity = LogVerbosity.Debug
            });

            // act
            var result = client.FuturesUsdt.Order.PlaceMultipleOrders(new []
            {
                new BinanceFuturesBatchOrder()
                {
                    Symbol = "Test",
                    Quantity = 3,
                    Side = OrderSide.Sell
                },
                new BinanceFuturesBatchOrder()
                {
                    Symbol = "Test2",
                    Quantity = 2,
                    Side = OrderSide.Buy
                },
            });

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.First().Success);
            Assert.IsFalse(result.Data.Skip(1).First().Success);
        }

        [TestCase]
        public void GetTradingStatus_Should_RespondWithSuccess()
        {
            // arrange
            var status = new BinanceTradingStatusWrapper()
            {
                Success = true,
                Message = "Test",
                Status = new BinanceTradingStatus()
                {
                    IsLocked = false,
                    PlannedRecoverTime = 0,
                    UpdateTime = new DateTime(2019, 1, 1),
                    TriggerConditions = new Dictionary<string, int>()
                    {
                        { "GCR", 100 },
                        { "IFER", 150 }
                    },
                    Indicators = new Dictionary<string, IEnumerable<BinanceIndicator>>()
                    {
                        { "BTCUSDT", new List<BinanceIndicator>
                            {
                                new BinanceIndicator()
                                {
                                    Count = 1,
                                    CurrentValue = 0.5m,
                                    IndicatorType = IndicatorType.CancellationRatio,
                                    TriggerValue = 0.95m
                                }
                            }
                        }
                    }
                }
            };

            var client = TestHelpers.CreateResponseClient(status, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.General.GetTradingStatus();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(status.Status, result.Data, "Indicators", "TriggerConditions"));
            Assert.IsTrue(status.Status.TriggerConditions["GCR"] == result.Data.TriggerConditions["GCR"]);
            Assert.IsTrue(status.Status.TriggerConditions["IFER"] == result.Data.TriggerConditions["IFER"]);
            Assert.IsTrue(TestHelpers.AreEqual(status.Status.Indicators["BTCUSDT"].First(), result.Data.Indicators["BTCUSDT"].First()));
        }

        [TestCase]
        public void StartUserStream_Should_RespondWithListenKey()
        {
            // arrange
            var key = new BinanceListenKey()
            {
                ListenKey = "123"
            };

            var client = TestHelpers.CreateResponseClient(key, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.UserStream.StartUserStream();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(key.ListenKey == result.Data);
        }

        [TestCase]
        public void KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.UserStream.KeepAliveUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void StopUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Spot.UserStream.StopUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase()]
        public void EnablingAutoTimestamp_Should_CallServerTime()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = true
            });

            // act
            try
            {
                client.Spot.Order.GetOpenOrders();
            }
            catch (Exception)
            {
                // Exception is thrown because stream is being read twice, doesn't happen normally
            }


            // assert
            Mock.Get(client.RequestFactory).Verify(f => f.Create(It.IsAny<HttpMethod>(), It.Is<string>((msg) => msg.Contains("/time")), It.IsAny<int>()), Times.Exactly(2));
        }

        [TestCase()]
        public void ReceivingBinanceError_Should_ReturnBinanceErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetErrorWithResponse(client, "{\"msg\": \"Error!\", \"code\": 123}", HttpStatusCode.BadRequest);

            // act
            var result = client.Spot.System.GetServerTime();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Error);
            Assert.IsTrue(result.Error.Code == 123);
            Assert.IsTrue(result.Error.Message == "Error!");
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

        //[Test]
        [TestCase("", "D0F0F055B496CBD9FD1C8CA6719D0B2253F54C667753F70AEF13F394D9161A8B")]
        public void AddingAuthToUriString_Should_GiveCorrectSignature(string parameters, string signature)
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));
            string uri = $"https://test.test-api.com{parameters}";

            // act
            var sign = authProvider.AddAuthenticationToParameters(uri, HttpMethod.Post, new Dictionary<string, object>(), true, PostParameters.InBody, ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.IsTrue((string)sign.Last().Value == signature);
        }

        [Test]
        public void AddingAuthToRequest_Should_AddApiKeyHeader()
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));
            var client = new HttpClient();
            var request = new Request(new HttpRequestMessage(HttpMethod.Get, "https://test.test-api.com"), client, 1);

            // act
            var sign = authProvider.AddAuthenticationToHeaders(request.Uri.ToString(), HttpMethod.Get, new Dictionary<string, object>(), true, PostParameters.InBody, ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.IsTrue(sign.First().Key == "X-MBX-APIKEY" && sign.First().Value == "TestKey");
        }

        [TestCase]
        public void Transfer_Should_RespondWithMarginTransaction()
        {
            // arrange
            var placed = new BinanceTransaction()
            {
                TransactionId = 1001
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Margin.Transfer("USDT", 1001, TransferDirectionType.MainToMargin);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
        }

        [TestCase]
        public void Borrow_Should_RespondWithMarginTransaction()
        {
            // arrange
            var placed = new BinanceTransaction()
            {
                TransactionId = 11
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Margin.Borrow("USDT", 2002);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
        }

        [TestCase]
        public void Repay_Should_RespondWithMarginTransaction()
        {
            // arrange
            var placed = new BinanceTransaction()
            {
                TransactionId = 11
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Margin.Repay("USDT", 2002);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
        }

        [TestCase]
        public void PlaceMarginOrder_Should_RespondWithMarginPlacedOrder()
        {
            // arrange
            var placed = new BinancePlacedOrder()
            {
                ClientOrderId = "test",
                OrderId = 100000000000,
                Symbol = "BNBBTC",
                CreateTime = new DateTime(2017, 1, 1)
            };

            var client = TestHelpers.CreateResponseClient(placed, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Margin.Order.PlaceMarginOrder("BTCUSDT", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(placed, result.Data));
        }

        [TestCase]
        public void CancelMarginOrder_Should_RespondWithCanceledOrder()
        {
            // arrange
            var canceled = new BinanceCanceledOrder()
            {
                ClientOrderId = "test",
                OrderId = 100000000000,
                Symbol = "BNBBTC",
                OriginalClientOrderId = "test2"
            };

            var client = TestHelpers.CreateResponseClient(canceled, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.Margin.Order.CancelMarginOrder("BNBBTC", orderId:123);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(canceled, result.Data));
        }

        [TestCase("BTCUSDT", true)]
        [TestCase("NANOUSDT", true)]
        [TestCase("NANOAUSDTA", true)]
        [TestCase("NANOBTC", true)]
        [TestCase("ETHBTC", true)]
        [TestCase("BEETC", true)]
        [TestCase("EETC", false)]
        [TestCase("KP3RBNB", true)]
        [TestCase("BTC-USDT", false)]
        [TestCase("BTC-USD", false)]
        public void CheckValidBinanceSymbol(string symbol, bool isValid)
        {
            if (isValid)
                Assert.DoesNotThrow(symbol.ValidateBinanceSymbol);
            else
                Assert.Throws(typeof(ArgumentException), symbol.ValidateBinanceSymbol);
        }
    }
}
