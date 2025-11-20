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

        public CallResult<BinanceResponse<BinanceReplaceOrderResult>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BinanceResponse<BinanceReplaceOrderResult> message)
        {
            if (message.Status != 200)
            {
                if (message.Status == 418 || message.Status == 429)
                {
                    // Rate limit error 
                    return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new BinanceRateLimitError(message.Error!.Code, message.Error!.Message)
                    {
                        RetryAfter = message.Error.Data!.RetryAfter
                    }, originalData);
                }

                if (message.Status == 400)
                {
                    if (message.Error!.Data == null)
                        return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new ServerError(message.Error.Code, _client.GetErrorInfo(message.Error.Code, message.Error.Message)));

                    return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new BinanceResponse<BinanceReplaceOrderResult>()
                    {
                        Id = message.Id,
                        Status = message.Status,
                        Ratelimits = message.Ratelimits,
                        Result = new BinanceReplaceOrderResult
                        {
                            CancelResponse = message.Error.Data!.CancelResponse,
                            CancelResult = message.Error.Data.CancelResult,
                            NewOrderResponse = message.Error.Data.NewOrderResponse,
                            NewOrderResult = message.Error.Data.NewOrderResult,
                        }
                    });
                }

                return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(new ServerError(message.Error!.Code.ToString(), _client.GetErrorInfo(message.Error!.Code, message.Error!.Message)), originalData);
            }

            return new CallResult<BinanceResponse<BinanceReplaceOrderResult>>(message, originalData, null);
        }
    }
}
