using FactoryAndStrategyPatternExample.Strategy;

namespace FactoryAndStrategyPatternExample.Factory
{
    public class EuReportSummaryGenerator : ReportSummaryGenerator
    {
        public EuReportSummaryGenerator(ISummaryGeneratingStrategy strategy) : base(strategy)
        {
        }
    }
}