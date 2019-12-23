using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics.Store.Sse;

namespace XorShift.BenchmarckDotNet.Benchmarks.Strore
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class Sse3UnroledStoreGlobalArrayBenchmark : XorshiftCommonBenchmark<Sse3UnroledStoreGlobalArray>
    {
    }
}