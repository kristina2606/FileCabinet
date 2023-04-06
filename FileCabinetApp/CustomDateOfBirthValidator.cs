using System;

namespace FileCabinetApp
{
    public class CustomDateOfBirthValidator : IRecordValidator
    {
        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year < 18 || DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year > 150)
            {
                throw new ArgumentException("not yet 18 years old or over 150.");
            }
        }
    }
}