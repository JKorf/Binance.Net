# Binance.Net vs CCXT and Binance.Api

This document compares measured `Binance.Net`, `Binance.Net` shared interfaces, `CCXT`, and `Binance.Api` performance for C#/.NET users building against Binance REST and WebSocket APIs.

The results show a consistent advantage for `Binance.Net`. It has the lowest allocation profile in every measured scenario, leads the server-time REST tests, is very competitive on ticker requests, and has the strongest WebSocket trade-ingestion results by a wide margin.

The shared-interface results are especially useful when comparing against CCXT's unified API style. `Binance.Net` shared clients keep most of the performance of the direct Binance.Net clients while still exposing exchange-agnostic abstractions.

## Benchmark source

The benchmark source is available in `Benchmark/Client/Program.cs`. It contains the direct Binance.Net benchmarks, shared-interface benchmarks, CCXT benchmarks, and `Binance.Api` benchmarks in one BenchmarkDotNet project: `Benchmark/Client/Binance.Net.Benchmark.Client.csproj`.

The benchmarks run against the local mock Binance server in `Benchmark/Server`, including the REST and WebSocket mock endpoints in `Benchmark/Server/Controllers/RestController.cs` and `Benchmark/Server/Controllers/WsController.cs`.

## Summary

| Area | Result |
|---|---|
| REST server time on .NET 10 | `Binance.Net` is fastest; CCXT is 1.04x slower and `Binance.Api` is 1.07x slower |
| REST server time on .NET Framework 4.8 | `Binance.Net` is fastest; CCXT is 1.07x slower and `Binance.Api` is 1.11x slower |
| REST ticker on .NET 10 | Direct `Binance.Net` is fastest; `Binance.Net` shared is close and allocates far less than CCXT |
| REST ticker on .NET Framework 4.8 | `Binance.Api` has the lowest mean, but direct and shared `Binance.Net` allocate much less memory |
| WebSocket trade stream on .NET 10 | Direct and shared `Binance.Net` are effectively tied; CCXT is 9.67x slower and `Binance.Api` is 3.06x slower |
| WebSocket trade stream on .NET Framework 4.8 | `Binance.Net` is fastest; shared `Binance.Net` is 1.17x slower, `Binance.Api` is 1.87x slower, and CCXT is 4.25x slower |
| Unified/shared API comparison | `Binance.Net` shared interfaces outperform CCXT unified results by a wide margin |
| Allocation profile | Direct `Binance.Net` allocates the least memory in every measured scenario; shared `Binance.Net` stays close |

The REST results show that `Binance.Net` has very low request/response overhead and consistently lower memory usage. The WebSocket results show a much larger difference under sustained message processing, where parsing, dispatch, and allocation behavior dominate.

### REST comparison

| Method | Runtime | Mean | Error | StdDev | Ratio | RatioSD | Gen0 | Gen1 | Allocated | Alloc Ratio |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| BinanceNet_ServerTime | .NET 10.0 | 120.9 ms | 1.48 ms | 1.98 ms | 1.00 | 0.02 | 250.0000 | - | 4.48 MB | 1.00 |
| CCXT_ServerTime | .NET 10.0 | 125.9 ms | 1.77 ms | 2.30 ms | 1.04 | 0.02 | 1000.0000 | - | 13.49 MB | 3.01 |
| BinanceApi_ServerTime | .NET 10.0 | 129.0 ms | 2.03 ms | 2.71 ms | 1.07 | 0.03 | 1000.0000 | - | 12.22 MB | 2.73 |
| BinanceNet_Ticker | .NET 10.0 | 136.6 ms | 3.08 ms | 4.00 ms | 1.13 | 0.04 | 333.3333 | - | 5.79 MB | 1.29 |
| BinanceNetShared_Ticker | .NET 10.0 | 141.1 ms | 2.24 ms | 2.99 ms | 1.17 | 0.03 | 333.3333 | - | 6.43 MB | 1.43 |
| CCXT_Ticker | .NET 10.0 | 147.0 ms | 2.65 ms | 3.53 ms | 1.22 | 0.03 | 2000.0000 | - | 25.91 MB | 5.79 |
| BinanceApi_Ticker | .NET 10.0 | 152.0 ms | 4.63 ms | 6.19 ms | 1.26 | 0.05 | 1500.0000 | - | 19.52 MB | 4.36 |
| BinanceNet_ServerTime | .NET Framework 4.8 | 167.0 ms | 3.09 ms | 4.12 ms | 1.00 | 0.03 | 2500.0000 | - | 15.9 MB | 1.00 |
| CCXT_ServerTime | .NET Framework 4.8 | 179.2 ms | 2.08 ms | 2.78 ms | 1.07 | 0.03 | 4666.6667 | - | 29.86 MB | 1.88 |
| BinanceApi_ServerTime | .NET Framework 4.8 | 185.0 ms | 2.48 ms | 3.31 ms | 1.11 | 0.03 | 4333.3333 | 333.3333 | 26.38 MB | 1.66 |
| BinanceApi_Ticker | .NET Framework 4.8 | 188.8 ms | 2.57 ms | 3.34 ms | 1.13 | 0.03 | 6000.0000 | 333.3333 | 36.4 MB | 2.29 |
| BinanceNet_Ticker | .NET Framework 4.8 | 193.3 ms | 3.72 ms | 4.96 ms | 1.16 | 0.04 | 2750.0000 | - | 17.28 MB | 1.09 |
| BinanceNetShared_Ticker | .NET Framework 4.8 | 199.2 ms | 4.14 ms | 5.38 ms | 1.19 | 0.04 | 3000.0000 | - | 18.51 MB | 1.16 |
| CCXT_Ticker | .NET Framework 4.8 | 201.6 ms | 3.92 ms | 5.23 ms | 1.21 | 0.04 | 7000.0000 | 333.3333 | 42.46 MB | 2.67 |

### REST findings

For server-time requests, `Binance.Net` is the clear leader in both time and memory:

- .NET 10: 120.9 ms and 4.48 MB for `Binance.Net`, compared with 125.9 ms and 13.49 MB for CCXT, and 129.0 ms and 12.22 MB for `Binance.Api`
- .NET Framework 4.8: 167.0 ms and 15.9 MB for `Binance.Net`, compared with 179.2 ms and 29.86 MB for CCXT, and 185.0 ms and 26.38 MB for `Binance.Api`

For ticker requests, direct `Binance.Net` is fastest on .NET 10 and has the lowest allocation profile on both runtimes:

- .NET 10: direct `Binance.Net` completes in 136.6 ms and allocates 5.79 MB. Shared `Binance.Net` completes in 141.1 ms and allocates 6.43 MB. CCXT takes 147.0 ms and allocates 25.91 MB.
- .NET Framework 4.8: `Binance.Api` has the lowest mean at 188.8 ms, but allocates 36.4 MB. Direct `Binance.Net` is close at 193.3 ms while allocating only 17.28 MB. Shared `Binance.Net` takes 199.2 ms and allocates 18.51 MB, still less than half of CCXT's 42.46 MB allocation.

The shared-interface ticker result is the most direct REST comparison with CCXT's unified approach. On .NET 10, shared `Binance.Net` is faster than CCXT and allocates about 75% less memory. On .NET Framework 4.8, shared `Binance.Net` is close on mean time and allocates less than half as much memory as CCXT.

## WebSocket comparison

| Method | Runtime | Mean | Error | StdDev | Ratio | RatioSD | Gen0 | Gen1 | Gen2 | Allocated | Alloc Ratio |
|---|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|
| BinanceNet_Trades | .NET 10.0 | 171.4 ms | 5.27 ms | 7.04 ms | 1.00 | 0.06 | 8000.0000 | - | - | 105.3 MB | 1.00 |
| BinanceNetShared_Trades | .NET 10.0 | 172.7 ms | 6.04 ms | 8.06 ms | 1.01 | 0.06 | 11000.0000 | - | - | 132.32 MB | 1.26 |
| BinanceApi_Trades | .NET 10.0 | 524.0 ms | 5.01 ms | 6.15 ms | 3.06 | 0.13 | 100000.0000 | 2000.0000 | - | 1206.43 MB | 11.46 |
| CCXT_Trades | .NET 10.0 | 1,654.3 ms | 67.50 ms | 90.11 ms | 9.67 | 0.65 | 405000.0000 | 51000.0000 | - | 4537.48 MB | 43.09 |
| BinanceNet_Trades | .NET Framework 4.8 | 821.6 ms | 6.57 ms | 8.30 ms | 1.00 | 0.01 | 34000.0000 | - | - | 204.23 MB | 1.00 |
| BinanceNetShared_Trades | .NET Framework 4.8 | 961.9 ms | 71.60 ms | 95.59 ms | 1.17 | 0.11 | 38000.0000 | - | - | 233.12 MB | 1.14 |
| BinanceApi_Trades | .NET Framework 4.8 | 1,538.3 ms | 10.58 ms | 14.13 ms | 1.87 | 0.03 | 236000.0000 | 1000.0000 | - | 1422.22 MB | 6.96 |
| CCXT_Trades | .NET Framework 4.8 | 3,494.1 ms | 73.85 ms | 93.39 ms | 4.25 | 0.12 | 790000.0000 | 198000.0000 | 3000.0000 | 4733.23 MB | 23.18 |

### WebSocket findings

The WebSocket benchmark is the clearest result. On .NET 10, direct `Binance.Net` completes the trade benchmark in 171.4 ms, and shared `Binance.Net` is effectively tied at 172.7 ms. `Binance.Api` takes 524.0 ms, and CCXT takes 1,654.3 ms. That makes `Binance.Api` 3.06x slower and CCXT 9.67x slower than the direct Binance.Net socket path.

The allocation difference is even larger. On .NET 10, direct `Binance.Net` allocates 105.3 MB and shared `Binance.Net` allocates 132.32 MB, while `Binance.Api` allocates 1,206.43 MB and CCXT allocates 4,537.48 MB. On .NET Framework 4.8, direct `Binance.Net` allocates 204.23 MB and shared `Binance.Net` allocates 233.12 MB, while `Binance.Api` allocates 1,422.22 MB and CCXT allocates 4,733.23 MB.

For market-data consumers, this is the most important part of the comparison. WebSocket workloads run continuously, often across many symbols. Higher allocation pressure increases GC activity, and slower dispatch reduces headroom for strategy logic, persistence, aggregation, and downstream event processing.

The shared-interface socket result is important for multi-exchange designs. It shows that the exchange-agnostic Binance.Net path remains close to the direct path, while CCXT's unified socket path is much slower and allocates dramatically more memory in this benchmark.
