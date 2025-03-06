namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountAsset
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = true;
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("balances")]
        public BinanceBalance[] Balances { get; set; } = Array.Empty<BinanceBalance>();
    }
}
