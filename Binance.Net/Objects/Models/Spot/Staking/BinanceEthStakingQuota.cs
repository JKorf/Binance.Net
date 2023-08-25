using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking quota
    /// </summary>
    public class BinanceEthStakingQuota
    {
        /// <summary>
        /// Staking quota left
        /// </summary>
        [JsonProperty("leftStakingPersonalQuota")]
        public decimal LeftStakingPersonalQuota { get; set; }
        /// <summary>
        /// Redemption quota left
        /// </summary>
        [JsonProperty("leftRedemptionPersonalQuota")]
        public decimal LeftRedemptionPersonalQuota { get; set; }
    }
}
