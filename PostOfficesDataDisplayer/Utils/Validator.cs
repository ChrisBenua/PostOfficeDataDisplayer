using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Utils
{
    /// <summary>
    /// Validator.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validates the int.
        /// </summary>
        /// <returns>The tuple of correctness of string and converted int</returns>
        /// <param name="s">String to be validated</param>
        /// <param name="valid">Validation predicate</param>
        public static (bool, int) ValidateInt(string s, Func<int, bool> valid)
        {
            int helper;
            if (int.TryParse(s, out helper) && valid(helper) || s.Length == 0)
            {
                return (true, helper);
            }

            return (false, helper);
        }

        /// <summary>
        /// Validates the double.
        /// </summary>
        /// <returns>The tuple of correctness of string and converted double</returns>
        /// <param name="s">String to be validated</param>
        /// <param name="valid">Validation predicate</param>
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
