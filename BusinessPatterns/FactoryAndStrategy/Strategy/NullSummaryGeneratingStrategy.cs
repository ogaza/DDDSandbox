namespace FactoryAndStrategyPatternExample.Strategy
{
    public class NullSummaryGeneratingStrategy : ISummaryGeneratingStrategy
    {
        public ReportSummary GenerateReportSummary(Report report)
        {
            return new ReportSummary();
        }
    }
}