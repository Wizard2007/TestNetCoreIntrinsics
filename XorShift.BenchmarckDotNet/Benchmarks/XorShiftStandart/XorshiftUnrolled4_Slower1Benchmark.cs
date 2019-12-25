using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled4_Slower1Benchmark : XorshiftCommonBenchmark<XorshiftUnrolled4_Slower1>
    {
    }
}