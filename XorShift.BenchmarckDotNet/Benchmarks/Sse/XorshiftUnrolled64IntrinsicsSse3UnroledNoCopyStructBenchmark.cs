
using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64IntrinsicsSse3UnroledNoCopyStructBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled64IntrinsicsSse3UnroledNoCopyStruct>
    {
    }
}