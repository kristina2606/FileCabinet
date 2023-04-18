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
            var methodName = "Create()";
            var methodParameters = $"with parameters FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.";

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
                this.LoggingEndMethod(methodName, $"edited record '{id}'.");
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);

                throw;
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
                this.LoggingEndMethod(methodName, "returned list with finded records.");

                return this.service.FindByDateOfBirth(dateOfBirth);
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);

                throw;
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
                this.LoggingEndMethod(methodName, "returned list with finded records.");

                return this.service.FindByFirstName(firstName);
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);

                throw;
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
                this.LoggingEndMethod(methodName, "returned list with finded records.");

                return this.service.FindByLastName(lastName);
            }
            catch (Exception ex)
            {
                this.LoggingError(methodName, ex.Message);

                throw;
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
                this.LoggingEndMethod(methodName, "returned all existing records.");

                return this.service.GetRecords();
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
        /// <returns>Returns the count of all existing records.</returns>
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
        /// Checks if records with the specified id exists and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
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
        /// <returns>Class containing the state of an object.</returns>
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
                this.LoggingEndMethod(methodName, $"deleted the record '{id}'.");
            }
            catch
            {
                this.LoggingError(methodName, $"Record #{id} doesn't exists.");

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

        private void WriteDateAndTime()
        {
            this.streamWriter.Write(DateTime.Now.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture) + " ");
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
