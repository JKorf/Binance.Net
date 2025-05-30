using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;

namespace Binance.Net.Clients
{
    /// <inheritdoc />
    public class BinanceUserClientProvider : IBinanceUserClientProvider
    {
        private static ConcurrentDictionary<string, IBinanceRestClient> _restClients = new ConcurrentDictionary<string, IBinanceRestClient>();
        private static ConcurrentDictionary<string, IBinanceSocketClient> _socketClients = new ConcurrentDictionary<string, IBinanceSocketClient>();

        private readonly IOptions<BinanceRestOptions> _restOptions;
        private readonly IOptions<BinanceSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public BinanceUserClientProvider(Action<BinanceOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BinanceUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<BinanceRestOptions> restOptions,
            IOptions<BinanceSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, BinanceEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public IBinanceRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, BinanceEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IBinanceSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, BinanceEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IBinanceRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, BinanceEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new BinanceRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IBinanceSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, BinanceEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new BinanceSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<BinanceRestOptions> SetRestEnvironment(BinanceEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new BinanceRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<BinanceSocketOptions> SetSocketEnvironment(BinanceEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new BinanceSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
