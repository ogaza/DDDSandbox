namespace FactoryAndStrategyPatternExample.Strategy
{
    public class EuSummaryGeneratingStrategy : ISummaryGeneratingStrategy
    {
        public ReportSummary GenerateReportSummary(Report report)
        {
            return new ReportSummary();
        }
    }
}