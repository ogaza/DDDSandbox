using FactoryAndStrategyPatternExample.Strategy;

namespace FactoryAndStrategyPatternExample.Factory
{
    public class ReportSummaryGeneratorFactory
    {
        public static IReportSummaryGenerator GetReportGenerator(DataOrigin dataOrigin)
        {
            var strategy = GeneratingReportSummaryStrategyResolver.
                ResolveTableGeneratingStrategy(dataOrigin);

            switch (dataOrigin)
            {
                case DataOrigin.Us:
                    return new UsReportSummaryGenerator(strategy);
                case DataOrigin.Eu:
                    return new EuReportSummaryGenerator(strategy);
                default:
                    return new NullReportSummaryGenerator(strategy);
            }
        }
    }
}