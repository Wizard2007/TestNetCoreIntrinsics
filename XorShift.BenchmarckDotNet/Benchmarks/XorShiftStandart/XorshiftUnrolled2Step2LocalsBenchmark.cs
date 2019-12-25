using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled2Step2LocalsBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled2Step2Locals>
    {
    }
}