using Atof_improved.Helpers;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved.Models
{
    public class Measure
    {
        private static int Index = 1;
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

        public DateTime Date;
        public double Result;
        public bool HasError;
        private int LineNumber;

        public Measure()
        {
            this.LineNumber = Index++;
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

            if (!DateTime.TryParseExact(this.CsvDate, validFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out Date))
            {
                this.HasError = true;
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

                bool result = value[0].Any(x => char.IsLetter(x));
                if (result)
                {
                    try
                    {
                        throw new Exception($"Line {this.LineNumber} cannot be converted into a number. Original value {this.CsvResult}, date {this.Date.ToShortDateString()}");
                    }
                    catch (Exception e)
                    {
                        FileHelper.ErrorLogging(e);
                        return;
                    }
                }

                double baseValue = Convert.ToDouble(value[0]);
                double exponentValue = Convert.ToDouble(value[1]);
                Result = baseValue * Math.Pow(10, exponentValue);

            } else
            {
                try
                {
                    if (!(Double.TryParse(this.CsvResult, out this.Result)))
                    {
                        throw new Exception($"Line {this.LineNumber} cannot be converted into a number. Original value {this.CsvResult}, date {this.Date.ToShortDateString()}");
                    }
                }
                catch (Exception e)
                {
                    FileHelper.ErrorLogging(e);
                }
            }
        }

    }
}
