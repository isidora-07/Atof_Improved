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
        private int LineNumber;

        public DateTime Date;
        public double Result;
        private string Error;
        public string getError()
        {
            return this.Error;
        }

        private void FormateDate()
        {
            string[] validFormats = { 
                "dd/MM/yyyy",
                "d/MM/yyyy",
                "d/M/yyyy",
                "dd/M/yyyy",

                "dd.MM.yyyy", 
                "d.MM.yyyy",
                "d.M.yyyy",
                "dd.M.yyyy",

                "dd.MM.yyyy.",
                "d.MM.yyyy.",
                "d.M.yyyy.",
                "dd.M.yyyy.",
            };
            if(!DateTime.TryParseExact(this.CsvDate, validFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out this.Date))
            {
                // to do...
                this.Error = "Losa godina";
            }

        }

        private void FormateResult()
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
                if(!Double.TryParse(this.CsvResult, out this.Result)){
                    // to do...
                    this.Error = "Los rezultat";
                }
            }
        }
    }
}
