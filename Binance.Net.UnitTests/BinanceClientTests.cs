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
using Binance.Net.Enums;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Binance.Net.Clients.Rest.Spot;
using System.Reflection;
using System.Diagnostics;
using Binance.Net.Clients.Socket;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Sockets;

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
            var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(expected, result.Data);
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
            var result = await client.SpotApi.Account.StartUserStreamAsync();

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
            var result = await client.SpotApi.Account.KeepAliveUserStreamAsync("test");

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
            var result = await client.SpotApi.Account.StopUserStreamAsync("test");

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
                await client.SpotApi.Trading.GetOpenOrdersAsync();
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
            var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();

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

        [Test]
        public void CheckRestInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(BinanceClient));
            var ignore = new string[] { "IBinanceClientUsdFutures", "IBinanceClientCoinFutures", "IBinanceClientSpot" };
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IBinanceClient") && !ignore.Contains(t.Name));
            
            foreach(var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod, $"Missing interface for method {method.Name} in {clientInterface.Name}");
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }

        [Test]
        public void CheckSocketInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(BinanceSocketClientSpot));
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IBinanceSocketClient"));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task<CallResult<UpdateSubscription>>))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod, $"Missing interface for method {method.Name} in {clientInterface.GetType().Name}");
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }
    }
}
