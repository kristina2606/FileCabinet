using System;
using System.Collections.ObjectModel;
using System.IO;

namespace FileCabinetApp
{
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private readonly FileStream fileStream;

        public FileCabinetFilesystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            throw new NotImplementedException();
        }

        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        public int GetStat()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
