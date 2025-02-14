using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Sub account margin transfer type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<SubAccountMarginTransferType>))] public  enum SubAccountMarginTransferType
    {
        /// <summary>
        /// Sub account spot to sub account margin
        /// </summary>
        [Map("1")]
        FromSubAccountSpotToSubAccountMargin,
        /// <summary>
        /// From sub account margin to sub account spot
        /// </summary>
        [Map("2")]
        FromSubAccountMarginToSubAccountSpot
    }
}
