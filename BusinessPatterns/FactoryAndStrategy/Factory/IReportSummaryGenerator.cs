namespace FactoryAndStrategyPatternExample.Factory
{
    public interface IReportSummaryGenerator
    {
        ReportSummary GenerateReportSummary(Report report);
    }
}