using Binance.Net.SubClients.Spot;

namespace Binance.Net.Interfaces.SubClients.Spot
{
    /// <summary>
    /// Spot interface
    /// </summary>
    public interface IBinanceClientSpot
    {
        /// <summary>
        /// Spot market endpoints
        /// </summary>
        IBinanceClientSpotMarket Market { get; }

        /// <summary>
        /// Spot order endpoints
        /// </summary>
        IBinanceClientSpotOrder Order { get; }

        /// <summary>
        /// Spot user stream endpoints
        /// </summary>
        IBinanceClientSpotUserStream UserStream { get; }
    }
}