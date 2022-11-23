using Atof_improved.Helpers;
using CsvHelper.Configuration.Attributes;
using System;
using System.Globalization;

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
                try
                {
                    this.FormateResult();
                } 
                catch (Exception e)
                {
                    FileHelper.ErrorLogging(e);
                }
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
            try
            {
                this.Result = CustomConvert.AtofImproved(this.CsvResult);
            } catch(Exception e)
            {
                this.HasError= true;
                throw new Exception($"Line {this.LineNumber} cannot be converted into a number. Original value {this.CsvResult}, date {this.Date.ToShortDateString()}");
            }

        }

    }
}
