using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class ContractTypeConverter : BaseConverter<ContractType>
    {
        public ContractTypeConverter() : this(true) { }
        public ContractTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ContractType, string>> Mapping => new List<KeyValuePair<ContractType, string>>
        {
            new KeyValuePair<ContractType, string>(ContractType.Perpetual, "PERPETUAL"),
            new KeyValuePair<ContractType, string>(ContractType.CurrentMonth, "CURRENT_MONTH"),
            new KeyValuePair<ContractType, string>(ContractType.CurrentQuarter, "CURRENT_QUARTER"),
            new KeyValuePair<ContractType, string>(ContractType.CurrentQuarterDelivering, "CURRENT_QUARTER DELIVERING"),
            new KeyValuePair<ContractType, string>(ContractType.NextQuarter, "NEXT_QUARTER"),
            new KeyValuePair<ContractType, string>(ContractType.NextQuarterDelivering, "NEXT_QUARTER DELIVERING"),
            new KeyValuePair<ContractType, string>(ContractType.NextMonth, "NEXT_MONTH"),
            new KeyValuePair<ContractType, string>(ContractType.Unknown, ""),
        };
    }
}
