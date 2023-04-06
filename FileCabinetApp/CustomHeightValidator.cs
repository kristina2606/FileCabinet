using System;

namespace FileCabinetApp
{
    public class CustomHeightValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Height < 145 || fileCabinetRecordNewData.Height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }
        }
    }
}