using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.SpotApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Testing;
using Binance.Net.Clients;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    internal class SharedRestIntegrationTest : SharedApiRestIntegrationTest
    {
        public override bool ManualRun => true;

        public override Dictionary<ISharedApi, SharedApiTestConfig> GetApis()
        {
            var client = new BinanceRestClient(x =>
            {
            });
            return new Dictionary<ISharedApi, SharedApiTestConfig>
            {
                { client.SpotApi.SharedApi, new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH") },
                { client.UsdFuturesApi.SharedApi, new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT")) },
                { client.CoinFuturesApi.SharedApi, new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualInverse, "ETH", "USD")) },
            };
        }

        [Test]
        public override Task RunAsync() => ExecuteAsync();
    }
}
