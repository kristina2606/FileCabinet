using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// User input validation.
    /// </summary>
    public static class UserInputHelpers
    {
        /// <summary>
        /// Validate user input.
        /// </summary>
        /// <typeparam name="T">Generic types.</typeparam>
        /// <param name="converter">Converts a string to a value of the required type.</param>
        /// <param name="validator">Checks the value against the criteria.</param>
        /// <returns>Resulting value.</returns>
        public static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }
    }
}
