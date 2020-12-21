using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Converters
{
    internal class BrokerageTransferTransactionStatusConverter : BaseConverter<BrokerageTransferTransactionStatus>
    {
        public BrokerageTransferTransactionStatusConverter() : this(true) { }
        public BrokerageTransferTransactionStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BrokerageTransferTransactionStatus, string>> Mapping =>
            new List<KeyValuePair<BrokerageTransferTransactionStatus, string>>
            {
                new KeyValuePair<BrokerageTransferTransactionStatus, string>(BrokerageTransferTransactionStatus.Init, "INIT"),
                new KeyValuePair<BrokerageTransferTransactionStatus, string>(BrokerageTransferTransactionStatus.Process, "PROCESS"),
                new KeyValuePair<BrokerageTransferTransactionStatus, string>(BrokerageTransferTransactionStatus.Success, "SUCCESS"),
                new KeyValuePair<BrokerageTransferTransactionStatus, string>(BrokerageTransferTransactionStatus.Failure, "FAILURE"),
            };
    }
}