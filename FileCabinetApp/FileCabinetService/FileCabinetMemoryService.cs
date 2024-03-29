﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FileCabinetApp.Constants;
using FileCabinetApp.Entity;
using FileCabinetApp.Enums;
using FileCabinetApp.Exceptions;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.RecordValidator;

[assembly: CLSCompliant(true)]

namespace FileCabinetApp.FileCabinetService
{
    /// <summary>
    /// Provides functionality to create, edit, and search entries. Finds records based on parameters.
    /// </summary>
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> memorizater = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.InvariantCultureIgnoreCase);

        private readonly IIdGenerator idGenerator = new IdGenerator();
        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetMemoryService"/> class.
        /// The class constructor takes a record validator parameter.
        /// </summary>
        /// <param name="validator">The record validator.</param>
        public FileCabinetMemoryService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.ValidateParametrs(fileCabinetRecordNewData);

            var id = this.idGenerator.GetNext();

            var record = new FileCabinetRecord
            {
                Id = id,
                FirstName = fileCabinetRecordNewData.FirstName,
                LastName = fileCabinetRecordNewData.LastName,
                DateOfBirth = fileCabinetRecordNewData.DateOfBirth,
                Gender = fileCabinetRecordNewData.Gender,
                Height = fileCabinetRecordNewData.Height,
                Weight = fileCabinetRecordNewData.Weight,
            };

            this.CreateRecord(record);
            return record.Id;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records.
        /// </summary>
        /// <returns>Returns the count of all existed and deleted records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            return (this.list.Count, 0);
        }

        /// <summary>
        /// Updates an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.memorizater.Clear();

            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            this.validator.ValidateParametrs(fileCabinetRecordNewData);

            var result = this.list.FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }

            result.FirstName = fileCabinetRecordNewData.FirstName;
            result.LastName = fileCabinetRecordNewData.LastName;
            result.DateOfBirth = fileCabinetRecordNewData.DateOfBirth;
            result.Gender = fileCabinetRecordNewData.Gender;
            result.Height = fileCabinetRecordNewData.Height;
            result.Weight = fileCabinetRecordNewData.Weight;
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>A class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list.ToArray());
        }

        /// <summary>
        /// Adds imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var records = fileCabinetServiceSnapshot.Records;
            var importExceptionByRecordId = new Dictionary<int, string>();
            bool isError = false;

            foreach (FileCabinetRecord record in records)
            {
                this.idGenerator.SkipId(record.Id);

                var recordNew = new FileCabinetRecordNewData(record.FirstName, record.LastName, record.DateOfBirth, record.Gender, record.Height, record.Weight);
                try
                {
                    this.validator.ValidateParametrs(recordNew);
                    if (this.IsExist(record.Id))
                    {
                        this.Update(record.Id, recordNew);
                    }
                    else
                    {
                        this.CreateRecord(record);
                    }
                }
                catch (Exception ex)
                {
                    importExceptionByRecordId.Add(record.Id, ex.Message);
                    isError = true;
                }
            }

            if (isError)
            {
                throw new ImportException(importExceptionByRecordId);
            }
        }

        /// <summary>
        /// Delete a record by id.
        /// </summary>
        /// <param name="id">The id of the record to remove.</param>
        public void Delete(int id)
        {
            this.memorizater.Clear();

            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            var valueForRemove = this.list.Find(x => x.Id == id);

            this.list.Remove(valueForRemove);
        }

        /// <summary>
        /// Defragments the data file. Only for FileCabinetFilesystemService.
        /// </summary>
        /// <returns>Count of purged records.Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            return 0;
        }

        /// <summary>
        /// Inserts a new record.
        /// </summary>
        /// <param name="record">New record from the user.</param>
        public void Insert(FileCabinetRecord record)
        {
            this.memorizater.Clear();

            if (this.IsExist(record.Id))
            {
                throw new ArgumentException("Record's id is exist.");
            }

            this.CreateRecord(record);
        }

        /// <summary>
        /// Finds records by parameters.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns found records.</returns>
        public IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type)
        {
            string key = null;

            if (conditions.Length > 0)
            {
                key = CreateKeyForMemorization(conditions, type);

                if (this.memorizater.TryGetValue(key, out var records))
                {
                    return records;
                }
            }

            var result = this.list.Where(x => RecordMatcher.IsMatch(x, conditions, type));

            if (key != null)
            {
                this.memorizater.Add(key, result.ToList());
            }

            return result;
        }

        /// <summary>
        /// Checks if a record with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if the record exists, false if the record does not exist.</returns>
        public bool IsExist(int id)
        {
            return this.list.Any(x => x.Id == id);
        }

        private static string CreateKeyForMemorization(Condition[] conditions, UnionType type)
        {
            var key = new StringBuilder();
            key.Append(CultureInfo.InvariantCulture, $"Find_ConditionFields:{string.Join(',', conditions.Select(x => x.Field))}");

            var fieldsValueForKey = new List<string>();

            foreach (Condition condition in conditions)
            {
                switch (condition.Field)
                {
                    case FileCabinetRecordFields.Id:
                        fieldsValueForKey.Add(condition.Value.Id.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.FirstName:
                        fieldsValueForKey.Add(condition.Value.FirstName);
                        break;
                    case FileCabinetRecordFields.LastName:
                        fieldsValueForKey.Add(condition.Value.LastName);
                        break;
                    case FileCabinetRecordFields.DateOfBirth:
                        fieldsValueForKey.Add(condition.Value.DateOfBirth.ToString(DateTimeConstants.FullDateFormat, CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Gender:
                        fieldsValueForKey.Add(condition.Value.Gender.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Height:
                        fieldsValueForKey.Add(condition.Value.Height.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Weight:
                        fieldsValueForKey.Add(condition.Value.Weight.ToString(CultureInfo.InvariantCulture));
                        break;
                }
            }

            key.Append(CultureInfo.InvariantCulture, $"_ConditionValue:{string.Join(',', fieldsValueForKey)}_UnionType:{type}");
            return key.ToString();
        }

        private void CreateRecord(FileCabinetRecord record)
        {
            this.list.Add(record);
        }
    }
}