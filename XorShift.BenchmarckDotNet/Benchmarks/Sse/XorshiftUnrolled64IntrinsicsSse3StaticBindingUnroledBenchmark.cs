using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroledBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroled>
    {
    }
}