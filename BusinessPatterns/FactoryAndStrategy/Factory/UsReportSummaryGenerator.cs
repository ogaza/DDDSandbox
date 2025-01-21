using FactoryAndStrategyPatternExample.Strategy;

namespace FactoryAndStrategyPatternExample.Factory
{
    public class UsReportSummaryGenerator : ReportSummaryGenerator
    {
        public UsReportSummaryGenerator(ISummaryGeneratingStrategy strategy) : base(strategy)
        {
        }
    }
}