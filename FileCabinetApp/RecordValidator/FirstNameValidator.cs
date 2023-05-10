using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Validates a new record from user input based on 'first name' validation parametrs.
    /// </summary>
    public class FirstNameValidator : IRecordValidator
    {
        private readonly int minLenght;
        private readonly int maxLenght;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">The minimum lenght of the first name allowed.</param>
        /// <param name="maxLenght">The maximum length of the first name allowed.</param>
        public FirstNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        /// <summary>
        /// Validates a new record from user input based on 'first name' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'first name' length is outside the allowed range.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the 'first name' value is null or empty.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < this.minLenght || fileCabinetRecordNewData.FirstName.Length > this.maxLenght)
            {
                throw new ArgumentException($"First name length must be between {this.minLenght} and {this.maxLenght}.");
            }
        }
    }
}