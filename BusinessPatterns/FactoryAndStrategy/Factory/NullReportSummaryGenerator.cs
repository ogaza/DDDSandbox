using FactoryAndStrategyPatternExample.Strategy;

namespace FactoryAndStrategyPatternExample.Factory
{
    public class NullReportSummaryGenerator : ReportSummaryGenerator
    {
        public NullReportSummaryGenerator(ISummaryGeneratingStrategy strategy) : base(strategy)
        {
        }
    }
}