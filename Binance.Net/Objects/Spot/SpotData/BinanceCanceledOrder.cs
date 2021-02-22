using System.Globalization;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// Information about a canceled order
    /// </summary>
    public class BinanceCanceledOrder: BinanceOrderBase, ICommonOrderId
    {
        string ICommonOrderId.CommonId => OrderId.ToString(CultureInfo.InvariantCulture);
    }
}
