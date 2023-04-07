using System;

namespace FileCabinetApp
{
    public class FirstNameValidator : IRecordValidator
    {
        private int minLenght;
        private int maxLenght;

        public FirstNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.FirstName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.FirstName.Length < this.minLenght || fileCabinetRecordNewData.FirstName.Length > this.maxLenght)
            {
                throw new ArgumentException($"first name length is less than {this.minLenght} or greater than {this.maxLenght}.");
            }
        }
    }
}