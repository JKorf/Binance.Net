using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    //[ThreadingDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    //[SimpleJob(RuntimeMoniker.Net90)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class SocketTests
    {
        public BinanceSocketClient Client;
        public ILogger Logger;

        private const int _receiveTarget = 10000; // Should match the number in the server


        [GlobalSetup(Target = nameof(NormalNew))]
        public void GlobalSetupNew()
        {
            CreateClient(true);
        }

        [GlobalSetup(Targets = [nameof(Normal), nameof(HighPerf)])]
        public void GlobalSetup()
        {
            CreateClient(false);
        }

        [Benchmark()]
        public async Task HighPerf()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await Client.SpotApi.ExchangeData.SubscribeToTradeUpdatesPerfAsync(["ETHUSDT"], x =>
            {
                received++;
                
                if (received == _receiveTarget)
                    waitEvent.Set();

                return new ValueTask();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark()]
        public async Task NormalNew()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await Client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _receiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [Benchmark()]
        public async Task Normal()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await Client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _receiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Client.Dispose();
        }

        private void CreateClient(bool enableNewDeserialization)
        {
            var logger = new LoggerFactory();
            //logger.AddProvider(new TraceLoggerProvider());
            Logger = logger.CreateLogger("Test");
            Client = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                EnabledNewDeserialization = enableNewDeserialization,
                RateLimiterEnabled = false,
                Environment = BinanceEnvironment.CreateCustom("Benchmark", "", "ws://localhost:5034", "", "", "", "", "", "", "", "")
            }), logger);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new SocketTests();
            //test.GlobalSetupNew();
            ////Console.ReadLine();
            //Console.WriteLine("Starting");
            //for (var i = 0; i < 2; i++)
            //{
            //    test.NormalNew().Wait();
            //}
            //Console.WriteLine("Finished");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkRunner.Run<SocketTests>();
        }
    }
}