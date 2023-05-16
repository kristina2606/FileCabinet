using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator.Implementation
{
    /// <summary>
    /// Validates a new record from user input based on 'date of birth' validation parametrs.
    /// </summary>
    public class DateOfBirthValidator : IRecordValidator
    {
        private readonly DateTime from;
        private readonly DateTime to;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="from">The minimum date of birth allowed.</param>
        /// <param name="to">The maximum date of birth allowed.</param>
        public DateOfBirthValidator(DateTime from, DateTime to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="minAge">The minimum age allowed.</param>
        /// <param name="maxAge">The maximum age allowed.</param>
        public DateOfBirthValidator(int minAge, int maxAge)
        {
            this.from = DateTime.Now.AddYears(-minAge);
            this.to = DateTime.Now.AddYears(-maxAge);
        }

        /// <summary>
        /// Validates a new record from user input based on 'date of birth' validation parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        /// <exception cref="ArgumentException">Thrown if the 'date of birth' value is outside the allowed range.</exception>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.DateOfBirth < this.from || fileCabinetRecordNewData.DateOfBirth > this.to)
            {
                throw new ArgumentException($"Date of birth must be between {this.from.ToShortDateString()} and {this.to.ToShortDateString()}");
            }
        }
    }
}