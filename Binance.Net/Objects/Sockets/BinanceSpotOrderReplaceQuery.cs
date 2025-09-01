using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotOrderReplaceQuery : Query<BinanceResponse<BinanceReplaceOrderResult>>
    {
        private readonly SocketApiClient _client;

        public BinanceSpotOrderReplaceQuery(SocketApiClient client, BinanceSocketQuery request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<BinanceResponse<BinanceReplaceOrderResult>>(request.Id.ToString(), HandleMessage);
        }

        public CallResult<BinanceResponse<BinanceReplaceOrderResult>> HandleMessage(SocketConnection connection, DataEvent<BinanceResponse<BinanceReplaceOrderResult>> message)
        {
            if (message.Data.Status != 200)
            {
                if (message.Data.Status == 418 || message.Data.Status == 429)
                {
                    // Rate limit error 
                    return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new BinanceRateLimitError(message.Data.Error!.Code, message.Data.Error!.Message)
                    {
                        RetryAfter = message.Data.Error.Data!.RetryAfter
                    }, message.OriginalData);
                }

                if (message.Data.Status == 400)
                {
                    if (message.Data.Error!.Data == null)
                        return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new ServerError(message.Data.Error.Code, _client.GetErrorInfo(message.Data.Error.Code, message.Data.Error.Message)));

                    return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new BinanceResponse<BinanceReplaceOrderResult>()
                    {
                        Id = message.Data.Id,
                        Status = message.Data.Status,
                        Ratelimits = message.Data.Ratelimits,
                        Result = new BinanceReplaceOrderResult
                        {
                            CancelResponse = message.Data.Error.Data!.CancelResponse,
                            CancelResult = message.Data.Error.Data.CancelResult,
                            NewOrderResponse = message.Data.Error.Data.NewOrderResponse,
                            NewOrderResult = message.Data.Error.Data.NewOrderResult,
                        }
                    });
                }

                return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new ServerError(message.Data.Error!.Code.ToString(), _client.GetErrorInfo(message.Data.Error!.Code, message.Data.Error!.Message)), message.OriginalData);
            }

            return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(message.Data, message.OriginalData, null);
        }
    }
}
