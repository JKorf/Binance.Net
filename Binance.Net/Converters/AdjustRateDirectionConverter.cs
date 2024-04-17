using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class AdjustRateDirectionConverter : BaseConverter<AdjustRateDirection>
    {
        public AdjustRateDirectionConverter() : this(true) { }
        public AdjustRateDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<AdjustRateDirection, string>> Mapping => new List<KeyValuePair<AdjustRateDirection, string>>
        {
            new KeyValuePair<AdjustRateDirection, string>(AdjustRateDirection.Reduced, "REDUCED"),
            new KeyValuePair<AdjustRateDirection, string>(AdjustRateDirection.Additional, "ADDITIONAL")
        };
    }
}
