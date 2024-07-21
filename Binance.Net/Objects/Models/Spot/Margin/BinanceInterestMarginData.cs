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
        [JsonPropertyName("vipLevel")]
        public string VipLevel { get; set; } = string.Empty;

        /// <summary>
        /// The coin
        /// </summary>        
        [JsonPropertyName("coin")]
        public string Coin { get; set; } = string.Empty;

        /// <summary>
        /// If coin can be transferred into cross
        /// </summary>
        [JsonPropertyName("transferIn")]
        public bool TransferIn { get; set; } = false;

        /// <summary>
        /// If coin can be borrowed in cross
        /// </summary>        
        [JsonPropertyName("borrowable")]
        public bool Borrowable { get; set; } = false;

        /// <summary>
        /// The daily interest
        /// </summary>
        [JsonPropertyName("dailyInterest")]
        public decimal DailyInterest { get; set; }

        /// <summary>
        /// The yearly interest
        /// </summary>
        [JsonPropertyName("yearlyInterest")]
        public decimal YearlyInterest { get; set; }

        /// <summary>
        /// The yearly interest
        /// </summary>
        [JsonPropertyName("borrowLimit")]
        public decimal BorrowLimit { get; set; }

        /// <summary>
        /// Cross marginable pairs for this coin
        /// </summary>
        [JsonPropertyName("marginablePairs")]
        public string[]? MarginablePairs { get; set; }

    }
}
