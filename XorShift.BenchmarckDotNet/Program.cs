using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using XorShift.BenchmarckDotNet.Benchmarks;
using XorShift.BenchmarckDotNet.Benchmarks.Avx;
using XorShift.BenchmarckDotNet.Benchmarks.Common;
using XorShift.BenchmarckDotNet.Benchmarks.Sse;
using XorShift.BenchmarckDotNet.Benchmarks.Strore;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;
using XorShift.Intrinsics.Store.Sse;

namespace XorShift.BenchmarckDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = CommonXorshiftBenchmarkConfig.Create(DefaultConfig.Instance);

            BenchmarkRunner.Run(new[]{ 
                BenchmarkConverter.TypeToBenchmarks( typeof(TestEmptyLoopBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(TestStoreAvxBenchmark), config),
                
                //BenchmarkConverter.TypeToBenchmarks( typeof(TestStoreAvx2Benchmark), config),                
                //BenchmarkConverter.TypeToBenchmarks( typeof(TestXorshiftUnrolled64IntrinsicsBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(TestXorshiftUnrolled64IntrinsicsUnroledBenchmark), config),                
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64Benchmark), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsAvx2UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsSse2UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsSse3UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsSse41UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsSse42UnroledBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(XorshiftUnrolled64IntrinsicsSse3UnroledNoCopyStructBenchmark), config)
                //BenchmarkConverter.TypeToBenchmarks( typeof(Sse3UnroledStoreGlobalArrayBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(Sse3UnroledStoreLocalArrayBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(Sse3UnroledStoreStackAllocBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks( typeof(Sse3UnroledStoreStackAllocBufBenchmark), config)

                });
        }
    }
}
