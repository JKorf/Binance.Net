using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of contract
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// ["<c>PERPETUAL</c>"] Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// ["<c>PERPETUAL DELIVERING</c>"] Perpetual delivering
        /// </summary>
        [Map("PERPETUAL DELIVERING")]
        PerpetualDelivering,
        /// <summary>
        /// ["<c>CURRENT_MONTH</c>"] Current month
        /// </summary>
        [Map("CURRENT_MONTH")]
        CurrentMonth,
        /// <summary>
        /// ["<c>CURRENT_QUARTER</c>"] Current quarter
        /// </summary>
        [Map("CURRENT_QUARTER")]
        CurrentQuarter,
        /// <summary>
        /// ["<c>CURRENT_QUARTER DELIVERING</c>"] Current quarter delivering
        /// </summary>
        [Map("CURRENT_QUARTER DELIVERING")]
        CurrentQuarterDelivering,
        /// <summary>
        /// ["<c>NEXT_QUARTER</c>"] Next quarter
        /// </summary>
        [Map("NEXT_QUARTER")]
        NextQuarter,
        /// <summary>
        /// ["<c>NEXT_QUARTER DELIVERING</c>"] Next quarter delivering
        /// </summary>
        [Map("NEXT_QUARTER DELIVERING")]
        NextQuarterDelivering,
        /// <summary>
        /// ["<c>NEXT_MONTH</c>"] Next month
        /// </summary>
        [Map("NEXT_MONTH")]
        NextMonth,
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("")]
        Unknown,
        /// <summary>
        /// ["<c>TRADIFI_PERPETUAL</c>"] Traditional finance perp contract
        /// </summary>
        [Map("TRADIFI_PERPETUAL")]
        PerpetualTradFi
    }
}

