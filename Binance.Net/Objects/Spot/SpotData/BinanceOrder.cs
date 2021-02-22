using System;
using System.Globalization;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// Information regarding a specific order
    /// </summary>
    public class BinanceOrder: BinanceOrderBase, ICommonOrder
    {
        string ICommonOrderId.CommonId => OrderId.ToString(CultureInfo.InvariantCulture);
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price;
        decimal ICommonOrder.CommonQuantity => Quantity;
        string ICommonOrder.CommonStatus => Status.ToString();
        bool ICommonOrder.IsActive => Status == OrderStatus.New || Status == OrderStatus.PartiallyFilled;

        IExchangeClient.OrderSide ICommonOrder.CommonSide =>
            Side == OrderSide.Sell ? IExchangeClient.OrderSide.Sell : IExchangeClient.OrderSide.Buy;

        IExchangeClient.OrderType ICommonOrder.CommonType
        {
            get
            {
                if (Type == OrderType.Limit)
                    return IExchangeClient.OrderType.Limit;
                if (Type == OrderType.Market)
                    return IExchangeClient.OrderType.Market;
                return IExchangeClient.OrderType.Other;
            }
        }
    }
}
