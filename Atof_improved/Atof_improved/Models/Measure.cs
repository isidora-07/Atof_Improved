using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved.Models
{
    public class Measure
    {
        private string _CsvDate;
        [Name("Datum")]
        public string CsvDate 
        {
            get { return _CsvDate; }
            set 
            {
                _CsvDate = value;
                this.FormateDate();
            }
        }

        private string _CsvResult;
        [Name("Rezultat")]
        public string CsvResult
        {
            get { return _CsvResult; }
            set 
            {
                _CsvResult = value;
                this.FormateResult();
            }
        }
        [Name("Komentar")]
        public string Comment { get; set; }

        private DateTime Date;
        private double Result;

        public void FormateDate()
        {
            string[] validFormats = { 
                "dd/mm/yyyy",
                "d/mm/yyyy",
                "d/m/yyyy",
                "dd/m/yyyy",

                "dd.mm.yyyy", 
                "d.mm.yyyy",
                "d.m.yyyy",
                "dd.m.yyyy",

                "dd.mm.yyyy.",
                "d.mm.yyyy.",
                "d.m.yyyy.",
                "dd.m.yyyy.",
            };
            if(!DateTime.TryParseExact(this.CsvDate, validFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out this.Date))
            {
                // to do...
            }

        }

        public void FormateResult()
        {
            if (this.CsvResult.Contains("E+") || this.CsvResult.Contains("e+"))
            {
                string[] separator = new string[1];
                if (this.CsvResult.Contains("E+"))
                {
                    separator[0] = "E+";
                } else
                {
                    separator[0] = "e+";
                }
                 
                var value = CsvResult.Split(separator, StringSplitOptions.None);

                double baseValue = Convert.ToDouble(value[0]);
                double exponentValue = Convert.ToDouble(value[1]);

                this.Result = baseValue * Math.Pow(10, exponentValue);
            } else
            {
                Double.TryParse(this.CsvResult, out this.Result);
            }
        }
    }
}
