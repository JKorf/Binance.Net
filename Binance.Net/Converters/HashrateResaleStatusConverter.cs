using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class HashrateResaleStatusConverter : BaseConverter<HashrateResaleStatus>
    {
        public HashrateResaleStatusConverter() : this(true) { }
        public HashrateResaleStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HashrateResaleStatus, string>> Mapping => new List<KeyValuePair<HashrateResaleStatus, string>>
        {
            new KeyValuePair<HashrateResaleStatus, string>(HashrateResaleStatus.Processing, "0"),
            new KeyValuePair<HashrateResaleStatus, string>(HashrateResaleStatus.Canceled, "1"),
            new KeyValuePair<HashrateResaleStatus, string>(HashrateResaleStatus.Terminated, "2"),
        };
    }
}
