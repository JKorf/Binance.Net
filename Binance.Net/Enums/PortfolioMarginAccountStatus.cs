using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Account status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PortfolioMarginAccountStatus>))]
    public enum PortfolioMarginAccountStatus
    {
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
        /// ["<c>SUPPLY_MARGIN</c>"] Supply margin
        /// </summary>
        [Map("SUPPLY_MARGIN")]
        SupplyMargin,
        /// <summary>
        /// ["<c>REDUCE_ONLY</c>"] Reduce only
        /// </summary>
        [Map("REDUCE_ONLY")]
        ReduceOnly,
        /// <summary>
        /// ["<c>ACTIVE_LIQUIDATION</c>"] Active liquidation
        /// </summary>
        [Map("ACTIVE_LIQUIDATION")]
        ActiveLiquidation,
        /// <summary>
        /// ["<c>FORCE_LIQUIDATION</c>"] Force liquidation
        /// </summary>
        [Map("FORCE_LIQUIDATION")]
        ForceLiquidation,
        /// <summary>
        /// ["<c>BANKRUPTED</c>"] Bankrupted
        /// </summary>
        [Map("BANKRUPTED")]
        Bankrupted
    }
}

