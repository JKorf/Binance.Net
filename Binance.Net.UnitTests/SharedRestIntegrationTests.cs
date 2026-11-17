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

namespace Binance.Net.UnitTests
{
    [TestFixture]
    internal class SharedRestIntegrationTests
    {
        private bool ManualRun { get; } = false;
        private static SharedSymbol _spotSymbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
        private static SharedSymbol _futuresSymbol = new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT");

        private bool ShouldRun()
        {
            var integrationTests = Environment.GetEnvironmentVariable("INTEGRATION");
            if (!ManualRun && integrationTests != "1")
                return false;

            return true;
        }

        private IBinanceRestClientSpotSharedApi GetSpotRestClient()
        {
            var collection = new ServiceCollection();
            collection.AddBinance(x => x.Rest.OutputOriginalData = true);
            collection.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Trace);
                x.AddProvider(new TraceLoggerProvider());
            });
            var sp = collection.BuildServiceProvider();
            return sp.GetRequiredService<IBinanceRestClient>().SpotApi.SharedApi;
        }

        private IBinanceRestClientUsdFuturesSharedApi GetFuturesRestClient()
        {
            var collection = new ServiceCollection();
            collection.AddBinance(x => x.Rest.OutputOriginalData = true);
            collection.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Trace);
                x.AddProvider(new TraceLoggerProvider());
            });
            var sp = collection.BuildServiceProvider();
            return sp.GetRequiredService<IBinanceRestClient>().UsdFuturesApi.SharedApi;
        }

        [Test]
        public async Task TestFuturesKlinesRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetKlinesAsync(new GetKlinesRequest(_futuresSymbol, SharedKlineInterval.OneDay));
            var result3 = await client.GetKlinesAsync(new GetKlinesRequest(_futuresSymbol, SharedKlineInterval.OneDay, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow));
            CheckResults([
                ("FuturesKlines", result1),
                ("FuturesKlinesTimed", result3),
                ]);
        }

        [Test]
        public async Task TestFuturesIndexKlinesRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetIndexPriceKlinesAsync(new GetKlinesRequest(_futuresSymbol, SharedKlineInterval.OneDay));
            var result3 = await client.GetIndexPriceKlinesAsync(new GetKlinesRequest(_futuresSymbol, SharedKlineInterval.OneDay, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow));
            CheckResults([
                ("FuturesIndexKlines", result1),
                ("FuturesIndexKlinesTimed", result3),
                ]);
        }

        [Test]
        public async Task TestSpotKlinesRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetKlinesAsync(new GetKlinesRequest(_spotSymbol, SharedKlineInterval.OneDay));
            var result3 = await client.GetKlinesAsync(new GetKlinesRequest(_spotSymbol, SharedKlineInterval.OneDay, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow));
            CheckResults([
                ("SpotKlines", result1),
                ("SpotKlinesTimed", result3),
                ]);
        }

        [Test]
        public async Task TestFuturesBookTickersRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetBookTickerAsync(new GetBookTickerRequest(_futuresSymbol));
            CheckResults("FuturesBookTicker", result1);
        }

        [Test]
        public async Task TestSpotBookTickersRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetBookTickerAsync(new GetBookTickerRequest(_spotSymbol));
            CheckResults("SpotBookTicker", result1);
        }

        [Test]
        public async Task TestFuturesOrderBookRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetOrderBookAsync(new GetOrderBookRequest(_futuresSymbol));
            CheckResults("FuturesOrderBook", result1);
        }

        [Test]
        public async Task TestSpotOrderBookRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetOrderBookAsync(new GetOrderBookRequest(_spotSymbol));
            CheckResults("SpotOrderBook", result1);
        }

        [Test]
        public async Task TestFuturesTickerRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetTickerAsync(new GetTickerRequest(_futuresSymbol));
            var result2 = await client.GetAllTickersAsync(new GetTickersRequest());
            CheckResults([
                ("FuturesTicker", result1),
                ("FuturesTickers", result2)
                ]);
        }

        [Test]
        public async Task TestSpotTickerRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetTickerAsync(new GetTickerRequest(_spotSymbol));
            var result2 = await client.GetAllTickersAsync(new GetTickersRequest());
            CheckResults([
                ("SpotTicker", result1),
                ("SpotTickers", result2)
                ]);
        }

        [Test]
        public async Task TestFuturesSymbolRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetFuturesSymbolsAsync(new GetSymbolsRequest());
            CheckResults([
                ("FuturesSymbols", result1)
                ]);
        }

        [Test]
        public async Task TestSpotSymbolRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetSpotSymbolsAsync(new GetSymbolsRequest());
            CheckResults([
                ("SpotSymbols", result1)
                ]);
        }

        [Test]
        public async Task TestFuturesTradesRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetRecentTradesAsync(new GetRecentTradesRequest(_futuresSymbol));
            CheckResults([
                ("FuturesTrades", result1)
                ]);
        }

        [Test]
        public async Task TestSpotTradesRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetSpotRestClient();
            var result1 = await client.GetRecentTradesAsync(new GetRecentTradesRequest(_spotSymbol));
            CheckResults([
                ("SpotTrades", result1)
                ]);
        }

        [Test]
        public async Task TestFuturesTradesHistoryRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetTradeHistoryAsync(new GetTradeHistoryRequest(_futuresSymbol, DateTime.UtcNow.AddDays(-1)));
            CheckResults([
                ("FuturesTrades", result1)
                ]);
        }

        [Test]
        public async Task TestFundingRateRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetFundingRateHistoryAsync(new GetFundingRateHistoryRequest(_futuresSymbol));
            var result2 = await client.GetFundingRateHistoryAsync(new GetFundingRateHistoryRequest(_futuresSymbol, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow));
            CheckResults([
                ("FuturesFundingRateHistory", result1),
                ("FuturesFundingRateHistoryTimed", result2)
                ]);
        }

        [Test]
        public async Task TestOpenInterestRequests()
        {
            if (!ShouldRun())
                return;

            var client = GetFuturesRestClient();
            var result1 = await client.GetOpenInterestAsync(new GetOpenInterestRequest(_futuresSymbol));
            CheckResults([
                ("FuturesOpenInterest", result1)
                ]);
        }

        private void CheckResults(string name, ICallResult result)
            => CheckResults([(name, result)]);
        private void CheckResults((string, ICallResult result)[] results)
        {
            foreach (var item in results)
            {
                if (!item.result.Success)
                    throw new Exception($"Failed to get {item.Item1}: " + item.result.Error);
            }
        }

        private void CheckResults<T>(string name, ICallResult<T[]> result)
            => CheckResults([(name, result)]);
        private void CheckResults<T>((string, ICallResult<T[]> result)[] results)
        {
            foreach (var item in results)
            {
                if (!item.result.Success)
                    throw new Exception($"Failed to get {item.Item1}: " + item.result.Error);

                if (item.result.Data.Length == 0)
                    throw new Exception($"No response data for {item.Item1}");
            }
        }
    }
}
