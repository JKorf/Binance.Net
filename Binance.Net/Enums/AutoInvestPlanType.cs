using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Plan type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestPlanType>))]
    public enum AutoInvestPlanType
    {
        /// <summary>
        /// ["<c>SINGLE</c>"] Single
        /// </summary>
        [Map("SINGLE")]
        Single,
        /// <summary>
        /// ["<c>INDEX</c>"] Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// ["<c>PORTFOLIO</c>"] Portfolio
        /// </summary>
        [Map("PORTFOLIO")]
        Portfolio,
        /// <summary>
        /// ["<c>ALL</c>"] All
        /// </summary>
        [Map("ALL")]
        All
    }
}

