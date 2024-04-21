using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects;
using CryptoExchange.Test.Net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class EndpointTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var tester = new EndpointTester<BinanceRestClient>("Endpoints/Spot/ExchangeData", "https://api.binance.com", IsAuthenticated);
            //await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", "serverTime");
            //await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            //await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(), "GetSystemStatus");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetDetailsAsync(), "GetAssetDetails");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl.Contains("signature");
        }
    }
}
