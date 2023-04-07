using System;

namespace FileCabinetApp
{
    public class WeightValidator : IRecordValidator
    {
        private int minValue;
        private int maxValue;

        public WeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Weight <= this.minValue && fileCabinetRecordNewData.Weight >= this.maxValue)
            {
                throw new ArgumentException($"weight less then {this.minValue} or more then{this.maxValue}.");
            }
        }
    }
}
