using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Binance.Api;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ccxt;

namespace Binance.Net.Benchmark.Client
{
    public class Program
    {
        public static int ServerPort = 5000;

        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new LibraryBenchmarksSocket();
            //test.Setup();
            //Console.ReadLine();
            //Console.WriteLine("Starting");
            //var sw = Stopwatch.StartNew();
            //for (var i = 0; i < 1; i++)
            //{
            //    test.Socket1Topic().Wait();
            //}
            //sw.Stop();
            //Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
