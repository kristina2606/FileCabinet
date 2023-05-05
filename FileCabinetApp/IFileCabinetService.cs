﻿using System;
using System.Collections.Generic;

namespace FileCabinetApp
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
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData);

        /// <summary>
        /// Gets the count of all existed and deleted records.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        (int activeRecords, int deletedRecords) GetStat();

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        FileCabinetServiceSnapshot MakeSnapshot();

        /// <summary>
        /// Adding imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot);

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        bool IsExist(int id);

        /// <summary>
        /// Defragments the data file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        int Purge();

        /// <summary>
        /// Insert new record.
        /// </summary>
        /// <param name="record">New record from user.</param>
        void Insert(FileCabinetRecord record);

        /// <summary>
        /// Finds records by parameters.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns finded records.</returns>
        IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type);

        /// <summary>
        /// Delete record by parametrs.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        void Delete(int id);

        /// <summary>
        /// Update an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">if records with the specified ID do not exist.</exception>
        void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData);
    }
}
