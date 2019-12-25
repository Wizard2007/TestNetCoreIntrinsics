using System;
using System.Globalization;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace XorShift.BenchmarckDotNet.Benchmarks.Columns
{
    public class AvgGenerationSpeedColumn : IColumn
    {
        public string Id => nameof(AvgGenerationSpeedColumn);

        public string ColumnName => "Avg MB/sec.";

        public string Legend => "Generation speed Mega butes per second";

        public UnitType UnitType => UnitType.Size;

        public bool AlwaysShow => true;

        public ColumnCategory Category => ColumnCategory.Params;

        public int PriorityInCategory => 0;

        public bool IsNumeric => true;

        public bool IsAvailable(Summary summary) => true;

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        {
            var report = summary.Reports.Where(x => x.BenchmarkCase == benchmarkCase).FirstOrDefault();
            var n = benchmarkCase.Parameters.Items.FirstOrDefault(x => x.Name == "N");

            if (n == null)
            {
                return "N/A";
            }

            return Math.Round(1000000000d *(int)n.Value/(1024*1024* report.ResultStatistics.Mean), 2).ToString("#,#.00#", CultureInfo.InvariantCulture);
        }

        public override string ToString() => ColumnName;
    }
}
