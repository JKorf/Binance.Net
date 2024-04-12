using System.Text;
using CryptoExchange.Net.Clients;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public string GetApiKey() => _credentials.Key!.GetString();

        public BinanceAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, RequestBodyFormat bodyFormat, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>() { { "X-MBX-APIKEY", _credentials.Key!.GetString() } };

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;
            var timestamp = GetMillisecondTimestamp(apiClient);
            parameters.Add("timestamp", timestamp);

            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
            {
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
                { "apiKey", _credentials.Key!.GetString() },
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
