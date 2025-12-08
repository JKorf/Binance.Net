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
        /// Short
        /// </summary>
        [Map("SHORT")]
        Short,
        /// <summary>
        /// Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// Both for One-way mode when placing an order
        /// </summary>
        [Map("BOTH")]
        Both
    }
}
