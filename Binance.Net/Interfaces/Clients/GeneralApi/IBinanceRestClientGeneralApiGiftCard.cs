using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.GiftCard;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance gift card endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiGiftCard
    {
        /// <summary>
        /// Create a single-token gift card
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data" /></para>
        /// </summary>
        /// <param name="token">The token type contained in the Binance Gift Card</param>
        /// <param name="amount">The amount of the token contained in the Binance Gift Card</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateSingleTokenGiftCard(string token, double amount, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Create a dual-token gift card
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data/Create-a-dual-token-gift-card" /></para>
        /// </summary>
        /// <param name="baseToken">The token you want to pay, example: BUSD</param>
        /// <param name="faceToken">The token you want to buy, example: BNB</param>
        /// <param name="baseTokenAmount">The base token asset quantity, example: 1.002</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinaceGiftCardData>>> CreateDualTokenGiftCard(string baseToken, string faceToken, double baseTokenAmount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a Binance Gift Card
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data/Redeem-a-Binance-Gift-Card" /></para>
        /// </summary>
        /// <param name="code">Redemption code of Binance Gift Card to be redeemed, supports both Plaintext and Encrypted code.</param>
        /// <param name="externalUid">External UID</param>
        /// <param name="useEncryption">Encrypts the code if true, sends as plaintext otherwise</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardRedeemData>>> RedeemGiftCard(string code, string? externalUid = null, bool useEncryption = true, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Verify Binance Gift Card by Gift Card Number
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data/Verify-Binance-Gift-Card-by-Gift-Card-Number" /></para>
        /// </summary>
        /// <param name="referenceNumber">The Gift Card Number</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardValidity>>> VerifyGiftCardByNumber(string referenceNumber, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get Token Limit
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data/Fetch-Token-Limit" /></para>
        /// </summary>
        /// <param name="baseToken">The token you want to pay, example: BUSD</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        
        Task<WebCallResult<BinanceGiftCardResponse<BinanceGiftCardTokenLimit>>> GetTokenLimit(string baseToken, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get RSA public key
        /// <para><a href="https://developers.binance.com/docs/gift_card/market-data/Fetch-RSA-Public-Key" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceGiftCardResponse<string>>> GetRsaPublicKey(long? receiveWindow = null, CancellationToken ct = default);
    }
}
