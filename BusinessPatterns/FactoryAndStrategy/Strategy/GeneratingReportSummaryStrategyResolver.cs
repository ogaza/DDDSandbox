namespace FactoryAndStrategyPatternExample.Strategy
{
    public class GeneratingReportSummaryStrategyResolver
    {
        public static ISummaryGeneratingStrategy ResolveTableGeneratingStrategy(
            DataOrigin summaryGeneratingStrategy)
        {
            switch (summaryGeneratingStrategy)
            {
                case DataOrigin.Us:
                    return new UsSummaryGeneratingStrategy();
                case DataOrigin.Eu:
                    return new EuSummaryGeneratingStrategy();
                default:
                    return new NullSummaryGeneratingStrategy();
            }
        }
    }
}
