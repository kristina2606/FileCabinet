using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator.Implementation
{
    /// <summary>
    /// Validates a new record from user input based on 'height' validation parametrs.
    /// </summary>
    public class HeightValidator : IRecordValidator
    {
        private readonly int minValue;
        private readonly int maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum allowed height.</param>
        /// <param name="maxValue">The maximum allowed height.</param>
        public HeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Validates a new record from user input based on 'height' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'height' value does not match the allowed range.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Height <= this.minValue || fileCabinetRecordNewData.Height > this.maxValue)
            {
                throw new ArgumentException($"Height must be between {this.minValue}  and  {this.maxValue}.");
            }
        }
    }
}
