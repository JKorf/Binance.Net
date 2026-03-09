using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Cancel replace mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<CancelReplaceMode>))]
    public enum CancelReplaceMode
    {
        /// <summary>
        /// ["<c>STOP_ON_FAILURE</c>"] If the cancel request fails, the new order placement will not be attempted.
        /// </summary>
        [Map("STOP_ON_FAILURE")]
        StopOnFailure,
        /// <summary>
        /// ["<c>ALLOW_FAILURE</c>"] New order placement will be attempted even if cancel request fails.
        /// </summary>
        [Map("ALLOW_FAILURE")]
        AllowFailure
    }
}

