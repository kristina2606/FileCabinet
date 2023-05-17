using System;

namespace FileCabinetApp.UserInputValidator
{
    /// <summary>
    /// Represents a user input validation for a new record.
    /// </summary>
    public interface IUserInputValidation
    {
        /// <summary>
        /// Validates the first name from user input.
        /// </summary>
        /// <param name="firstName">The first name of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateFirstName(string firstName);

        /// <summary>
        /// Validates the last name from user input.
        /// </summary>
        /// <param name="lastName">The last name of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateLastName(string lastName);

        /// <summary>
        /// Validates the date of birth from user input.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateDateOfBirth(DateTime dateOfBirth);

        /// <summary>
        /// Validates the gender from user input.
        /// </summary>
        /// <param name="gender">The gender of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateGender(char gender);

        /// <summary>
        /// Validates the height from user input.
        /// </summary>
        /// <param name="height">The height of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateHeight(short height);

        /// <summary>
        /// Validates the weight from user input.
        /// </summary>
        /// <param name="weight">The weight of a person.</param>
        /// <returns>Returns a tuple with information about how the validation went and the value being validated.</returns>
        Tuple<bool, string> ValidateWeight(decimal weight);
    }
}
