using Application.DTOs;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace SerializersBenchmark;

[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[MemoryDiagnoser]
public class Dictionaries
{
    [Benchmark(Baseline = true)]
    public void Small_Dictionary_DynamicCapacity()
    {
        const int size = 100;
        Dictionary<int, Merchant> dict = new();
        for (int i = 0; i < size; i++)
        {
            dict.Add(i, new Merchant { MerchantId = i });
        }
    }

    [Benchmark]
    public void Small_Dictionary_PlannedCapacity()
    {
        const int size = 100;
        Dictionary<int, Merchant> dict = new(size);
        for (int i = 0; i < size; i++)
        {
            dict.Add(i, new Merchant { MerchantId = i });
        }
    }

    [Benchmark]
    public void Large_Dictionary_DynamicCapacity()
    {
        const int size = 5000;
        Dictionary<int, Merchant> dict = new();
        for (int i = 0; i < size; i++)
        {
            dict.Add(i, new Merchant { MerchantId = i });
        }
    }

    [Benchmark]
    public void Large_Dictionary_PlannedCapacity()
    {
        const int size = 5000;
        Dictionary<int, Merchant> dict = new(size);
        for (int i = 0; i < size; i++)
        {
            dict.Add(i, new Merchant { MerchantId = i });
        }
    }

    [Benchmark]
    public void Large_Dictionary_BelowPlannedCapacity()
    {
        const int size = 100;
        Dictionary<int, Merchant> dict = new(size);
        for (int i = 0; i < 5000; i++)
        {
            dict.Add(i, new Merchant { MerchantId = i });
        }
    }
}