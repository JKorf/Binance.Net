using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Direction of a transfer
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<TransferDirection>))] public  enum TransferDirection
    {
        /// <summary>
        /// Roll-in
        /// </summary>
        [Map("ROLL_IN")]
        RollIn,
        /// <summary>
        /// Roll-out
        /// </summary>
        [Map("ROLL_OUT")]
        RollOut
    }
}
