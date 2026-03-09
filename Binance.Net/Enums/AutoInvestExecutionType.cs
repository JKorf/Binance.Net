using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Execution type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestExecutionType>))]
    public enum AutoInvestExecutionType
    {
        /// <summary>
        /// ["<c>ONE_TIME</c>"] One time
        /// </summary>
        [Map("ONE_TIME")]
        OneTime,
        /// <summary>
        /// ["<c>RECURRING</c>"] Recurring
        /// </summary>
        [Map("RECURRING")]
        Recurring
    }
}

