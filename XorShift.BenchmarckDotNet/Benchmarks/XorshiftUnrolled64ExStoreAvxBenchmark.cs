using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64ExStoreAvxBenchmark : XorshiftCommonBenchmark
    {
        #region Private fields

        
        private XorshiftUnrolled64ExStoreAvx _xorshiftUnrolled64ExStoreAvx = new XorshiftUnrolled64ExStoreAvx();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64ExStoreAvx.NextBytes(_buffer);

        #endregion
    }
}