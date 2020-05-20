using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class SubAccountTransferTypeConverter : BaseConverter<SubAccountTransferType>
    {
        public SubAccountTransferTypeConverter() : this(true)
        {
        }

        public SubAccountTransferTypeConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<SubAccountTransferType, string>> Mapping =>
            new List<KeyValuePair<SubAccountTransferType, string>>
            {
                new KeyValuePair<SubAccountTransferType, string>(
                    SubAccountTransferType.FromSpotMain, "1"),
                new KeyValuePair<SubAccountTransferType, string>(
                    SubAccountTransferType.ToSpotMain, "2"),
            };
    }
}