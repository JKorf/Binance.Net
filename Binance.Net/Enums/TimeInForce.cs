using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The time the order will be active for
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// GoodTillCanceled orders will stay active until they are filled or canceled
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// GoodTillCrossing orders will post only
        /// </summary>
        [Map("GTX")]
        GoodTillCrossing,
        /// <summary>
        /// Good til the order expires or is canceled
        /// </summary>
        [Map("GTE_GTC")]
        GoodTillExpiredOrCanceled,
        /// <summary>
        /// Good til date
        /// </summary>
        [Map("GTD")]
        GoodTillDate
    }
}
