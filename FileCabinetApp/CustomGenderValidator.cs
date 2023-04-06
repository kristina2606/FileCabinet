using System;

namespace FileCabinetApp
{
    public class CustomGenderValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Gender != 'f' && fileCabinetRecordNewData.Gender != 'F' && fileCabinetRecordNewData.Gender != 'm' && fileCabinetRecordNewData.Gender != 'M')
            {
                throw new ArgumentException("gender must be 'f','F' or 'm','M'.");
            }
        }
    }
}