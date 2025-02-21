
using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Margin level status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginLevelStatus>))] public  enum MarginLevelStatus
    {
        /// <summary>
        /// Excessive
        /// </summary>
        [Map("EXCESSIVE")]
        Excessive,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// Margin call
        /// </summary>
        [Map("MARGIN_CALL")]
        MarginCall,
        /// <summary>
        /// Pre-liquidation
        /// </summary>
        [Map("PRE_LIQUIDATION")]
        PreLiquidation,
        /// <summary>
        /// Force liquidation
        /// </summary>
        [Map("FORCE_LIQUIDATION")]
        ForceLiquidation
    }
}
