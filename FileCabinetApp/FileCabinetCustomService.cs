using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with new validate parametrs.
    /// </summary>
    public class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Validate a new record from user input with new validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public override void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName) || string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < 2 || fileCabinetRecordNewData.FirstName.Length > 15)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 15");
            }

            if (fileCabinetRecordNewData.LastName.Length < 2 || fileCabinetRecordNewData.LastName.Length > 20)
            {
                throw new ArgumentException("last name length is less than 2 or greater than 20");
            }

            if (DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year < 18 || DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year > 150)
            {
                throw new ArgumentException("not yet 18 years old or over 150.");
            }

            if (fileCabinetRecordNewData.Gender != 'f' && fileCabinetRecordNewData.Gender != 'F' && fileCabinetRecordNewData.Gender != 'm' && fileCabinetRecordNewData.Gender != 'M')
            {
                throw new ArgumentException("gender must be 'f','F' or 'm','M'.");
            }

            if (fileCabinetRecordNewData.Height < 145 || fileCabinetRecordNewData.Height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }

            if (fileCabinetRecordNewData.Weight <= 40)
            {
                throw new ArgumentException("weight very small.");
            }
        }
    }
}
