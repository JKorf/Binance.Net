using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking account
    /// </summary>
    public class BinanceEthStakingAccount
    {
        /// <summary>
        /// Total profit in Beth
        /// </summary>
        [JsonProperty("cumulativeProfitInBETH")]
        public decimal TotalProfitInBeth { get; set; }
        /// <summary>
        /// Last day profit in Beth
        /// </summary>
        [JsonProperty("lastDayProfitInBETH")]
        public decimal LastDayProfitInBeth { get; set; }
    }
}
