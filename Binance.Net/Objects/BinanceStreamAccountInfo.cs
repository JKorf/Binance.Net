using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceStreamAccountInfo: BinanceStreamEvent
    {
        [JsonProperty("m")]
        public double MakerCommission { get; set; }
        [JsonProperty("t")]
        public double TakerCommission { get; set; }
        [JsonProperty("b")]
        public double BuyerCommission { get; set; }
        [JsonProperty("s")]
        public double SellerCommission { get; set; }
        [JsonProperty("T")]
        public bool CanTrade { get; set; }
        [JsonProperty("W")]
        public bool CanWithdraw { get; set; }
        [JsonProperty("D")]
        public bool CanDeposit { get; set; }
        [JsonProperty("B")]
        public List<BinanceStreamBalance> Balances { get; set; }
    }

    public class BinanceStreamBalance
    {
        [JsonProperty("a")]
        public string Asset { get; set; }
        [JsonProperty("f")]
        public double Free { get; set; }
        [JsonProperty("l")]
        public double Locked { get; set; }
    }
}
