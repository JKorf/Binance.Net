using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// C2C order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<C2COrderStatus>))] public  enum C2COrderStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// Buyer has paid
        /// </summary>
        [Map("BUYER_PAYED")]
        BuyerPayed,
        /// <summary>
        /// Distributing
        /// </summary>
        [Map("DISTRIBUTING")]
        Distributing,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("COMPLETED")]
        Completed,
        /// <summary>
        /// In appeal
        /// </summary>
        [Map("IN_APPEAL")]
        InAppeal,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELLED")]
        Canceled,
        /// <summary>
        /// CanceledBySystem
        /// </summary>
        [Map("CANCELLED_BY_SYSTEM")]
        CanceledBySystem
    }
}
