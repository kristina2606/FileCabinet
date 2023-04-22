using System;
using System.Collections.Generic;
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
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.CreateRecord(fileCabinetRecordNewData);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Create", stopWatch);
            }
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
                stopWatch.Stop();

                MeasuringExecutionTime("Edit", stopWatch);
            }
        }

        /// <summary>
        /// Finds all records by date of birth and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public IEnumerable<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.FindByDateOfBirth(dateOfBirth);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Find by dateOfBirth", stopWatch);
            }
        }

        /// <summary>
        /// Finds all records by first name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public IEnumerable<FileCabinetRecord> FindByFirstName(string firstName)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.FindByFirstName(firstName);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Find by firstName", stopWatch);
            }
        }

        /// <summary>
        /// Finds all records by last name and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public IEnumerable<FileCabinetRecord> FindByLastName(string lastName)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.FindByLastName(lastName);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Find by lastName", stopWatch);
            }
        }

        /// <summary>
        /// Gets all existing records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.GetRecords();
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("List", stopWatch);
            }
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.GetStat();
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Stat", stopWatch);
            }
        }

        /// <summary>
        /// Checks if records with the specified id exists and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.IsExist(id);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("IsExist", stopWatch);
            }
        }

        /// <summary>
        /// Passes the state of an object and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.MakeSnapshot();
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Export", stopWatch);
            }
        }

        /// <summary>
        /// Defragments the data file and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                return this.service.Purge();
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Purge", stopWatch);
            }
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
                stopWatch.Stop();

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
                stopWatch.Stop();

                MeasuringExecutionTime("Import", stopWatch);
            }
        }

        public void Insert(FileCabinetRecord record)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                this.service.Insert(record);
            }
            finally
            {
                stopWatch.Stop();

                MeasuringExecutionTime("Insert", stopWatch);
            }
        }

        private static void MeasuringExecutionTime(string methodName, Stopwatch stopWatch)
        {
            var ticks = stopWatch.ElapsedTicks;

            Console.WriteLine($"{methodName} method execution duration is {ticks} ticks.");
        }
    }
}
