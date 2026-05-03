using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Models
{
    /// Модель данных о медианной заработной плате
    public class SalaryRecord : IAnalyzable
    {
        public int Year { get; set; }

        /// Медианная зарплата мужчин (руб.)
        public double MaleSalary { get; set; }

        /// Медианная зарплата женщин (руб.)
        public double FemaleSalary { get; set; }

        public string GetPeriodLabel() => Year.ToString();

        // Первичное значение
        public double GetPrimaryValue() => (MaleSalary + FemaleSalary) / 2.0;
    }
}
