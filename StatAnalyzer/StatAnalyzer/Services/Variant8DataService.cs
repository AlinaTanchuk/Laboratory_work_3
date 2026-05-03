using System;
using System.Collections.Generic;
using System.Linq;
using StatAnalyzer.Models;

namespace StatAnalyzer.Services
{
    /// Сервис - медианная заработная плата за 15 лет.
    public class Variant8DataService : BaseDataService<SalaryRecord>
    {
        /// Вычислить максимальный и минимальный % роста/падения зарплат
        /// отдельно для мужчин и женщин.
        public (double maleMax, int maleMaxYear, double maleMin, int maleMinYear,
                double femaleMax, int femaleMaxYear, double femaleMin, int femaleMinYear)
            CalculateSalaryChangeStats(List<SalaryRecord> records)
        {
            if (records == null || records.Count < 2)
                throw new ArgumentException("Нужно минимум 2 записи.");

            var sorted = records.OrderBy(r => r.Year).ToList();

            double maleMax = double.MinValue, maleMin = double.MaxValue;
            double femaleMax = double.MinValue, femaleMin = double.MaxValue;
            int maleMaxYear = 0, maleMinYear = 0, femaleMaxYear = 0, femaleMinYear = 0;

            for (int i = 1; i < sorted.Count; i++)
            {
                var prev = sorted[i - 1];
                var curr = sorted[i];

                if (prev.MaleSalary > 0)
                {
                    double maleChange = (curr.MaleSalary - prev.MaleSalary) / prev.MaleSalary * 100.0;
                    if (maleChange > maleMax) { maleMax = maleChange; maleMaxYear = curr.Year; }
                    if (maleChange < maleMin) { maleMin = maleChange; maleMinYear = curr.Year; }
                }

                if (prev.FemaleSalary > 0)
                {
                    double femaleChange = (curr.FemaleSalary - prev.FemaleSalary) / prev.FemaleSalary * 100.0;
                    if (femaleChange > femaleMax) { femaleMax = femaleChange; femaleMaxYear = curr.Year; }
                    if (femaleChange < femaleMin) { femaleMin = femaleChange; femaleMinYear = curr.Year; }
                }
            }

            return (maleMax, maleMaxYear, maleMin, maleMinYear,
                    femaleMax, femaleMaxYear, femaleMin, femaleMinYear);
        }
    }
}
