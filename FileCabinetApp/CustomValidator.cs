using System;

namespace FileCabinetApp
{
    /// <summary>
    ///  Validate a new record from user input with settings for adults.
    /// </summary>
    public class CustomValidator : IRecordValidator
    {
        /// <summary>
        /// Validate a new record from user input withsettings for adults.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the recocrd.</param>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 15.The age is less than 18 or greater 150.
        /// The gender isn't equal 'f','F' or 'm','M'. The height is less than 145 or greater than 250. The weight is less than 40.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            new FirstNameValidator(2, 15).ValidateParametrs(fileCabinetRecordNewData);
            new LastNameValidator(2, 20).ValidateParametrs(fileCabinetRecordNewData);
            new DateOfBirthValidator(18, 150).ValidateParametrs(fileCabinetRecordNewData);
            new GenderValidator('f', 'm', StringComparison.InvariantCultureIgnoreCase).ValidateParametrs(fileCabinetRecordNewData);
            new HeightValidator(145, 250).ValidateParametrs(fileCabinetRecordNewData);
            new WeightValidator(40, 300).ValidateParametrs(fileCabinetRecordNewData);
        }
    }
}
