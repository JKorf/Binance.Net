using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of an order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>PENDING_NEW</c>"] Order is not yet active
        /// </summary>
        [Map("PENDING_NEW")]
        PendingNew,
        /// <summary>
        /// ["<c>NEW</c>"] Order is new
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>PARTIALLY_FILLED</c>"] Order is partly filled, still has quantity left to fill
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>FILLED</c>"] The order has been filled and completed
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELED</c>"] The order has been canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>PENDING_CANCEL</c>"] The order is in the process of being canceled  (currently unused)
        /// </summary>
        [Map("PENDING_CANCEL")]
        PendingCancel,
        /// <summary>
        /// ["<c>REJECTED</c>"] The order has been rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>EXPIRED</c>"] The order has expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>NEW_INSURANCE</c>"] Liquidation with Insurance Fund
        /// </summary>
        [Map("NEW_INSURANCE")]
        Insurance,
        /// <summary>
        /// ["<c>NEW_ADL</c>"] Counterparty Liquidation
        /// </summary>
        [Map("NEW_ADL")]
        Adl,
        /// <summary>
        /// ["<c>EXPIRED_IN_MATCH</c>"] Expired because of trigger SelfTradePrevention
        /// </summary>
        [Map("EXPIRED_IN_MATCH")]
        ExpiredInMatch
    }
}

