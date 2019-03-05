using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Utils
{
    public static class Validator
    {

        public static (bool, int) ValidateInt(string s, Func<int, bool> valid)
        {
            int helper;
            if (int.TryParse(s, out helper) && valid(helper) || s.Length == 0)
            {
                return (true, helper);
            }

            return (false, helper);
        }

        public static (bool, double) ValidateDouble(string s, Func<double, bool> valid)
        {
            double helper;
            if (s.Contains(','))
            {
                return (false, 0);
            }
            if (double.TryParse(s, out helper) && valid(helper) || s.Length == 0)
            {
                return (true, helper);
            }

            return (false, helper);
        }

    }
}
