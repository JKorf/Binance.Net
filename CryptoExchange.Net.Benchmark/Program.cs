using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonConverter(typeof(EnumConverter<TestEnum>))]
public enum TestEnum
{
    [Map("Value 1")]
    Value1,
    [Map("Value 2")]
    Value2,
    [Map("Value 3")]
    Value3,
    [Map("Value 4")]
    Value4,
    [Map("Value 5")]
    Value5,
}

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class Tester
{
    private JsonSerializerOptions _oldOptions;

    [GlobalSetup(Targets = [nameof(TestUnmatchingCapitalizationOptimized), nameof(TestMatchingCapitalizationOptimized)])]
    public void SetupOptimized()
    {
        EnumConverter<TestEnum>.UseOptimistic = true;
        EnumConverter<TestEnum>.GetString(TestEnum.Value5); // initialize mapping to exclude from benchmark
        EnumConverterOld<TestEnum>.GetString(TestEnum.Value5); // initialize mapping to exclude from benchmark
        _oldOptions = new JsonSerializerOptions
        {
            Converters = { new EnumConverterOld<TestEnum>() }
        };
    }

    [GlobalSetup(Targets = [nameof(TestUnmatchingCapitalizationNotOptimized), nameof(TestMatchingCapitalizationNotOptimized)])]
    public void SetupNotOptimized()
    {
        EnumConverter<TestEnum>.UseOptimistic = false;
        EnumConverter<TestEnum>.GetString(TestEnum.Value5); // initialize mapping to exclude from benchmark
        EnumConverterOld<TestEnum>.GetString(TestEnum.Value5); // initialize mapping to exclude from benchmark
        _oldOptions = new JsonSerializerOptions
        {
            Converters = { new EnumConverterOld<TestEnum>() }
        };
    }

    [Benchmark()]
    public async Task TestMatchingCapitalizationOld()
    {
        var json = "\"Value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json);
    }

    [Benchmark()]
    public async Task TestMatchingCapitalizationNotOptimized()
    {
        var json = "\"Value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json);
    }

    [Benchmark()]
    public async Task TestMatchingCapitalizationOptimized()
    {
        var json = "\"Value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json);
    }

    [Benchmark()]
    public async Task TestUnmatchingCapitalizationOld()
    {
        var json = "\"value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json, _oldOptions);
    }


    [Benchmark()]
    public async Task TestUnmatchingCapitalizationNotOptimized()
    {
        var json = "\"value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json);
    }

    [Benchmark()]
    public async Task TestUnmatchingCapitalizationOptimized()
    {
        var json = "\"value 3\"";
        var result = JsonSerializer.Deserialize<TestEnum>(json);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        for(var i = 0; i < 10; i++)
        {
            EnumConverter<TestEnum>.UseOptimistic = true;
            var json = "\"value 3\"";
            var result = JsonSerializer.Deserialize<TestEnum>(json);
        }


        //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
