namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth)
        {
            // TODO: �������� ���������� ������
            return 0;
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