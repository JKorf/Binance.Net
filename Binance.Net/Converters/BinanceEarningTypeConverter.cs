using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Converters
{
    internal class BinanceEarningTypeConverter : BaseConverter<BinanceEarningType>
    {
        public BinanceEarningTypeConverter() : this(true) { }
        public BinanceEarningTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BinanceEarningType, string>> Mapping => new List<KeyValuePair<BinanceEarningType, string>>
        {
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.MiningWallet, "0"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.MiningAddress, "5"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.PoolSavings, "7"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.Transfered, "8"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.IncomeTransfer, "31"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.HashrateResaleMiningWallet, "32"),
            new KeyValuePair<BinanceEarningType, string>(BinanceEarningType.HashrateResalePoolSavings, "33")
        };
    }
}
