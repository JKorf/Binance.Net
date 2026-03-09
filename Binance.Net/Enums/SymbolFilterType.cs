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
        /// ["<c>PRICE_FILTER</c>"] Price filter
        /// </summary>
        [Map("PRICE_FILTER")]
        Price,
        /// <summary>
        /// ["<c>PERCENT_PRICE</c>"] Price percent filter
        /// </summary>
        [Map("PERCENT_PRICE")]
        PricePercent,
        /// <summary>
        /// ["<c>LOT_SIZE</c>"] Lot size filter
        /// </summary>
        [Map("LOT_SIZE")]
        LotSize,
        /// <summary>
        /// ["<c>MARKET_LOT_SIZE</c>"] Market lot size filter
        /// </summary>
        [Map("MARKET_LOT_SIZE")]
        MarketLotSize,
        /// <summary>
        /// ["<c>MIN_NOTIONAL</c>"] Min notional filter
        /// </summary>
        [Map("MIN_NOTIONAL")]
        MinNotional,
        /// <summary>
        /// ["<c>MAX_NUM_ORDERS</c>"] Max orders filter
        /// </summary>
        [Map("MAX_NUM_ORDERS")]
        MaxNumberOrders,
        /// <summary>
        /// ["<c>MAX_NUM_ALGO_ORDERS</c>"] Max algo orders filter
        /// </summary>
        [Map("MAX_NUM_ALGO_ORDERS")]
        MaxNumberAlgorithmicOrders,
        /// <summary>
        /// ["<c>ICEBERG_PARTS</c>"] Max iceberg parts filter
        /// </summary>
        [Map("ICEBERG_PARTS")]
        IcebergParts,
        /// <summary>
        /// ["<c>MAX_POSITION</c>"] Max position filter
        /// </summary>
        [Map("MAX_POSITION")]
        MaxPosition,
        /// <summary>
        /// ["<c>PERCENT_PRICE_BY_SIDE</c>"] Price filter by side
        /// </summary>
        [Map("PERCENT_PRICE_BY_SIDE")]
        PercentagePriceBySide,
        /// <summary>
        /// ["<c>TRAILING_DELTA</c>"] Trailing delta filter
        /// </summary>,
        [Map("TRAILING_DELTA")]
        TrailingDelta,
        /// <summary>
        /// ["<c>NOTIONAL</c>"] Notional filter
        /// </summary>
        [Map("NOTIONAL")]
        Notional,
        /// <summary>
        /// ["<c>EXCHANGE_MAX_NUM_ICEBERG_ORDERS</c>"] Max Iceberg Orders filter
        /// </summary>
        [Map("EXCHANGE_MAX_NUM_ICEBERG_ORDERS")]
        IcebergOrders,
        /// <summary>
        /// ["<c>POSITION_RISK_CONTROL</c>"] Position Risk Control Filter
        /// </summary>
        [Map("POSITION_RISK_CONTROL")]
        PositionRiskControl,
        /// <summary>
        /// ["<c>MAX_NUM_ORDER_AMENDS</c>"] Max number of edits per order
        /// </summary>
        [Map("MAX_NUM_ORDER_AMENDS")]
        OrderAmends,
        /// <summary>
        /// ["<c>MAX_NUM_ORDER_LISTS</c>"] Max number of order lists
        /// </summary>
        [Map("MAX_NUM_ORDER_LISTS")]
        OrderLists
    }
}

