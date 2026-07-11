# CollectionsBenchmark

Small list: 100 elements
Large list: 5000 elements


### Lists

- Sized lists are slighter faster than unplanned lists
- Unplanned lists allocate more memory

```
BenchmarkDotNet v0.15.2, Windows 11 (10.0.26200.8655)
AMD Ryzen 5 7600X 4.70GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.301
  [Host]     : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                          | Mean        | Error     | StdDev      | Ratio | RatioSD | Rank | Gen0    | Gen1    | Allocated | Alloc Ratio |
|-------------------------------- |------------:|----------:|------------:|------:|--------:|-----:|--------:|--------:|----------:|------------:|
| Small_List_PlannedCapacity      |    479.3 ns |   9.14 ns |    12.81 ns |  0.80 |    0.03 |    1 |  0.5293 |  0.0172 |   8.65 KB |        0.87 |
| Small_List_DynamicCapacity      |    597.3 ns |  11.71 ns |    18.24 ns |  1.00 |    0.04 |    2 |  0.6084 |  0.0200 |   9.95 KB |        1.00 |
| Large_List_PlannedCapacity      | 24,915.0 ns | 488.13 ns |   842.01 ns | 41.75 |    1.87 |    3 | 26.3062 | 13.1226 | 429.74 KB |       43.18 |
| Large_List_BelowPlannedCapacity | 27,273.2 ns | 539.26 ns |   790.45 ns | 45.70 |    1.89 |    4 | 29.9988 | 19.9585 | 490.04 KB |       49.23 |
| Large_List_DynamicCapacity      | 31,137.8 ns | 617.99 ns | 1,303.55 ns | 52.18 |    2.67 |    5 | 31.7383 | 15.8386 | 518.91 KB |       52.14 |
```

### Dictionaries

- Sized dictionaries are faster than unplanned dictionaries
- Unplanned dictionaries allocates more memory

```
BenchmarkDotNet v0.15.2, Windows 11 (10.0.26200.8655)
AMD Ryzen 5 7600X 4.70GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.301
  [Host]     : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                                | Mean         | Error       | StdDev      | Ratio  | RatioSD | Rank | Gen0     | Gen1     | Gen2     | Allocated | Alloc Ratio |
|-------------------------------------- |-------------:|------------:|------------:|-------:|--------:|-----:|---------:|---------:|---------:|----------:|------------:|
| Small_Dictionary_PlannedCapacity      |     511.2 ns |     8.97 ns |     8.39 ns |   0.52 |    0.02 |    1 |   0.6599 |   0.0257 |        - |  10.79 KB |        0.61 |
| Small_Dictionary_DynamicCapacity      |     981.5 ns |    19.40 ns |    34.48 ns |   1.00 |    0.05 |    2 |   1.0815 |   0.0477 |        - |  17.69 KB |        1.00 |
| Large_Dictionary_PlannedCapacity      |  70,320.4 ns | 1,818.78 ns | 5,039.84 ns |  71.73 |    5.68 |    3 |  43.4570 |  43.4570 |  43.4570 | 550.35 KB |       31.12 |
| Large_Dictionary_DynamicCapacity      |  96,593.4 ns | 1,919.72 ns | 1,795.70 ns |  98.54 |    3.84 |    4 |  90.8203 |  90.8203 |  90.8203 | 831.42 KB |       47.01 |
| Large_Dictionary_BelowPlannedCapacity | 107,421.4 ns | 2,127.14 ns | 6,034.36 ns | 109.58 |    7.20 |    5 | 111.0840 | 111.0840 | 111.0840 | 917.85 KB |       51.89 |
```

## Arrays

- Allocate arrays with ArrayPool shared buffer is faster than in regular arrays
- Allocate small (or large) primitive (or reference) type arrays with ArrayPool shared buffer are fast and have similar performance
- Allocate larger arrays with ArrayPool are faster than regular array
- Allocate small (or large) primitive type arrays with StackAlloc or Span are faster

```
BenchmarkDotNet v0.15.2, Windows 11 (10.0.26200.8655)
AMD Ryzen 5 7600X 4.70GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.301
  [Host]     : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 10.0.9 (10.0.926.27113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method                              | Mean       | Error     | StdDev     | Median     | Ratio  | RatioSD | Rank | Gen0   | Allocated | Alloc Ratio |
|------------------------------------ |-----------:|----------:|-----------:|-----------:|-------:|--------:|-----:|-------:|----------:|------------:|
| Small_Primitive_SpanStackAlloc      |   2.018 ns | 0.0052 ns |  0.0044 ns |   2.018 ns |   0.97 |    0.03 |    1 |      - |         - |          NA |
| Small_Primitive_UnsafeStackAlloc    |   2.037 ns | 0.0066 ns |  0.0055 ns |   2.036 ns |   0.98 |    0.03 |    1 |      - |         - |          NA |
| Small_Primitive_Array               |   2.072 ns | 0.0604 ns |  0.0719 ns |   2.046 ns |   1.00 |    0.05 |    1 |      - |         - |          NA |
| Large_Primitive_SharedArrayPool     |   6.156 ns | 0.0092 ns |  0.0082 ns |   6.154 ns |   2.97 |    0.10 |    2 |      - |         - |          NA |
| Small_Primitive_SharedArrayPool     |   6.157 ns | 0.0149 ns |  0.0125 ns |   6.156 ns |   2.97 |    0.10 |    2 |      - |         - |          NA |
| Large_ReferenceType_SharedArrayPool |   7.390 ns | 0.0684 ns |  0.0607 ns |   7.357 ns |   3.57 |    0.12 |    3 |      - |         - |          NA |
| Small_ReferenceType_SharedArrayPool |   7.508 ns | 0.0089 ns |  0.0079 ns |   7.509 ns |   3.63 |    0.12 |    3 |      - |         - |          NA |
| Small_ReferenceType_Array           |  10.260 ns | 0.0654 ns |  0.0612 ns |  10.275 ns |   4.96 |    0.16 |    4 | 0.0492 |     824 B |          NA |
| Large_Primitive_SpanStackAlloc      | 120.653 ns | 0.8206 ns |  0.7275 ns | 120.825 ns |  58.28 |    1.91 |    5 |      - |         - |          NA |
| Large_Primitive_UnsafeStackAlloc    | 121.243 ns | 0.7480 ns |  0.6631 ns | 121.387 ns |  58.57 |    1.91 |    5 |      - |         - |          NA |
| Large_Primitive_Array               | 201.115 ns | 2.9552 ns |  2.7643 ns | 199.663 ns |  97.15 |    3.39 |    6 | 1.1947 |   20024 B |          NA |
| Large_ReferenceType_Array           | 432.440 ns | 8.3614 ns | 20.0334 ns | 424.697 ns | 208.89 |   11.74 |    7 | 2.3866 |   40024 B |          NA |
```