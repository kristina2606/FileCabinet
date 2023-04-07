using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input.
    /// </summary>
    public class DefaultValidator : IRecordValidator
    {
        /// <summary>
        /// Validate a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            new FirstNameValidator(2, 60).ValidateParametrs(fileCabinetRecordNewData);
            new LastNameValidator(2, 60).ValidateParametrs(fileCabinetRecordNewData);
            new DateOfBirthValidator(0, 75).ValidateParametrs(fileCabinetRecordNewData);
            new GenderValidator('f', 'm', StringComparison.InvariantCulture).ValidateParametrs(fileCabinetRecordNewData);
            new HeightValidator(0, 250).ValidateParametrs(fileCabinetRecordNewData);
            new WeightValidator(0, 300).ValidateParametrs(fileCabinetRecordNewData);
        }
    }
}
