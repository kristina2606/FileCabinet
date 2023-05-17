using System;

namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Contains helper method for user input validation.
    /// </summary>
    public static class UserInputHelpers
    {
        /// <summary>
        /// Reads and validates user input.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="converter">A function that converts a string to the desired type.</param>
        /// <param name="validator">A function that validates the converted value.</param>
        /// <returns>The resulting value.</returns>
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

        /// <summary>
        /// Prompts the user for a yes or no answer.
        /// </summary>
        /// <param name="prompt">The information about the data to be overwritten.</param>
        /// <param name="defaultAnswer">The default response if the user does not enter anything.</param>
        /// <returns>The user's choice.</returns>
        public static bool ReadYesOrNo(string prompt, bool defaultAnswer)
        {
            string chooseAnswer;

            Console.Write(prompt);

            if (defaultAnswer)
            {
                chooseAnswer = " [Y/n] ";
            }
            else
            {
                chooseAnswer = " [y/N] ";
            }

            Console.Write(chooseAnswer);

            var usersAnswer = Console.ReadLine().ToLowerInvariant();
            while (!string.IsNullOrEmpty(usersAnswer))
            {
                if (usersAnswer == "y")
                {
                    return true;
                }
                else if (usersAnswer == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("You entered an invalid character.");
                    Console.Write(prompt);
                    Console.Write(chooseAnswer);
                    usersAnswer = Console.ReadLine().ToLowerInvariant();
                }
            }

            return defaultAnswer;
        }

        /// <summary>
        /// Converts and validates user input parameters.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="converter">A function that converts a string to the desired type.</param>
        /// <param name="validator">A function that validates the converted value.</param>
        /// <param name="value">The string value to be converted and validated.</param>
        /// <returns>The resulting value.</returns>
        /// <exception cref="ArgumentException">Thrown when the validation fails.</exception>
        public static T Convert<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator, string value)
        {
            var conversionResult = converter(value);

            if (conversionResult.Item1 && validator(conversionResult.Item3).Item1)
            {
                return conversionResult.Item3;
            }

            throw new ArgumentException($"Validation failed: {conversionResult.Item2}.");
        }
    }
}
