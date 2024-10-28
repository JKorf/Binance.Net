using Binance.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Globalization;

var collection = new ServiceCollection();
collection.AddBinance();
var provider = collection.BuildServiceProvider();

var trackerFactory = provider.GetRequiredService<IBinanceTrackerFactory>();

// Create and start the tracker, keep track of the last 10 minutes
var tracker = trackerFactory.CreateTradeTracker(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), period: TimeSpan.FromMinutes(10));
var result = await tracker.StartAsync();
if (!result.Success)
{
    Console.WriteLine(result);
    return;
}

// Create Spectre table
var table = new Table();
table.ShowRowSeparators = true;
table.AddColumn("5 Min Data").AddColumn("-5 Min", x => { x.RightAligned(); })
     .AddColumn("Now", x => { x.RightAligned(); })
     .AddColumn("Dif", x => { x.RightAligned(); });

table.AddRow("Count", "", "", "");
table.AddRow("Average price", "", "", "");
table.AddRow("Average weighted price", "", "", "");
table.AddRow("Buy/Sell Ratio", "", "", "");
table.AddRow("Volume", "", "", "");
table.AddRow("Value", "", "", "");
table.AddRow("Complete", "", "", "");
table.AddRow("", "", "", "");
table.AddRow("Status", "", "", "");
table.AddRow("Synced From", "", "", "");

// Set default culture for currency display
CultureInfo ci = new CultureInfo("en-US");
Thread.CurrentThread.CurrentCulture = ci;
Thread.CurrentThread.CurrentUICulture = ci;

await AnsiConsole.Live(table)
    .StartAsync(async ctx =>
    {
        while (true)
        {
            // Get the stats from 10 minutes until 5 minutes ago
            var secondLastMinute = tracker.GetStats(DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(-5));

            // Get the stats from 5 minutes ago until now
            var lastMinute = tracker.GetStats(DateTime.UtcNow.AddMinutes(-5));

            // Get the differences between them
            var compare = secondLastMinute.CompareTo(lastMinute);

            // Update the columns
            UpdateDec(0, 1, secondLastMinute.TradeCount);
            UpdateDec(0, 2, lastMinute.TradeCount);
            UpdateStr(0, 3, $"[{(compare.TradeCountDif.Difference < 0 ? "red" : "green")}]{compare.TradeCountDif.Difference} / {compare.TradeCountDif.PercentageDifference}%[/]");

            UpdateStr(1, 1, secondLastMinute.AveragePrice?.ToString("C"));
            UpdateStr(1, 2, lastMinute.AveragePrice?.ToString("C"));
            UpdateStr(1, 3, $"[{(compare.AveragePriceDif?.Difference < 0 ? "red" : "green")}]{compare.AveragePriceDif?.Difference?.ToString("C")} / {compare.AveragePriceDif?.PercentageDifference}%[/]");

            UpdateStr(2, 1, secondLastMinute.VolumeWeightedAveragePrice?.ToString("C"));
            UpdateStr(2, 2, lastMinute.VolumeWeightedAveragePrice?.ToString("C"));
            UpdateStr(2, 3, $"[{(compare.VolumeWeightedAveragePriceDif?.Difference < 0 ? "red" : "green")}]{compare.VolumeWeightedAveragePriceDif?.Difference?.ToString("C")} / {compare.VolumeWeightedAveragePriceDif?.PercentageDifference}%[/]");

            UpdateDec(3, 1, secondLastMinute.BuySellRatio);
            UpdateDec(3, 2, lastMinute.BuySellRatio);
            UpdateStr(3, 3, $"[{(compare.BuySellRatioDif?.Difference < 0 ? "red" : "green")}]{compare.BuySellRatioDif?.Difference} / {compare.BuySellRatioDif?.PercentageDifference}%[/]");

            UpdateDec(4, 1, secondLastMinute.Volume);
            UpdateDec(4, 2, lastMinute.Volume);
            UpdateStr(4, 3, $"[{(compare.VolumeDif.Difference < 0 ? "red" : "green")}]{compare.VolumeDif.Difference} / {compare.VolumeDif.PercentageDifference}%[/]");

            UpdateStr(5, 1, secondLastMinute.QuoteVolume.ToString("C"));
            UpdateStr(5, 2, lastMinute.QuoteVolume.ToString("C"));
            UpdateStr(5, 3, $"[{(compare.QuoteVolumeDif.Difference < 0 ? "red" : "green")}]{compare.QuoteVolumeDif.Difference?.ToString("C")} / {compare.QuoteVolumeDif.PercentageDifference}%[/]");

            UpdateStr(6, 1, secondLastMinute.Complete.ToString());
            UpdateStr(6, 2, lastMinute.Complete.ToString());

            UpdateStr(8, 1, tracker.Status.ToString());
            UpdateStr(9, 1, tracker.SyncedFrom?.ToString());

            ctx.Refresh();
            await Task.Delay(500);
        }
    });


void UpdateDec(int row, int col, decimal? val)
{
    table.UpdateCell(row, col, val?.ToString() ?? string.Empty);
}

void UpdateStr(int row, int col, string? val)
{
    table.UpdateCell(row, col, val ?? string.Empty);
}
