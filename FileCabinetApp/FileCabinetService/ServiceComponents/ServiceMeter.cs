using System;
using System.Collections.Generic;
using System.Diagnostics;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService.ServiceComponents
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
        /// <param name="service">The file cabinet service.</param>
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
        /// Updates an already existing entry by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            try
            {
                this.service.Update(id, fileCabinetRecordNewData);
            }
            finally
            {
                stopWatch.Stop();
                MeasuringExecutionTime("Edit", stopWatch);
            }
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <returns>Returns the number of all existed and deleted records.</returns>
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
        /// Checks if a record with the specified id exists and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if the record exists, false if the record does not exist.</returns>
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
        /// <returns>A class containing the state of an object.</returns>
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
        /// Deletes a record by id and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="id">The id of the record to remove.</param>
        public void Delete(int id)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            try
            {
                this.service.Delete(id);
            }
            finally
            {
                stopWatch.Stop();
                MeasuringExecutionTime("Remove", stopWatch);
            }
        }

        /// <summary>
        /// Adds imported records to existing records and measures the running time of service methods and displays them on the screen.
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

        /// <summary>
        /// Inserts new record and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="record">New record from the user.</param>
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

        /// <summary>
        /// Finds records by parameters and measures the running time of service methods and displays them on the screen.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns found records.</returns>
        public IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            try
            {
                return this.service.Find(conditions, type);
            }
            finally
            {
                stopWatch.Stop();
                MeasuringExecutionTime("Find()", stopWatch);
            }
        }

        private static void MeasuringExecutionTime(string methodName, Stopwatch stopWatch)
        {
            var ticks = stopWatch.ElapsedTicks;

            Console.WriteLine($"{methodName} method execution duration is {ticks} ticks.");
        }
    }
}
