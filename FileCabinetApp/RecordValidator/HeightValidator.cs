using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Validate a new record from user input with 'height' validate parametrs.
    /// </summary>
    public class HeightValidator : IRecordValidator
    {
        private readonly int minValue;
        private readonly int maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">Min growth.</param>
        /// <param name="maxValue">Max growth.</param>
        public HeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Validate a new record from user input with 'height' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Height <= this.minValue || fileCabinetRecordNewData.Height > this.maxValue)
            {
                throw new ArgumentException($"height less then {this.minValue} or more then{this.maxValue}.");
            }
        }
    }
}
