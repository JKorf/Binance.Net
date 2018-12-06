using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class SymbolFilterTypeConverter : BaseConverter<SymbolFilterType>
    {
        public SymbolFilterTypeConverter(): this(true) { }
        public SymbolFilterTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<SymbolFilterType, string> Mapping => new Dictionary<SymbolFilterType, string>
        {
            { SymbolFilterType.LotSize, "LOT_SIZE" },
            { SymbolFilterType.MarketLotSize, "MARKET_LOT_SIZE" },
            { SymbolFilterType.MinNotional, "MIN_NOTIONAL" },
            { SymbolFilterType.Price, "PRICE_FILTER" },
            { SymbolFilterType.PricePercent, "PERCENT_PRICE" },
            { SymbolFilterType.IcebergParts, "ICEBERG_PARTS" },
            { SymbolFilterType.MaxNumberOrders, "MAX_NUM_ORDERS" },
            { SymbolFilterType.MaxNumberIcebergOrders, "MAX_NUM_ICEBERG_ORDERS" },
            { SymbolFilterType.MaxNumberAlgorithmicOrders, "MAX_NUM_ALGO_ORDERS" }
        };
    }
}
