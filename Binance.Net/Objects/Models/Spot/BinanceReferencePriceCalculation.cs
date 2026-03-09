using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Price calculation
    /// </summary>
    public record BinanceReferencePriceCalculation
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>calculationType</c>"] Calculation type
        /// </summary>
        [JsonPropertyName("calculationType")]
        public PriceRuleCalcType CalculationType { get; set; }
        /// <summary>
        /// ["<c>bucketCount</c>"] Bucket count
        /// </summary>
        [JsonPropertyName("bucketCount")]
        public long? BucketCount { get; set; }
        /// <summary>
        /// ["<c>bucketWidthMs</c>"] Bucket width in milliseconds
        /// </summary>
        [JsonPropertyName("bucketWidthMs")]
        public long? BucketWidth { get; set; }
        /// <summary>
        /// ["<c>externalCalculationId</c>"] External calculation id
        /// </summary>
        [JsonPropertyName("externalCalculationId")]
        public long? ExternalCalculationId { get; set; }
    }


}
