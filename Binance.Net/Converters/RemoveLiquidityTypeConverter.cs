using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class RemoveLiquidityTypeConverter : BaseConverter<RemoveLiquidityType>
    {
        public RemoveLiquidityTypeConverter() : this(true) { }
        public RemoveLiquidityTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<RemoveLiquidityType, string>> Mapping => new List<KeyValuePair<RemoveLiquidityType, string>>
        {
            new KeyValuePair<RemoveLiquidityType, string>(RemoveLiquidityType.Single, "SINGLE"),
            new KeyValuePair<RemoveLiquidityType, string>(RemoveLiquidityType.Combined, "COMBINATION")
        };
    }
}
