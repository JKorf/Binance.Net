namespace Binance.Net.Enums
{
    /// <summary>
    /// Filter type
    /// </summary>
    public enum SymbolFilterType
    {
        /// <summary>
        /// Unknown filter type
        /// </summary>
        Unknown,
        /// <summary>
        /// Price filter
        /// </summary>
        Price,
        /// <summary>
        /// Price percent filter
        /// </summary>
        PricePercent,
        /// <summary>
        /// Lot size filter
        /// </summary>
        LotSize,
        /// <summary>
        /// Market lot size filter
        /// </summary>
        MarketLotSize,
        /// <summary>
        /// Min notional filter
        /// </summary>
        MinNotional,
        /// <summary>
        /// Max orders filter
        /// </summary>
        MaxNumberOrders,
        /// <summary>
        /// Max algo orders filter
        /// </summary>
        MaxNumberAlgorithmicOrders,
        /// <summary>
        /// Max iceberg parts filter
        /// </summary>
        IcebergParts,
    }
}
