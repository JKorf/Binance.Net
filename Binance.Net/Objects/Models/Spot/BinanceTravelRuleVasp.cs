namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule VASP
    /// </summary>
    public record BinanceTravelRuleVasp
    {
        /// <summary>
        /// ["<c>vaspName</c>"] The VASP name.
        /// </summary>
        [JsonPropertyName("vaspName")]
        public string VaspName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>vaspCode</c>"] The VASP code.
        /// </summary>
        [JsonPropertyName("vaspCode")]
        public string VaspCode { get; set; } = string.Empty;
    }
}

