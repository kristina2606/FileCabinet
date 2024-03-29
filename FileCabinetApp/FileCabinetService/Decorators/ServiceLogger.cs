﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileCabinetApp.Constants;
using FileCabinetApp.Entity;
using FileCabinetApp.Enums;
using FileCabinetApp.Exceptions;
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
        /// <param name="service">The file cabinet service.</param>
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
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', " +
                                   $"DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString(DateTimeConstants.EUDateFormat, CultureInfo.InvariantCulture)}', " +
                                   $"Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.";

            this.LoggingActivity(methodName, methodParameters);

            try
            {
                int id = this.service.CreateRecord(fileCabinetRecordNewData);
                this.LoggingEndMethod(methodName, $"returned '{id}'.");
                return id;
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an already existing record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var methodName = "Edit()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', " +
                                   $"DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString(DateTimeConstants.EUDateFormat, CultureInfo.InvariantCulture)}', " +
                                   $"Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}' for record '{id}'.";

            this.LoggingActivity(methodName, methodParameters);

            try
            {
                this.service.Update(id, fileCabinetRecordNewData);
                this.LoggingEndMethod(methodName, $"edited record '{id}'.");
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns the number of all existed and deleted records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            var methodName = "Stat()";

            this.LoggingActivity(methodName);

            try
            {
                var (activeRecords, deletedRecords) = this.service.GetStat();
                this.LoggingEndMethod(methodName, $"returned count of all active records '{activeRecords}' and all deleted records '{deletedRecords}'.");
                return (activeRecords, deletedRecords);
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Checks if a record with the specified id exists and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if the record exists, false if the record does not exist.</returns>
        public bool IsExist(int id)
        {
            var methodName = "IsExist()";

            this.LoggingActivity(methodName);

            try
            {
                var status = this.service.IsExist(id);
                this.LoggingEndMethod(methodName, $"returned status of records '{status}'.");
                return status;
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Passes the state of an object and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>А сlass containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var methodName = "Export()";

            this.LoggingActivity(methodName);

            try
            {
                this.LoggingEndMethod(methodName, "returned an instance of the class.");
                return this.service.MakeSnapshot();
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
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

            this.LoggingActivity(methodName);

            try
            {
                var countPurgedRecords = this.service.Purge();
                this.LoggingEndMethod(methodName, $"returned a count of purged records '{countPurgedRecords}'.");
                return countPurgedRecords;
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deleteы record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id of the record to remove.</param>
        public void Delete(int id)
        {
            var methodName = "Remove()";

            this.LoggingActivity(methodName);

            try
            {
                this.service.Delete(id);
                this.LoggingEndMethod(methodName, $"deleted the record '{id}'.");
            }
            catch
            {
                this.LoggingError(methodName, $"Record #{id} doesn't exists.");
                throw;
            }
        }

        /// <summary>
        /// Adds imported records to existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var methodName = "Import()";

            this.LoggingActivity(methodName);

            try
            {
                this.service.Restore(fileCabinetServiceSnapshot);
                this.LoggingEndMethod(methodName, "import all records.");
            }
            catch (ImportException dict)
            {
                this.WriteDateAndTime();

                foreach (var exeption in dict.ImportExceptionByRecordId)
                {
                    this.streamWriter.WriteLine($"Record with id = {exeption.Key} - {exeption.Value}.");
                }

                throw;
            }
        }

        /// <summary>
        /// Inserts a new record and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="record">New record from the user.</param>
        public void Insert(FileCabinetRecord record)
        {
            var methodName = "Insert()";

            this.LoggingActivity(methodName);

            try
            {
                this.service.Insert(record);
                this.LoggingEndMethod(methodName, $"insert the record '{record.Id}'.");
            }
            catch
            {
                this.LoggingError(methodName, $"Record #{record.Id} is exists.");
                throw;
            }
        }

        /// <summary>
        /// Finds records by parameters and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns found records.</returns>
        public IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type)
        {
            var methodName = "Find()";
            var methodParameters = $"with parameter: '{string.Join(",", conditions.Select(x => x.Field))}'.";

            this.LoggingActivity(methodName, methodParameters);

            try
            {
                this.LoggingEndMethod(methodName, "returned list with finded records.");
                return this.service.Find(conditions, type);
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);
                throw;
            }
        }

        private void WriteDateAndTime()
        {
            this.streamWriter.Write(DateTime.Now.ToString(DateTimeConstants.EUDateTimeFormat, CultureInfo.InvariantCulture) + " ");
        }

        private void LoggingError(string methodName, string exceptionMessage)
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine(methodName, exceptionMessage);
        }

        private void LoggingActivity(string methodName, string methodParameters = "")
        {
            this.WriteDateAndTime();
            this.streamWriter.Write($"Calling {methodName}");

            if (!string.IsNullOrEmpty(methodParameters))
            {
                this.streamWriter.Write($" with parameter {methodParameters}");
            }

            this.streamWriter.WriteLine();
        }

        private void LoggingEndMethod(string methodName, string resultOfMethod)
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"{methodName} {resultOfMethod}");
        }
    }
}
