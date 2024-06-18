namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin interest data
    /// </summary>
    public record BinanceInterestMarginData
    {
        /// <summary>
        /// Vip level
        /// </summary>
        [JsonProperty("vipLevel")]
        public string VipLevel { get; set; } = string.Empty;

        /// <summary>
        /// The coin
        /// </summary>        
        [JsonProperty("coin")]
        public string Coin { get; set; } = string.Empty;

        /// <summary>
        /// If coin can be transferred into cross
        /// </summary>
        [JsonProperty("transferIn")]
        public bool TransferIn { get; set; } = false;

        /// <summary>
        /// If coin can be borrowed in cross
        /// </summary>        
        [JsonProperty("borrowable")]
        public bool Borrowable { get; set; } = false;

        /// <summary>
        /// The daily interest
        /// </summary>
        [JsonProperty("dailyInterest")]
        public decimal DailyInterest { get; set; }

        /// <summary>
        /// The yearly interest
        /// </summary>
        [JsonProperty("yearlyInterest")]
        public decimal YearlyInterest { get; set; }

        /// <summary>
        /// The yearly interest
        /// </summary>
        [JsonProperty("borrowLimit")]
        public decimal BorrowLimit { get; set; }

        /// <summary>
        /// Cross marginable pairs for this coin
        /// </summary>
        [JsonProperty("marginablePairs")]
        public string[]? MarginablePairs { get; set; }

    }
}
