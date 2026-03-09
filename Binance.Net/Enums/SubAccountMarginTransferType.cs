using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Sub account margin transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountMarginTransferType>))]
    public enum SubAccountMarginTransferType
    {
        /// <summary>
        /// ["<c>1</c>"] Sub account spot to sub account margin
        /// </summary>
        [Map("1")]
        FromSubAccountSpotToSubAccountMargin,
        /// <summary>
        /// ["<c>2</c>"] From sub account margin to sub account spot
        /// </summary>
        [Map("2")]
        FromSubAccountMarginToSubAccountSpot
    }
}

