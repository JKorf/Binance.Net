using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class IncomeTypeConverter: BaseConverter<IncomeType>
    {
        public IncomeTypeConverter(): this(true) { }
        public IncomeTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<IncomeType, string>> Mapping => new List<KeyValuePair<IncomeType, string>>
        {
            new KeyValuePair<IncomeType, string>(IncomeType.Transfer, "TRANSFER"),
            new KeyValuePair<IncomeType, string>(IncomeType.WelcomeBonus, "WELCOME_BONUS"),
            new KeyValuePair<IncomeType, string>(IncomeType.RealizedPnL, "REALIZED_PNL"),
            new KeyValuePair<IncomeType, string>(IncomeType.FundingFee, "FUNDING_FEE"),
            new KeyValuePair<IncomeType, string>(IncomeType.Commission, "COMMISSION"),
            new KeyValuePair<IncomeType, string>(IncomeType.InsuranceClear, "INSURANCE_CLEAR"),
        };
    }
}
