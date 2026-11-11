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
    [SimpleJob(RuntimeMoniker.Net48, invocationCount: 1)]
    [SimpleJob(RuntimeMoniker.Net11_0, invocationCount: 1)]
    public class LibraryComparisonBenchmarksSocket
    {
        private BinanceSocketClient _binanceNetClient;
        private BenchmarkCcxtClient _ccxtClient;
        private BinanceSocketApiClient _binanceApiClient;

        [GlobalSetup(Targets = [nameof(BinanceNet_Trades), nameof(BinanceNetShared_Trades), nameof(BinanceApi_Trades)])]
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

            _binanceNetClient = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = env,
            }), null);



            _binanceApiClient = new BinanceSocketApiClient(new BinanceSocketApiClientOptions
            {
                AutoReconnect = false,
            });
            BinanceAddress.Default.SpotSocketApiStreamAddress = "ws://localhost:" + Program.ServerPort;
        }

        [IterationSetup(Target = nameof(CCXT_Trades))]
        public void SetupCcxt()
        {
            _ccxtClient = CreateWs();
            _ccxtClient.LoadMarkets().GetAwaiter().GetResult();
        }

        [Benchmark(Baseline = true), IterationCount(25)]
        public async Task BinanceNet_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _binanceNetClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;

                if (received == TradeStream.Count)
                    waitEvent.Set();
            }, CancellationToken.None);

            if (!result.Success)
                throw new InvalidOperationException(result.Error.ToString());
            try
            {
                if (!await waitEvent.WaitAsync(TimeSpan.FromSeconds(30)))
                    throw new TimeoutException($"Received {received} of {TradeStream.Count} trades.");
            }
            finally
            {
                await result.Data.CloseAsync();
            }
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
                if (received == TradeStream.Count)
                    waitEvent.Set();
            }, CancellationToken.None);

            if (!result.Success)
                throw new InvalidOperationException(result.Error.ToString());
            try
            {
                if (!await waitEvent.WaitAsync(TimeSpan.FromSeconds(30)))
                    throw new TimeoutException($"Received {received} of {TradeStream.Count} trades.");
            }
            finally
            {
                await result.Data.CloseAsync();
            }
        }

        [Benchmark, IterationCount(25)]
        public async Task CCXT_Trades()
        {
            using var timeout = new CancellationTokenSource();
            var deadline = Task.Delay(TimeSpan.FromSeconds(30), timeout.Token);
            try
            {
                var subscription = _ccxtClient.WatchTrades("ETH/USDT");
                if (await Task.WhenAny(subscription, deadline) == deadline)
                    throw new TimeoutException("CCXT did not deliver the first ETH/USDT trade batch.");
                await subscription;

                if (await Task.WhenAny(_ccxtClient.StreamCompleted, deadline) == deadline)
                    throw new TimeoutException($"CCXT processed {_ccxtClient.Received} of {TradeStream.Count} trades.");
            }
            finally
            {
                timeout.Cancel();
                await _ccxtClient.Close();
            }
        }

        private sealed class BenchmarkCcxtClient : ccxt.pro.binance
        {
            private readonly TaskCompletionSource<bool> _completed = new(TaskCreationOptions.RunContinuationsAsynchronously);
            public Task StreamCompleted => _completed.Task;
            public int Received { get; private set; }

            public override void handleTrade(ccxt.Exchange.WebSocketClient client, object message)
            {
                base.handleTrade(client, message);
                // WatchTrades delivers batches and can miss the last notification between awaits.
                if (++Received == TradeStream.Count)
                    _completed.TrySetResult(true);
            }
        }

        [Benchmark, IterationCount(25)]
        public async Task BinanceApi_Trades()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await _binanceApiClient.Spot.SubscribeToTradesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == TradeStream.Count)
                    waitEvent.Set();
            }, CancellationToken.None);

            if (!result.Success)
                throw new InvalidOperationException(result.Error.ToString());
            try
            {
                if (!await waitEvent.WaitAsync(TimeSpan.FromSeconds(30)))
                    throw new TimeoutException($"Received {received} of {TradeStream.Count} trades.");
            }
            finally
            {
                await result.Data.CloseAsync();
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _binanceNetClient?.Dispose();
        }
        private static BenchmarkCcxtClient CreateWs()
        {
            var client = new BenchmarkCcxtClient();
            client.enableRateLimit = false;
            client.newUpdates = true;
            client.options["tradesLimit"] = TradeStream.Count;

            var urls = (Dictionary<string, object>)client.urls;
            var apiUrls = (Dictionary<string, object>)urls["api"];
            apiUrls["public"] = "http://localhost:" + Program.ServerPort + "/api/v3";
            client.options["fetchMarkets"] = new Dictionary<string, object> { ["types"] = new List<object> { "spot" } };
            apiUrls["ws"] = new Dictionary<string, object>
            {
                ["spot"] = "ws://localhost:" + Program.ServerPort + "/stream",
            };

            return client;
        }
    }
}
