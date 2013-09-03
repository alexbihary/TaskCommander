using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskCommander.Helpers
{
    /// <summary>
    /// Static class of validation function generators.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Generate a validator function to check that a given string will parse as a valid int value.
        /// </summary>
        /// <param name="min">Optional minimum value. Defaults to int.MinValue.</param>
        /// <param name="max">Optional minimum value. Defaults to int.MinValue.</param>
        /// <returns>Func<string, bool></returns>
        public static Func<string, bool> IntValidator(int min = int.MinValue, int max = int.MaxValue) 
        {
            return s =>
            {
                int integer;
                return int.TryParse(s, out integer) && integer >= min && integer <= max;
            };
        }

        /// <summary>
        /// Generate a validator function to check that a given string will parse as a valid double value.
        /// </summary>
        /// <param name="min">Optional minimum value. Defaults to double.MinValue.</param>
        /// <param name="max">Optional maximum value. Defaults to double.MaxValue.</param>
        /// <returns>Func<string, bool></returns>
        public static Func<string, bool> DoubleValidator(double min = double.MinValue, double max = double.MaxValue)
        {
            return s =>
            {
                double doubleValue;
                return double.TryParse(s, out doubleValue) && doubleValue >= min && doubleValue <= max;
            };
        }

        /// <summary>
        /// Generate a validator function to check that a given string will parse as a valid bool value.
        /// </summary>
        /// <returns>Func<string, bool></returns>
        public static Func<string, bool> BoolValidator()
        {
            return s =>
            {
                bool boolValue;
                return bool.TryParse(s, out boolValue);
            };
        }
    }
}
