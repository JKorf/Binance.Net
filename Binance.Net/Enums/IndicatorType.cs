using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Types of indicators
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IndicatorType>))]
    public enum IndicatorType
    {
        /// <summary>
        /// ["<c>UFR</c>"] Unfilled ratio
        /// </summary>
        [Map("UFR")]
        UnfilledRatio,
        /// <summary>
        /// ["<c>IFER</c>"] Expired orders ratio
        /// </summary>
        [Map("IFER")]
        ExpirationRatio,
        /// <summary>
        /// ["<c>GCR</c>"] Canceled orders ratio
        /// </summary>
        [Map("GCR")]
        CancelationRatio
    }
}

