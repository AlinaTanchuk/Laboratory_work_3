using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatAnalyzer.Models;

namespace StatAnalyzer.Services
{
    public class Variant9DataService : BaseDataService<HousingRecord>
    {
        public (string mostExpensive, double maxGrowthPct,
                string mostCheap, double maxDropPct)
            CalculatePriceChangeStats(List<HousingRecord> records)
        {
            if (records == null || records.Count < 2)
                throw new ArgumentException("Нужно минимум 2 записи.");

            var sorted = records.OrderBy(r => r.Year).ToList();
            var first = sorted.First();
            var last = sorted.Last();

            double change1 = (last.Price1Room - first.Price1Room) / first.Price1Room * 100.0;
            double change2 = (last.Price2Room - first.Price2Room) / first.Price2Room * 100.0;
            double change3 = (last.Price3Room - first.Price3Room) / first.Price3Room * 100.0;

            var changes = new Dictionary<string, double>
            {
                { "1-комнатные", change1 },
                { "2-комнатные", change2 },
                { "3-комнатные", change3 }
            };

            string mostExpensive = changes.OrderByDescending(x => x.Value).First().Key;
            double maxGrowthPct = changes[mostExpensive];

            string mostCheap = changes.OrderBy(x => x.Value).First().Key;
            double maxDropPct = changes[mostCheap];

            return (mostExpensive, maxGrowthPct, mostCheap, maxDropPct);
        }
    }
}
