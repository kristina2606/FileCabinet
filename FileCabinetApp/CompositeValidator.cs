using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp
{
    public class CompositeValidator : IRecordValidator
    {
        private readonly List<IRecordValidator> validators;

        public CompositeValidator(IEnumerable<IRecordValidator> validators)
        {
            this.validators = validators.ToList();
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            foreach (var validator in this.validators)
            {
                validator.ValidateParametrs(fileCabinetRecordNewData);
            }
        }
    }
}
