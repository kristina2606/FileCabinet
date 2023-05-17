using System;
using System.Globalization;
using System.Linq;
using FileCabinetApp.Constants;

namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Provides conversion methods for converting strings to various data types.
    /// </summary>
    public static class Converters
    {
        /// <summary>
        /// Converts a string to a string value type.
        /// </summary>
        /// <param name="name">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, string> StringConverter(string name)
        {
            bool isCorrect = name.All(char.IsLetter);

            return new Tuple<bool, string, string>(isCorrect, name, name);
        }

        /// <summary>
        /// Converts a string to a DateTime value type.
        /// </summary>
        /// <param name="date">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            bool isConverted = DateTime.TryParseExact(date, DateTimeConstants.EUDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var day);

            return new Tuple<bool, string, DateTime>(isConverted, date, day);
        }

        /// <summary>
        /// Converts a string to a char value type.
        /// </summary>
        /// <param name="inputGender">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, char> CharConverter(string inputGender)
        {
            bool isConverted = char.TryParse(inputGender, out var gender);

            return new Tuple<bool, string, char>(isConverted, inputGender, gender);
        }

        /// <summary>
        /// Converts a string to a short value type.
        /// </summary>
        /// <param name="inputHeight">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, short> ShortConverter(string inputHeight)
        {
            bool isConverted = short.TryParse(inputHeight, CultureInfo.InvariantCulture, out var height);

            return new Tuple<bool, string, short>(isConverted, inputHeight, height);
        }

        /// <summary>
        /// Converts a string to a decimal value type.
        /// </summary>
        /// <param name="inputWeight">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, decimal> DecimalConverter(string inputWeight)
        {
            bool isConverted = short.TryParse(inputWeight, CultureInfo.InvariantCulture, out var weight);

            return new Tuple<bool, string, decimal>(isConverted, inputWeight, weight);
        }

        /// <summary>
        /// Converts a string to a int value type.
        /// </summary>
        /// <param name="inputNumber">The string value to convert.</param>
        /// <returns>A tuple containing information about the conversion result, the original string, and the converted value.</returns>
        public static Tuple<bool, string, int> IntConverter(string inputNumber)
        {
            bool isConverted = int.TryParse(inputNumber, CultureInfo.InvariantCulture, out var number);

            return new Tuple<bool, string, int>(isConverted, inputNumber, number);
        }
    }
}
