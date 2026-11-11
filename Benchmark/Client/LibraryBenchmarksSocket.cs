using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Benchmark.Client
{

    [MemoryDiagnoser]
#if !VERSION_COMPARISON
    [Config(typeof(Config))]
#endif
    public class LibraryBenchmarksSocket
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                var baseJob = Job.Default;

                AddJob(
                    baseJob
                        .WithId("NET11_0")
                        .WithIterationCount(20)
                        .WithRuntime(CoreRuntime.Core11_0));
                AddJob(
                    baseJob
                        .WithId("NET481")
                        .WithIterationCount(20)
                        .WithRuntime(ClrRuntime.Net48));
            }
        }

        public BinanceSocketClient SocketClient;
        public ILogger Logger;

        [GlobalSetup()]
        public void Setup()
        {
#if VERSION_COMPARISON
            Program.VerifyLibraries();
#endif
            CreateClient();
        }

        [Benchmark(Baseline = true)]
        public Task Socket1Topic() => ReceiveTradesAsync(["ETHUSDT"]);

        [Benchmark()]
        public Task Socket100Topics()
        {
            var topics = new string[100];
            for (var i = 0; i < 100; i++)
                topics[i] = "DUMMY" + i;

            topics[50] = "ETHUSDT";

            return ReceiveTradesAsync(topics);
        }

        private async Task ReceiveTradesAsync(string[] topics)
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(topics, x =>
            {
                received++;
                if (received == TradeStream.Count)
                    waitEvent.Set();

            }, CancellationToken.None);

            if (!result.Success)
                throw new InvalidOperationException($"Subscription failed: {result.Error}");
            try
            {
                if (!await waitEvent.WaitAsync(TimeSpan.FromSeconds(30)))
                    throw new TimeoutException($"Received {received} of {TradeStream.Count} trade updates.");
            }
            finally
            {
                await result.Data.CloseAsync();
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            SocketClient.Dispose();
        }

        private void CreateClient()
        {
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:" + Program.ServerPort, "ws://localhost:" + Program.ServerPort, "", "", "", "", "", "", "", "");
            SocketClient = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }
}
