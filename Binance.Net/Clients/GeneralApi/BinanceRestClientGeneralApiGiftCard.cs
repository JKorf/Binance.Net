using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models.Spot.GiftCard;
using System.Security.Cryptography;
using System.Text;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc />
    internal class BinanceRestClientGeneralApiGiftCard : IBinanceRestClientGeneralApiGiftCard
    {
        private static readonly RequestDefinitionCache _definitions = new();

        private readonly BinanceRestClientGeneralApi _baseClient;

        internal BinanceRestClientGeneralApiGiftCard(BinanceRestClientGeneralApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Create a single token gift card

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateSingleTokenGiftCardAsync(string token, double amount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("token", token);
            parameters.Add("amount", amount);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/giftcard/createCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinaceGiftCardData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Create a dual token gift card

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateDualTokenGiftCardAsync(string baseToken, string faceToken, double baseTokenAmount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("token", baseToken);
            parameters.Add("faceToken", faceToken);
            parameters.Add("baseTokenAmount", baseTokenAmount);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/giftcard/buyCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinaceGiftCardData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Redeem a Binance Gift card

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>> RedeemGiftCardAsync(string code, string? externalUid = null, bool useEncryption = true, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
#if NETSTANDARD2_0
            if (useEncryption)
                return HttpResult.Fail<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(nameof(useEncryption), "Encryption is not supported when targeting NETSTANDARD2_0. Please disable `useEncryption` or upgrade your target framework."));
            else
                parameters.Add("code", code);
#else
            if (useEncryption)
            {
                var keyResult = await GetRsaPublicKeyAsync(receiveWindow, ct).ConfigureAwait(false);

                if (!keyResult.Success || string.IsNullOrEmpty(keyResult.Data?.Data))
                    return HttpResult.Fail<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(keyResult);

                using var rsa = RSA.Create();
                var publicKeyBytes = Convert.FromBase64String(keyResult.Data.Data);
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

                var plainBytes = Encoding.UTF8.GetBytes(code);
                var encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);

                var encryptedBase64 = Convert.ToBase64String(encryptedBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

                parameters.Add("code", encryptedBase64);
            }
            else
                parameters.Add("code", code);
            #endif
            parameters.Add("externalUid", externalUid);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/giftcard/redeemCode", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Verify Binance Gift Card by Gift Card Number

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<BinanceGiftCardValidity>>> VerifyGiftCardByNumberAsync(string referenceNumber, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("referenceNo", referenceNumber);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/giftcard/verify", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardValidity>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Fetch Token Limit

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<BinanceGiftCardTokenLimit[]>>> GetTokenLimitAsync(string baseToken, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("baseToken", baseToken);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/giftcard/buyCode/token-limit", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<BinanceGiftCardTokenLimit[]>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Fetch RSA Public Key

        /// <inheritdoc />
        public async Task<HttpResult<BinanceGiftCardResponse<string>>> GetRsaPublicKeyAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("recvWindow", receiveWindow ?? (long)_baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/giftcard/cryptography/rsa-public-key", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceGiftCardResponse<string>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
