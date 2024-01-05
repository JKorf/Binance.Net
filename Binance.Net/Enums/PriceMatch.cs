using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Price matching type
    /// </summary>
    public enum PriceMatch
    {
        /// <summary>
        /// No price match
        /// </summary>
        [Map("NONE")]
        None,
        /// <summary>
        /// Counterparty best price
        /// </summary>
        [Map("OPPONENT")]
        Opponent,
        /// <summary>
        /// Counterparty 5th best price
        /// </summary>
        [Map("OPPONENT_5")]
        Opponent5,
        /// <summary>
        /// Counterparty 10th best price
        /// </summary>
        [Map("OPPONENT_10")]
        Opponent10,
        /// <summary>
        /// Counterparty 20th best price
        /// </summary>
        [Map("OPPONENT_20")]
        Opponent20,
        /// <summary>
        /// The best price on the same side of the order book
        /// </summary>
        [Map("QUEUE")]
        Queue,
        /// <summary>
        /// The 5th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_5")]
        Queue5,
        /// <summary>
        /// The 10th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_10")]
        Queue10,
        /// <summary>
        /// The 20th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_20")]
        Queue20
    }
}
