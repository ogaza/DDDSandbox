namespace FactoryAndStrategyPatternExample.Strategy
{
    public class UsSummaryGeneratingStrategy : ISummaryGeneratingStrategy
    {
        public ReportSummary GenerateReportSummary(Report report)
        {
            return new ReportSummary();
        }
    }
}