using CryptoExchange.Net.Trackers.UserData;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IBinanceTrackerFactory : ITrackerFactory
    {
        IUserDataTracker CreateUserDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null);

        IUserDataTracker CreateUserDataTracker(UserDataTrackerConfig config);
    }
}
