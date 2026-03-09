using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Brokerage transfer transaction status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BrokerageTransferTransactionStatus>))]
    public enum BrokerageTransferTransactionStatus
    {
        /// <summary>
        /// ["<c>INIT</c>"] Init
        /// </summary>
        [Map("INIT")]
        Init,
        /// <summary>
        /// ["<c>PROCESS</c>"] Process
        /// </summary>
        [Map("PROCESS")]
        Process,
        /// <summary> 
        /// ["<c>SUCCESS</c>"] Success 
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary> 
        /// ["<c>FAILURE</c>"] Failure 
        /// </summary>
        [Map("FAILURE")]
        Failure,
    }
}
