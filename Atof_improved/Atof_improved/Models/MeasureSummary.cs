using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved.Models
{
    public class MeasureSummary
    {
        [Name("Mesec")]
        public string Month { get; set; }
        [Name("Godine")]
        public int Year { get; set; }
        [Name("UkupnoMerenja")]
        public double Count { get;  set; }
        [Name("Suma")]
        public double TotalSum { get; set; }

    }
}
