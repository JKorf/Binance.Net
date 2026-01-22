using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class RestTester
    {
        public BinanceRestClient RestClient;

        [GlobalSetup(Targets = [nameof(RestUpdated)])]
        public void SetupUpdatedDeserialization()
        {
            CreateClient();
        }

        [Benchmark()]
        public async Task RestUpdated()
        {
            for (var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            }
        }

        private void CreateClient()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider(LogLevel.Information));
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:57589", "ws://localhost:57589", "", "", "", "", "", "", "", "");
            RestClient = new BinanceRestClient(null, logger, Options.Create(new BinanceRestOptions
            {
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }

    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class SocketTester
    {
        public BinanceSocketClient SocketClient;
        public ILogger Logger;

        private const int _socketUpdateReceiveTarget = 10000; // Should match the number in the server


        [GlobalSetup]
        public void SetupUpdatedDeserialization()
        {
            CreateClient();
        }

        [Benchmark()]
        public async Task SocketHighPerf()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesPerfAsync(["ETHUSDT"], x =>
            {
                received++;

                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark()]
        public async Task Socket()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _socketUpdateReceiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            SocketClient.Dispose();
        }

        private void CreateClient()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider(LogLevel.Information));
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:57589", "ws://localhost:57589", "", "", "", "", "", "", "", "");
            SocketClient = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = env
            }), logger);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new Tester();
            //test.SetupUpdatedDeserialization();
            //Console.ReadLine();
            //Console.WriteLine("Starting");
            //var sw = Stopwatch.StartNew();
            //for (var i = 0; i < 100; i++)
            //{
            //    test.SocketHighPerf().Wait();
            //}
            //sw.Stop();
            //Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}