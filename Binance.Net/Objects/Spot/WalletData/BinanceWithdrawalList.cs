using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Information about a withdrawal
    /// </summary>
    public class BinanceWithdrawal
    {
        /// <summary>
        /// The id of the withdrawal
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// The time the withdrawal was applied for
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// The amount of the withdrawal
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The address the asset was withdrawn to
        /// </summary>
        public string Address { get; set; } = "";
        /// <summary>
        /// Tag for the address
        /// </summary>
        public string AddressTag { get; set; } = "";
        /// <summary>
        /// The transaction id of the withdrawal
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = "";
        /// <summary>
        /// Transaction fee for the withdrawal
        /// </summary>
        public decimal TransactionFee { get; set; }
        /// <summary>
        /// The asset that was withdrawn
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = "";

        /// <summary>
        /// Network that was used
        /// </summary>
        public string Network { get; set; } = "";

        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public WithdrawalStatus Status { get; set; }

        /// <summary>
        /// Transfer type: 1 for internal transfer, 0 for external transfer 
        /// </summary>
        [JsonConverter(typeof(WithdrawDepositTransferTypeConverter))]
        public WithdrawDepositTransferType TransferType { get; set; }
    }
}
