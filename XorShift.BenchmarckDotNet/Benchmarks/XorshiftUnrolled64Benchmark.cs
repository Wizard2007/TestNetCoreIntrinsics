using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64Benchmark
    {
        #region Private fields

        private byte[] _buffer = new byte[10*1024*1024];
        private XorshiftUnrolled64 _xorshiftUnrolled64 = new XorshiftUnrolled64();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64.NextBytes(_buffer);


        #endregion
    }
}