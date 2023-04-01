using System;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with settings for adults.
    /// </summary>
    public class UserInputValidationCustom : IUserInputValidation
    {
        /// <summary>
        /// Gets first name from user input.
        /// </summary>
        /// <param name="firstName">First name of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateFirstName(string firstName)
        {
            bool validationStatus = true;
            if (string.IsNullOrEmpty(firstName) || firstName.Length < 2 || firstName.Length > 15)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, firstName);
        }

        /// <summary>
        /// Gets last name from user input.
        /// </summary>
        /// <param name="lastName">Last name of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateLastName(string lastName)
        {
            bool validationStatus = true;
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 2 || lastName.Length > 20)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, lastName);
        }

        /// <summary>
        /// Gets date of birth from user input.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateDateOfBirth(DateTime dateOfBirth)
        {
            bool validationStatus = true;
            if (DateTime.Now.Year - dateOfBirth.Year < 18 || DateTime.Now.Year - dateOfBirth.Year > 150)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, dateOfBirth.ToShortDateString());
        }

        /// <summary>
        /// Gets gender from user input.
        /// </summary>
        /// <param name="gender">Gender of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateGender(char gender)
        {
            bool validationStatus = true;
            if (gender != 'f' && gender != 'F' && gender != 'm' && gender != 'M')
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, gender.ToString());
        }

        /// <summary>
        /// Gets height from user input.
        /// </summary>
        /// <param name="height">Height of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateHeight(short height)
        {
            bool validationStatus = true;
            if (height < 145 || height > 250)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets weight from user input.
        /// </summary>
        /// <param name="weight">Weight of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> ValidateWeight(decimal weight)
        {
            bool validationStatus = true;
            if (weight <= 40)
            {
                validationStatus = false;
            }

            return new Tuple<bool, string>(validationStatus, weight.ToString(CultureInfo.InvariantCulture));
        }
    }
}
