using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    public class XorshiftCommonBenchmark<T> where T  : Random, new() 
    {
        #region Private fields

        private byte[] _buffer = new byte[0];

        protected T _xorshift = new T();

        #endregion

        [ParamsSource(nameof(Buffers))]
        public int N { get; set; }
        public IEnumerable<int> Buffers => new[] {
            1000, 5000, 
            10000, 50000,
            100000, 500000,
            1000000, 5000000,
            10000000, 20000000,

            1024, 5*1024, 
            10*1024, 50*1024, 
            100*1024, 500*1024,
            1024*1024, 5*1024*1024,
            10*1024*1024, 20*1024*1024
        };

        #region Benchmarks

        [Benchmark]
        public void TestFillBuffer()
            => _xorshift.NextBytes(_buffer);

        #endregion

        #region Iteration Setup

        [IterationSetup]
        public void IterationSetup()
            => _buffer = (_buffer?.Length ?? 0) != N ? new byte[N] : _buffer;
        
        #endregion
    }
}