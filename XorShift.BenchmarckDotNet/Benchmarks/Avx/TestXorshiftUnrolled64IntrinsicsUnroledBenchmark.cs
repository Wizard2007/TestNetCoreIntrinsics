
using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Benchmarks.Common;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class TestXorshiftUnrolled64IntrinsicsUnroledBenchmark : XorshiftCommonBenchmark<TestXorshiftUnrolled64IntrinsicsUnroled>
    {
    }
}