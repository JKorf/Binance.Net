using System.Text;
using CryptoExchange.Net.Clients;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            headers ??= new Dictionary<string, string>();
            headers.Add("X-MBX-APIKEY", _credentials.Key);

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
            parameters ??= new Dictionary<string, object>();
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("timestamp", timestamp);

            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
            {
                if (uriParameters != null)
                    uri = uri.SetParameters(uriParameters, arraySerialization);
                parameters.Add("signature", SignHMACSHA256(parameterPosition == HttpMethodParameterPosition.InUri ? uri.Query.Replace("?", "") : parameters.ToFormData()));
            }
            else
            {
                var parameterString = parameters.ToFormData();
                var sign = SignRSASHA256(Encoding.ASCII.GetBytes(parameterString), SignOutputType.Base64);
                parameters.Add("signature", sign);
            }
        }

        public Dictionary<string, object> AuthenticateSocketParameters(Dictionary<string, object> providedParameters)
        {
            var sortedParameters = new SortedDictionary<string, object>(providedParameters)
            {
                { "apiKey", _credentials.Key },
                { "timestamp", DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow) }
            };
            var paramString = string.Join("&", sortedParameters.Select(p => p.Key + "=" + Convert.ToString(p.Value, CultureInfo.InvariantCulture)));

            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
            {
                var sign = SignHMACSHA256(paramString);
                var result = sortedParameters.ToDictionary(p => p.Key, p => p.Value);
                result.Add("signature", sign);
                return result;
            }
            else
            {
                var sign = SignRSASHA256(Encoding.ASCII.GetBytes(paramString), SignOutputType.Base64);
                var result = sortedParameters.ToDictionary(p => p.Key, p => p.Value);
                result.Add("signature", sign);
                return result;
            }
        }
    }
}
