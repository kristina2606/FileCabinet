using System;

namespace FileCabinetApp
{
    public class DefaultGenderValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Gender != 'f' && fileCabinetRecordNewData.Gender != 'm')
            {
                throw new ArgumentException("gender must be 'f' or 'm'.");
            }
        }
    }
}