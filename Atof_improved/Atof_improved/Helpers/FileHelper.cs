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

        public static void PrintOutput<T>(List<T> list)
        {

        }

        public static void ErrorLogging(Exception e)
        {
            string strPath = "../../../output.err";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine(e.Message);
            }

        }

    }
}
