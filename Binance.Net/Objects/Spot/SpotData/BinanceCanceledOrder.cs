using System.Globalization;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// Information about a canceled order
    /// </summary>
    public class BinanceCanceledOrder: BinanceOrderBase, ICommonOrderId
    {
        string ICommonOrderId.CommonId => Id.ToString(CultureInfo.InvariantCulture);
        /// <summary>
        /// If isolated margin
        /// </summary>
        public bool? IsIsolated { get; set; }
    }
}
