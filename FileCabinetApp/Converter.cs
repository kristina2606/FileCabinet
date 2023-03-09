using System;
using System.Globalization;
using System.Linq;

namespace FileCabinetApp
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
            bool a = IsStringCorrect(name);

            return new Tuple<bool, string, string>(a, name, name);
        }

        /// <summary>
        /// Converts a string to a DateTime type.
        /// </summary>
        /// <param name="date">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            bool a = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var day);

            return new Tuple<bool, string, DateTime>(a, date, day);
        }

        /// <summary>
        /// Converts a string to a char type.
        /// </summary>
        /// <param name="inputGender">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, char> CharConverter(string inputGender)
        {
            bool a = char.TryParse(inputGender, out var gender);

            return new Tuple<bool, string, char>(a, inputGender, gender);
        }

        /// <summary>
        /// Converts a string to a short type.
        /// </summary>
        /// <param name="inputHeight">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, short> ShortConverter(string inputHeight)
        {
            bool a = short.TryParse(inputHeight, CultureInfo.InvariantCulture, out var height);

            return new Tuple<bool, string, short>(a, inputHeight, height);
        }

        /// <summary>
        /// Converts a string to a decimal type.
        /// </summary>
        /// <param name="inputWeight">Get string from user input.</param>
        /// <returns>Returns information about how the validation went, the string to convert, and the converted data type.</returns>
        public static Tuple<bool, string, decimal> DecimalConverter(string inputWeight)
        {
            bool a = short.TryParse(inputWeight, CultureInfo.InvariantCulture, out var weight);

            return new Tuple<bool, string, decimal>(a, inputWeight, weight);
        }

        private static bool IsStringCorrect(string name)
        {
            bool result = name.All(letter => char.IsLetter(letter));

            return result;
        }
    }
}
