using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with 'date of birth' validate parametrs.
    /// </summary>
    public class DateOfBirthValidator : IRecordValidator
    {
        private readonly DateTime from;
        private readonly DateTime to;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="from">Min date of birth.</param>
        /// <param name="to">Max date of birth.</param>
        public DateOfBirthValidator(DateTime from, DateTime to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="minAge">Min age.</param>
        /// <param name="maxAge">Max age.</param>
        public DateOfBirthValidator(int minAge, int maxAge)
        {
            this.from = DateTime.Now.AddYears(-minAge);
            this.to = DateTime.Now.AddYears(-maxAge);
        }

        /// <summary>
        /// Validate a new record from user input with 'date of birth' validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">Exception if the incoming entry does not match the parameters.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.DateOfBirth < this.from || fileCabinetRecordNewData.DateOfBirth > this.to)
            {
                throw new ArgumentException($"date of birth less than {this.from.ToShortDateString()} or greater than {this.to.ToShortDateString()}");
            }
        }
    }
}