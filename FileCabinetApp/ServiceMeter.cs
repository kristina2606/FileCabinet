using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FileCabinetApp
{
    /// <summary>
    /// Measures the running time of service methods and displays them on the screen.
    /// </summary>
    public class ServiceMeter : IFileCabinetService
    {
        private readonly IFileCabinetService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMeter"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public ServiceMeter(IFileCabinetService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Creates a new record from user input and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
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

        /// <summary>
        /// Edits an already existing entry by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.service.EditRecord(id, fileCabinetRecordNewData);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Edit method execution duration is {ts.Ticks} ticks.");
        }

        /// <summary>
        /// Finds all records by date of birth and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
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

        /// <summary>
        /// Finds all records by first name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
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

        /// <summary>
        /// Finds all records by last name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
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

        /// <summary>
        /// Gets all existing records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var allRecords = this.service.GetRecords();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"List records method execution duration is {ts.Ticks} ticks.");

            return allRecords;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            (int activeRecords, int deletedRecords) = this.service.GetStat();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Stat method execution duration is {ts.Ticks} ticks.");

            return (activeRecords, deletedRecords);
        }

        /// <summary>
        /// Checks if records with the specified id exists and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var status = this.service.IsExist(id);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Is exist method execution duration is {ts.Ticks} ticks.");

            return status;
        }

        /// <summary>
        /// Passes the state of an object and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var snapshot = this.service.MakeSnapshot();

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Export method execution duration is {ts.Ticks} ticks.");

            return snapshot;
        }

        /// <summary>
        /// Defragments the data file and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
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

        /// <summary>
        /// Remove record by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.service.Remove(id);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Remove method execution duration is {ts.Ticks} ticks.");
        }

        /// <summary>
        /// Adding imported records to existing records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                this.service.Restore(fileCabinetServiceSnapshot);
            }
            finally
            {
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Import method execution duration is {ts.Ticks} ticks.");
            }
        }
    }
}
