using System;
using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class RandomBenchmark : XorshiftCommonBenchmark<Random>
    {
    }
}