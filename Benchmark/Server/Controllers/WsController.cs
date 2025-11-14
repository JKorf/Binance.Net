using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
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
        [HttpGet]
        public async Task Get()
        {
            var webSocket = await Request.HttpContext.WebSockets.AcceptWebSocketAsync();

            // Start after receiving sub request
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            var msg = JsonSerializer.Deserialize<SubscribeMessage>(Encoding.UTF8.GetString(buffer, 0, result.Count));

            var totalWritter = 0;

            // Sub response
            var subResponseBytes = Encoding.UTF8.GetBytes("{\"id\":" + msg.Id + ",\"result\": null}");
            await webSocket.SendAsync(
                    subResponseBytes,
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    CancellationToken.None);
            totalWritter += subResponseBytes.Length;

            var messageBytes = Encoding.UTF8.GetBytes("{\"stream\":\"ethusdt@trade\",\"data\":{\"e\":\"trade\",\"E\":1763116829354,\"s\":\"ETHUSDT\",\"t\":3165660468,\"p\":\"3187.96000000\",\"q\":\"0.00170000\",\"T\":1763116829353,\"m\":false,\"M\":true}}");
            for (var i = 0;i < 1000; i++)
            {
                //Console.WriteLine("Writing " + ++i);
                await webSocket.SendAsync(
                    messageBytes,
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    CancellationToken.None
                );
                totalWritter += messageBytes.Length;
            }

            Console.WriteLine(totalWritter + " written");
            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "done", CancellationToken.None);
        }
    }

    public record SubscribeMessage
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
