using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Result from a change margin type request
    /// </summary>
    public class BinanceFuturesChangeMarginTypeResult
    {
        /// <summary>
        /// Response code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Response message
        /// </summary>
        [JsonProperty("msg")]
        public string? Message { get; set; }
    }
}
