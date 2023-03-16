using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileCabinetApp
{
    /// <summary>
    /// Works with binary notation.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private readonly FileStream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="fileStream">Open binary record stream.</param>
        public FileCabinetFilesystemService(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        /// <summary>
        /// Writes the data passed to it in a data file.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                list.Add(record.record);
            }

            var id = list.Count + 1;

            this.WriteBinary(fileCabinetRecordNewData, 0, id, this.fileStream.Length);

            return id;
        }

        /// <summary>
        ///  Edits an already existing record in binary file by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            long position = 0;

            foreach (var record in this.GetRecordsInternal())
            {
                if (record.record.Id == id)
                {
                    position = record.position;
                }
            }

            this.WriteBinary(fileCabinetRecordNewData, 0, id, position);
        }

        /// <summary>
        /// Finds all records by date of birth in binary file.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                if (record.record.DateOfBirth == dateOfBirth)
                {
                    list.Add(record.record);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Finds all records by first name in binary file.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                if (record.record.FirstName.ToLowerInvariant() == firstName.ToLowerInvariant())
                {
                    list.Add(record.record);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Finds all records by last name in binary file.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                if (record.record.LastName.ToLowerInvariant() == lastName.ToLowerInvariant())
                {
                    list.Add(record.record);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Reads all available records from the data file.
        /// </summary>
        /// <returns>Returns all available records from the data file.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                list.Add(record.record);
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Gets the number of records stored in the file.
        /// </summary>
        /// <returns>Returns the number of records stored in the file.</returns>
        public int GetStat()
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                list.Add(record.record);
            }

            return list.Count;
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
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            foreach (var record in this.GetRecordsInternal())
            {
                list.Add(record.record);
            }

            return new FileCabinetServiceSnapshot(list.ToArray());
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

        private void WriteBinary(FileCabinetRecordNewData fileCabinetRecordNewData, short status, int id, long position)
        {
            using (BinaryWriter writer = new BinaryWriter(this.fileStream, Encoding.ASCII, true))
            {
                writer.BaseStream.Seek(position, SeekOrigin.Begin);

                writer.Write(status);
                writer.Write(id);
                writer.Write(CreateCharArray(fileCabinetRecordNewData.FirstName));
                writer.Write(CreateCharArray(fileCabinetRecordNewData.LastName));
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Year);
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Month);
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Day);
                writer.Write(fileCabinetRecordNewData.Gender);
                writer.Write(fileCabinetRecordNewData.Height);
                writer.Write(fileCabinetRecordNewData.Weight);
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

                    var status = reader.ReadInt16();
                    record.Id = reader.ReadInt32();
                    record.FirstName = string.Concat(reader.ReadChars(60).Where(c => char.IsLetter(c)));
                    record.LastName = string.Concat(reader.ReadChars(60).Where(c => char.IsLetter(c)));
                    record.DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    record.Gender = reader.ReadChar();
                    record.Height = reader.ReadInt16();
                    record.Weight = reader.ReadDecimal();

                    yield return (position, record);
                }
            }
        }
    }
}
