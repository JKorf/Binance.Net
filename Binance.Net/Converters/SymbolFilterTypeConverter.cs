using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class SymbolFilterTypeConverter : BaseConverter<SymbolFilterType>
    {
        public SymbolFilterTypeConverter(): this(true) { }
        public SymbolFilterTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<SymbolFilterType, string> Mapping => new Dictionary<SymbolFilterType, string>()
        {
            { SymbolFilterType.LotSize, "LOT_SIZE" },
            { SymbolFilterType.MinNotional, "MIN_NOTIONAL" },
            { SymbolFilterType.PriceFilter, "PRICE_FILTER" },
            { SymbolFilterType.IcebergParts, "ICEBERG_PARTS" },
            { SymbolFilterType.MaxNumberAlogitmicalOrders, "MAX_NUM_ALGO_ORDERS" },
        };
    }
}
