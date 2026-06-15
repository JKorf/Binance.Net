using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Binance.Net.Clients
{
    /// <inheritdoc />
    public class BinanceUserClientProvider :
        UserClientProvider<
            IBinanceRestClient, 
            IBinanceSocketClient,
            BinanceRestOptions,
            BinanceSocketOptions,
            BinanceCredentials,
            BinanceEnvironment>, IBinanceUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => BinanceExchange.Metadata.Id;

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
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IBinanceRestClient ConstructRestClient(
            HttpClient httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<BinanceRestOptions> options) => new BinanceRestClient(httpClient, loggerFactory, options);

        /// <inheritdoc />
        protected override IBinanceSocketClient ConstructSocketClient(
            ILoggerFactory? loggerFactory,
            IOptions<BinanceSocketOptions> options) => new BinanceSocketClient(options, loggerFactory);
    }
}
