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
        /// <param name="validators">Type of validate parametrs.</param>
        public CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = validators.ToList();
        }

        /// <summary>
        /// Validate a new record from user input with new validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            foreach (var validator in validators)
            {
                validator.ValidateParametrs(fileCabinetRecordNewData);
            }
        }
    }
}
