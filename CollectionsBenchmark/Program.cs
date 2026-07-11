using BenchmarkDotNet.Running;
using SerializersBenchmark;

// Раскомментируйте нужный бенчмарк для запуска:
// BenchmarkRunner.Run<Lists>();
// BenchmarkRunner.Run<Dictionaries>();
BenchmarkRunner.Run<Arrays>();

Console.WriteLine("done!");
Console.ReadLine();