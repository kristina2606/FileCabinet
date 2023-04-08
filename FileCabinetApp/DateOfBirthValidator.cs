using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'date of birth' validate parametrs.
    /// </summary>
    public class DateOfBirthValidator : IRecordValidator
    {
        private readonly int from;
        private readonly int to;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="from">Min date of birth.</param>
        /// <param name="to">Max date of birth.</param>
        public DateOfBirthValidator(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Validate a new record from user input with 'date of birth' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year < this.from || DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year > this.to)
            {
                throw new ArgumentException($"not yet {this.from} years old or over {this.to}.");
            }
        }
    }
}