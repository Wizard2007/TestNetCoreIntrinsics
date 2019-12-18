using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64ExStoreAvx2Benchmark : XorshiftCommonBenchmark
    {
        #region Private fields

        
        private XorshiftUnrolled64ExStoreAvx2 _xorshiftUnrolled64ExStoreAvx2 = new XorshiftUnrolled64ExStoreAvx2();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64ExStoreAvx2.NextBytes(_buffer);

        #endregion
    }
}