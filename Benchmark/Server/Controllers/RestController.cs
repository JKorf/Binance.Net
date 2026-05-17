using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Binance.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("api/v3")]
    public class RestController : ControllerBase
    {
        [HttpGet("time")]
        public object Get()
        {
            Response.ContentType = "application/json";
            var response = new { serverTime = 1763802578 };
            return response;            
        }

        [HttpGet("exchangeInfo")]
        public object GetExchangeInfo()
        {
            Response.ContentType = "application/json";
            return new
            {
                timezone = "UTC",
                serverTime = 1763802578,
                rateLimits = Array.Empty<object>(),
                exchangeFilters = Array.Empty<object>(),
                symbols = new[]
                {
                    new
                    {
                        symbol = "ETHUSDT",
                        status = "TRADING",
                        baseAsset = "ETH",
                        baseAssetPrecision = 8,
                        quoteAsset = "USDT",
                        quotePrecision = 8,
                        quoteAssetPrecision = 8,
                        orderTypes = new[] { "LIMIT", "MARKET" },
                        icebergAllowed = true,
                        ocoAllowed = true,
                        otoAllowed = true,
                        quoteOrderQtyMarketAllowed = true,
                        allowTrailingStop = true,
                        cancelReplaceAllowed = true,
                        isSpotTradingAllowed = true,
                        isMarginTradingAllowed = true,
                        filters = Array.Empty<object>(),
                        permissions = new[] { "SPOT" },
                        permissionSets = new[] { new[] { "SPOT" } },
                        defaultSelfTradePreventionMode = "NONE",
                        allowedSelfTradePreventionModes = new[] { "NONE" }
                    }
                }
            };
        }

        [HttpGet("ticker/24hr")]
        public object GetTicker([FromQuery] string symbol)
        {
            Response.ContentType = "application/json";
            return new
            {
                symbol = symbol ?? "ETHUSDT",
                priceChange = "10.00000000",
                priceChangePercent = "0.500",
                weightedAvgPrice = "2005.00000000",
                prevClosePrice = "2000.00000000",
                lastPrice = "2010.00000000",
                lastQty = "0.25000000",
                bidPrice = "2009.90000000",
                bidQty = "1.00000000",
                askPrice = "2010.10000000",
                askQty = "1.00000000",
                openPrice = "2000.00000000",
                highPrice = "2020.00000000",
                lowPrice = "1990.00000000",
                volume = "12345.67800000",
                quoteVolume = "24765432.10000000",
                openTime = 1763800000000,
                closeTime = 1763803600000,
                firstId = 1000,
                lastId = 2000,
                count = 1001
            };
        }
    }
}
