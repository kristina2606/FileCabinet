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
            int id;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                id = this.service.CreateRecord(fileCabinetRecordNewData);
            }
            finally
            {
                MeasuringExecutionTime("Create", stopWatch);
            }

            return id;
        }

        /// <summary>
        /// Edits an already existing entry by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                this.service.EditRecord(id, fileCabinetRecordNewData);
            }
            finally
            {
                MeasuringExecutionTime("Edit", stopWatch);
            }
        }

        /// <summary>
        /// Finds all records by date of birth and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                findedRecords = this.service.FindByDateOfBirth(dateOfBirth);
            }
            finally
            {
                MeasuringExecutionTime("Find by dateOfBirth", stopWatch);
            }

            return findedRecords;
        }

        /// <summary>
        /// Finds all records by first name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                findedRecords = this.service.FindByFirstName(firstName);
            }
            finally
            {
                MeasuringExecutionTime("Find by firstName", stopWatch);
            }

            return findedRecords;
        }

        /// <summary>
        /// Finds all records by last name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                findedRecords = this.service.FindByLastName(lastName);
            }
            finally
            {
                MeasuringExecutionTime("Find by lastName", stopWatch);
            }

            return findedRecords;
        }

        /// <summary>
        /// Gets all existing records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            ReadOnlyCollection<FileCabinetRecord> allRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                allRecords = this.service.GetRecords();
            }
            finally
            {
                MeasuringExecutionTime("List", stopWatch);
            }

            return allRecords;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int activeRecords, deletedRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                (activeRecords, deletedRecords) = this.service.GetStat();
            }
            finally
            {
                MeasuringExecutionTime("Stat", stopWatch);
            }

            return (activeRecords, deletedRecords);
        }

        /// <summary>
        /// Checks if records with the specified id exists and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            bool status;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                status = this.service.IsExist(id);
            }
            finally
            {
                MeasuringExecutionTime("IsExist", stopWatch);
            }

            return status;
        }

        /// <summary>
        /// Passes the state of an object and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            FileCabinetServiceSnapshot snapshot;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                snapshot = this.service.MakeSnapshot();
            }
            finally
            {
                MeasuringExecutionTime("Export", stopWatch);
            }

            return snapshot;
        }

        /// <summary>
        /// Defragments the data file and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            int countPurgedRecords;

            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                countPurgedRecords = this.service.Purge();
            }
            finally
            {
                MeasuringExecutionTime("Purge", stopWatch);
            }

            return countPurgedRecords;
        }

        /// <summary>
        /// Remove record by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                this.service.Remove(id);
            }
            finally
            {
                MeasuringExecutionTime("Remove", stopWatch);
            }
        }

        /// <summary>
        /// Adding imported records to existing records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                this.service.Restore(fileCabinetServiceSnapshot);
            }
            finally
            {
                MeasuringExecutionTime("Import", stopWatch);
            }
        }

        private static void MeasuringExecutionTime(string methodName, Stopwatch stopWatch)
        {
            stopWatch.Stop();
            var ticks = stopWatch.ElapsedTicks;

            Console.WriteLine($"{methodName} method execution duration is {ticks} ticks.");
        }
    }
}
