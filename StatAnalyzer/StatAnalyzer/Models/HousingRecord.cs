using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Models
{
    public class HousingRecord : IAnalyzable
    {
        public int Year { get; set; }

        public double Price1Room { get; set; }

        public double Price2Room { get; set; }

        public double Price3Room { get; set; }

        public string GetPeriodLabel() => Year.ToString();

        public double GetPrimaryValue() => Price1Room;
    }
}
