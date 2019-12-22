using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks.Common
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class TestEmptyLoopBenchmark : XorshiftCommonBenchmark<TestEmptyLoop>
    {
    }
}