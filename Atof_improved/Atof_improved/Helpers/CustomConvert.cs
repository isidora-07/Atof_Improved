using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atof_improved.Helpers
{
    public class CustomConvert
    {
        public static double AtofImproved(string stringValue)
        {
            double result = 0;
            if (stringValue.Contains("E") || stringValue.Contains("e"))
            {
                string[] separator = new string[1];
                if (stringValue.Contains("E"))
                {
                    separator[0] = "E";
                }
                else
                {
                    separator[0] = "e";
                }

                var value = stringValue.Split(separator, StringSplitOptions.None);
                char sign = value[1].First();
                double baseValue;
                double exponentValue;

                baseValue = Atof(value[0]);
                exponentValue = Atof(value[1].Substring(1)); // because of sign

                if(sign == '+')
                {
                    result = baseValue * Math.Pow(10, exponentValue);
                } else
                {
                    result = baseValue / Math.Pow(10, exponentValue);
                }
            }
            else
            {
                result = Atof(stringValue);
            }
            return result;
        }

        public static double Atof(string stringValue)
        {
            double result = 0;
            double floatResult = 0;
            string[] stringValueSplited = stringValue.Split('.');
            if (stringValueSplited.Length > 2)
            {
                throw new Exception();
            }

            string wholePartOfNumberString = stringValueSplited[0];
            int level = (int) Math.Pow(10, wholePartOfNumberString.Length - 1);

            foreach(char c in wholePartOfNumberString)
            {
                int digit = ToInt(c);
                result += level * digit;
                level /= 10;
            }

            if(stringValueSplited.Length == 2) 
            {
                string floatPartOfNumberString = stringValueSplited[1];
                level = (int)Math.Pow(10, floatPartOfNumberString.Length - 1);

                foreach (char c in floatPartOfNumberString)
                {
                    int digit = ToInt(c);
                    floatResult += level * digit;
                    level /= 10;
                }

                floatResult *= Math.Pow(10, -floatPartOfNumberString.Length);
            }

            return result + floatResult;
        }

        public static int ToInt(char c)
        {
            int digit = c - '0';
            if(digit < 0 || digit > 9)
            {
                throw new Exception();
            }
            return digit;
        }
    }
}
