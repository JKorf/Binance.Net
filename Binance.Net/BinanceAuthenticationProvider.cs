using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System.Net;
using System.Text;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider<BinanceCredentials>
    {
        public override ApiCredentialsType[] SupportedCredentialTypes =>
            [ApiCredentialsType.Hmac,
            ApiCredentialsType.Rsa,
            ApiCredentialsType.Ed25519];

        public override string Key => ApiCredentials.Key;

        public BinanceAuthenticationProvider(BinanceCredentials credential) : base(credential)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MBX-APIKEY", ApiCredentials.Key);

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
                { "apiKey", ApiCredentials.ApiKey },
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
            if (ApiCredentials.CredentialType == ApiCredentialsType.Hmac)
                return SignHMACSHA256(ApiCredentials.GetCredential<HMACCredential>()!, data);
#if NET8_0_OR_GREATER
            else if (ApiCredentials.CredentialType == ApiCredentialsType.Ed25519)
                return SignEd25519(ApiCredentials.GetCredential<ED25519Credential>()!, data, SignOutputType.Base64);
#endif
            else
                return SignRSASHA256(ApiCredentials.GetCredential<RSACredential>()!, Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
        }
    }
}
