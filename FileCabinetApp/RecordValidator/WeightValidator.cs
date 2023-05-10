using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Validates a new record from user input based on 'weight' validation parametrs.
    /// </summary>
    public class WeightValidator : IRecordValidator
    {
        private readonly int minValue;
        private readonly int maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum allowed weight.</param>
        /// <param name="maxValue">The maximum allowed weight.</param>
        public WeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Validates a new record from user input based on 'weight' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'weight' value does not match the allowed range.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Weight <= this.minValue && fileCabinetRecordNewData.Weight >= this.maxValue)
            {
                throw new ArgumentException($"Weight must be between {this.minValue} and {this.maxValue}.");
            }
        }
    }
}
