using System;

namespace FileCabinetApp
{
    public class DefaultDateOfBirthValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (fileCabinetRecordNewData.DateOfBirth > DateTime.Now || fileCabinetRecordNewData.DateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentException("date of birth is less than 01-Jun-1950 or greater today's date");
            }
        }
    }
}