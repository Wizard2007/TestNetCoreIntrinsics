using BenchmarkDotNet.Attributes;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    public class XorshiftCommonBenchmark<T> where T  : Xorshift, new() 
    {
        #region Private fields

        private byte[] _buffer = new byte[10*1024*1024];
        //private byte[] _buffer = new byte[256*1024];
        protected T _xorshift = new T();

        #endregion

                #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshift.NextBytes(_buffer);


        #endregion
    }
}