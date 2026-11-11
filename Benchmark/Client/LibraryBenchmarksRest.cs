using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Benchmark.Client
{
    [MemoryDiagnoser]
#if !VERSION_COMPARISON
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net11_0)]
#endif
    public class LibraryBenchmarksRest
    {
        public BinanceRestClient RestClient;

        [GlobalSetup(Targets = [nameof(RestUpdated)])]
        public void SetupUpdatedDeserialization()
        {
#if VERSION_COMPARISON
            Program.VerifyLibraries();
#endif
            CreateClient();
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public async Task RestUpdated()
        {
            for (var i = 0; i < 1000; i++)
            {
                var result = await RestClient.SpotApi.ExchangeData.GetServerTimeAsync();
                if (!result.Success)
                    throw new InvalidOperationException(result.Error.ToString());
            }
        }

        [GlobalCleanup]
        public void Cleanup() => RestClient.Dispose();

        private void CreateClient()
        {
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:" + Program.ServerPort, "ws://localhost:" + Program.ServerPort, "", "", "", "", "", "", "", "");
            RestClient = new BinanceRestClient(null, null, Options.Create(new BinanceRestOptions
            {
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }
}
