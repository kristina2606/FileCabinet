using System;
using System.Globalization;

namespace FileCabinetApp.UserInputValidator
{
    /// <summary>
    /// Represents a user input validation for a new record with default settings.
    /// </summary>
    public class UserInputValidationDafault : IUserInputValidation
    {
        /// <summary>
        /// Validates the first name from user input.
        /// </summary>
        /// <param name="firstName">The first name of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateFirstName(string firstName)
        {
            bool validationStatus = true;
            if (string.IsNullOrEmpty(firstName) || firstName.Length < 2 || firstName.Length > 60)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, firstName);
        }

        /// <summary>
        /// Validates the last name from user input.
        /// </summary>
        /// <param name="lastName">The last name of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateLastName(string lastName)
        {
            bool validationStatus = true;
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 2 || lastName.Length > 60)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, lastName);
        }

        /// <summary>
        /// Validates the date of birth from user input.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateDateOfBirth(DateTime dateOfBirth)
        {
            bool validationStatus = true;
            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, dateOfBirth.ToShortDateString());
        }

        /// <summary>
        /// Validates the gender from user input.
        /// </summary>
        /// <param name="gender">The gender of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateGender(char gender)
        {
            bool validationStatus = true;
            if (gender != 'f' && gender != 'm')
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, gender.ToString());
        }

        /// <summary>
        /// Validates the height from user input.
        /// </summary>
        /// <param name="height">The height of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateHeight(short height)
        {
            bool validationStatus = true;
            if (height <= 0 || height > 250)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Validates the weight from user input.
        /// </summary>
        /// <param name="weight">The weight of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateWeight(decimal weight)
        {
            bool validationStatus = true;
            if (weight <= 0)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, weight.ToString(CultureInfo.InvariantCulture));
        }
    }
}
