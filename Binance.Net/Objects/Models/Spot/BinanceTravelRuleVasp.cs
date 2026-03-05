namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule VASP
    /// </summary>
    public record BinanceTravelRuleVasp
    {
        /// <summary>
        /// The VASP name.
        /// </summary>
        [JsonPropertyName("vaspName")]
        public string VaspName { get; set; } = string.Empty;
        /// <summary>
        /// The VASP code.
        /// </summary>
        [JsonPropertyName("vaspCode")]
        public string VaspCode { get; set; } = string.Empty;
    }
}
