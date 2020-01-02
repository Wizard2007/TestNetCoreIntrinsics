using BenchmarkDotNet.Attributes;
using XorShift.BenchmarckDotNet.Configs;
using XorShift.Intrinsics;

namespace XorShift.BenchmarckDotNet.Benchmarks
{    
    [DisassemblyDiagnoser(printAsm: true, printSource: true, printIL: true, printPrologAndEpilog: true, recursiveDepth: 5, printDiff: true )]
    [Config(typeof(CommonXorshiftBenchmarkConfig))]
    public class XorshiftUnrolled64IntrinsicsSse3UnroledFluentBenchmark : XorshiftCommonBenchmark<XorshiftUnrolled64IntrinsicsSse3UnroledFluent>
    {
    }
}