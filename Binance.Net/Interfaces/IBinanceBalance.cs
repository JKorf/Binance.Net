namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Balance data
    /// </summary>
    public interface IBinanceBalance
	{
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        string Asset { get; set; }
        /// <summary>
        /// The quantity that isn't locked in a trade
        /// </summary>
        decimal Available { get; set; }
        /// <summary>
        /// The quantity that is currently locked in a trade
        /// </summary>
        decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        decimal Total { get; }
	}
}
