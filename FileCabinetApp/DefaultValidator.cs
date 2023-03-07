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
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < 2 || fileCabinetRecordNewData.FirstName.Length > 60)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 60");
            }

            if (string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.LastName.Length < 2 || fileCabinetRecordNewData.LastName.Length > 60)
            {
                throw new ArgumentException("last name length is less than 2 or greater than 60");
            }

            if (fileCabinetRecordNewData.DateOfBirth > DateTime.Now || fileCabinetRecordNewData.DateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentException("date of birth is less than 01-Jun-1950 or greater today's date");
            }

            if (fileCabinetRecordNewData.Gender != 'f' && fileCabinetRecordNewData.Gender != 'm')
            {
                throw new ArgumentException("gender must be 'f' or 'm'.");
            }

            if (fileCabinetRecordNewData.Height <= 0 || fileCabinetRecordNewData.Height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }

            if (fileCabinetRecordNewData.Weight <= 0)
            {
                throw new ArgumentException("weight very small or very large.");
            }
        }
    }
}
