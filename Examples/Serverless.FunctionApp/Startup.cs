﻿using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Serverless.FunctionApp.Startup))]
namespace Serverless.FunctionApp
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			// Binance Setup
			builder.Services.AddSingleton<IBinanceSocketClient>(x => new BinanceSocketClient(new BinanceSocketClientOptions
			{
				ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(Environment.GetEnvironmentVariable("BinanceApiKey"), Environment.GetEnvironmentVariable("BinanceSecretKey"))
			}));

			builder.Services.AddTransient<IBinanceClient>(x => new BinanceClient(new BinanceClientOptions
			{
				ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(Environment.GetEnvironmentVariable("BinanceApiKey"), Environment.GetEnvironmentVariable("BinanceSecretKey"))
			}));

			builder.Services.AddSingleton<IBinanceDataProvider, BinanceDataProvider>();

			// additional setup
			string insKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
			builder.Services.AddApplicationInsightsTelemetry(insKey);

		}
	}
}
