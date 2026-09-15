using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;

namespace Binance.Net.Objects.Options
{
    /// <summary>
    /// Binance options
    /// </summary>
    public class BinanceOptions : LibraryOptions<BinanceRestOptions, BinanceSocketOptions, BinanceCredentials, BinanceEnvironment>
    {
        /// <summary>
        /// Whether to allow the client to adjust the clientOrderId parameter send by the user when placing orders to include a client reference. This reference is used by the exchange to allocate a small percentage of the paid trading fees to developer of this library. Defaults to false.<br />
        /// Note that:<br />
        /// * It does not impact the amount of fees a user pays in any way<br />
        /// * It does not impact functionality. The reference is added just before sending the request and removed again during data deserialization<br />
        /// * It does respect client order id field limitations. For example if the user provided client order id parameter is too long to fit the reference it will not be added<br />
        /// * Toggling this option might fail operations using a clientOrderId parameter for pre-existing orders which were placed before the toggle. Operations on orders placed after the toggle will work as expected. It's advised to toggle when there are no open orders
        /// </summary>
        public bool AllowAppendingClientOrderId { get; set; } = false;
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();

        /// <summary>
        /// Create BinanceOptions instance using the provided configuration action
        /// </summary>
        public static BinanceOptions Create(Action<BinanceOptions>? configure = null)
        {
            var options = CreateUnconfigured();
            configure?.Invoke(options);
            return Normalize(options);
        }

        /// <summary>
        /// Create BinanceOptions using the provided IConfiguration
        /// </summary>
        public static BinanceOptions Create(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = CreateUnconfigured();

            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(
                    "Invalid Binance configuration provided",
                    ex);
            }

            // Resolve environment configuration based on names
            if (options.Environment != null)
            {
                options.Environment =
                    BinanceEnvironment.GetEnvironmentByName(options.Environment.Name)
                    ?? options.Environment;
            }

            if (options.Rest?.Environment != null)
            {
                options.Rest.Environment =
                    BinanceEnvironment.GetEnvironmentByName(options.Rest.Environment.Name)
                    ?? options.Rest.Environment;
            }

            if (options.Socket?.Environment != null)
            {
                options.Socket.Environment =
                    BinanceEnvironment.GetEnvironmentByName(options.Socket.Environment.Name)
                    ?? options.Socket.Environment;
            }

            return Normalize(options);
        }

        private static BinanceOptions CreateUnconfigured()
        {
            var options = new BinanceOptions();

            // Clear transport defaults so configuration precedence can be determined.
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;

            return options;
        }

        /// <summary>
        /// Configure Rest and Socket specific options based on the global options
        /// </summary>
        private static BinanceOptions Normalize(BinanceOptions options)
        {
            if (options.Rest == null)
                throw new ArgumentException("REST options cannot be null", nameof(options));

            if (options.Socket == null)
                throw new ArgumentException("Socket options cannot be null", nameof(options));

            options.Rest.Environment =
                options.Rest.Environment
                    ?? options.Environment
                    ?? BinanceEnvironment.Live;

            options.Socket.Environment =
                options.Socket.Environment
                    ?? options.Environment
                    ?? BinanceEnvironment.Live;

            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.ApiCredentials ??= options.ApiCredentials;

            options.Rest.AllowAppendingClientOrderId |= options.AllowAppendingClientOrderId;
            options.Socket.AllowAppendingClientOrderId |= options.AllowAppendingClientOrderId;
            return options;
        }
    }
}
