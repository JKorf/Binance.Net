using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Customized fixed project position
    /// </summary>
    public record BinanceCustomizedFixedProjectPosition
    {
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Can transfer
        /// </summary>
        public bool CanTransfer { get; set; }
        /// <summary>
        /// Create timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTimestamp")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Lot
        /// </summary>
        public int Lot { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        public int PositionId { get; set; }
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// Time of purchase
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime PurchaseTime { get; set; }
        /// <summary>
        /// Redeem date
        /// </summary>
        public string RedeemDate { get; set; } = string.Empty;
        /// <summary>
        /// Start time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public ProjectStatus Status { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public ProjectType Type { get; set; }

    }
}
