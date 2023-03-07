using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Сhecking the values of the input parameters of the FileCabinetService class.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Validate a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public override void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var validateParametrs = new DefaultValidator();
            validateParametrs.ValidateParametrs(fileCabinetRecordNewData);
        }
    }
}
