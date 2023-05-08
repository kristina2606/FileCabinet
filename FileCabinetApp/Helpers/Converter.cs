using System;
using System.Globalization;
using System.Linq;

namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Converts a string to a value of the desired type.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Checks a string for characters.
        /// </summary>
        /// <param name="name">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, string> StringConverter(string name)
        {
            bool isConverted = IsStringCorrect(name);

            return new Tuple<bool, string, string>(isConverted, name, name);
        }

        /// <summary>
        /// Converts a string to a DateTime type.
        /// </summary>
        /// <param name="date">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            bool isConverted = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var day);

            return new Tuple<bool, string, DateTime>(isConverted, date, day);
        }

        /// <summary>
        /// Converts a string to a char type.
        /// </summary>
        /// <param name="inputGender">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, char> CharConverter(string inputGender)
        {
            bool isConverted = char.TryParse(inputGender, out var gender);

            return new Tuple<bool, string, char>(isConverted, inputGender, gender);
        }

        /// <summary>
        /// Converts a string to a short type.
        /// </summary>
        /// <param name="inputHeight">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, short> ShortConverter(string inputHeight)
        {
            bool isConverted = short.TryParse(inputHeight, CultureInfo.InvariantCulture, out var height);

            return new Tuple<bool, string, short>(isConverted, inputHeight, height);
        }

        /// <summary>
        /// Converts a string to a decimal type.
        /// </summary>
        /// <param name="inputWeight">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, decimal> DecimalConverter(string inputWeight)
        {
            bool isConverted = short.TryParse(inputWeight, CultureInfo.InvariantCulture, out var weight);

            return new Tuple<bool, string, decimal>(isConverted, inputWeight, weight);
        }

        /// <summary>
        /// Converts a string to a integer type.
        /// </summary>
        /// <param name="inputNumber">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, int> IntConverter(string inputNumber)
        {
            bool isConverted = int.TryParse(inputNumber, CultureInfo.InvariantCulture, out var number);

            return new Tuple<bool, string, int>(isConverted, inputNumber, number);
        }

        private static bool IsStringCorrect(string name)
        {
            return name.All(letter => char.IsLetter(letter));
        }
    }
}
