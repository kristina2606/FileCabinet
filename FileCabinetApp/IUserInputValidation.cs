using System;

namespace FileCabinetApp
{
    /// <summary>
    ///  Validate a new record from user input.
    /// </summary>
    public interface IUserInputValidation
    {
        /// <summary>
        /// Gets first name from user input.
        /// </summary>
        /// <param name="firstName">First name of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> FirstNameValidator(string firstName);

        /// <summary>
        /// Gets last name from user input.
        /// </summary>
        /// <param name="lastName">Last name of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> LastNameValidator(string lastName);

        /// <summary>
        /// Gets date of birth from user input.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> DateOfBirthValidator(DateTime dateOfBirth);

        /// <summary>
        /// Gets gender from user input.
        /// </summary>
        /// <param name="gender">Gender of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> GenderValidator(char gender);

        /// <summary>
        /// Gets height from user input.
        /// </summary>
        /// <param name="height">Height of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> HeightValidator(short height);

        /// <summary>
        /// Gets weight from user input.
        /// </summary>
        /// <param name="weight">Weight of a person.</param>
        /// <returns>Returns information about how the validation went and the value being validated.</returns>
        public Tuple<bool, string> WeightValidator(decimal weight);
    }
}
