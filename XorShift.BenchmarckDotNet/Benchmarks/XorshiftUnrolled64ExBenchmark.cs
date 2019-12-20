using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64ExBenchmark : XorshiftCommonBenchmark
    {
        #region Private fields

        
        private XorshiftUnrolled64Intrinsics _xorshiftUnrolled64Ex = new XorshiftUnrolled64Intrinsics();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64Ex.NextBytes(_buffer);

        #endregion
    }
}