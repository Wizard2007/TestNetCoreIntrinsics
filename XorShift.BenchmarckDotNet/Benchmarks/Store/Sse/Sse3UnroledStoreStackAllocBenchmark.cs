using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Benchmarks.Common;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics.Store.Sse;

namespace XorShift.BenchmarckDotNet.Benchmarks.Strore
{
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class Sse3UnroledStoreStackAllocBenchmark : XorshiftCommonBenchmark<Sse3UnroledStoreStackAlloc>
    {
    }
}