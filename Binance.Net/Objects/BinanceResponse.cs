using System;
using System.Collections.Generic;
using Binance.Net.Objects.Models.Spot;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Binance response
    /// </summary>
    public class BinanceResponse
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Result status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Error info
        /// </summary>
        public BinanceResponseError? Error { get; set; }

        /// <summary>
        /// Rate limit info
        /// </summary>
        public IEnumerable<BinanceCurrentRateLimit> Ratelimits { get; set; } = new List<BinanceCurrentRateLimit>();

    }

    /// <summary>
    /// Binance response
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public class BinanceResponse<T> : BinanceResponse
    {
        /// <summary>
        /// Data result
        /// </summary>
        public T Result { get; set; } = default!;
    }

    /// <summary>
    /// Binance error response
    /// </summary>
    public class BinanceResponseError
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Error data
        /// </summary>
        [JsonProperty("data")]
        public BinanceResponseErrorData? Data { get; set; }
    }

    /// <summary>
    /// Error data
    /// </summary>
    public class BinanceResponseErrorData 
    {
        /// <summary>
        /// Server time
        /// </summary>
        [JsonProperty("serverTime")]
        public DateTime? ServerTime { get; set; }
        /// <summary>
        /// Retry after time
        /// </summary>
        [JsonProperty("retryAfter")]
        public DateTime? RetryAfter { get; set; }
    }
}
