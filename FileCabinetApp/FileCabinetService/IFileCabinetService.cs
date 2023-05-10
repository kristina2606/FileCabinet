using System.Collections.Generic;
using FileCabinetApp.FileCabinetService.ServiceComponents;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService
{
    /// <summary>
    /// Creates, edits and checks in entries. Finds records by parameters.
    /// </summary>
    public interface IFileCabinetService
    {
        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData);

        /// <summary>
        /// Gets the count of all existing and deleted records.
        /// </summary>
        /// <returns>Returns the number of all existed and deleted records.</returns>
        (int activeRecords, int deletedRecords) GetStat();

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>A class containing the state of an object.</returns>
        FileCabinetServiceSnapshot MakeSnapshot();

        /// <summary>
        /// Adds imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot);

        /// <summary>
        /// Checks if a record with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if the record exists, false if the record does not exist.</returns>
        bool IsExist(int id);

        /// <summary>
        /// Defragments the data file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        int Purge();

        /// <summary>
        /// Inserts a new record.
        /// </summary>
        /// <param name="record">New record from the user.</param>
        void Insert(FileCabinetRecord record);

        /// <summary>
        /// Finds records by parameters.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns found records.</returns>
        IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type);

        /// <summary>
        /// Deletes a record by id.
        /// </summary>
        /// <param name="id">The id of the record to remove.</param>
        void Delete(int id);

        /// <summary>
        /// Updates an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData);
    }
}
