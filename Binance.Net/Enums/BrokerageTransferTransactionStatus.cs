using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Brokerage transfer transaction status
    /// </summary>
    public enum BrokerageTransferTransactionStatus
    {
        /// <summary>
        /// Init
        /// </summary>
        [Map("INIT")]
        Init,
        /// <summary>
        /// Process
        /// </summary>
        [Map("PROCESS")]
        Process,
        /// <summary> 
        /// Success 
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary> 
        /// Failure 
        /// </summary>
        [Map("FAILURE")]
        Failure,
    }
}