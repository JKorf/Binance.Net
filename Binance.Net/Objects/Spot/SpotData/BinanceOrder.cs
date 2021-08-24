using System;
using System.Globalization;
using Binance.Net.Enums;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;

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
        IExchangeClient.OrderStatus ICommonOrder.CommonStatus =>
            Status == OrderStatus.New || Status == OrderStatus.PartiallyFilled ? IExchangeClient.OrderStatus.Active :
            Status == OrderStatus.Filled ? IExchangeClient.OrderStatus.Filled :
            IExchangeClient.OrderStatus.Canceled;

        bool ICommonOrder.IsActive => Status == OrderStatus.New || Status == OrderStatus.PartiallyFilled;

        IExchangeClient.OrderSide ICommonOrder.CommonSide =>
            Side == OrderSide.Sell ? IExchangeClient.OrderSide.Sell : IExchangeClient.OrderSide.Buy;

        DateTime ICommonOrder.CommonOrderTime => CreateTime;

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
