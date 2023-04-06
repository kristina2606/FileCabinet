using System;

namespace FileCabinetApp
{
    public class DefaultLastNameValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.LastName.Length < 2 || fileCabinetRecordNewData.LastName.Length > 60)
            {
                throw new ArgumentException("last name length is less than 2 or greater than 60");
            }
        }
    }
}