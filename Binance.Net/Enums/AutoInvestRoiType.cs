using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Auto invest ROI type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestRoiType>))]
    public enum AutoInvestRoiType
    {
        /// <summary>
        /// ["<c>SEVEN_DAY</c>"] Seven days
        /// </summary>
        [Map("SEVEN_DAY")]
        SevenDay,
        /// <summary>
        /// ["<c>THREE_MONTH</c>"] Three months
        /// </summary>
        [Map("THREE_MONTH")]
        ThreeMonth,
        /// <summary>
        /// ["<c>SIX_MONTH</c>"] Six months
        /// </summary>
        [Map("SIX_MONTH")]
        SixMonth,
        /// <summary>
        /// ["<c>ONE_YEAR</c>"] One year
        /// </summary>
        [Map("ONE_YEAR")]
        OneYear,
        /// <summary>
        /// ["<c>THREE_YEAR</c>"] Three year
        /// </summary>
        [Map("THREE_YEAR")]
        ThreeYear,
        /// <summary>
        /// ["<c>FIVE_YEAR</c>"] Five year
        /// </summary>
        [Map("FIVE_YEAR")]
        FiveYear
    }
}

