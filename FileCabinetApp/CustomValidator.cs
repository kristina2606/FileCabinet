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
            new CustomFirstNameValidator().ValidateParametrs(fileCabinetRecordNewData);
            new CustomLastNameValidator().ValidateParametrs(fileCabinetRecordNewData);
            new CustomDateOfBirthValidator().ValidateParametrs(fileCabinetRecordNewData);
            new CustomGenderValidator().ValidateParametrs(fileCabinetRecordNewData);
            new CustomHeightValidator().ValidateParametrs(fileCabinetRecordNewData);
            new CustomWeightValidator().ValidateParametrs(fileCabinetRecordNewData);
        }
    }
}
