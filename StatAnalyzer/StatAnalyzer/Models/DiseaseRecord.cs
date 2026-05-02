using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Models
{
   
    /// Модель данных об инфекционных заболеваниях 
    public class DiseaseRecord : IAnalyzable
    {
        public int Year { get; set; }

        /// Название инфекции
        public string DiseaseName { get; set; }

        /// Число заболевших (чел. на 100 000 населения)
        public double Incidence { get; set; }

        public string GetPeriodLabel() => Year.ToString();
        public double GetPrimaryValue() => Incidence;
    }
}
