using Atof_improved.Helpers;
using Atof_improved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Measure> measures = new List<Measure>();
            measures = FileHelper.ReadFile<Measure>("../../../Tests/input.csv");

            var sortedMonth = measures.OrderBy(x => x.Date).ToList();
            var sorted = sortedMonth.OrderBy(x => x.Date.Year)
                .Where(x => String.IsNullOrEmpty(x.getError())).ToList();

            var previousMonth = 0;
            var previousYear = 0;
            List<MeasureSummary> summaries = new List<MeasureSummary>();
            foreach (var measure in sorted)
            {
                if (measure.Date.Month != previousMonth || measure.Date.Year != previousYear)
                {
                    MeasureSummary summary = new MeasureSummary();
                    summary.Month = measure.Date.ToString("MMM");
                    summary.Year = measure.Date.Year;
                    summary.TotalSum = measure.Result;
                    summary.Count = 1;
                    summaries.Add(summary);
                }
                summaries.Last().TotalSum += measure.Result;
                summaries.Last().Count++;
                previousMonth = measure.Date.Month;
                previousYear = measure.Date.Year;
            }
        }
    }
}
