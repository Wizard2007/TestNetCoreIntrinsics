using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64ExBenchmark
    {
        #region Private fields

        private byte[] _buffer = new byte[10*1024*1024];
        
        private XorshiftUnrolled64Ex _xorshiftUnrolled64Ex = new XorshiftUnrolled64Ex();

        #endregion

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshiftUnrolled64Ex.NextBytes(_buffer);


        #endregion
    }
}