using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FileCabinetApp.FileCabinetService.ServiceComponents;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.RecordValidator;

namespace FileCabinetApp.FileCabinetService
{
    /// <summary>
    /// Provides functionality to work with the file system using binary notation.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private const int LengthOfOneRecord = 157;
        private const short DefaultStatus = 0;
        private const short MaskForDelete = 0b_0000_0100;

        private readonly FileStream fileStream;
        private readonly IRecordValidator validator;

        private readonly IIdGenerator idGenerator = new IdGenerator();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="fileStream">The open binary file stream.</param>
        /// <param name="validator">The record validator.</param>
        public FileCabinetFilesystemService(FileStream fileStream, IRecordValidator validator)
        {
            this.fileStream = fileStream;
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

            if (this.IsExist(id))
            {
                this.idGenerator.SkipId(this.GetExistingRecords().Max(x => x.Id));

                id = this.idGenerator.GetNext();
            }

            this.CreateRecord(ConvertToFileCabinetRecord(fileCabinetRecordNewData, id));
            return id;
        }

        /// <summary>
        /// Updates an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            this.validator.ValidateParametrs(fileCabinetRecordNewData);

            foreach ((long position, FileCabinetRecord record, short status) in this.GetRecordsInternal())
            {
                if (record.Id == id && (status & MaskForDelete) == 0)
                {
                    this.WriteBinary(ConvertToFileCabinetRecord(fileCabinetRecordNewData, id), DefaultStatus, position);
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the number of all existed and deleted records.
        /// </summary>
        /// <returns>Returns the number of all existed and deleted records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int allRecords = (int)this.fileStream.Length / LengthOfOneRecord;
            int activeRecords = this.GetExistingRecords().Count();

            return (activeRecords, allRecords - activeRecords);
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>A class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.GetExistingRecords().ToArray());
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
        /// Deletes a record by id.
        /// </summary>
        /// <param name="id">The id of the record to remove.</param>
        public void Delete(int id)
        {
            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            foreach ((long position, FileCabinetRecord record, short status) in this.GetRecordsInternal())
            {
                if (record.Id == id)
                {
                    using var writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true);
                    writer.BaseStream.Seek(position, SeekOrigin.Begin);
                    writer.Write(MaskForDelete | status);
                    break;
                }
            }
        }

        /// <summary>
        /// Defragments the data file.
        /// </summary>
        /// <returns>Count of purged records.</returns>
        public int Purge()
        {
            var positionForWrite = 0;
            var countOfrecordsForPurge = this.GetStat().deletedRecords;

            foreach (FileCabinetRecord record in this.GetExistingRecords())
            {
                this.WriteBinary(record, DefaultStatus, positionForWrite);
                positionForWrite += LengthOfOneRecord;
            }

            this.fileStream.SetLength(positionForWrite);
            return countOfrecordsForPurge;
        }

        /// <summary>
        /// Inserts a new record.
        /// </summary>
        /// <param name="record">New record from the user.</param>
        public void Insert(FileCabinetRecord record)
        {
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
            return this.GetExistingRecords().Where(x => RecordMatcher.IsMatch(x, conditions, type));
        }

        /// <summary>
        /// Checks if a record with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if the record exists, false if the record does not exist.</returns>
        public bool IsExist(int id)
        {
            return this.GetExistingRecords().Any(x => x.Id == id);
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

        private static (FileCabinetRecord record, short status) ReadOneRecordFromFile(BinaryReader reader, long positionToRead)
        {
            reader.BaseStream.Seek(positionToRead, SeekOrigin.Begin);

            var record = new FileCabinetRecord();

            short status = reader.ReadInt16();
            record.Id = reader.ReadInt32();
            record.FirstName = new string(reader.ReadChars(60)).TrimEnd((char)0);
            record.LastName = new string(reader.ReadChars(60)).TrimEnd((char)0);
            record.DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            record.Gender = reader.ReadChar();
            record.Height = reader.ReadInt16();
            record.Weight = reader.ReadDecimal();

            return (record, status);
        }

        private void WriteBinary(FileCabinetRecord record, short status, long position)
        {
            using var writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true);
            writer.BaseStream.Seek(position, SeekOrigin.Begin);

            writer.Write(status);
            writer.Write(record.Id);
            writer.Write(CreateCharArray(record.FirstName));
            writer.Write(CreateCharArray(record.LastName));
            writer.Write(record.DateOfBirth.Year);
            writer.Write(record.DateOfBirth.Month);
            writer.Write(record.DateOfBirth.Day);
            writer.Write(record.Gender);
            writer.Write(record.Height);
            writer.Write(record.Weight);
        }

        private IEnumerable<(long position, FileCabinetRecord record, short status)> GetRecordsInternal()
        {
            using var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true);
            long positionForRead = 0;

            while (positionForRead < reader.BaseStream.Length)
            {
                var (record, status) = ReadOneRecordFromFile(reader, positionForRead);

                yield return (positionForRead, record, status);
                positionForRead += LengthOfOneRecord;
            }
        }

        private void CreateRecord(FileCabinetRecord record)
        {
            this.WriteBinary(record, DefaultStatus, this.fileStream.Length);
        }

        private IEnumerable<FileCabinetRecord> GetExistingRecords()
        {
            return this.GetRecordsInternal()
                            .Where(x => (x.status & MaskForDelete) == 0)
                            .Select(record => record.record);
        }
    }
}
