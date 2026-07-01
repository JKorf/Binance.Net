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
        public override string Key => ApiCredentials.Credential!.Key;

        public BinanceAuthenticationProvider(BinanceCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MBX-APIKEY", ApiCredentials.Credential!.Key);

            if (!request.RequestDefinition.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var parameters = request.GetPositionParameters() ?? new Parameters(ParameterSerializationSettings.Default);
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

        public Parameters ProcessRequest(SocketApiClient apiClient, Parameters? providedParameters)
        {
            var parameters = new Parameters(BinanceExchange._socketParameterSignSettings);
            if (providedParameters?.Count > 0)
            {
                foreach (var param in providedParameters)
                    parameters.AddRaw(param.Key, param.Value);
            }

            parameters.Add("apiKey", ApiCredentials.Credential!.Key);
            parameters.Add("timestamp", GetMillisecondTimestampLong(apiClient));
            var paramString = string.Join("&", parameters.Select(p => p.Key + "=" + Convert.ToString(p.Value, CultureInfo.InvariantCulture)));

            string sign = Sign(paramString);
            parameters.Add("signature", sign);
            return parameters;
        }

        private string Sign(string data)
        {
            if (ApiCredentials.Credential is HMACCredential hmacCred)
                return SignHMACSHA256(hmacCred, data);
#if NET8_0_OR_GREATER
            else if (ApiCredentials.Credential is Ed25519Credential ed25519Cred)
                return SignEd25519(ed25519Cred, data, SignOutputType.Base64);
#endif
            else if (ApiCredentials.Credential is RSACredential rsaCred)
                return SignRSASHA256(rsaCred, Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
            else
                throw new NotImplementedException();
        }
    }
}
