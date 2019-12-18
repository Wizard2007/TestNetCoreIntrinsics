using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64ExUnroledBenchmark : XorshiftCommonBenchmark
    {
        #region Private fields

        
        private XorshiftUnrolled64ExUnroled _xorshiftUnrolled64ExUnroled = new XorshiftUnrolled64ExUnroled();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64ExUnroled.NextBytes(_buffer);

        #endregion
    }
}