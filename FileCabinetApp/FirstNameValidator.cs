using Newtonsoft.Json;
using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'first name' validate parametrs.
    /// </summary>
    public class FirstNameValidator : IRecordValidator
    {
        private readonly int minLenght;
        private readonly int maxLenght;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">Min lenght of first name.</param>
        /// <param name="maxLenght">Max lenght of first name.</param>
        public FirstNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        /// <summary>
        /// Validate a new record from user input with 'first name' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        /// <exception cref="ArgumentNullException">Exception if the incoming entry is null.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < this.minLenght || fileCabinetRecordNewData.FirstName.Length > this.maxLenght)
            {
                throw new ArgumentException($"first name length is less than {this.minLenght} or greater than {this.maxLenght}.");
            }
        }
    }
}