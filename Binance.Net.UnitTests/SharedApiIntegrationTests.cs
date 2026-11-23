using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests
{
    [TestFixture]
    public class BinanceSharedApiRestIntegrationTest : SharedApiRestIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync() => await ExecuteAsync();

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBinance();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBinanceSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedRest client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");

            else if (options.SupportedTradingModes.Contains(TradingMode.PerpetualInverse))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualInverse, "ETH", "USD"), "ETH");

            return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }
    }

    [TestFixture]
    public class BinanceSharedApiSocketIntegrationTest : SharedApiSocketIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            var result = await ExecuteAsync();
            foreach (var item in result)
                Assert.Warn(item.Message);
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddBinance();
            collection.AddLogging(x =>
            {
                var logLevel = LogLevel.Trace;
                x.SetMinimumLevel(logLevel);
                x.AddProvider(new TraceLoggerProvider());
            });
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IBinanceSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedSocket client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");

            else if (options.SupportedTradingModes.Contains(TradingMode.PerpetualInverse))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualInverse, "ETH", "USD"), "ETH");

            return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }
    }
}
