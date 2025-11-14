using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Security.Cryptography;
using System.Text;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
    //[ThreadingDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class SocketTests
    {
        private readonly BinanceSocketClient client = GetClient();

        [Benchmark()]
        public async Task HighPerf()
        {
            //var client = GetClient();
            var waitEvent = new AsyncResetEvent(false, false);
            var result = await client.SpotApi.ExchangeData.SubscribeToTradeUpdatesPerfAsync(["ETHUSDT"], x =>
            {
                //Debug.WriteLine($"{x}");
                return new ValueTask();
            }, CancellationToken.None);

            result.Data.ConnectionClosed += () => waitEvent.Set();
            await waitEvent.WaitAsync();
            //client.Dispose();
        }

        [Benchmark()]
        public async Task Normal()
        {
            //var client = GetClient();
            var waitEvent = new AsyncResetEvent(false, false);
            var result = await client.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync(["ETHUSDT"], x =>
            {
                //Debug.WriteLine($"{++i}: {x}");
            }, CancellationToken.None);

            result.Data.ConnectionClosed += () => waitEvent.Set();
            await waitEvent.WaitAsync();
            //client.Dispose();
        }

        private static BinanceSocketClient GetClient()
        {
            var logger = new LoggerFactory();
            //logger.AddProvider(new TraceLoggerProvider());
            var client = new BinanceSocketClient(Options.Create(new BinanceSocketOptions
            {
                ReconnectPolicy = ReconnectPolicy.Disabled,
                RateLimiterEnabled = false,
                Environment = BinanceEnvironment.CreateCustom("Benchmark", "", "ws://localhost:5034", "", "", "", "", "", "", "", "")
            }), logger);
            return client;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //var test = new SocketTests();
            //for (var i = 0; i < 128; i++)
            //{
            //    test.HighPerf().Wait();
            //}
            //Console.ReadLine();

            var summary = BenchmarkRunner.Run<SocketTests>();
        }
    }
}