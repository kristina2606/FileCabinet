using System;

namespace FileCabinetApp
{
    public class CustomWeightValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Weight <= 40 && fileCabinetRecordNewData.Weight >= 300)
            {
                throw new ArgumentException("weight very small.");
            }
        }
    }
}