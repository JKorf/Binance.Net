using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class RedeemTypeConverter : BaseConverter<RedeemType>
    {
        public RedeemTypeConverter() : this(true) { }
        public RedeemTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<RedeemType, string>> Mapping => new List<KeyValuePair<RedeemType, string>>
        {
            new KeyValuePair<RedeemType, string>(RedeemType.Fast, "FAST"),
            new KeyValuePair<RedeemType, string>(RedeemType.Normal, "NORMAL")
        };
    }
}
