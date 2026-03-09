using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List status type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ListStatusType>))]
    public enum ListStatusType
    {
        /// <summary>
        /// ["<c>RESPONSE</c>"] Failed action
        /// </summary>
        [Map("RESPONSE")]
        Response,
        /// <summary>
        /// ["<c>EXEC_STARTED</c>"] Placed
        /// </summary>
        [Map("EXEC_STARTED")]
        ExecutionStarted,
        /// <summary>
        /// ["<c>ALL_DONE</c>"] Order list is done
        /// </summary>
        [Map("ALL_DONE")]
        Done
    }
}

