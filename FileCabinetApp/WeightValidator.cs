using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'weight' validate parametrs.
    /// </summary>
    public class WeightValidator : IRecordValidator
    {
        private readonly int minValue;
        private readonly int maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">Min weight.</param>
        /// <param name="maxValue">Max weight.</param>
        public WeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Validate a new record from user input with 'weight' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Weight <= this.minValue && fileCabinetRecordNewData.Weight >= this.maxValue)
            {
                throw new ArgumentException($"weight less then {this.minValue} or more then{this.maxValue}.");
            }
        }
    }
}
