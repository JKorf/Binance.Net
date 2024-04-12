using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class LiquidityTypeConverter : BaseConverter<LiquidityType>
    {
        public LiquidityTypeConverter() : this(true) { }
        public LiquidityTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<LiquidityType, string>> Mapping => new List<KeyValuePair<LiquidityType, string>>
        {
            new KeyValuePair<LiquidityType, string>(LiquidityType.Single, "SINGLE"),
            new KeyValuePair<LiquidityType, string>(LiquidityType.Combined, "COMBINATION")
        };
    }
}
