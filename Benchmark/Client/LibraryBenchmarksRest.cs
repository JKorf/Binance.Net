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
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    public class LibraryBenchmarksRest
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
            var env = BinanceEnvironment.CreateCustom("Benchmark", "http://localhost:" + Program.ServerPort, "ws://localhost:" + Program.ServerPort, "", "", "", "", "", "", "", "");
            RestClient = new BinanceRestClient(null, null, Options.Create(new BinanceRestOptions
            {
                RateLimiterEnabled = false,
                Environment = env
            }));
        }
    }
}
