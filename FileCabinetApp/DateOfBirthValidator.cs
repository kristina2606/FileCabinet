using System;

namespace FileCabinetApp
{
    public class DateOfBirthValidator : IRecordValidator
    {
        private int from;
        private int to;

        public DateOfBirthValidator(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year < this.from || DateTime.Now.Year - fileCabinetRecordNewData.DateOfBirth.Year > this.to)
            {
                throw new ArgumentException($"not yet {this.from} years old or over {this.to}.");
            }
        }
    }
}