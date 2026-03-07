using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Binance pay trade
    /// </summary>
    [SerializationModel]
    public record BinancePayTrade
    {
        /// <summary>
        /// ["<c>uid</c>"] The user identifier.
        /// </summary>
        [JsonPropertyName("uid")]
        public long? Uid { get; set; }
        /// <summary>
        /// ["<c>counterpartyId</c>"] The counterparty identifier.
        /// </summary>
        [JsonPropertyName("counterpartyId")]
        public long? CounterPartyId { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>note</c>"] Note
        /// </summary>
        [JsonPropertyName("note")]
        public string? Note { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public PayOrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>transactionId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionTime</c>"] The transaction time.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("transactionTime")]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>totalPaymentFee</c>"] Total fee
        /// </summary>
        [JsonPropertyName("totalPaymentFee")]
        public decimal TotalPaymentFee { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletType</c>"] Main wallet type
        /// </summary>
        [JsonPropertyName("walletType")]
        public PayWalletType WalletType { get; set; }
        /// <summary>
        /// ["<c>fundsDetail</c>"] Fund details
        /// </summary>
        [JsonPropertyName("fundsDetail")]
        public BinancePayTradeDetails[] Details { get; set; } = Array.Empty<BinancePayTradeDetails>();
        /// <summary>
        /// ["<c>payerInfo</c>"] Payer info
        /// </summary>
        [JsonPropertyName("payerInfo")]
        public BinancePayTradeParticipantInfo PayerInfo { get; set; } = new BinancePayTradeParticipantInfo();
        /// <summary>
        /// ["<c>receiverInfo</c>"] Receiver info
        /// </summary>
        [JsonPropertyName("receiverInfo")]
        public BinancePayTradeParticipantInfo ReceiverInfo { get; set; } = new BinancePayTradeParticipantInfo();
    }

    /// <summary>
    /// Pay trade funds details
    /// </summary>
    public record BinancePayTradeDetails
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }

    /// <summary>
    /// Pay trade receiver info
    /// </summary>
    public record BinancePayTradeParticipantInfo
    {
        /// <summary>
        /// ["<c>name</c>"] Nickname or merchant name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Account type，USER for personal，MERCHANT for merchant
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>email</c>"] Email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>binanceId</c>"] Binance uid
        /// </summary>
        [JsonPropertyName("binanceId")]
        public long BinanceId { get; set; }
        /// <summary>
        /// ["<c>accountId</c>"] Binance pay id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>countryCode</c>"] International area code
        /// </summary>
        [JsonPropertyName("countryCode")]
        public long CountryCode { get; set; }
        /// <summary>
        /// ["<c>phoneNumber</c>"] Phone number
        /// </summary>
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mobileCode</c>"] Country code
        /// </summary>
        [JsonPropertyName("mobileCode")]
        public string MobileCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>unmaskData</c>"] Unmask data
        /// </summary>
        [JsonPropertyName("unmaskData")]
        public bool? UnmaskData { get; set; }
    }
}

