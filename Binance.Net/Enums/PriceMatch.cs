using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Price matching type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceMatch>))]
    public enum PriceMatch
    {
        /// <summary>
        /// ["<c>NONE</c>"] No price match
        /// </summary>
        [Map("NONE")]
        None,
        /// <summary>
        /// ["<c>OPPONENT</c>"] Counterparty best price
        /// </summary>
        [Map("OPPONENT")]
        Opponent,
        /// <summary>
        /// ["<c>OPPONENT_5</c>"] Counterparty 5th best price
        /// </summary>
        [Map("OPPONENT_5")]
        Opponent5,
        /// <summary>
        /// ["<c>OPPONENT_10</c>"] Counterparty 10th best price
        /// </summary>
        [Map("OPPONENT_10")]
        Opponent10,
        /// <summary>
        /// ["<c>OPPONENT_20</c>"] Counterparty 20th best price
        /// </summary>
        [Map("OPPONENT_20")]
        Opponent20,
        /// <summary>
        /// ["<c>QUEUE</c>"] The best price on the same side of the order book
        /// </summary>
        [Map("QUEUE")]
        Queue,
        /// <summary>
        /// ["<c>QUEUE_5</c>"] The 5th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_5")]
        Queue5,
        /// <summary>
        /// ["<c>QUEUE_10</c>"] The 10th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_10")]
        Queue10,
        /// <summary>
        /// ["<c>QUEUE_20</c>"] The 20th best price on the same side of the order book
        /// </summary>
        [Map("QUEUE_20")]
        Queue20
    }
}

