namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth)
        {
            // TODO: �������� ���������� ������
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
            };

            this.list.Add(record);

            return record.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            // TODO: �������� ���������� ������
            return Array.Empty<FileCabinetRecord>();
        }

        public int GetStat()
        {
            // TODO: �������� ���������� ������
            return this.list.Count;
        }
    }
}