﻿using System.Globalization;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;

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
