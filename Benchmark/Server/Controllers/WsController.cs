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
            using var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var ct = HttpContext.RequestAborted;
            var buffer = new byte[8192];
            var combined = string.IsNullOrEmpty(streamPath);
            if (string.Equals(streamPath, "ethusdt@trade", StringComparison.OrdinalIgnoreCase))
                await PushTradeUpdates(socket, ct, combined);

            while (socket.State == WebSocketState.Open)
            {
                using var request = new MemoryStream();
                WebSocketReceiveResult frame;
                do
                {
                    frame = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                    if (frame.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
                        return;
                    }
                    request.Write(buffer, 0, frame.Count);
                } while (!frame.EndOfMessage);

                request.Position = 0;
                var msg = JsonSerializer.Deserialize<SubscribeMessage>(request)!;
                await SendAsync(socket, JsonSerializer.Serialize(new { result = (object?)null, id = msg.Id }));
                if (msg.Method == "SUBSCRIBE" && msg.Params.Contains("ethusdt@trade", StringComparer.OrdinalIgnoreCase))
                    await PushTradeUpdates(socket, ct, combined);
            }
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
            for (var i = 0; i < TradeStream.Count; i++)
            {
                if (ct.IsCancellationRequested)
                    break;

                var trade = "{\"e\":\"trade\",\"E\":"+time+",\"s\":\"ETHUSDT\",\"t\":" + (TradeStream.FirstId + i) + ",\"p\":\"3187.96000000\",\"q\":\"0.00170000\",\"T\":"+time+",\"m\":false,\"M\":true}";
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


        }

    }

    public record SubscribeMessage
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("params")]
        public string[] Params { get; set; } = [];
    }
}
