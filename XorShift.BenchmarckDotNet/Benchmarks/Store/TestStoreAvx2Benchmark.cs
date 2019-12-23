using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks.Strore
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class TestStoreAvx2Benchmark : XorshiftCommonBenchmark<TestStoreAvx2>
    {
    }
}