namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Binance project info
    /// </summary>
    public class BinanceProject
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";
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
        public bool NeedsKYC { get; set; }
        /// <summary>
        /// Project id
        /// </summary>
        public string ProjectId { get; set; } = "";

        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; } = "";
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = "";
        /// <summary>
        /// Has area limitation
        /// </summary>
        public bool WithAreaLimitation { get; set; }
    }
}
