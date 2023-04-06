using System;

namespace FileCabinetApp
{
    public class DefaultWeightValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Weight <= 0 && fileCabinetRecordNewData.Weight >= 300)
            {
                throw new ArgumentException("weight very small or very large.");
            }
        }
    }
}