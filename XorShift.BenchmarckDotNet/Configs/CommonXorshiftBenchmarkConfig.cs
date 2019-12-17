using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains.CsProj;

namespace XorShift.BenchmarckDotNet.Configs
{
    public class CommonXorshiftBenchmarkConfig : ManualConfig
    {
        public CommonXorshiftBenchmarkConfig()
        {
            UnionRule = ConfigUnionRule.Union;            
            Options = this.Options | ConfigOptions.JoinSummary;
            SummaryStyle = SummaryStyle.Default.WithMaxParameterColumnWidth(50);
            Add(
                Job.Default
                    .With(RunStrategy.Monitoring)
                    .With(Jit.RyuJit)
                    .With(Platform.X64)
                    .With(CsProjCoreToolchain.NetCoreApp31)
                    .WithIterationCount(10)
                    .WithInvocationCount(32)
                    .WithUnrollFactor(16)
                    .WithLaunchCount(1)
                    .WithWarmupCount(0)
            );

            Add(StatisticColumn.Mean,
                StatisticColumn.Min,
                StatisticColumn.Max,
                StatisticColumn.Error,
                StatisticColumn.StdDev);

            Add(CsvExporter.Default);
            Add(CsvMeasurementsExporter.Default);
            Add(HtmlExporter.Default);
            Add(AsciiDocExporter.Default);
            Add(MarkdownExporter.Default);            
        }
    }
}
