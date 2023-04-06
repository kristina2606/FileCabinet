using System;

namespace FileCabinetApp
{
    public class CustomFirstNameValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < 2 || fileCabinetRecordNewData.FirstName.Length > 15)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 15");
            }
        }
    }
}