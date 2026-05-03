using System;
using System.Collections.Generic;
using System.Linq;
using StatAnalyzer.Models;

namespace StatAnalyzer.Services
{

    public class Variant15DataService : BaseDataService<DiseaseRecord>
    {

        /// Определить, заболеваемость какой инфекцией снизилась больше всего и меньше всего
        /// (сравнение первого и последнего года по каждой болезни)
        public (string mostDeclined, double mostDeclinedPct,
                string leastDeclined, double leastDeclinedPct)
            CalculateDiseaseChangeStats(List<DiseaseRecord> records)
        {
            if (records == null || records.Count < 2)
                throw new ArgumentException("Нужно минимум 2 записи.");

            // Группируем по болезни
            var grouped = records.GroupBy(r => r.DiseaseName);

            var changes = new Dictionary<string, double>();

            foreach (var group in grouped)
            {
                var sorted = group.OrderBy(r => r.Year).ToList();
                if (sorted.Count < 2) continue;

                double first = sorted.First().Incidence;
                double last = sorted.Last().Incidence;

                if (first == 0) continue;

                // Отрицательное значение = снижение
                double changePct = (last - first) / first * 100.0;
                changes[group.Key] = changePct;
            }

            if (changes.Count == 0)
                throw new InvalidOperationException("Нет данных для сравнения.");

            // Больше всего снизилась = самое отрицательное значение
            string mostDeclined = changes.OrderBy(x => x.Value).First().Key;
            double mostDeclinedPct = changes[mostDeclined];

            // Меньше всего снизилась = наименее отрицательное (ближе к 0 или рост)
            string leastDeclined = changes.OrderByDescending(x => x.Value).First().Key;
            double leastDeclinedPct = changes[leastDeclined];

            return (mostDeclined, mostDeclinedPct, leastDeclined, leastDeclinedPct);
        }
    }
}
