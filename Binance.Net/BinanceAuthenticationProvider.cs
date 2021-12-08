using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateBodyRequest(
            RestApiClient apiClient, 
            Uri uri, 
            HttpMethod method,
            SortedDictionary<string, object> parameters,
            Dictionary<string, string> headers,
            bool auth, 
            ArrayParametersSerialization arraySerialization)
        {
            headers.Add("X-MBX-APIKEY", Credentials.Key!.GetString());

            if (!auth)
                return;

            parameters.Add("timestamp", GetTimestamp(apiClient));
            parameters.Add("signature", SignHMACSHA256(parameters.ToFormData()));
        }

        public override void AuthenticateUriRequest(
           RestApiClient apiClient,
           ref Uri uri,
           HttpMethod method,
           SortedDictionary<string, object> parameters,
           Dictionary<string, string> headers,
           bool auth,
           ArrayParametersSerialization arraySerialization)
        {
            headers.Add("X-MBX-APIKEY", Credentials.Key!.GetString());

            if (!auth)
                return;

            uri = uri.AddQueryParmeter("timestamp", GetTimestamp(apiClient));
            uri = uri.AddQueryParmeter("signature", SignHMACSHA256(uri.Query.Replace("?", "")));
        }

        internal string GetTimestamp(RestApiClient apiClient)
        {
            return DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow.Add(apiClient.GetTimeOffset()))!.Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
