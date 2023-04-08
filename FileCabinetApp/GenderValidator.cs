using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'gender' validate parametrs.
    /// </summary>
    public class GenderValidator : IRecordValidator
    {
        private readonly char requiredFirstValue;
        private readonly char requiredSecondValue;
        private readonly StringComparison sc;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenderValidator"/> class.
        /// </summary>
        /// <param name="requiredFirstValue">First gender identities.</param>
        /// <param name="requiredSecondValue">Second gender identities.</param>
        /// <param name="sc">Specifies culture and case.</param>
        public GenderValidator(char requiredFirstValue, char requiredSecondValue, StringComparison sc)
        {
            this.requiredFirstValue = requiredFirstValue;
            this.requiredSecondValue = requiredSecondValue;
            this.sc = sc;
        }

        /// <summary>
        /// Validate a new record from user input with 'gender' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (!fileCabinetRecordNewData.Gender.ToString().Equals(this.requiredFirstValue.ToString(), this.sc) && !fileCabinetRecordNewData.Gender.ToString().Equals(this.requiredSecondValue.ToString(), this.sc))
            {
                throw new ArgumentException($"gender must be {this.requiredFirstValue} or {this.requiredSecondValue}.");
            }
        }
    }
}