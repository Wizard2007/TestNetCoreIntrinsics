using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics.Store.Sse;

namespace XorShift.BenchmarckDotNet.Benchmarks
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class Sse3UnroledStoreStackAllocBufBenchmark : XorshiftCommonBenchmark<Sse3UnroledStoreStackAllocBuf>
    {
    }
}