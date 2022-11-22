using Atof_improved.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved.Helpers
{
    public class FileHelper
    {
        private static bool FirstTimeErrorLogging = true;
        public static List<T> ReadFile<T>(string filePath)
        {
            List<T> records = new List<T>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<T>().ToList();
            }
            return records;
        }
        public static void ErrorLogging(Exception e)
        {
            string filePath = "../../../output.err";
            if(FirstTimeErrorLogging)
            {
                File.WriteAllText(filePath, string.Empty);
                FirstTimeErrorLogging = false;
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            using (StreamWriter stream = File.AppendText(filePath))
            {
                stream.WriteLine(e.Message);
            }
        }

        public static void PrintOutput(List<MeasureSummary> summaries)
        {
            string filePath = "../../../output.csv";
            File.WriteAllText(filePath, string.Empty);

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            string header = "Mesec, Godina, Ukupno merenja, Suma";
            File.WriteAllText(filePath, header + Environment.NewLine);

            using (StreamWriter stream = File.AppendText(filePath))
            {
                foreach(var summary in summaries)
                {
                    stream.WriteLine(summary.Month + ", " + summary.Year + ", " + summary.Count + ", " + summary.TotalSum);
                }
            }
        }

    }
}
