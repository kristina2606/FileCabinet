using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace FileCabinetApp
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
            int id = 0;

            var methodName = "Create()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.";

            this.LoggingActivity(methodName, methodParameters);

            try
            {
                id = this.service.CreateRecord(fileCabinetRecordNewData);
            }
            finally
            {
                var resultOfMethod = $"returned '{id}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }

            return id;
        }

        /// <summary>
        /// Edits an already existing entry by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var methodName = "Edit()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}' for record '{id}'.";

            this.LoggingActivity(methodName, methodParameters);
            try
            {
                this.service.EditRecord(id, fileCabinetRecordNewData);
            }
            finally
            {
                var resultOfMethod = $"edited record '{id}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Finds all records by date of birth and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            var methodName = "Find() by date of birth";
            var methodParameters = $"with parameter DateOfBirth = '{dateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}'.";

            this.LoggingActivity(methodName, methodParameters);
            try
            {
                return this.service.FindByDateOfBirth(dateOfBirth);
            }
            finally
            {
                var resultOfMethod = "returned list with finded records.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Finds all records by first name and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            var methodName = "Find() by first name";
            var methodParameters = $"with parameter FirstName = '{firstName}'.";

            this.LoggingActivity(methodName, methodParameters);
            try
            {
                return this.service.FindByFirstName(firstName);
            }
            finally
            {
                var resultOfMethod = "returned list with finded records.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Finds all records by last name and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            var methodName = "Find() by last name";
            var methodParameters = $"with parameter LastName = '{lastName}'.";

            this.LoggingActivity(methodName, methodParameters);
            try
            {
                return this.service.FindByLastName(lastName);
            }
            finally
            {
                var resultOfMethod = "returned list with finded records.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Gets all existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            var methodName = "List()";

            this.LoggingActivity(methodName);
            try
            {
                return this.service.GetRecords();
            }
            finally
            {
                var resultOfMethod = "returned all existing records.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int activeRecords = 0, deletedRecords = 0;
            var methodName = "Stat()";

            this.LoggingActivity(methodName);
            try
            {
                (activeRecords, deletedRecords) = this.service.GetStat();
            }
            finally
            {
                var resultOfMethod = $"returned count of all active records '{activeRecords}' and all deleted records '{deletedRecords}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }

            return (activeRecords, deletedRecords);
        }

        /// <summary>
        /// Checks if records with the specified id exists and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            bool status = false;
            var methodName = "IsExist()";

            this.LoggingActivity(methodName);
            try
            {
                status = this.service.IsExist(id);
            }
            finally
            {
                var resultOfMethod = $"returned status of records '{status}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }

            return status;
        }

        /// <summary>
        /// Passes the state of an object and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            var methodName = "Export()";

            this.LoggingActivity(methodName);
            try
            {
                return this.service.MakeSnapshot();
            }
            finally
            {
                var resultOfMethod = "returned an instance of the class.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Defragments the data file and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            int countPurgedRecords = 0;
            var methodName = "Purge()";

            this.LoggingActivity(methodName);
            try
            {
                countPurgedRecords = this.service.Purge();
            }
            finally
            {
                var resultOfMethod = $"returned a count of purged records '{countPurgedRecords}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }

            return countPurgedRecords;
        }

        /// <summary>
        /// Remove record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            var methodName = "Remove()";

            this.LoggingActivity(methodName);
            try
            {
                this.service.Remove(id);
            }
            finally
            {
                var resultOfMethod = $"deleted the record '{id}'.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        /// <summary>
        /// Adding imported records to existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var methodName = "Import()";

            this.LoggingActivity(methodName);
            try
            {
                this.service.Restore(fileCabinetServiceSnapshot);
            }
            finally
            {
                var resultOfMethod = "import all records.";

                this.LoggingEndMethod(methodName, resultOfMethod);
            }
        }

        private void WriteDateAndTime()
        {
            this.streamWriter.Write(DateTime.Now.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture) + " ");
        }

        private void LoggingActivity(string methodName, string methodParameters = "")
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling {methodName} {methodParameters}");
        }

        private void LoggingEndMethod(string methodName, string resultOfMethod)
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"{methodName} {resultOfMethod}");
        }
    }
}
