using FactoryAndStrategyPatternExample.Strategy;

namespace FactoryAndStrategyPatternExample.Factory
{
    public abstract class ReportSummaryGenerator : IReportSummaryGenerator
    {
        protected ISummaryGeneratingStrategy Strategy;

        protected ReportSummaryGenerator(ISummaryGeneratingStrategy strategy)
        {
            Strategy = strategy;
        }

        public ReportSummary GenerateReportSummary(Report report)
        {
            return Strategy.GenerateReportSummary(report);
        }
    }
}