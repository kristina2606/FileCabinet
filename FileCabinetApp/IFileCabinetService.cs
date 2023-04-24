﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        /// Gets all existing records.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>
        /// Gets the count of all existed and deleted records.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        (int activeRecords, int deletedRecords) GetStat();

        /// <summary>
        /// Edits an already existing entry by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">if records with the specified ID do not exist.</exception>
        void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData);

        /// <summary>
        /// Finds all records by first name.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        IEnumerable<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>
        /// Finds all records by last name.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        IEnumerable<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>
        /// Finds all records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        IEnumerable<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth);

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
        /// Remove record by id.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        void Remove(int id);

        /// <summary>
        /// Defragments the data file.
        /// </summary>
        /// <returns>Count of purged records. Only for FileCabinetFilesystemService.</returns>
        int Purge();

        void Insert(FileCabinetRecord record);
    }
}
