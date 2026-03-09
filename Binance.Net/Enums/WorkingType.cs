using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of working
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WorkingType>))]
    public enum WorkingType
    {
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price type
        /// </summary>
        [Map("MARK_PRICE")]
        Mark,
        /// <summary>
        /// ["<c>CONTRACT_PRICE</c>"] Contract price type
        /// </summary>
        [Map("CONTRACT_PRICE")]
        Contract
    }
}

