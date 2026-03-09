using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple earn subscription type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubscriptionType>))]
    public enum SubscriptionType
    {
        /// <summary>
        /// ["<c>AUTO</c>"] Auto subscribe
        /// </summary>
        [Map("AUTO")]
        Auto,
        /// <summary>
        /// ["<c>NORMAL</c>"] Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// ["<c>CONVERT</c>"] Locked to flexible
        /// </summary>
        [Map("CONVERT")]
        Convert,
        /// <summary>
        /// ["<c>LOAN</c>"] Flexible loan
        /// </summary>
        [Map("LOAN")]
        Loan,
        /// <summary>
        /// ["<c>AI</c>"] Auto invest
        /// </summary>
        [Map("AI")]
        AutoInvest,
        /// <summary>
        /// ["<c>TRANSFER</c>"] Locked saving to flexible
        /// </summary>
        [Map("TRANSFER")]
        Transfer
    }
}

