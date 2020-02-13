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

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceFuturesClientTests
    {
        [TestCase(1508837063996)]
        [TestCase(1507156891385)]
        public void GetServerTime_Should_RespondWithServerTimeDateTime(long milisecondsTime)
        {
            // arrange
            DateTime expected = new DateTime(1970, 1, 1).AddMilliseconds(milisecondsTime);
            var time = new BinanceCheckTime() { ServerTime = expected };
            var client = TestHelpers.CreateFuturesResponseClient(JsonConvert.SerializeObject(time), new BinanceFuturesClientOptions() { AutoTimestamp = false });

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

            var client = TestHelpers.CreateFuturesResponseClient(JsonConvert.SerializeObject(expected));

            // act
            var result = client.Get24HPrice("BNBBTC");

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
            var client = TestHelpers.CreateFuturesResponseClient("{\"lastUpdateId\":123,\"asks\": [[0.1, 1.1], [0.2, 2.2]], \"bids\": [[0.3,3.3], [0.4,4.4]]}");

            // act
            var result = client.GetOrderBook("BNBBTC");

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

            var client = TestHelpers.CreateFuturesResponseClient(accountInfo, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetAccountInfo();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo, result.Data, "Balances"));
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo.Balances.ToList()[0], result.Data.Balances.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(accountInfo.Balances.ToList()[1], result.Data.Balances.ToList()[1]));
        }

        [TestCase]
        public void GetAggregatedTrades_Should_RespondWithGetAggregatedTrades()
        {
            // arrange
            var trades = new[]
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

            var client = TestHelpers.CreateFuturesResponseClient(trades);

            // act
            var result = client.GetAggregatedTrades("BNBBTC");

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

            var client = TestHelpers.CreateFuturesResponseClient(prices);

            // act
            var result = client.GetAllBookPrices();

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

            var client = TestHelpers.CreateFuturesResponseClient(orders, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetAllOrders("BNBBTC");

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

            var client = TestHelpers.CreateFuturesResponseClient(prices);

            // act
            var result = client.GetAllPrices();

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Count(), prices.Length);
            Assert.IsTrue(TestHelpers.AreEqual(prices[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(prices[1], result.Data.ToList()[1]));
        }

        [TestCase]
        public void GetKlines_Should_RespondWithKlines()
        {
            // arrange
            var klines = new[]
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

            var client = TestHelpers.CreateFuturesResponseClient(JsonConvert.SerializeObject(new object[]
            {
                new object[] { 0, 0.5m, 0.3m, 0.4m, 0.2m, 0.8m, 0, 0.1m, 10, 0.6m, 0.7m},
                new object[] { 0, 1.3m, 1.1m, 1.2m, 1.0m, 1.6m, 0, 0.9m, 20, 1.4m, 1.5m }
            }), new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetKlines("BNBBTC", KlineInterval.OneMinute);

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
                    Symbol = "ETHBTC",
                    Time =  new DateTime(2016, 1, 1)
                }
            };

            var client = TestHelpers.CreateFuturesResponseClient(trades, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetMyTrades("BNBBTC");

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

            var client = TestHelpers.CreateFuturesResponseClient(orders, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetOpenOrders("BNBBTC");

            // assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(orders.Length, result.Data.Count());
            Assert.IsTrue(TestHelpers.AreEqual(orders[0], result.Data.ToList()[0]));
            Assert.IsTrue(TestHelpers.AreEqual(orders[1], result.Data.ToList()[1]));
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

            var client = TestHelpers.CreateFuturesResponseClient(canceled, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.CancelOrder("BNBBTC",orderId:123);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(canceled, result.Data));
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

            var client = TestHelpers.CreateFuturesResponseClient(placed, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.PlaceOrder("BNBBTC", OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: 1, price: 2);

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

            var client = TestHelpers.CreateFuturesResponseClient(order, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.GetOrder("BNBBTC", orderId: 1);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(order, result.Data));
        }

        [TestCase]
        public void StartUserStream_Should_RespondWithListenKey()
        {
            // arrange
            var key = new BinanceListenKey()
            {
                ListenKey = "123"
            };

            var client = TestHelpers.CreateFuturesResponseClient(key, new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.StartUserStream();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(key.ListenKey == result.Data);
        }

        [TestCase]
        public void KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateFuturesResponseClient("{}", new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.KeepAliveUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public void StopUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateFuturesResponseClient("{}", new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = client.StopUserStream("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase()]
        public void EnablingAutoTimestamp_Should_CallServerTime()
        {
            // arrange
            var client = TestHelpers.CreateFuturesResponseClient("{}", new BinanceFuturesClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = true
            });

            // act
            try
            {
                client.GetOpenOrders();
            }
            catch (Exception)
            {
                // Exception is thrown because stream is being read twice, doesn't happen normally
            }


            // assert
            Mock.Get(client.RequestFactory).Verify(f => f.Create(It.IsAny<HttpMethod>(), It.Is<string>((msg) => msg.Contains("/time"))), Times.Exactly(2));
        }

        [TestCase()]
        public void ReceivingBinanceError_Should_ReturnBinanceErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateFuturesClient();
            TestHelpers.SetErrorWithResponse(client, "{\"msg\": \"Error!\", \"code\": 123}", HttpStatusCode.BadRequest);

            // act
            var result = client.Ping();

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
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"), ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.AreEqual(authProvider.Credentials.Key.GetString(), "TestKey");
            Assert.AreEqual(authProvider.Credentials.Secret.GetString(), "TestSecret");
        }

        //[Test]
        [TestCase("", "D0F0F055B496CBD9FD1C8CA6719D0B2253F54C667753F70AEF13F394D9161A8B")]
        public void AddingAuthToUriString_Should_GiveCorrectSignature(string parameters, string signature)
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"), ArrayParametersSerialization.MultipleValues);
            string uri = $"https://test.test-api.com{parameters}";

            // act
            var sign = authProvider.AddAuthenticationToParameters(uri, HttpMethod.Post, new Dictionary<string, object>(), true);

            // assert
            Assert.IsTrue((string)sign.Last().Value == signature);
        }

        [Test]
        public void AddingAuthToRequest_Should_AddApiKeyHeader()
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"), ArrayParametersSerialization.MultipleValues);
            var client = new HttpClient();
            var request = new Request(new HttpRequestMessage(HttpMethod.Get, "https://test.test-api.com"), client);

            // act
            var sign = authProvider.AddAuthenticationToHeaders(request.Uri.ToString(), HttpMethod.Get, new Dictionary<string, object>(), true);

            // assert
            Assert.IsTrue(sign.First().Key == "X-MBX-APIKEY" && sign.First().Value == "TestKey");
        }

        [TestCase("BTCUSDT", true)]
        [TestCase("NANOUSDT", true)]
        [TestCase("NANOAUSDTA", true)]
        [TestCase("NANOAUSDTADD", false)]
        [TestCase("NANOBTC", true)]
        [TestCase("ETHBTC", true)]
        [TestCase("BEETC", true)]
        [TestCase("EETC", false)]
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
