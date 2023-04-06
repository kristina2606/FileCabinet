using System;
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
        private const short MaskForDelete = 0b_0000_0100;
        private const StringComparison ScType = StringComparison.InvariantCultureIgnoreCase;

        private readonly FileStream fileStream;
        private readonly IRecordValidator validator;

        private readonly IIdGenerator idGenerator = new IdGenerator();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="fileStream">Open binary record stream.</param>
        /// <param name="validator">Validation parameter.</param>
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
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.Validate(fileCabinetRecordNewData);

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
        ///  Edits an already existing record in binary file by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            this.validator.Validate(fileCabinetRecordNewData);

            foreach (var (position, record, status) in this.GetRecordsInternal())
            {
                if (record.Id == id && (status & MaskForDelete) == 0)
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
            var list = this.GetExistingRecords()
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
            var list = this.GetExistingRecords()
                .Where(record => record.FirstName.Equals(firstName, ScType))
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
            var list = this.GetExistingRecords()
                .Where(record => record.LastName.Equals(lastName, ScType))
                .ToList();

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Reads all available records from the data file.
        /// </summary>
        /// <returns>Returns all available records from the data file.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.GetExistingRecords().ToList());
        }

        /// <summary>
        /// Gets the number of all existed and deleted records stored in the file.
        /// </summary>
        /// <returns>Returns the number of all existed and deleted records stored in the file.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            int allRecords = (int)this.fileStream.Length / LengthOfOneRecord;
            var activeRecords = this.GetExistingRecords().Count();

            return (activeRecords, allRecords - activeRecords);
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.GetExistingRecords().ToArray());
        }

        /// <summary>
        /// Adding imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Сlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var records = fileCabinetServiceSnapshot.Records;
            Dictionary<int, string> importExceptionByRecordId = new Dictionary<int, string>();
            bool isError = false;

            foreach (var record in records)
            {
                this.idGenerator.SkipId(record.Id);
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
        /// Remove record by id.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Remove(int id)
        {
            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            foreach (var (position, record, status) in this.GetRecordsInternal())
            {
                if (record.Id == id)
                {
                    using (BinaryWriter writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true))
                    {
                        writer.BaseStream.Seek(position, SeekOrigin.Begin);

                        writer.Write(MaskForDelete | status);
                    }

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

            foreach (var record in this.GetExistingRecords())
            {
                this.WriteBinary(record, DefaultStatus, positionForWrite);
                positionForWrite += LengthOfOneRecord;
            }

            this.fileStream.SetLength(positionForWrite);
            return countOfrecordsForPurge;
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
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

        private void WriteBinary(FileCabinetRecord record, short status, long position)
        {
            using (BinaryWriter writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true))
            {
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
        }

        private IEnumerable<(long position, FileCabinetRecord record, short status)> GetRecordsInternal()
        {
            var positionForRead = 0;

            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                while (positionForRead < reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(positionForRead, SeekOrigin.Begin);
                    FileCabinetRecord record = new FileCabinetRecord();

                    short status = reader.ReadInt16();
                    record.Id = reader.ReadInt32();
                    record.FirstName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.LastName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    record.Gender = reader.ReadChar();
                    record.Height = reader.ReadInt16();
                    record.Weight = reader.ReadDecimal();

                    yield return (positionForRead, record, status);
                    positionForRead += LengthOfOneRecord;
                }
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
