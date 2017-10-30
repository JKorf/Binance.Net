using System;
using System.IO;
using System.Net;
using System.Text;
using Binance.Net.Interfaces;
using Binance.Net.Logging;
using Binance.Net.Objects;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class OtherTests
    {
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
            Assert.Throws(typeof(ArgumentException), () => client.SetApiCredentials(key, secret));
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("test", null)]
        [TestCase("test", "")]
        [TestCase(null, "test")]
        [TestCase("", "test")]
        public void SettingEmptyValuesForDefaultAPICredentials_Should_ThrowException(string key, string secret)
        {
            // arrange
            // act
            // assert
            Assert.Throws(typeof(ArgumentException), () => BinanceDefaults.SetDefaultApiCredentials(key, secret));
        }

        [TestCase()]
        public void SettingLogOutput_Should_RedirectLogOutput()
        {
            // arrange
            var client = PrepareClient(JsonConvert.SerializeObject(new BinancePing()));
            var stringBuilder = new StringBuilder();

            // act
            client.SetLogVerbosity(LogVerbosity.Debug);
            client.SetLogOutput(new StringWriter(stringBuilder));
            client.Ping();

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(stringBuilder.ToString()));
        }

        [TestCase()]
        public void SettingDefaults_Should_ImpactNewClients()
        {
            // arrange
            var stringBuilder = new StringBuilder();
            BinanceDefaults.SetDefaultApiCredentials("test", "test");
            BinanceDefaults.SetDefaultLogOutput(new StringWriter(stringBuilder));
            BinanceDefaults.SetDefaultLogVerbosity(LogVerbosity.Debug);

            var client = PrepareClient(JsonConvert.SerializeObject(new BinancePing()));

            // act
            Assert.DoesNotThrow(() => client.GetAccountInfo());

            // assert
            Assert.IsFalse(string.IsNullOrEmpty(stringBuilder.ToString()));
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
