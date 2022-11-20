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
            List<Measure> merenja = new List<Measure>();
            merenja = FileHelper.ReadFile<Measure>("../../../Tests/input.csv");
        }
    }
}
