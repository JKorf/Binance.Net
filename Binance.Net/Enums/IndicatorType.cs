using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Types of indicators
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IndicatorType>))] public  enum IndicatorType
    {
        /// <summary>
        /// Unfilled ratio
        /// </summary>
        [Map("UFR")]
        UnfilledRatio,
        /// <summary>
        /// Expired orders ratio
        /// </summary>
        [Map("IFER")]
        ExpirationRatio,
        /// <summary>
        /// Canceled orders ratio
        /// </summary>
        [Map("GCR")]
        CancelationRatio
    }
}
