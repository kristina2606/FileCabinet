using System.Collections.Generic;
using System.Linq;
using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Сontains a set of different validators.
    /// </summary>
    public class CompositeValidator : IRecordValidator
    {
        private readonly List<IRecordValidator> validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeValidator"/> class.
        /// </summary>
        /// <param name="validators">The validators to include in the composite validator.</param>
        public CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = validators.ToList();
        }

        /// <summary>
        /// Validates a new record with the specified parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            foreach (var validator in this.validators)
            {
                validator.ValidateParametrs(fileCabinetRecordNewData);
            }
        }
    }
}
