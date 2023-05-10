using System;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Helpers
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
            do
            {
                if (usersAnswer == "y")
                {
                    return true;
                }
                else if (usersAnswer == "n")
                {
                    return false;
                }
                else if (!string.IsNullOrEmpty(usersAnswer))
                {
                    Console.WriteLine("You entered an invalid character.");
                    Console.Write(prompt);
                    Console.Write(chooseAnswer);
                    usersAnswer = Console.ReadLine().ToLowerInvariant();
                }
            }
            while (!string.IsNullOrEmpty(usersAnswer));

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

        /// <summary>
        /// Creates an array of conditions based on the specified array of fields.
        /// </summary>
        /// <param name="fields">The array of fields to create conditions from.</param>
        /// <param name="inputValidation">The user input validation.</param>
        /// <returns>>An array of conditions.</returns>
        public static Condition[] CreateConditions(string[] fields, IUserInputValidation inputValidation)
        {
            if (fields.Length == 0)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            var conditions = new Condition[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                var splitResult = fields[i].Trim().Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (splitResult.Length != 2)
                {
                    throw new ArgumentException("Incorrect parameters were provided.");
                }

                var field = splitResult[0];
                var value = splitResult[1];

                if (!Enum.TryParse<FileCabinetRecordFields>(field, true, out var enumField))
                {
                    throw new ArgumentException($"Unknown search criteria: {field}");
                }

                var condition = new Condition
                {
                    Field = enumField,
                    Value = new FileCabinetRecord(),
                };

                switch (condition.Field)
                {
                    case FileCabinetRecordFields.Id:
                        condition.Value.Id = Converter.IntConverter(value).Item3;
                        break;
                    case FileCabinetRecordFields.FirstName:
                        condition.Value.FirstName = Convert(Converter.StringConverter, inputValidation.ValidateFirstName, value);
                        break;
                    case FileCabinetRecordFields.LastName:
                        condition.Value.LastName = Convert(Converter.StringConverter, inputValidation.ValidateLastName, value);
                        break;
                    case FileCabinetRecordFields.DateOfBirth:
                        condition.Value.DateOfBirth = Convert(Converter.DateConverter, inputValidation.ValidateDateOfBirth, value);
                        break;
                    case FileCabinetRecordFields.Gender:
                        condition.Value.Gender = Convert(Converter.CharConverter, inputValidation.ValidateGender, value);
                        break;
                    case FileCabinetRecordFields.Height:
                        condition.Value.Height = Convert(Converter.ShortConverter, inputValidation.ValidateHeight, value);
                        break;
                    case FileCabinetRecordFields.Weight:
                        condition.Value.Weight = Convert(Converter.DecimalConverter, inputValidation.ValidateWeight, value);
                        break;
                    default:
                        throw new ArgumentException($"Unknown search criteria: {condition.Field}");
                }

                conditions[i] = condition;
            }

            return conditions;
        }
    }
}
