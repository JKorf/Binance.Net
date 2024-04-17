using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class SubAccountTransferSubAccountTypeConverter : BaseConverter<SubAccountTransferSubAccountType>
    {
        public SubAccountTransferSubAccountTypeConverter() : this(true)
        {
        }

        public SubAccountTransferSubAccountTypeConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<SubAccountTransferSubAccountType, string>> Mapping =>
            new List<KeyValuePair<SubAccountTransferSubAccountType, string>>
            {
                new KeyValuePair<SubAccountTransferSubAccountType, string>(
                    SubAccountTransferSubAccountType.TransferIn, "1"),
                new KeyValuePair<SubAccountTransferSubAccountType, string>(
                    SubAccountTransferSubAccountType.TransferOut, "2"),
            };
    }
}