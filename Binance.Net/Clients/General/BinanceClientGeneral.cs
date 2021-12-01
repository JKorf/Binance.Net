using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using Binance.Net.Interfaces.Clients.General;
using Binance.Net.Interfaces.Clients.Rest.Spot;
using Binance.Net.Clients.Rest.Spot;
using Binance.Net.Clients.Base;

namespace Binance.Net.Clients.Rest.CoinFutures
{
    /// <inheritdoc cref="IBinanceClientCoinFutures" />
    public class BinanceClientGeneral : BinanceClientBaseSpot, IBinanceClientGeneral
    {
        #region fields 
        private readonly BinanceClient _baseClient;
        internal readonly BinanceClientOptions Options;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceClientGeneralBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralFutures Futures { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralLending Lending { get; }
        public IBinanceClientGeneralMining Mining { get; }
        public IBinanceClientGeneralSubAccount SubAccount { get; }
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClientGeneral(BinanceClient baseClient, BinanceClientOptions options): base(options)
        {
            Options = options;
            _baseClient = baseClient;

            Brokerage = new BinanceClientGeneralBrokerage(this);
            Futures = new BinanceClientGeneralFutures(this);
            Lending = new BinanceClientGeneralLending(this);
            Mining = new BinanceClientGeneralMining(this);
            SubAccount = new BinanceClientGeneralSubAccount(this);
        }
        #endregion

        internal string GetTimestamp()
        {
            var offset = Options.AutoTimestamp ? CalculatedTimeOffset : 0;
            offset += Options.SpotApiOptions.TimestampOffset.TotalMilliseconds;
            return DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow.AddMilliseconds(offset))!.Value.ToString(CultureInfo.InvariantCulture);
        }

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (Options.AutoTimestamp && (!TimeSynced || DateTime.UtcNow - LastTimeSync > Options.AutoTimestampRecalculationInterval))
                return await _baseClient.SpotApi.ExchangeData.GetServerTimeAsync(TimeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            if (signed)
            {
                var timestampResult = await ((BinanceClientSpotMarket)_baseClient.SpotApi).CheckAutoTimestamp(cancellationToken).ConfigureAwait(false);
                if (!timestampResult)
                    return new WebCallResult<T>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                parameters.Add("timestamp", GetTimestamp());
            }

            return await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight).ConfigureAwait(false);
        }

    }
}
