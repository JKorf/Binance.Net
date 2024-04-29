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
            var client = new BinanceRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new EndpointTester<BinanceRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api.binance.com", IsAuthenticated, stjCompare: false);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", "serverTime");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSystemStatusAsync(), "GetSystemStatus");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetDetailsAsync(), "GetAssetDetails");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl.Contains("signature");
        }
    }
}
