using CryptoExchange.Net.Trackers.UserData;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IBinanceTrackerFactory : ITrackerFactory
    {
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null);
        IUserSpotDataTracker CreateUserSpotDataTracker(UserDataTrackerConfig config);
        IUserFuturesDataTracker CreateUserUsdFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null);
        IUserFuturesDataTracker CreateUserUsdFuturesDataTracker(UserDataTrackerConfig config);
        IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, BinanceEnvironment? environment = null);
        IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(UserDataTrackerConfig config);
    }
}
