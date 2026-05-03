using System;
using System.Collections.Generic;
using System.Linq;
using StatAnalyzer.Models;

namespace StatAnalyzer.Services
{
    // Сервис для варианта 5: численность населения России за 15 лет.
    // Наследует базовую логику загрузки и скользящей средней.
    public class Variant5DataService : BaseDataService<PopulationRecord>
    {
        // Вычислить максимальный процент прироста и убыли населения за год.
        // Возвращает: (максПрирост%, год), (максУбыль%, год)
        public (double maxGrowth, int maxGrowthYear, double maxDecline, int maxDeclineYear)
            CalculateGrowthStats(List<PopulationRecord> records)
        {
            if (records == null || records.Count < 2)
                throw new ArgumentException("Нужно минимум 2 записи для вычисления изменений.");

            // Сортируем по году на случай неупорядоченных данных
            var sorted = records.OrderBy(r => r.Year).ToList();

            double maxGrowth = double.MinValue;
            double maxDecline = double.MaxValue;
            int maxGrowthYear = 0, maxDeclineYear = 0;

            for (int i = 1; i < sorted.Count; i++)
            {
                double prev = sorted[i - 1].Population;
                double curr = sorted[i].Population;

                if (prev == 0) continue;

                double changePercent = (curr - prev) / prev * 100.0;

                if (changePercent > maxGrowth)
                {
                    maxGrowth = changePercent;
                    maxGrowthYear = sorted[i].Year;
                }

                if (changePercent < maxDecline)
                {
                    maxDecline = changePercent;
                    maxDeclineYear = sorted[i].Year;
                }
            }

            return (maxGrowth, maxGrowthYear, maxDecline, maxDeclineYear);
        }
    }
}
