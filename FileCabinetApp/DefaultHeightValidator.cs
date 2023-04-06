using System;

namespace FileCabinetApp
{
    public class DefaultHeightValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.Height <= 0 || fileCabinetRecordNewData.Height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }
        }
    }
}