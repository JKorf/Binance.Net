using NUnit.Framework;
using Moq;
using System.IO;
using System.Net;
using System.Text;
using System;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.UnitTests
{
    [TestFixture()]
    public class BinanceNetTest
    {
        [TestCase(1508837063996)]
        [TestCase(1507156891385)]
        public void GetServerTime_Should_RespondWithServerTimeDateTime(long milisecondsTime)
        {
            // arrange
            DateTime expected = new DateTime(1970, 1, 1).AddMilliseconds(milisecondsTime);
            var time = new BinanceCheckTime() { ServerTime = expected };
            SetNextResponse(JsonConvert.SerializeObject(time));

            // act
            var serverTime = BinanceClient.GetServerTime();

            // assert
            Assert.AreEqual(serverTime.Data, expected);
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

            SetNextResponse(JsonConvert.SerializeObject(expected));

            // act
            var result = BinanceClient.Get24HPrices("BNBBTC");

            // assert
            Assert.AreEqual(expected.AskPrice, result.Data.AskPrice);
        }

        private void SetNextResponse(string responseData)
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

            BinanceClient.RequestFactory = factory.Object;
        }
    }   

}
