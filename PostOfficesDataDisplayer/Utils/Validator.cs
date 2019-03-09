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

        /// <summary>
        /// Validates Phone Number.
        /// </summary>
        /// <param name="phone">phone string</param>
        /// <returns>The tuple of correctness and error string.</returns>
        public static (bool, string) ValidatePhoneNumber(string phone, bool stillEditing = false)
        {
            foreach (var s in phone.Split(new string[] {"; " }, StringSplitOptions.RemoveEmptyEntries))
            {

                int rightBraces = s.Count(el => el == ')');
                int lastRightBrace = s.LastIndexOf(')');
                int leftBraces = s.Count(el => el == '(');
                int leftBrace = s.IndexOf('(');
                List<int> dashesPosition = new List<int>();
                for (int i = 0; i < s.Length; ++i)
                {
                    if (s[i] == '-')
                    {
                        dashesPosition.Add(i);
                    }
                }

                if (s.Contains("доб."))
                {
                    continue;
                }

                if (stillEditing && s.Count(el => !(Char.IsWhiteSpace(el) || el == '-')) < 12)
                {
                    continue;
                }

                if (rightBraces > 1 || leftBraces > 1 || rightBraces == 0 || leftBraces == 0)
                {
                    return (false, "Invalid numbers of braces in phoneNumber");
                }

                if (s.Count(el => Char.IsDigit(el)) != 10)
                {
                    return (false, "Invalid amount of digits in phone");
                }

                if (dashesPosition.Count != 2)
                {
                    return (false, "Invalid number of dashes");
                }

                if (dashesPosition[1] - dashesPosition[0] - 1 != 2)
                {
                    return (false, "Invalid amount of digits in phone number");
                }

                if (!(lastRightBrace - leftBrace >= 3 && lastRightBrace - leftBrace <= 5))
                {
                    return (false, "Invalid code in braces");
                }
            }

            return (true, null);
        }

    }
}
