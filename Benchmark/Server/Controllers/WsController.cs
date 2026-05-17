using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Binance.Net.Benchmark.Controllers
{
    [ApiController]
    [Route("")]
    public class WsController : ControllerBase
    {
        [HttpGet("stream")]
        [HttpGet("stream/{*streamPath}")]
        [HttpGet("ws/{*streamPath}")]
        public async Task Get(string? streamPath = null)
        {
            var webSocket = await Request.HttpContext.WebSockets.AcceptWebSocketAsync();

            var cts = new CancellationTokenSource();

            var useCombinedStreamPayload = string.IsNullOrEmpty(streamPath);

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

                        var response = "{\"result\":null,\"id\":" + msg.Id + "}";
                        await SendAsync(webSocket, response);

                        if (msg.Params.Any(x => string.Equals(x, "ethusdt@trade", StringComparison.OrdinalIgnoreCase)))
                        {
                            _ = PushTradeUpdates(webSocket, cts.Token, useCombinedStreamPayload);
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

        private async Task PushTradeUpdates(WebSocket webSocket, CancellationToken ct, bool combinedStreamPayload)
        {
            var time = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            for (var i = 0; i < 1_000_000; i++)
            {
                if (ct.IsCancellationRequested)
                    break;

                var trade = "{\"e\":\"trade\",\"E\":"+time+",\"s\":\"ETHUSDT\",\"t\":" + (5000000000L + i) + ",\"p\":\"3187.96000000\",\"q\":\"0.00170000\",\"T\":"+time+",\"m\":false,\"M\":true}";
                var combinedTrade = "{\"stream\":\"ethusdt@trade\",\"data\":" + trade + "}";

                if (combinedStreamPayload)
                {
                    await SendAsync(webSocket, Encoding.UTF8.GetBytes(combinedTrade));
                }
                else
                {
                    await SendAsync(webSocket, Encoding.UTF8.GetBytes(trade));
                }
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
