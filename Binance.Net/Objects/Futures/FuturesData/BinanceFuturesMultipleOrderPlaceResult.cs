using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    internal class BinanceFuturesMultipleOrderPlaceResult: BinanceFuturesPlacedOrder
    {
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = "";
    }
}
