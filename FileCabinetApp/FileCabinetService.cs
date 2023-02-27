namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, char gender, uint phoneNumber, decimal height)
        {
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                PhoneNumber = phoneNumber,
                Height = height,
            };

            this.list.Add(record);

            return record.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            var copyOfTheList = this.list.ToArray();

            return copyOfTheList;
        }

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}