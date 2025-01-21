namespace FactoryAndStrategyPatternExample.Strategy
{
    public interface ISummaryGeneratingStrategy
    {
        ReportSummary GenerateReportSummary(Report report);
    }
}