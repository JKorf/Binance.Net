using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Subscription cycle
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestSubscriptionCycle>))]
    public enum AutoInvestSubscriptionCycle
    {
        /// <summary>
        /// ["<c>H1</c>"] One hour
        /// </summary>
        [Map("H1")]
        OneHour,
        /// <summary>
        /// ["<c>H4</c>"] Four hour
        /// </summary>
        [Map("H4")]
        FourHour,
        /// <summary>
        /// ["<c>H8</c>"] Eight hour
        /// </summary>
        [Map("H8")]
        EightHour,
        /// <summary>
        /// ["<c>H12</c>"] Twelve hour
        /// </summary>
        [Map("H12")]
        TwelveHour,
        /// <summary>
        /// ["<c>DAILY</c>"] Daily
        /// </summary>
        [Map("DAILY")]
        Daily,
        /// <summary>
        /// ["<c>WEEKLY</c>"] Weekly
        /// </summary>
        [Map("WEEKLY")]
        Weekly,
        /// <summary>
        /// ["<c>BI_WEEKLY</c>"] Bi-Weekly
        /// </summary>
        [Map("BI_WEEKLY")]
        BiWeekly,
        /// <summary>
        /// ["<c>MONTHLY</c>"] Monthly
        /// </summary>
        [Map("MONTHLY")]
        Monthly
    }
}

