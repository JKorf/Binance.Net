using Binance.Net.Converters;

namespace Binance.Net.Enums
{
    /// <summary>
    /// User position mode
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<PositionMode>))] public  enum PositionMode
    {
        /// <summary>
        /// In the Hedge Mode, one contract can hold positions in both long and short directions at the same time, and hedge positions in different directions under the same contract.
        /// </summary>
        Hedge,
        /// <summary>
        /// In the One-way Mode, one contract can only hold positions in one direction.
        /// </summary>
        OneWay
    }
}
