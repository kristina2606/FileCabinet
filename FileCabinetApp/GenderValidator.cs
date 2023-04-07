using System;

namespace FileCabinetApp
{
    public class GenderValidator : IRecordValidator
    {
        private char requiredFirstValue;
        private char requiredSecondValue;
        private StringComparison sc;

        public GenderValidator(char requiredFirstValue, char requiredSecondValue, StringComparison sc)
        {
            this.requiredFirstValue = requiredFirstValue;
            this.requiredSecondValue = requiredSecondValue;
            this.sc = sc;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (!fileCabinetRecordNewData.Gender.ToString().Equals(this.requiredFirstValue.ToString(), this.sc) && !fileCabinetRecordNewData.Gender.ToString().Equals(this.requiredSecondValue.ToString(), this.sc))
            {
                throw new ArgumentException($"gender must be {this.requiredFirstValue} or {this.requiredSecondValue}.");
            }
        }
    }
}