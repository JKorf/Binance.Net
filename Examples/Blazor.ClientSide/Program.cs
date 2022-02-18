using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Binance.Net;
using Binance.Net.Interfaces;
using Blazor.DataProvider;
using CryptoExchange.Net.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Binance.Net.Objects;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Clients;

namespace Blazor.ClientSide
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                LogLevel = LogLevel.Debug,
                HttpClient = new HttpClient()
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IBinanceClient, BinanceClient>();
            builder.Services.AddScoped<IBinanceSocketClient, BinanceSocketClient>();
            builder.Services.AddScoped<BinanceDataProvider>();

            await builder.Build().RunAsync();
        }
    }
}
