using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionSide>))]
    public enum PositionSide
    {
        /// <summary>
        /// ["<c>SHORT</c>"] Short
        /// </summary>
        [Map("SHORT")]
        Short,
        /// <summary>
        /// ["<c>LONG</c>"] Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// ["<c>BOTH</c>"] Both for One-way mode when placing an order
        /// </summary>
        [Map("BOTH")]
        Both
    }
}

