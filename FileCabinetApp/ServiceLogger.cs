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

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Create() with FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.");
            try
            {
                id = this.service.CreateRecord(fileCabinetRecordNewData);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Create() returned '{id}'.");
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
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Edit() to record {id} with new datas FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.");
            try
            {
                this.service.EditRecord(id, fileCabinetRecordNewData);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Edit() edited record {id}.");
            }
        }

        /// <summary>
        /// Finds all records by date of birth and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Find() by date of birth  DateOfBirth = '{dateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}'.");
            try
            {
                findedRecords = this.service.FindByDateOfBirth(dateOfBirth);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Find() by date of birth returned list with finded records.");
            }

            return findedRecords;
        }

        /// <summary>
        /// Finds all records by first name and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Find() by first name FirstName = '{firstName}'.");
            try
            {
                findedRecords = this.service.FindByFirstName(firstName);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Find() by first name returned list with finded records.");
            }

            return findedRecords;
        }

        /// <summary>
        /// Finds all records by last name and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            ReadOnlyCollection<FileCabinetRecord> findedRecords;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Find() by last name LastName = '{lastName}'.");
            try
            {
                findedRecords = this.service.FindByLastName(lastName);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Find() by last name returned list with finded records.");
            }

            return findedRecords;
        }

        /// <summary>
        /// Gets all existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            ReadOnlyCollection<FileCabinetRecord> allRecords;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling List().");
            try
            {
                allRecords = this.service.GetRecords();
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"List() returned all existing records.");
            }

            return allRecords;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int activeRecords = 0, deletedRecords = 0;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Stat().");
            try
            {
                (activeRecords, deletedRecords) = this.service.GetStat();
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Stat() returned count of all active records {activeRecords} and all deleted records {deletedRecords}.");
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

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling IsExist() for record {id}.");
            try
            {
                status = this.service.IsExist(id);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"IsExist() returned status of records '{status}'.");
            }

            return status;
        }

        /// <summary>
        /// Passes the state of an object and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            FileCabinetServiceSnapshot snapshot;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Export().");
            try
            {
                snapshot = this.service.MakeSnapshot();
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Export() returned an instance of the class.");
            }

            return snapshot;
        }

        /// <summary>
        /// Defragments the data file and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            int countPurgedRecords = 0;

            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Purge().");
            try
            {
                countPurgedRecords = this.service.Purge();
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Purge() returned a count of purged records {countPurgedRecords}.");
            }

            return countPurgedRecords;
        }

        /// <summary>
        /// Remove record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Remove().");
            try
            {
                this.service.Remove(id);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Remove() deleted the record {id}.");
            }
        }

        /// <summary>
        /// Adding imported records to existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            this.WriteDateAndTime();
            this.streamWriter.WriteLine($"Calling Import().");

            try
            {
                this.service.Restore(fileCabinetServiceSnapshot);
            }
            finally
            {
                this.WriteDateAndTime();
                this.streamWriter.WriteLine($"Import() all records.");
            }
        }

        private void WriteDateAndTime()
        {
            this.streamWriter.Write(DateTime.Now.ToString("MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture) + " ");
        }
    }
}
