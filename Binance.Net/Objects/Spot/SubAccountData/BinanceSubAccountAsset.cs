using System.Collections.Generic;
using Binance.Net.Objects.Spot.SpotData;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    internal class BinanceSubAccountAsset
    {
        public List<BinanceBalance> Balances { get; set; } = new List<BinanceBalance>();
    }
}
