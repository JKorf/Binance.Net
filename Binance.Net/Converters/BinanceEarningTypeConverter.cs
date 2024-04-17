using Binance.Net.Enums;

namespace Binance.Net.Converters
{
    internal class BinanceEarningTypeConverter : BaseConverter<BinanceEarningType>
    {
        public BinanceEarningTypeConverter() : this(true) { }
        public BinanceEarningTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BinanceEarningType, string>> Mapping => new List<KeyValuePair<BinanceEarningType, string>>
        {
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.MiningWallet, "0"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.MergedMining, "1"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.ActivityBonus, "2"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.Rebate, "3"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.SmartPool, "4"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.MiningAddress, "5"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.IncomeTransfer, "6"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.PoolSavings, "7"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.Transfered, "8"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.IncomeTransfer, "31"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.HashrateResaleMiningWallet, "32"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.HashrateResalePoolSavings, "33")
        };
    }
}
