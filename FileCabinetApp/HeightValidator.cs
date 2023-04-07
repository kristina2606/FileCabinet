using System;

namespace FileCabinetApp
{
    public class HeightValidator : IRecordValidator
    {
        private int minValue;
        private int maxValue;

        public HeightValidator(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Height <= this.minValue || fileCabinetRecordNewData.Height > this.maxValue)
            {
                throw new ArgumentException($"height less then {this.minValue} or more then{this.maxValue}.");
            }
        }
    }
}
