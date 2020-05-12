using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class SymbolFilterTypeConverter : BaseConverter<SymbolFilterType>
    {
        public SymbolFilterTypeConverter(): this(true) { }
        public SymbolFilterTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SymbolFilterType, string>> Mapping => new List<KeyValuePair<SymbolFilterType, string>>
        {
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.LotSize, "LOT_SIZE"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.MarketLotSize, "MARKET_LOT_SIZE"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.MinNotional, "MIN_NOTIONAL"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.Price, "PRICE_FILTER"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.PricePercent, "PERCENT_PRICE"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.IcebergParts, "ICEBERG_PARTS"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.MaxNumberOrders, "MAX_NUM_ORDERS"),
            new KeyValuePair<SymbolFilterType, string>(SymbolFilterType.MaxNumberAlgorithmicOrders, "MAX_NUM_ALGO_ORDERS"),
        };
    }
}
