using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Filter type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolFilterType>))]
    [SerializationModel]
    public enum SymbolFilterType
    {
        /// <summary>
        /// Unknown filter type
        /// </summary>
        [Map("")]
        Unknown,
        /// <summary>
        /// Price filter
        /// </summary>
        [Map("PRICE_FILTER")]
        Price,
        /// <summary>
        /// Price percent filter
        /// </summary>
        [Map("PERCENT_PRICE")]
        PricePercent,
        /// <summary>
        /// Lot size filter
        /// </summary>
        [Map("LOT_SIZE")]
        LotSize,
        /// <summary>
        /// Market lot size filter
        /// </summary>
        [Map("MARKET_LOT_SIZE")]
        MarketLotSize,
        /// <summary>
        /// Min notional filter
        /// </summary>
        [Map("MIN_NOTIONAL")]
        MinNotional,
        /// <summary>
        /// Max orders filter
        /// </summary>
        [Map("MAX_NUM_ORDERS")]
        MaxNumberOrders,
        /// <summary>
        /// Max algo orders filter
        /// </summary>
        [Map("MAX_NUM_ALGO_ORDERS")]
        MaxNumberAlgorithmicOrders,
        /// <summary>
        /// Max iceberg parts filter
        /// </summary>
        [Map("ICEBERG_PARTS")]
        IcebergParts,
        /// <summary>
        /// Max position filter
        /// </summary>
        [Map("MAX_POSITION")]
        MaxPosition,
        /// <summary>
        /// Price filter by side
        /// </summary>
        [Map("PERCENT_PRICE_BY_SIDE")]
        PercentagePriceBySide,
        /// <summary>
        /// Trailing delta filter
        /// </summary>,
        [Map("TRAILING_DELTA")]
        TrailingDelta,
        /// <summary>
        /// Notional filter
        /// </summary>
        [Map("NOTIONAL")]
        Notional,
        /// <summary>
        /// Max Iceberg Orders filter
        /// </summary>
        [Map("EXCHANGE_MAX_NUM_ICEBERG_ORDERS")]
        IcebergOrders,
        /// <summary>
        /// Position Risk Control Filter
        /// </summary>
        [Map("POSITION_RISK_CONTROL")]
        PositionRiskControl,
        /// <summary>
        /// Max number of edits per order
        /// </summary>
        [Map("MAX_NUM_ORDER_AMENDS")]
        OrderAmends,
        /// <summary>
        /// Max number of order lists
        /// </summary>
        [Map("MAX_NUM_ORDER_LISTS")]
        OrderLists
    }
}
