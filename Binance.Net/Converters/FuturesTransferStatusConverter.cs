using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class FuturesTransferStatusConverter : BaseConverter<FuturesTransferStatus>
    {
        public FuturesTransferStatusConverter() : this(true) { }
        public FuturesTransferStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<FuturesTransferStatus, string>> Mapping => new List<KeyValuePair<FuturesTransferStatus, string>>
        {
            new KeyValuePair<FuturesTransferStatus, string>(FuturesTransferStatus.Pending, "PENDING"),
            new KeyValuePair<FuturesTransferStatus, string>(FuturesTransferStatus.Confirmed, "CONFIRMED"),
            new KeyValuePair<FuturesTransferStatus, string>(FuturesTransferStatus.Failed, "FAILED"),
        };
    }
}
