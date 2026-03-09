using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Direction of a transfer
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferDirection>))]
    public enum TransferDirection
    {
        /// <summary>
        /// ["<c>ROLL_IN</c>"] Roll-in
        /// </summary>
        [Map("ROLL_IN")]
        RollIn,
        /// <summary>
        /// ["<c>ROLL_OUT</c>"] Roll-out
        /// </summary>
        [Map("ROLL_OUT")]
        RollOut
    }
}

