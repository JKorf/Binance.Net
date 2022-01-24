namespace Binance.Net.Enums
{
    public enum SpotOrderType
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
        /// Take profit order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfit,
        /// <summary>
        /// Take profit limit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfitLimit,
        /// <summary>
        /// Same as a limit order, however it will fail if the order would immediately match, therefor preventing taker orders
        /// </summary>
        LimitMaker
    }

    public enum FuturesOrderType
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
        /// Stop order. Execute a limit order when price reaches a specific Stop price
        /// </summary>
        Stop,
        /// <summary>
        /// Stop market order. Execute a market order when price reaches a specific Stop price
        /// </summary>
        StopMarket,
        /// <summary>
        /// Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfit,
        /// <summary>
        /// Take profit market order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfitMarket,
        /// <summary>
        /// A trailing stop order will execute an order when the price drops below a certain percentage from its all time high since the order was activated
        /// </summary>
        TrailingStopMarket,
        /// <summary>
        /// A liquidation order
        /// </summary>
        Liquidation
    }
}
