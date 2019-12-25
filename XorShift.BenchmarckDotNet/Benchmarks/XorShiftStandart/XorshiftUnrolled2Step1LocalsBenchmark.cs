using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled2Step1LocalsBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled2Step1Locals>
    {
    }
}