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
        public void Validate(FileCabinetRecordNewData fileCabinetRecordNewData)
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
