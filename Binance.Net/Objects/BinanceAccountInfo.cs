using System.Collections.Generic;

namespace Binance.Net.Objects
{
    public class BinanceAccountInfo
    {
        public double MakerCommission { get; set; }
        public double TakerCommission { get; set; }
        public double BuyerCommission { get; set; }
        public double SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public List<BinanceBalance> Balances { get; set; }
    }

    public class BinanceBalance
    {
        public string Asset { get; set; }
        public double Free { get; set; }
        public double Locked { get; set; }
    }
}
