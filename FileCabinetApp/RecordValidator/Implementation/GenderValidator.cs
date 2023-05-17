using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator.Implementation
{
    /// <summary>
    /// Validates a new record from user input based on 'gender' validation parametrs.
    /// </summary>
    public class GenderValidator : IRecordValidator
    {
        private readonly string requiredFirstValue;
        private readonly string requiredSecondValue;
        private readonly StringComparison comparison;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenderValidator"/> class.
        /// </summary>
        /// <param name="requiredFirstValue">The first gender identity.</param>
        /// <param name="requiredSecondValue">The second gender identity.</param>
        /// <param name="comparison">The string comparison type.</param>
        public GenderValidator(char requiredFirstValue, char requiredSecondValue, StringComparison comparison)
        {
            this.requiredFirstValue = requiredFirstValue.ToString();
            this.requiredSecondValue = requiredSecondValue.ToString();
            this.comparison = comparison;
        }

        /// <summary>
        /// Validates a new record from user input based on 'gender' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'gender' value does not match the allowed values.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var gender = fileCabinetRecordNewData.Gender.ToString();

            if (!gender.Equals(this.requiredFirstValue, this.comparison) && !gender.Equals(this.requiredSecondValue, this.comparison))
            {
                throw new ArgumentException($"Gender must be {this.requiredFirstValue} or {this.requiredSecondValue}.");
            }
        }
    }
}