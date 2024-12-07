using BenchmarkDotNet.Running;
using Memory.ParseFromString;

namespace Memory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Point3D.ParseOptimized("(1.1, 2.2, 3.3)");
            BenchmarkRunner.Run<Point3DBenchmarks>();
        }
    }
}
