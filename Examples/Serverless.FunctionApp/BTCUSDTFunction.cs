using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Binance.Net.Interfaces;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights;
using System.Web.Http;
using System.Threading;

namespace Serverless.FunctionApp
{
	public class BTCUSDTFunction
	{
		private readonly IBinanceClient _binanceClient;
		private readonly IBinanceDataProvider _dataProvider;
		private readonly TelemetryClient _telemetryClient;

		public BTCUSDTFunction(IBinanceClient client, IBinanceDataProvider dataProvider, TelemetryClient telemetryClient)
		{
			_binanceClient = client;
			_dataProvider = dataProvider;
			_telemetryClient = telemetryClient;
		}

		[FunctionName(nameof(BTCUSDTFunction))]
		public async Task<IActionResult> Get(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
			ILogger log)
		{
			decimal price;
			try
			{
				price = GetPrice();
			}
			catch (Exception ex)
			{
				_telemetryClient.TrackException(ex);
				return new BadRequestObjectResult(ex.Message);
			}

			return new JsonResult(price);
		}

		private decimal GetPrice()
		{
			var price = _dataProvider.LastKline?.Data.Close;
			if (price == null || price == 0 )
			{
				Thread.Sleep(TimeSpan.FromSeconds(3));
				price = GetPrice();
			}
			return price ?? 0;
		}
	}
}
