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
    //[SimpleJob(RuntimeMoniker.Net48)]
    //[SimpleJob(RuntimeMoniker.Net90)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class SocketTests
    {
        public BinanceSocketClient SocketClient;
        public BinanceRestClient RestClient;
        public ILogger Logger;

        private const int _receiveTarget = 10000; // Should match the number in the server


        [GlobalSetup(Targets = [nameof(SocketUpdate), nameof(RestUpdate)])]
        public void GlobalSetupNew()
        {
            CreateClient(true);
        }

        [GlobalSetup(Targets = [nameof(Socket), nameof(SocketHighPerf), nameof(Rest)])]
        public void GlobalSetup()
        {
            CreateClient(false);
        }

        //[Benchmark()]
        public async Task SocketHighPerf()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesPerfAsync(["ETHUSDT"], x =>
            {
                received++;

                if (received == _receiveTarget)
                    waitEvent.Set();

                return new ValueTask();
            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        //[Benchmark()]
        public async Task SocketUpdate()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _receiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }

        //[Benchmark()]
        public async Task Socket()
        {
            var waitEvent = new AsyncResetEvent(false, false);
            var received = 0;
            var result = await SocketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                received++;
                if (received == _receiveTarget)
                    waitEvent.Set();

            }, CancellationToken.None);

            await waitEvent.WaitAsync();
            await result.Data.CloseAsync();
        }


        [Benchmark()]
        public async Task Rest()
        {
            for(var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            }
        }

        [Benchmark()]
        public async Task RestUpdate()
        {
            for (var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            SocketClient.Dispose();
            RestClient.Dispose();
        }

        private void CreateClient(bool enableNewDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider(LogLevel.Information));
            //Logger = logger.CreateLogger("Test");
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:5034", "ws://localhost:5034", "", "", "", "", "", "", "", "");
            SocketClient = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                UseUpdatedDeserialization = enableNewDeserialization,
                RateLimiterEnabled = false,
                Environment = env
            }), logger);
            RestClient = new BinanceRestClient(null, logger, Options.Create(new BinanceRestOptions
            {
                UseUpdatedDeserialization = enableNewDeserialization,
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new SocketTests();
            //test.GlobalSetupNew();
            //Console.ReadLine();
            //Console.WriteLine("Starting");
            //for (var i = 0; i < 10; i++)
            //{
            //    test.RestUpdate().Wait();
            //}
            //Console.WriteLine("Finished");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkRunner.Run<SocketTests>();
        }
    }
}