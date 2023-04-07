using System;

namespace FileCabinetApp
{
    public class LastNameValidator : IRecordValidator
    {
        private int minLenght;
        private int maxLenght;

        public LastNameValidator(int minLenght, int maxLenght)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        public void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (string.IsNullOrEmpty(fileCabinetRecordNewData.LastName))
            {
                throw new ArgumentNullException(nameof(fileCabinetRecordNewData));
            }

            if (fileCabinetRecordNewData.LastName.Length < this.minLenght || fileCabinetRecordNewData.LastName.Length > this.maxLenght)
            {
                throw new ArgumentException($"last name length is less than {this.minLenght} or greater than {this.maxLenght}");
            }
        }
    }
}