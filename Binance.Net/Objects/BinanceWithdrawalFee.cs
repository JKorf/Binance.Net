using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceWithdrawalFee
    {
        public decimal WithdrawFee { get; set; }
        public bool Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
