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
        [Name("Rezultat")]
        public string Result { get; set; }
        [Name("Komentar")]
        public string Comment { get; set; }
        private DateTime Date;

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
    }
}
