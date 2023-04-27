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

        /// <summary>
        /// Promt [Y/n] oor [y/N] for user.
        /// </summary>
        /// <param name="prompt">Information about the data to be overwritten. </param>
        /// <param name="defaultAnswer">Default response if the user does not enter anything.</param>
        /// <returns>Returns the user's choice.</returns>
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
        /// Validate user input parametrs.
        /// </summary>
        /// <typeparam name="T">Generic types.</typeparam>
        /// <param name="converter">Converts a string to a value of the required type.</param>
        /// <param name="validator">Checks the value against the criteria.</param>
        /// <param name="value">The string value to be converted and validated.</param>
        /// <returns>>Resulting value.</returns>
        /// <exception cref="ArgumentException">An exception with an error message received from the validator function.</exception>
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
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
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
                var condition = fields[i].Trim().Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (condition.Length != 2)
                {
                    throw new ArgumentException("You introduced an incorrect parametrs.");
                }

                conditions[i] = new Condition
                {
                    Field = condition[0],
                    Value = new FileCabinetRecord(),
                };

                switch (conditions[i].Field)
                {
                    case "id":
                        conditions[i].Value.Id = Converter.IntConverter(condition[1]).Item3;
                        break;
                    case "firstname":
                        conditions[i].Value.FirstName = Convert(Converter.StringConverter, inputValidation.ValidateFirstName, condition[1]);
                        break;
                    case "lastname":
                        conditions[i].Value.LastName = Convert(Converter.StringConverter, inputValidation.ValidateLastName, condition[1]);
                        break;
                    case "dateofbirth":
                        conditions[i].Value.DateOfBirth = Convert(Converter.DateConverter, inputValidation.ValidateDateOfBirth, condition[1]);
                        break;
                    case "gender":
                        conditions[i].Value.Gender = Convert(Converter.CharConverter, inputValidation.ValidateGender, condition[1]);
                        break;
                    case "height":
                        conditions[i].Value.Height = Convert(Converter.ShortConverter, inputValidation.ValidateHeight, condition[1]);
                        break;
                    case "weight":
                        conditions[i].Value.Weight = Convert(Converter.DecimalConverter, inputValidation.ValidateWeight, condition[1]);
                        break;
                    default:
                        throw new ArgumentException($"Unknown search criteria: {conditions[i].Field}");
                }
            }

            return conditions;
        }
    }
}
