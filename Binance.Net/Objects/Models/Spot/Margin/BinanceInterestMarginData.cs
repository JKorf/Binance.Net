namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin interest data
    /// </summary>
    [SerializationModel]
    public record BinanceInterestMarginData
    {
        /// <summary>
        /// ["<c>vipLevel</c>"] Vip level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }

        /// <summary>
        /// ["<c>coin</c>"] The coin
        /// </summary>        
        [JsonPropertyName("coin")]
        public string Coin { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>transferIn</c>"] If coin can be transferred into cross
        /// </summary>
        [JsonPropertyName("transferIn")]
        public bool TransferIn { get; set; } = false;

        /// <summary>
        /// ["<c>borrowable</c>"] If coin can be borrowed in cross
        /// </summary>        
        [JsonPropertyName("borrowable")]
        public bool Borrowable { get; set; } = false;

        /// <summary>
        /// ["<c>dailyInterest</c>"] The daily interest
        /// </summary>
        [JsonPropertyName("dailyInterest")]
        public decimal DailyInterest { get; set; }

        /// <summary>
        /// ["<c>yearlyInterest</c>"] The yearly interest
        /// </summary>
        [JsonPropertyName("yearlyInterest")]
        public decimal YearlyInterest { get; set; }

        /// <summary>
        /// ["<c>borrowLimit</c>"] The yearly interest
        /// </summary>
        [JsonPropertyName("borrowLimit")]
        public decimal BorrowLimit { get; set; }

        /// <summary>
        /// ["<c>marginablePairs</c>"] Cross marginable pairs for this coin
        /// </summary>
        [JsonPropertyName("marginablePairs")]
        public string[]? MarginablePairs { get; set; }

    }
}

