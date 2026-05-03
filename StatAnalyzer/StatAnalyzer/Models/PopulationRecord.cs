using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Models
{
    // Модель данных о численности населения России (вариант 5).
    // Реализует IAnalyzable — полиморфизм при отображении данных.
    public class PopulationRecord : IAnalyzable
    {
        public int Year { get; set; }

        public double Population { get; set; }

        public string GetPeriodLabel() => Year.ToString();
        public double GetPrimaryValue() => Population;
    }
}
