using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System.Net;
using System.Text;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public override ApiCredentialsType[] SupportedCredentialTypes => 
            [ApiCredentialsType.Hmac,
            ApiCredentialsType.RsaPem,
            ApiCredentialsType.RsaXml,
            ApiCredentialsType.Ed25519];

        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MBX-APIKEY", ApiKey);

            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var parameters = request.GetPositionParameters() ?? new Dictionary<string, object>();
            parameters.Add("timestamp", timestamp);

            if (request.ParameterPosition == HttpMethodParameterPosition.InUri)
            {
                var queryString = request.GetQueryString();
                var signature = Sign(queryString);
                parameters.Add("signature", signature);
                request.SetQueryString($"{queryString}&signature={WebUtility.UrlEncode(signature)}");
            }
            else
            {
                var parameterData = request.BodyParameters?.ToFormData() ?? string.Empty;
                var signature = Sign(parameterData);
                parameters.Add("signature", signature);
                request.SetBodyContent($"{parameterData}&signature={WebUtility.UrlEncode(signature)}");
            }
        }

        public Dictionary<string, object> ProcessRequest(SocketApiClient apiClient, Dictionary<string, object> providedParameters)
        {
            var sortedParameters = new SortedDictionary<string, object>(providedParameters)
            {
                { "apiKey", ApiKey },
                { "timestamp", GetMillisecondTimestampLong(apiClient) }
            };
            var paramString = string.Join("&", sortedParameters.Select(p => p.Key + "=" + Convert.ToString(p.Value, CultureInfo.InvariantCulture)));

            string sign = Sign(paramString);
            var result = sortedParameters.ToDictionary(p => p.Key, p => p.Value);
            result.Add("signature", sign);
            return result;
        }

        private string Sign(string data)
        {
            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
                return SignHMACSHA256(data);
            else if (_credentials.CredentialType == ApiCredentialsType.Ed25519)
                return SignEd25519(data, SignOutputType.Base64);
            else
                return SignRSASHA256(Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
        }

    }
}
