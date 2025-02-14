using Binance.Net.Converters;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of rebate
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<RebateType>))] public  enum RebateType
    {
        /// <summary>
        /// Commission rebate
        /// </summary>
        CommissionRebate = 1,
        /// <summary>
        /// Referral kickback
        /// </summary>
        ReferralKickback = 2
    }
}
