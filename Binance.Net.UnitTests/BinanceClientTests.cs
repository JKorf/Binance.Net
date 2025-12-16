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
                DateTimeConverter.ParseFromString("1499827319559", null),
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
                DateTimeConverter.ParseFromString("1499827319559", null),
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
