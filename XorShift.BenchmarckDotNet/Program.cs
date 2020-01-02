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
                
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3UnroledFluentBenchmark), config),
                //BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3UnroledFluentWithOutLocalVarBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3UnroledBenchmark), config)
            });
/*
            BenchmarkRunner.Run(new[]{                 

                #region Standart XorShift

                BenchmarkConverter.TypeToBenchmarks(typeof(RandomBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftSafeBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftSafeLocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnsafeSillyBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnsafeBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnsafeLocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled2Step1LocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled2Step2LocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled2Step3LocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled4Benchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled4LocalsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled4_Slower1Benchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled4_Slower2Benchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64Benchmark), config),
                
                #endregion

                #region Intrinsics XorShift

                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsAvx2UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse2UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse41UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse42UnroledBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(XorshiftUnrolled64IntrinsicsSse3UnroledNoCopyStructBenchmark), config)
                
                #endregion
                
                });

            BenchmarkRunner.Run(new[]{ 
                BenchmarkConverter.TypeToBenchmarks(typeof(Sse3UnroledStoreGlobalArrayBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(Sse3UnroledStoreLocalArrayBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(Sse3UnroledStoreStackAllocBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(Sse3UnroledStoreStackAllocBufBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(TestStoreAvxBenchmark), config),                
                BenchmarkConverter.TypeToBenchmarks(typeof(TestStoreAvx2Benchmark), config),                
                BenchmarkConverter.TypeToBenchmarks(typeof(TestXorshiftUnrolled64IntrinsicsBenchmark), config),
                BenchmarkConverter.TypeToBenchmarks(typeof(TestXorshiftUnrolled64IntrinsicsUnroledBenchmark), config),
            });
            BenchmarkRunner.Run(new[]{                 
                BenchmarkConverter.TypeToBenchmarks(typeof(StringTest), config)
            });
            */
        }
    }
}
