using CryptoExchange.Net.Clients;
using System.Text;

namespace Binance.Net
{
    internal class BinanceAuthenticationProvider : AuthenticationProvider
    {
        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac, ApiCredentialsType.RsaPem, ApiCredentialsType.RsaXml];
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
                request.SetQueryString($"{queryString}&signature={signature}");
            }
            else
            {
                var parameterData = request.BodyParameters?.ToFormData() ?? string.Empty;
                var signature = Sign(parameterData);
                parameters.Add("signature", signature);
                request.SetBodyContent($"{parameterData}&signature={signature}");
            }
        }

        private string Sign(string data)
        {
            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
                return SignHMACSHA256(data);
            else
                return SignRSASHA256(Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
        }

        public Dictionary<string, object> AuthenticateSocketParameters(Dictionary<string, object> providedParameters)
        {
            var sortedParameters = new SortedDictionary<string, object>(providedParameters)
            {
                { "apiKey", ApiKey },
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
