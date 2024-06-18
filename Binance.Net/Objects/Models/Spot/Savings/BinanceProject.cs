namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Binance project info
    /// </summary>
    public record BinanceProject
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Display priority
        /// </summary>
        public int DisplayPriority { get; set; }
        /// <summary>
        /// Duration
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// Interest per lot
        /// </summary>
        public decimal InterestPerLot { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Lot size
        /// </summary>
        public decimal LotSize { get; set; }
        /// <summary>
        /// Lots low limit
        /// </summary>
        public int LotsLowLimit { get; set; }
        /// <summary>
        /// Lots purchased
        /// </summary>
        public int LotsPurchased { get; set; }
        /// <summary>
        /// Lots upper limit
        /// </summary>
        public int LotsUpLimit { get; set; }
        /// <summary>
        /// Max number of lots per user
        /// </summary>
        public int MaxLotsPerUser { get; set; }
        /// <summary>
        /// Needs know your customer
        /// </summary>
        public bool NeedKYC { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;

        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Has area limitation
        /// </summary>
        public bool WithAreaLimitation { get; set; }
    }
}
