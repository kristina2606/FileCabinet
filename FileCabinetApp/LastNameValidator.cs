using Newtonsoft.Json;
using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'last name' validate parametrs.
    /// </summary>
    public class LastNameValidator : IRecordValidator
    {
        private readonly int minLenght;
        private readonly int maxLenght;

        /// <summary>
        /// Initializes a new instance of the <see cref="LastNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">Min lenght of last name.</param>
        /// <param name="maxLenght">Max lenght of last name.</param>s
        [JsonConstructor]
        public LastNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        /// <summary>
        /// Validate a new record from user input with 'last name' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        /// <exception cref="ArgumentNullException">Exception if the incoming entry is null.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.LastName.Length < this.minLenght || fileCabinetRecordNewData.LastName.Length > this.maxLenght)
            {
                throw new ArgumentException($"last name length is less than {this.minLenght} or greater than {this.maxLenght}");
            }
        }
    }
}