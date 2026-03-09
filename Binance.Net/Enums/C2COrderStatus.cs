using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// C2C order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<C2COrderStatus>))]
    public enum C2COrderStatus
    {
        /// <summary>
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// ["<c>BUYER_PAYED</c>"] Buyer has paid
        /// </summary>
        [Map("BUYER_PAYED")]
        BuyerPayed,
        /// <summary>
        /// ["<c>DISTRIBUTING</c>"] Distributing
        /// </summary>
        [Map("DISTRIBUTING")]
        Distributing,
        /// <summary>
        /// ["<c>COMPLETED</c>"] Completed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// ["<c>IN_APPEAL</c>"] In appeal
        /// </summary>
        [Map("IN_APPEAL")]
        InAppeal,
        /// <summary>
        /// ["<c>CANCELLED</c>"] Canceled
        /// </summary>
        [Map("CANCELLED")]
        Canceled,
        /// <summary>
        /// ["<c>CANCELLED_BY_SYSTEM</c>"] CanceledBySystem
        /// </summary>
        [Map("CANCELLED_BY_SYSTEM")]
        CanceledBySystem
    }
}

