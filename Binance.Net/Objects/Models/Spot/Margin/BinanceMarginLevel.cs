namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Personal margin level information
    /// </summary>
    public record BinanceMarginLevel
    {
        /// <summary>
        /// The level at which your margin level is considered normal.
        /// </summary>
        [JsonPropertyName("normalBar")]
        public double NormalLevel { get; set; } = 0.0;
        /// <summary>
        /// The level at which you will be margin called (asked to deposit more funds)
        /// </summary>
        [JsonPropertyName("marginCallBar")]
        public double MarginCallLevel { get; set; } = 0.0;
        /// <summary>
        /// The level at which your positions will be liquidated until your account balances
        /// </summary>
        [JsonPropertyName("forceLiquidationBar")]
        public double ForcedLiquidationLevel { get; set; } = 0.0;
    }
}
