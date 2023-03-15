using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace FileCabinetApp
{
    /// <summary>
    /// Works with binary notation.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private readonly List<byte> counter = new List<byte>();
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
            short status = 0;

            int id = this.counter.Count + 1;
            this.counter.Add(0);

            char[] firstName = new char[60];
            for (var i = 0; i < fileCabinetRecordNewData.FirstName.Length; i++)
            {
                firstName[i] = fileCabinetRecordNewData.FirstName[i];
            }

            char[] lastName = new char[60];
            for (var i = 0; i < fileCabinetRecordNewData.LastName.Length; i++)
            {
                lastName[i] = fileCabinetRecordNewData.LastName[i];
            }

            using (BinaryWriter writer = new BinaryWriter(this.fileStream, Encoding.UTF8, true))
            {
                writer.Write(status);
                writer.Write(id);
                writer.Write(firstName);
                writer.Write(lastName);
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Year);
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Month);
                writer.Write(fileCabinetRecordNewData.DateOfBirth.Day);
                writer.Write(fileCabinetRecordNewData.Gender);
                writer.Write(fileCabinetRecordNewData.Height);
                writer.Write(fileCabinetRecordNewData.Weight);
            }

            return id;
        }

        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            using (var reader = new BinaryReader(this.fileStream))
            {
                reader.BaseStream.Position = 0;
                while (reader.PeekChar() > -1)
                {                    
                    short status = reader.ReadInt16();
                    int id = reader.ReadInt32();
                    char[] firstName = reader.ReadChars(60);
                    char[] laststName = reader.ReadChars(60);
                    int year = reader.ReadInt32();
                    int month = reader.ReadInt32();
                    int day = reader.ReadInt32();
                    char gender = reader.ReadChar();
                    short height = reader.ReadInt16();
                    decimal weight = reader.ReadInt16();

                    list.Add(new FileCabinetRecord
                    {
                        Id = id,
                        FirstName = firstName.ToString(),
                        LastName = laststName.ToString(),
                        DateOfBirth = new DateTime(year, month, day),
                        Gender = gender,
                        Height = height,
                        Weight = weight,
                    });
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        public int GetStat()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            throw new NotImplementedException();
        }
    }
}
