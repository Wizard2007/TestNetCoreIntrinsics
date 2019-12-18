using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64EmptyBenchmark : XorshiftCommonBenchmark
    {
        #region Private fields

        
        private XorshiftUnrolled64Empty _xorshiftUnrolled64Empty = new XorshiftUnrolled64Empty();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64Empty.NextBytes(_buffer);

        #endregion
    }
}