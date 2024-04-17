using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class LendingTypeConverter : BaseConverter<LendingType>
    {
        public LendingTypeConverter() : this(true) { }
        public LendingTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<LendingType, string>> Mapping => new List<KeyValuePair<LendingType, string>>
        {
            new KeyValuePair<LendingType, string>(LendingType.Activity, "ACTIVITY"),
            new KeyValuePair<LendingType, string>(LendingType.CustomizedFixed, "CUSTOMIZED_FIXED"),
            new KeyValuePair<LendingType, string>(LendingType.Daily, "DAILY")
        };
    }
}
