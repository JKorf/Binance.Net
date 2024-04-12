using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class FiatPaymentStatusConverter : BaseConverter<FiatPaymentStatus>
    {
        public FiatPaymentStatusConverter() : this(true) { }
        public FiatPaymentStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FiatPaymentStatus, string>> Mapping => new List<KeyValuePair<FiatPaymentStatus, string>>
        {
            new KeyValuePair<FiatPaymentStatus, string>(FiatPaymentStatus.Processing, "Processing"),
            new KeyValuePair<FiatPaymentStatus, string>(FiatPaymentStatus.Completed, "Completed"),
            new KeyValuePair<FiatPaymentStatus, string>(FiatPaymentStatus.Failed, "Failed"),
            new KeyValuePair<FiatPaymentStatus, string>(FiatPaymentStatus.Refunded, "Refunded"),
        };
    }
}
