using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64IntrinsicsAvx2UnroledBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled64IntrinsicsAvx2Unroled>
    {
    }
}