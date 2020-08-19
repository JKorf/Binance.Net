
namespace Binance.Net.Enums
{
    /// <summary>
    /// Margin level status
    /// </summary>
    public enum MarginLevelStatus
    {
        /// <summary>
        /// Excessive
        /// </summary>
        Excessive,
        /// <summary>
        /// Normal
        /// </summary>
        Normal,
        /// <summary>
        /// Margin call
        /// </summary>
        MarginCall,
        /// <summary>
        /// Pre-liquidation
        /// </summary>
        PreLiquidation,
        /// <summary>
        /// Force liquidation
        /// </summary>
        ForceLiquidation
    }
}
