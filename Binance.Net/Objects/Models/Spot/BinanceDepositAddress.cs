namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    public record BinanceDepositAddress
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Address tag
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Asset the address is for
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
    }
}
