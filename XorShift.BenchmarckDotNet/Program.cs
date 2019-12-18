using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using XorShift.BenchmarckDotNet.Benchmarks;
using XorShift.BenchmarckDotNet.Configs;

namespace XorShift.BenchmarckDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = CommonXorshiftBenchmarkConfig.Create(DefaultConfig.Instance);

            BenchmarkRunner.Run(new[]{                
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64EmptyBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64Benchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64ExBenchmark), config),                
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64ExUnroledBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64ExStoreAvxBenchmark), config),                
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64ExStoreAvx2Benchmark), config)

                });
        }
    }
}
