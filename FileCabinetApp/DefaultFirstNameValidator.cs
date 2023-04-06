using System;

namespace FileCabinetApp
{
    public class DefaultFirstNameValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < 2 || fileCabinetRecordNewData.FirstName.Length > 60)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 60");
            }
        }
    }
}