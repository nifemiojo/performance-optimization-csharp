using BenchmarkDotNet.Attributes;

namespace Memory.ParseFromString;
[MemoryDiagnoser]
public class Point3DBenchmarks
{
    private string[] testData;

    [GlobalSetup]
    public void Setup()
    {
        testData = Enumerable.Range(0, 1000000)
            .Select(i => $"({i * 1.1}, {i * 2.2}, {i * 3.3})")
            .ToArray();
    }

    [Benchmark(Baseline = true)]
    public void BaselineParsing()
    {
        foreach (var data in testData)
        {
            Point3D.ParseBaseline(data);
        }
    }

    [Benchmark]
    public void OptimizedParsing()
    {
        foreach (var data in testData)
        {
            Point3D.ParseOptimized(data);
        }
    }
}
