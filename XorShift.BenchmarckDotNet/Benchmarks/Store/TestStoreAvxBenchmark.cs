using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks.Store
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class TestStoreAvxBenchmark : XorshiftCommonBenchmark<TestStoreAvx>
    {
    }
}