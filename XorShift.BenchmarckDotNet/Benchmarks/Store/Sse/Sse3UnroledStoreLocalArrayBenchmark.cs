using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics.Store.Sse;

namespace XorShift.BenchmarckDotNet.Benchmarks.Strore
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class Sse3UnroledStoreLocalArrayBenchmark : XorshiftCommonBenchmark<Sse3UnroledStoreLocalArray>
    {
    }
}