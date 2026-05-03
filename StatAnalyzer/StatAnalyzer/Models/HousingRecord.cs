using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Models
{
    // Модель данных о ценах на жильё за один год
    public class HousingRecord : IAnalyzable
    {
        public int Year { get; set; }

        public double Price1Room { get; set; } // Цена за кв. метр в однокомнатной квартире

        public double Price2Room { get; set; } // Цена за кв. метр в двухкомнатной квартире

        public double Price3Room { get; set; } // Цена за кв. метр в трёхкомнатной квартире

        public string GetPeriodLabel() => Year.ToString();

        public double GetPrimaryValue() => Price1Room;
    }
}
