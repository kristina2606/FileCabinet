using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FileCabinetApp
{
    public class ServiceMeter : IFileCabinetService
    {
        private readonly IFileCabinetService service;

        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int id = this.service.CreateRecord(fileCabinetRecordNewData);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Create method execution duration is {ts.Ticks} ticks.");

            return id;
        }

        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.service.EditRecord(id, fileCabinetRecordNewData);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Edit method execution duration is {ts.Ticks} ticks.");
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var findedRecords = this.service.FindByDateOfBirth(dateOfBirth);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Find by date of birth method execution duration is {ts.Ticks} ticks.");

            return findedRecords;
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var findedRecords = this.service.FindByFirstName(firstName);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Find by first name method execution duration is {ts.Ticks} ticks.");

            return findedRecords;
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var findedRecords = this.service.FindByLastName(lastName);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Find by last name method execution duration is {ts.Ticks} ticks.");

            return findedRecords;
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var allRecords = this.service.GetRecords();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Get records method execution duration is {ts.Ticks} ticks.");

            return allRecords;
        }

        public (int activeRecords, int deletedRecords) GetStat()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            (int activeRecords, int deletedRecords) = this.service.GetStat();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Get stat method execution duration is {ts.Ticks} ticks.");

            return (activeRecords, deletedRecords);
        }

        public bool IsExist(int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var stat = this.service.IsExist(id);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Is exist method execution duration is {ts.Ticks} ticks.");

            return stat;
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var snapshot = this.service.MakeSnapshot();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Make snapshot method execution duration is {ts.Ticks} ticks.");

            return snapshot;
        }

        public int Purge()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var countPurgedRecords = this.service.Purge();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Purge method execution duration is {ts.Ticks} ticks.");

            return countPurgedRecords;
        }

        public void Remove(int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.service.Remove(id);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Remove method execution duration is {ts.Ticks} ticks.");
        }

        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.service.Restore(fileCabinetServiceSnapshot);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Restore method execution duration is {ts.Ticks} ticks.");
        }
    }
}
