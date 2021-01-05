namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of an order
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit orders will be placed at a specific price. If the price isn't available in the order book for that asset the order will be added in the order book for someone to fill.
        /// </summary>
        Limit,
        /// <summary>
        /// Market order will be placed without a price. The order will be executed at the best price available at that time in the order book.
        /// </summary>
        Market,
        /// <summary>
        /// Stop loss order. Will execute a market order when the price drops below a price to sell and therefor limit the loss
        /// </summary>
        StopLoss,
        /// <summary>
        /// Stop loss order. Will execute a limit order when the price drops below a price to sell and therefor limit the loss
        /// </summary>
        StopLossLimit,
        /// <summary>
        /// Stop loss order. Will execute a market order when the price drops below a price to sell and therefor limit the loss
        /// </summary>
        Stop,
        /// <summary>
        /// Stop loss order. Will be executed at the best price available at that time in the order book
        /// </summary>
        StopMarket,
        /// <summary>
        /// Take profit order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfit,
        /// <summary>
        /// Take profit order. Will be executed at the best price available at that time in the order book
        /// </summary>
        TakeProfitMarket,
        /// <summary>
        /// Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfitLimit,
        /// <summary>
        /// Same as a limit order, however it will fail if the order would immediately match, therefor preventing taker orders
        /// </summary>
        LimitMaker,
        /// <summary>
        /// Trailing stop order will be placed without a price. The order will be executed at the best price available at that time in the order book.
        /// </summary>
        TrailingStopMarket,
        /// <summary>
        /// 
        /// </summary>
        Liquidation
    }
}
