namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule VASP
    /// </summary>
    public record BinanceTravelRuleVasp
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("vaspName")]
        public string VaspName { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("vaspCode")]
        public string VaspCode { get; set; } = string.Empty;
    }
}
