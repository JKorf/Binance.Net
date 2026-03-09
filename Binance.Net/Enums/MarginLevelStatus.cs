using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Margin level status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginLevelStatus>))]
    public enum MarginLevelStatus
    {
        /// <summary>
        /// ["<c>EXCESSIVE</c>"] Excessive
        /// </summary>
        [Map("EXCESSIVE")]
        Excessive,
        /// <summary>
        /// ["<c>NORMAL</c>"] Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// ["<c>MARGIN_CALL</c>"] Margin call
        /// </summary>
        [Map("MARGIN_CALL")]
        MarginCall,
        /// <summary>
        /// ["<c>PRE_LIQUIDATION</c>"] Pre-liquidation
        /// </summary>
        [Map("PRE_LIQUIDATION")]
        PreLiquidation,
        /// <summary>
        /// ["<c>FORCE_LIQUIDATION</c>"] Force liquidation
        /// </summary>
        [Map("FORCE_LIQUIDATION")]
        ForceLiquidation
    }
}

