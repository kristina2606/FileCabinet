using System;
using FileCabinetApp.Enums;
using FileCabinetApp.Helpers;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.Models
{
    /// <summary>
    /// Represents a search condition for records.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Gets or sets the field by which the search will be performed in the records.
        /// </summary>
        /// <value>The field by which the search will be performed in the records.</value>
        public FileCabinetRecordFields Field { get; set; }

        /// <summary>
        /// Gets or sets the value that contains the search values.
        /// </summary>
        /// <value>The value that contains the search values.</value>
        public FileCabinetRecord Value { get; set; }

        /// <summary>
        /// Creates an array of conditions based on the specified array of fields.
        /// </summary>
        /// <param name="fields">The array of fields to create conditions from.</param>
        /// <param name="inputValidation">The user input validation.</param>
        /// <returns>>An array of conditions.</returns>
        public static Condition[] Create(string[] fields, IUserInputValidation inputValidation)
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

                SetNewValueByField(inputValidation, value, condition);

                conditions[i] = condition;
            }

            return conditions;
        }

        private static void SetNewValueByField(IUserInputValidation inputValidation, string value, Condition condition)
        {
            switch (condition.Field)
            {
                case FileCabinetRecordFields.Id:
                    condition.Value.Id = Converters.IntConverter(value).Item3;
                    break;
                case FileCabinetRecordFields.FirstName:
                    condition.Value.FirstName = UserInputHelpers.Convert(Converters.StringConverter, inputValidation.ValidateFirstName, value);
                    break;
                case FileCabinetRecordFields.LastName:
                    condition.Value.LastName = UserInputHelpers.Convert(Converters.StringConverter, inputValidation.ValidateLastName, value);
                    break;
                case FileCabinetRecordFields.DateOfBirth:
                    condition.Value.DateOfBirth = UserInputHelpers.Convert(Converters.DateConverter, inputValidation.ValidateDateOfBirth, value);
                    break;
                case FileCabinetRecordFields.Gender:
                    condition.Value.Gender = UserInputHelpers.Convert(Converters.CharConverter, inputValidation.ValidateGender, value);
                    break;
                case FileCabinetRecordFields.Height:
                    condition.Value.Height = UserInputHelpers.Convert(Converters.ShortConverter, inputValidation.ValidateHeight, value);
                    break;
                case FileCabinetRecordFields.Weight:
                    condition.Value.Weight = UserInputHelpers.Convert(Converters.DecimalConverter, inputValidation.ValidateWeight, value);
                    break;
                default:
                    throw new ArgumentException($"Unknown search criteria: {condition.Field}");
            }
        }
    }
}