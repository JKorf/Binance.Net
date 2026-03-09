using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Conditional order types
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ConditionalOrderType>))]
    public enum ConditionalOrderType
    {
        /// <summary>
        /// ["<c>STOP</c>"] Stop order. Execute a limit order when price reaches a specific Stop price
        /// </summary>
        [Map("STOP")]
        Stop,
        /// <summary>
        /// ["<c>STOP_MARKET</c>"] Stop market order. Execute a market order when price reaches a specific Stop price
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfit,
        /// <summary>
        /// ["<c>TAKE_PROFIT_MARKET</c>"] Take profit market order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
        /// <summary>
        /// ["<c>TRAILING_STOP_MARKET</c>"] A trailing stop order will execute an order when the price drops below a certain percentage from its all time high since the order was activated
        /// </summary>
        [Map("TRAILING_STOP_MARKET")]
        TrailingStopMarket
    }
}

