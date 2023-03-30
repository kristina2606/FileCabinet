﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Works with binary notation.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private const int LengthOfOneRecord = 157;
        private const short DefaultStatus = 0;

        private readonly IIdGenerator idGenerator = new IdGenerator();
        private readonly FileStream fileStream;
        private readonly IRecordValidator validator = new DefaultValidator();
        private readonly Dictionary<int, string> importExeptions = new Dictionary<int, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="fileStream">Open binary record stream.</param>
        public FileCabinetFilesystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.Validate(fileCabinetRecordNewData);

            var id = this.idGenerator.GetNext();

            this.WriteBinary(ConvertToFileCabinetRecord(fileCabinetRecordNewData, id), DefaultStatus, this.fileStream.Length);

            return id;
        }

        /// <summary>
        ///  Edits an already existing record in binary file by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.Validate(fileCabinetRecordNewData);

            foreach (var (position, record) in this.GetRecordsInternal())
            {
                if (record.Id == id)
                {
                    this.WriteBinary(ConvertToFileCabinetRecord(fileCabinetRecordNewData, id), DefaultStatus, position);
                    break;
                }
            }
        }

        /// <summary>
        /// Finds all records by date of birth in binary file.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            var list = this.GetRecordsInternal()
                .Select(record => record.record)
                .Where(record => record.DateOfBirth == dateOfBirth)
                .ToList();

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Finds all records by first name in binary file.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            var list = this.GetRecordsInternal()
                .Select(record => record.record)
                .Where(record => record.FirstName.ToLowerInvariant() == firstName.ToLowerInvariant())
                .ToList();

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Finds all records by last name in binary file.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            var list = this.GetRecordsInternal()
                .Select(record => record.record)
                .Where(record => record.LastName.ToLowerInvariant() == lastName.ToLowerInvariant())
                .ToList();

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Reads all available records from the data file.
        /// </summary>
        /// <returns>Returns all available records from the data file.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.GetRecordsInternal()
                .Select(record => record.record)
                .ToList());
        }

        /// <summary>
        /// Gets the number of records stored in the file.
        /// </summary>
        /// <returns>Returns the number of records stored in the file.</returns>
        public int GetStat()
        {
            return (int)this.fileStream.Length / LengthOfOneRecord;
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            return this.GetRecordsInternal().Any(x => x.record.Id == id);
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.GetRecordsInternal()
                .Select(record => record.record)
                .ToArray());
        }

        /// <summary>
        /// Adding imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var records = fileCabinetServiceSnapshot.Records;

            this.idGenerator.SkipId(records.Max(x => x.Id));

            foreach (var record in records)
            {
                var recordNew = new FileCabinetRecordNewData(record.FirstName, record.LastName, record.DateOfBirth, record.Gender, record.Height, record.Weight);
                try
                {
                    this.validator.Validate(recordNew);

                    if (this.IsExist(record.Id))
                    {
                        this.EditRecord(record.Id, recordNew);
                    }
                    else
                    {
                        this.Create(record);
                    }
                }
                catch (Exception ex)
                {
                    this.importExeptions.Add(record.Id, ex.Message);
                }
            }
        }

        /// <summary>
        /// Get all import exeptions.
        /// </summary>
        /// <returns>Dictionary with key 'id with exeption' and value 'messege of exeption.</returns>
        public Dictionary<int, string> GetAllImportExeptions()
        {
            return this.importExeptions;
        }

        private static char[] CreateCharArray(string name)
        {
            char[] newName = new char[60];
            for (var i = 0; i < name.Length; i++)
            {
                newName[i] = name[i];
            }

            return newName;
        }

        private static FileCabinetRecord ConvertToFileCabinetRecord(FileCabinetRecordNewData fileCabinetRecordNewData, int id)
        {
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

            return record;
        }

        private void WriteBinary(FileCabinetRecord fileCabinetRecord, short status, long position)
        {
            using (BinaryWriter writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true))
            {
                writer.BaseStream.Seek(position, SeekOrigin.Begin);

                writer.Write(status);
                writer.Write(fileCabinetRecord.Id);
                writer.Write(CreateCharArray(fileCabinetRecord.FirstName));
                writer.Write(CreateCharArray(fileCabinetRecord.LastName));
                writer.Write(fileCabinetRecord.DateOfBirth.Year);
                writer.Write(fileCabinetRecord.DateOfBirth.Month);
                writer.Write(fileCabinetRecord.DateOfBirth.Day);
                writer.Write(fileCabinetRecord.Gender);
                writer.Write(fileCabinetRecord.Height);
                writer.Write(fileCabinetRecord.Weight);
            }
        }

        private IEnumerable<(long position, FileCabinetRecord record)> GetRecordsInternal()
        {
            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    FileCabinetRecord record = new FileCabinetRecord();
                    long position = reader.BaseStream.Position;

                    reader.ReadInt16();
                    record.Id = reader.ReadInt32();
                    record.FirstName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.LastName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    record.Gender = reader.ReadChar();
                    record.Height = reader.ReadInt16();
                    record.Weight = reader.ReadDecimal();

                    yield return (position, record);
                }
            }
        }

        private void Create(FileCabinetRecord record)
        {
            this.WriteBinary(record, DefaultStatus, this.fileStream.Length);
        }
    }
}
