using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Binance.Api;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryComparisonBenchmarksSocket
    {
        private static readonly int _socketUpdateReceiveTarget = 10_0000;

        private BinanceSocketClient _binanceNetClient;
        private ccxt.pro.binance _ccxtClient;
        private BinanceSocketApiClient _binanceApiClient;

        [GlobalSetup]
        public async Task Setup()
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

            _binanceNetClient = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = env,
            }), null);

            _ccxtClient = CreateWs();
            await _ccxtClient.LoadMarkets();

            _binanceApiClient = new BinanceSocketApiClient(new BinanceSocketApiClientOptions
            {
                AutoReconnect = false,
            });
            BinanceAddress.Default.SpotSocketApiStreamAddress = "ws://localhost:" + Program.ServerPort;
        }

        [Benchmark(Baseline = true), IterationCount(25)]
        public async Task BinanceNet_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _binanceNetClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceNetShared_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var request = new SubscribeTradeRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"));
            var result = await _binanceNetClient.SpotApi.SharedClient.SubscribeToTradeUpdatesAsync(request, x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var trades = await _ccxtClient.WatchTrades("ETH/USDT");
                    received += trades.Count;

                    if (received >= _socketUpdateReceiveTarget)
                        break;
                }

                waitEvent.Set();
            });

            await waitEvent.WaitAsync();
            await _ccxtClient.Close();
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceApi_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _binanceApiClient.Spot.SubscribeToTradesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _binanceNetClient?.Dispose();
        }
        public static ccxt.pro.binance CreateWs()
        {
            var client = new ccxt.pro.binance();
            client.enableRateLimit = false;
            client.newUpdates = true;

            var urls = (Dictionary<string, object>)client.urls;
            var apiUrls = (Dictionary<string, object>)urls["api"];
            apiUrls["ws"] = new Dictionary<string, object>
            {
                ["spot"] = "ws://localhost:" + Program.ServerPort + "/stream",
            };

            return client;
        }
    }
}
