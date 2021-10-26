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
using Binance.Net.Objects.Futures.FuturesData;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceClientTests
    {
        [TestCase(1508837063996)]
        [TestCase(1507156891385)]
        public async Task GetServerTime_Should_RespondWithServerTimeDateTime(long milisecondsTime)
        {
            // arrange
            DateTime expected = new DateTime(1970, 1, 1).AddMilliseconds(milisecondsTime);
            var time = new BinanceCheckTime() { ServerTime = expected };
            var client = TestHelpers.CreateResponseClient(JsonConvert.SerializeObject(time), new BinanceClientOptions() { AutoTimestamp = false });

            // act
            var result = await client.Spot.System.GetServerTimeAsync();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(expected, result.Data);
        }
       
        [TestCase]
        public async Task PlaceMultipleOrders_Should_RespondWithResultList()
        {
            // arrange
            var response =
                "[\r\n    {\r\n        \"clientOrderId\": \"testOrder\",\r\n        \"cumQuote\": \"0\",\r\n        \"executedQty\": \"0\",\r\n        \"orderId\": 22542179,\r\n        \"avgPrice\": \"0.00000\",\r\n        \"origQty\": \"10\",\r\n        \"price\": \"0\",\r\n        \"reduceOnly\": false,\r\n        \"side\": \"BUY\",\r\n        \"positionSide\": \"SHORT\",\r\n        \"status\": \"NEW\",\r\n        \"stopPrice\": \"9300\",        // please ignore when order type is TRAILING_STOP_MARKET\r\n        \"symbol\": \"BTCUSDT\",\r\n        \"timeInForce\": \"GTC\",\r\n        \"type\": \"TRAILING_STOP_MARKET\",\r\n        \"activatePrice\": \"9020\",    // activation price, only return with TRAILING_STOP_MARKET order\r\n        \"priceRate\": \"0.3\",         // callback rate, only return with TRAILING_STOP_MARKET order\r\n        \"updateTime\": 1566818724722,\r\n        \"workingType\": \"CONTRACT_PRICE\"\r\n    },\r\n    {\r\n        \"code\": -2022, \r\n        \"msg\": \"ReduceOnly Order is rejected.\"\r\n    }\r\n]";

            var client = TestHelpers.CreateResponseClient(response, new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false,
                LogLevel = LogLevel.Debug
            });

            // act
            var result = await client.FuturesUsdt.Order.PlaceMultipleOrdersAsync(new []
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
        public async Task StartUserStream_Should_RespondWithListenKey()
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
            var result = await client.Spot.UserStream.StartUserStreamAsync();

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(key.ListenKey == result.Data);
        }

        [TestCase]
        public async Task KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = await client.Spot.UserStream.KeepAliveUserStreamAsync("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase]
        public async Task StopUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("Test", "Test"),
                AutoTimestamp = false
            });

            // act
            var result = await client.Spot.UserStream.StopUserStreamAsync("test");

            // assert
            Assert.IsTrue(result.Success);
        }

        [TestCase()]
        public async Task EnablingAutoTimestamp_Should_CallServerTime()
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
                await client.Spot.Order.GetOpenOrdersAsync();
            }
            catch (Exception)
            {
                // Exception is thrown because stream is being read twice, doesn't happen normally
            }


            // assert
            Mock.Get(client.RequestFactory).Verify(f => f.Create(It.IsAny<HttpMethod>(), It.Is<string>((msg) => msg.Contains("/time")), It.IsAny<int>()), Times.Exactly(2));
        }

        [TestCase()]
        public async Task ReceivingBinanceError_Should_ReturnBinanceErrorAndNotSuccess()
        {
            // arrange
            var client = TestHelpers.CreateClient();
            TestHelpers.SetErrorWithResponse(client, "{\"msg\": \"Error!\", \"code\": 123}", HttpStatusCode.BadRequest);

            // act
            var result = await client.Spot.System.GetServerTimeAsync();

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
            var sign = authProvider.AddAuthenticationToParameters(uri, HttpMethod.Post, new Dictionary<string, object>(), true, HttpMethodParameterPosition.InBody, ArrayParametersSerialization.MultipleValues);

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
            var sign = authProvider.AddAuthenticationToHeaders(request.Uri.ToString(), HttpMethod.Get, new Dictionary<string, object>(), true, HttpMethodParameterPosition.InBody, ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.IsTrue(sign.First().Key == "X-MBX-APIKEY" && sign.First().Value == "TestKey");
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
