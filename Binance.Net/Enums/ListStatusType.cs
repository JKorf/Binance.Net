using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List status type
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<ListStatusType>))] public  enum ListStatusType
    {
        /// <summary>
        /// Failed action
        /// </summary>
        [Map("RESPONSE")]
        Response,
        /// <summary>
        /// Placed
        /// </summary>
        [Map("EXEC_STARTED")]
        ExecutionStarted,
        /// <summary>
        /// Order list is done
        /// </summary>
        [Map("ALL_DONE")]
        Done
    }
}
