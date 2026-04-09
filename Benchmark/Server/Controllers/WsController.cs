using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Binance.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("stream")]
    public class WsController : ControllerBase
    {
        private const int _sendTarget = 1000000; // Should match the number in the client

        [HttpGet]
        public async Task Get()
        {
            var webSocket = await Request.HttpContext.WebSockets.AcceptWebSocketAsync();

            var cts = new CancellationTokenSource();
            
            _ = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    // Listen for subscribe requests
                    var buffer = new byte[8096];
                    var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        cts.Cancel();

                        if (webSocket.State == WebSocketState.CloseReceived)
                        {
                            //Console.WriteLine("Closed received, sending close response");
                            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", default).ConfigureAwait(false);
                        }
                        else
                        {
                            //Console.WriteLine("Closed received as answer on close request");
                        }
                    }

                    else
                    {
                        var msg = JsonSerializer.Deserialize<SubscribeMessage>(Encoding.UTF8.GetString(buffer, 0, result.Count))!;

                        var totalWritten = 0;

                        // Sub response
                        var response = "{\"id\":" + msg.Id + ",\"result\": null}";
                        await SendAsync(webSocket, response);
                        totalWritten += response.Length;

                        if (msg.Params.Any(x => x == "ethusdt@trade"))
                        {
                            _ = PushTradeUpdates(webSocket, cts.Token);
                        }
                    }
                }
            });

            
            try
            {
                await Task.Delay(-1, cts.Token);
            }
            catch (Exception) { }            

            //Console.WriteLine("Finished");
        }
        private async Task SendAsync(WebSocket webSocket, string message)
        {
            await webSocket.SendAsync(Encoding.UTF8.GetBytes(message),
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        private async Task SendAsync(WebSocket webSocket, byte[] data)
        {
            await webSocket.SendAsync(data,
                WebSocketMessageType.Text,
                endOfMessage: true,
                CancellationToken.None);
        }

        private async Task PushTradeUpdates(WebSocket webSocket, CancellationToken ct)
        {
            var messageBytes = Encoding.UTF8.GetBytes("{\"stream\":\"ethusdt@trade\",\"data\":{\"e\":\"trade\",\"E\":1763116829354,\"s\":\"ETHUSDT\",\"t\":3165660468,\"p\":\"3187.96000000\",\"q\":\"0.00170000\",\"T\":1763116829353,\"m\":false,\"M\":true}}");
            for (var i = 0; i < _sendTarget; i++)
            {
                if (ct.IsCancellationRequested)
                    break;

                await SendAsync(webSocket, messageBytes);
            }

            if (!ct.IsCancellationRequested)
            {
                //Console.WriteLine("Writing done, closing output");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "done", CancellationToken.None);
            }
            else
            {
                //Console.WriteLine("Writing done, cancellation already requested");
            }

            try
            {
                await Task.Delay(5000, ct);
            }
            catch (Exception) { }
        }

    }

    public record SubscribeMessage
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("params")]
        public string[] Params { get; set; }
    }
}
