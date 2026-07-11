using System.Buffers;
using System.Threading;
using Application.DTOs;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace SerializersBenchmark;

[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[MemoryDiagnoser]
public class Arrays
{
    [Benchmark(Baseline = true)]
    public void Small_Primitive_Array()
    {
        int[] array = new int[100];
        Volatile.Write(ref array[0], 1);
    }

    [Benchmark]
    public unsafe void Small_Primitive_UnsafeStackAlloc()
    {
        int* vectors = stackalloc int[100];
        Volatile.Write(ref *vectors, 1);
    }

    [Benchmark]
    public void Small_Primitive_SpanStackAlloc()
    {
        Span<int> vectors = stackalloc int[100];
        Volatile.Write(ref vectors[0], 1);
    }

    [Benchmark]
    public unsafe void Large_Primitive_UnsafeStackAlloc()
    {
        int* vectors = stackalloc int[5000];
        Volatile.Write(ref *vectors, 1);
    }

    [Benchmark]
    public void Large_Primitive_SpanStackAlloc()
    {
        Span<int> vectors = stackalloc int[5000];
        Volatile.Write(ref vectors[0], 1);
    }

    [Benchmark]
    public void Small_Primitive_SharedArrayPool()
    {
        var pool = ArrayPool<int>.Shared;
        int[] array = pool.Rent(100);
        pool.Return(array);
    }

    [Benchmark]
    public void Small_ReferenceType_Array()
    {
        Merchant[] array = new Merchant[100];
        GC.KeepAlive(array);
    }

    [Benchmark]
    public void Small_ReferenceType_SharedArrayPool()
    {
        var pool = ArrayPool<Merchant>.Shared;
        Merchant[] array = pool.Rent(100);
        pool.Return(array);
    }

    [Benchmark]
    public void Large_Primitive_Array()
    {
        int[] array = new int[5000];
        Volatile.Write(ref array[0], 1);
    }

    [Benchmark]
    public void Large_Primitive_SharedArrayPool()
    {
        var pool = ArrayPool<int>.Shared;
        int[] array = pool.Rent(5000);
        pool.Return(array);
    }

    [Benchmark]
    public void Large_ReferenceType_Array()
    {
        Merchant[] array = new Merchant[5000];
        GC.KeepAlive(array);
    }

    [Benchmark]
    public void Large_ReferenceType_SharedArrayPool()
    {
        var pool = ArrayPool<Merchant>.Shared;
        Merchant[] array = pool.Rent(5000);
        pool.Return(array);
    }
}
