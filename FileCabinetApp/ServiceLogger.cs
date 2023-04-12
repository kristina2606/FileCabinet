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
        private const string DateTimeFormat = "MM/dd/yyyy HH:mm";
        private readonly IFileCabinetService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLogger"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public ServiceLogger(IFileCabinetService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Creates a new record from user input and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            int id;

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Create() with FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.");

                id = this.service.CreateRecord(fileCabinetRecordNewData);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Create() returned '{id}'.");
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
            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Edit() to record {id} with new datas FirstName = '{fileCabinetRecordNewData.FirstName}', LastName = '{fileCabinetRecordNewData.LastName}', DateOfBirth = '{fileCabinetRecordNewData.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}', Gender = '{fileCabinetRecordNewData.Gender}', Height = '{fileCabinetRecordNewData.Height}', weight = '{fileCabinetRecordNewData.Weight}'.");

                this.service.EditRecord(id, fileCabinetRecordNewData);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Edit() edited record {id}.");
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

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Find() by date of birth  DateOfBirth = '{dateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}'.");

                findedRecords = this.service.FindByDateOfBirth(dateOfBirth);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Find() by date of birth returned list with finded records.");
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

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Find() by first name FirstName = '{firstName}'.");

                findedRecords = this.service.FindByFirstName(firstName);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Find() by first name returned list with finded records.");
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

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Find() by last name LastName = '{lastName}'.");

                findedRecords = this.service.FindByLastName(lastName);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Find() by last name returned list with finded records.");
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

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling List().");

                allRecords = this.service.GetRecords();

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"List() returned all existing records.");
            }

            return allRecords;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int activeRecords, deletedRecords;

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Stat().");

                (activeRecords, deletedRecords) = this.service.GetStat();

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Stat() returned count of all active records {activeRecords} and all deleted records {deletedRecords}.");
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
            bool status;

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling IsExist() for record {id}.");

                status = this.service.IsExist(id);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"IsExist() returned status of records '{status}'.");
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

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Export().");

                snapshot = this.service.MakeSnapshot();

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Export() returned an instance of the class.");
            }

            return snapshot;
        }

        /// <summary>
        /// Defragments the data file and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            int countPurgedRecords;

            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Purge().");

                countPurgedRecords = this.service.Purge();

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Purge() returned a count of purged records {countPurgedRecords}.");
            }

            return countPurgedRecords;
        }

        /// <summary>
        /// Remove record by id and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Remove().");

                this.service.Remove(id);

                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Remove() deleted the record {id}.");
            }
        }

        /// <summary>
        /// Adding imported records to existing records and saves information about service method calls and passed parameters to a text file.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            using (StreamWriter sw = new StreamWriter("command.txt", true))
            {
                sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                sw.WriteLine($"Calling Import().");
                try
                {
                    this.service.Restore(fileCabinetServiceSnapshot);
                }
                finally
                {
                    sw.Write(DateTime.Now.ToString(DateTimeFormat, CultureInfo.InvariantCulture) + " ");
                    sw.WriteLine($"Import() all records.");
                }
            }
        }
    }
}
