using Binance.Net.UnitTests.TestImplementations;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Requests;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CryptoExchange.Net.Objects;
using System.Threading.Tasks;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using NUnit.Framework.Legacy;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Binance.Net.Interfaces.Clients;
using System.Diagnostics;

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
            var client = TestHelpers.CreateResponseClient(JsonSerializer.Serialize(time));

            // act
            var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();

            // assert
            Assert.That(result.Success);
            Assert.That(expected == result.Data);
        }
       
        [TestCase]
        public async Task StartUserStream_Should_RespondWithListenKey()
        {
            // arrange
            var key = new BinanceListenKey()
            {
                ListenKey = "123"
            };

            var client = TestHelpers.CreateResponseClient(key, options =>
            {
                options.ApiCredentials = new ApiCredentials("Test", "Test");
                options.SpotOptions.AutoTimestamp = false;
            });

            // act
            var result = await client.SpotApi.Account.StartUserStreamAsync();

            // assert
            Assert.That(result.Success);
            Assert.That(key.ListenKey == result.Data);
        }

        [TestCase]
        public async Task KeepAliveUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", options =>
            {
                options.ApiCredentials = new ApiCredentials("Test", "Test");
                options.SpotOptions.AutoTimestamp = false;
            });

            // act
            var result = await client.SpotApi.Account.KeepAliveUserStreamAsync("test");

            // assert
            Assert.That(result.Success);
        }

        [TestCase]
        public async Task StopUserStream_Should_Respond()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", options => { options.ApiCredentials = new ApiCredentials("Test", "Test"); options.SpotOptions.AutoTimestamp = false; });

            // act
            var result = await client.SpotApi.Account.StopUserStreamAsync("test");

            // assert
            Assert.That(result.Success);
        }

        [TestCase()]
        public async Task EnablingAutoTimestamp_Should_CallServerTime()
        {
            // arrange
            var client = TestHelpers.CreateResponseClient("{}", options =>
            {
                options.ApiCredentials = new ApiCredentials("Test", "Test");
                options.SpotOptions.AutoTimestamp = true;
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
            Mock.Get(client.SpotApi.RequestFactory).Verify(f => f.Create(It.IsAny<HttpMethod>(), It.Is<Uri>((uri) => uri.ToString().Contains("/time")), It.IsAny<int>()), Times.Exactly(2));
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
            ClassicAssert.IsFalse(result.Success);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error.ErrorCode == "123");
            Assert.That(result.Error.Message == "Error!");
        }

        [Test]
        public void ProvidingApiCredentials_Should_SaveApiCredentials()
        {
            // arrange
            // act
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));

            // assert
            Assert.That(authProvider.ApiKey == "TestKey");
        }

        [Test]
        public void AddingAuthToRequest_Should_AddApiKeyHeader()
        {
            // arrange
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"));
            var client = new HttpClient();
            var request = new Request(new HttpRequestMessage(HttpMethod.Get, "https://test.test-api.com"), client, 1);

            // act
            var headers = new Dictionary<string, string>();
            IDictionary<string, object> uriParams = null;
            IDictionary<string, object> bodyParams = null;
            authProvider.AuthenticateRequest(
                new BinanceRestApiClient(new TraceLogger(), new BinanceRestOptions(), new BinanceRestOptions().SpotOptions),
                request.Uri,
                HttpMethod.Get,
                ref uriParams,
                ref bodyParams,
                ref headers,
                true, ArrayParametersSerialization.MultipleValues,
                HttpMethodParameterPosition.InUri,
                RequestBodyFormat.Json);

            // assert
            Assert.That(headers.First().Key == "X-MBX-APIKEY" && headers.First().Value == "TestKey");
        }

        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("vmPUZE6mv9SD5VNHk4HlWFsOr6aKE2zvsw0MuIgwCIPy6utIco14y7Ju91duEh8A", "NhqPtmdSJYdKjVHjA7PZj4Mge3R5YNiP1e3UZjInClVN65XAbvqqM6A7H5fATj0j"));
            var client = (RestApiClient)new BinanceRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return bodyParams["signature"].ToString();
                },
                "c8db56825ae71d6d79447849e617115f4a920fa2acdcab2b053c4b2838bd6b71",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                    { "side", "BUY" },
                    { "type", "LIMIT" },
                    { "timeInForce", "GTC" },
                    { "quantity", "1" },
                    { "price", "0.1" },
                    { "recvWindow", "5000" },
                },
                DateTimeConverter.ParseFromString("1499827319559"),
                true,
                false);
        }

        [Test]
        public void CheckSignatureExample2()
        {
            var authProvider = new BinanceAuthenticationProvider(new ApiCredentials("vmPUZE6mv9SD5VNHk4HlWFsOr6aKE2zvsw0MuIgwCIPy6utIco14y7Ju91duEh8A", "NhqPtmdSJYdKjVHjA7PZj4Mge3R5YNiP1e3UZjInClVN65XAbvqqM6A7H5fATj0j"));
            var client = (RestApiClient)new BinanceRestClient().SpotApi;
            client.ParameterPositions[HttpMethod.Post] = HttpMethodParameterPosition.InUri;
            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return uriParams["signature"].ToString();
                },
                "c8db56825ae71d6d79447849e617115f4a920fa2acdcab2b053c4b2838bd6b71",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                    { "side", "BUY" },
                    { "type", "LIMIT" },
                    { "timeInForce", "GTC" },
                    { "quantity", "1" },
                    { "price", "0.1" },
                    { "recvWindow", "5000" },
                },
                DateTimeConverter.ParseFromString("1499827319559"),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BinanceRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BinanceSocketClient>();
        }

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.binance.com")]
        [TestCase(TradeEnvironmentNames.Testnet, "https://testnet.binance.vision")]
        [TestCase("us", "https://api.binance.us")]
        [TestCase("", "https://api.binance.com")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Binance:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBinance(configuration.GetSection("Binance"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBinanceRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Binance", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBinance(configuration.GetSection("Binance"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBinanceRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.binance.com"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Binance:Environment:Name", "testnet" },
                    { "Binance:Rest:Environment:Name", "us" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBinance(configuration.GetSection("Binance"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBinanceRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.binance.us"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBinance(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IBinanceRestClient>();
            var socketClient = provider.GetRequiredService<IBinanceSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
