using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService.ServiceComponents
{
    /// <summary>
    /// Saves information about service method calls and passed parameters to a text file.
    /// </summary>
    public class ServiceLogger : IFileCabinetService
    {
        private readonly IFileCabinetService service;
        private readonly StreamWriter streamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLogger"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="streamWriter">Stream to write text file.</param>
        public ServiceLogger(IFileCabinetService service, StreamWriter streamWriter)
        {
            this.service = service;
            this.streamWriter = streamWriter;
        }

        /// <summary>
        /// Creates a new record from user input and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var methodName = "Create()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.";

            LoggingActivity(methodName, methodParameters);

            try
            {
                int id = service.CreateRecord(fileCabinetRecordNewData);
                LoggingEndMethod(methodName, $"returned '{id}'.");
                return id;
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Update an already existing entry by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var methodName = "Edit()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}' for record '{id}'.";

            LoggingActivity(methodName, methodParameters);
            try
            {
                service.Update(id, fileCabinetRecordNewData);
                LoggingEndMethod(methodName, $"edited record '{id}'.");
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            var methodName = "Stat()";

            LoggingActivity(methodName);
            try
            {
                var (activeRecords, deletedRecords) = service.GetStat();
                LoggingEndMethod(methodName, $"returned count of all active records '{activeRecords}' and all deleted records '{deletedRecords}'.");
                return (activeRecords, deletedRecords);
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Checks if records with the specified id exists and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            var methodName = "IsExist()";

            LoggingActivity(methodName);
            try
            {
                var status = service.IsExist(id);
                LoggingEndMethod(methodName, $"returned status of records '{status}'.");
                return status;
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Passes the state of an object and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var methodName = "Export()";

            LoggingActivity(methodName);
            try
            {
                LoggingEndMethod(methodName, "returned an instance of the class.");

                return service.MakeSnapshot();
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Defragments the data file and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            string methodName = "Purge()";

            LoggingActivity(methodName);
            try
            {
                var countPurgedRecords = service.Purge();
                LoggingEndMethod(methodName, $"returned a count of purged records '{countPurgedRecords}'.");
                return countPurgedRecords;
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Delete record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Delete(int id)
        {
            var methodName = "Remove()";

            LoggingActivity(methodName);
            try
            {
                service.Delete(id);
                LoggingEndMethod(methodName, $"deleted the record '{id}'.");
            }
            catch
            {
                LoggingError(methodName, $"Record #{id} doesn't exists.");

                throw;
            }
        }

        /// <summary>
        /// Adding imported records to existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var methodName = "Import()";

            LoggingActivity(methodName);
            try
            {
                service.Restore(fileCabinetServiceSnapshot);
                LoggingEndMethod(methodName, "import all records.");
            }
            catch (ImportException dict)
            {
                WriteDateAndTime();

                foreach (var exeption in dict.ImportExceptionByRecordId)
                {
                    streamWriter.WriteLine($"Record with id = {exeption.Key} - {exeption.Value}.");
                }

                throw;
            }
        }

        /// <summary>
        /// Insert new record and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="record">New record from user.</param>
        public void Insert(FileCabinetRecord record)
        {
            var methodName = "Insert()";

            LoggingActivity(methodName);
            try
            {
                service.Insert(record);
                LoggingEndMethod(methodName, $"insert the record '{record.Id}'.");
            }
            catch
            {
                LoggingError(methodName, $"Record #{record.Id} is exists.");

                throw;
            }
        }

        /// <summary>
        /// Finds records by parameters and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns finded records.</returns>
        public IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type)
        {
            var methodName = "Find()";
            var methodParameters = $"with parameter: '{string.Join(",", conditions.Select(x => x.Field))}'.";

            LoggingActivity(methodName, methodParameters);
            try
            {
                LoggingEndMethod(methodName, "returned list with finded records.");

                return service.Find(conditions, type);
            }
            catch (Exception ex)
            {
                LoggingError(methodName, ex.Message);

                throw;
            }
        }

        private void WriteDateAndTime()
        {
            streamWriter.Write(DateTime.Now.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture) + " ");
        }

        private void LoggingError(string methodName, string exceptionMessage)
        {
            WriteDateAndTime();
            streamWriter.WriteLine(methodName, exceptionMessage);
        }

        private void LoggingActivity(string methodName, string methodParameters = "")
        {
            WriteDateAndTime();
            streamWriter.Write($"Calling {methodName}");
            if (!string.IsNullOrEmpty(methodParameters))
            {
                streamWriter.Write($" with parameter {methodParameters}");
            }

            streamWriter.WriteLine();
        }

        private void LoggingEndMethod(string methodName, string resultOfMethod)
        {
            WriteDateAndTime();
            streamWriter.WriteLine($"{methodName} {resultOfMethod}");
        }
    }
}
