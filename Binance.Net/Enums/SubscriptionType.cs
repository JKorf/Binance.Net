using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple earn subscription type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<SubscriptionType>))] public  enum SubscriptionType
    {
        /// <summary>
        /// Auto subscribe
        /// </summary>
        [Map("AUTO")]
        Auto,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// Locked to flexible
        /// </summary>
        [Map("CONVERT")]
        Convert,
        /// <summary>
        /// Flexible loan
        /// </summary>
        [Map("LOAN")]
        Loan,
        /// <summary>
        /// Auto invest
        /// </summary>
        [Map("AI")]
        AutoInvest,
        /// <summary>
        /// Locked saving to flexible
        /// </summary>
        [Map("TRANSFER")]
        Transfer
    }
}
