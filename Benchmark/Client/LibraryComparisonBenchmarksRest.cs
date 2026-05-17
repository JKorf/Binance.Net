using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Binance.Api;
using Binance.Net.Clients;
using ccxt;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryComparisonBenchmarksRest
    {
        private BinanceRestClient _binanceNetClient;
        private binance _ccxtClient;
        private BinanceRestApiClient _binanceApiClient;

        [GlobalSetup]
        public void Setup()
        {
            var env = BinanceEnvironment.CreateCustom(
                "Benchmark",
                "http://localhost:" + Program.ServerPort,
                "ws://localhost:" + Program.ServerPort,
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "");

            _binanceNetClient = new BinanceRestClient(x =>
            {
                x.RateLimiterEnabled = false;
                x.Environment = env;
            });

            _ccxtClient = CreateRest();

#pragma warning disable CS0612 // Type or member is obsolete
            _binanceApiClient = new BinanceRestApiClient(new BinanceRestApiClientOptions
            {
                RateLimiters = [],
                RateLimiterEnabled = false,
            });
#pragma warning restore CS0612 // Type or member is obsolete
            BinanceAddress.Default.SpotRestApiAddress = "http://localhost:" + Program.ServerPort;
        }

        [Benchmark(Baseline = true), IterationCount(25)]
        public async Task BinanceNet_ServerTime()
        {
            for (var i = 0; i < 1000; i++)
                _ = await _binanceNetClient.SpotApi.ExchangeData.GetServerTimeAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_ServerTime()
        {
            for (var i = 0; i < 1000; i++)
                _ = await _ccxtClient.publicGetTime();
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceApi_ServerTime()
        {
            for (var i = 0; i < 1000; i++)
                _ = await _binanceApiClient.Spot.GetTimeAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceNet_Ticker()
        {
            for (var i = 0; i < 1000; i++)
                _ = await _binanceNetClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceNetShared_Ticker()
        {
            var request = new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"));
            for (var i = 0; i < 1000; i++)
                _ = await _binanceNetClient.SpotApi.SharedClient.GetSpotTickerAsync(request);
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_Ticker()
        {
            var parameters = new Dictionary<string, object>
            {
                ["symbol"] = "ETHUSDT"
            };
            for (var i = 0; i < 1000; i++)
                _ = await _ccxtClient.publicGetTicker24hr(parameters);
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceApi_Ticker()
        {
            for (var i = 0; i < 1000; i++)
                _ = await _binanceApiClient.Spot.GetTickerAsync("ETHUSDT");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _binanceNetClient?.Dispose();
        }

        public static binance CreateRest()
        {
            var client = new binance();
            client.enableRateLimit = false;
            client.newUpdates = true;

            var urls = (Dictionary<string, object>)client.urls;
            var apiUrls = (Dictionary<string, object>)urls["api"];
            apiUrls["public"] = "http://localhost:" + Program.ServerPort + "/api/v3";
            return client;
        }
    }
}
