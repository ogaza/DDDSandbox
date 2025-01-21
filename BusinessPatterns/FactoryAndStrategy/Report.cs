using System.Data;

namespace FactoryAndStrategyPatternExample
{
    public class Report
    {
        public DataTable Data { get; }

        public Report(DataTable data)
        {
            Data = data;
        }
    }
}