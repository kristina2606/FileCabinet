using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Validates a new record from user input based on 'last name' validation parametrs.
    /// </summary>
    public class LastNameValidator : IRecordValidator
    {
        private readonly int minLenght;
        private readonly int maxLenght;

        /// <summary>
        /// Initializes a new instance of the <see cref="LastNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">The minimum lenght of the last name allowed.</param>
        /// <param name="maxLenght">The maximum length of the last name allowed.</param>
        public LastNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        /// <summary>
        /// Validates a new record from user input based on 'last name' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'last name' length is outside the allowed range.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the 'last name' value is null or empty.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.LastName.Length < this.minLenght || fileCabinetRecordNewData.LastName.Length > this.maxLenght)
            {
                throw new ArgumentException($"Last name length must be between {this.minLenght} and {this.maxLenght}.");
            }
        }
    }
}