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
            short status = 0;

            var id = (int)(this.fileStream.Length / 157) + 1;
            this.WriteBinary(fileCabinetRecordNewData, status, id, this.fileStream.Length);

            return id;
        }

        /// <summary>
        ///  Edits an already existing record in binary file by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            int pos = 2;
            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                while (pos < reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                    if (reader.ReadInt32() == id)
                    {
                        pos -= 2;
                        break;
                    }

                    pos += 157;
                }
            }

            this.WriteBinary(fileCabinetRecordNewData, 0, id, pos);
        }

        /// <summary>
        /// Finds all records by date of birth in binary file.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            var position = 126;

            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                while (position < reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    var date = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

                    if (date == dateOfBirth)
                    {
                        position -= 126;

                        reader.BaseStream.Seek(position, SeekOrigin.Begin);

                        AddToListFromBinaryFile(list, reader);

                        position += 126;
                    }

                    position += 157;
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
            return new ReadOnlyCollection<FileCabinetRecord>(this.FindByParametr(firstName, 6, 6));
        }

        /// <summary>
        /// Finds all records by last name in binary file.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.FindByParametr(lastName, 66, 66));
        }

        /// <summary>
        /// Reads all available records from the data file.
        /// </summary>
        /// <returns>Returns all available records from the data file.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    AddToListFromBinaryFile(list, reader);
                }
            }

            return new ReadOnlyCollection<FileCabinetRecord>(list);
        }

        /// <summary>
        /// Gets the number of records stored in the file.
        /// </summary>
        /// <returns>Returns the number of records stored in the file.</returns>
        public int GetStat()
        {
            return (int)this.fileStream.Length / 157;
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            bool a = false;
            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                int pos = 2;

                while (pos < reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                    if (reader.ReadInt32() == id)
                    {
                        a = true;
                        break;
                    }

                    pos += 157;
                }
            }

            return a;
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    AddToListFromBinaryFile(list, reader);
                }
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

        private static void AddToListFromBinaryFile(List<FileCabinetRecord> list, BinaryReader reader)
        {
            reader.ReadInt16();

            list.Add(new FileCabinetRecord
            {
                Id = reader.ReadInt32(),
                FirstName = string.Concat(reader.ReadChars(60).Where(c => char.IsLetter(c))),
                LastName = string.Concat(reader.ReadChars(60).Where(c => char.IsLetter(c))),
                DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()),
                Gender = reader.ReadChar(),
                Height = reader.ReadInt16(),
                Weight = reader.ReadDecimal(),
            });
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

        private List<FileCabinetRecord> FindByParametr(string nameFind, long position, int count)
        {
            List<FileCabinetRecord> list = new List<FileCabinetRecord>();

            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                while (position < reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);

                    var name = string.Concat(reader.ReadChars(60).Where(c => char.IsLetter(c)));

                    if (name.ToLowerInvariant() == nameFind.ToLowerInvariant())
                    {
                        position -= count;

                        reader.BaseStream.Seek(position, SeekOrigin.Begin);

                        AddToListFromBinaryFile(list, reader);

                        position += count;
                    }

                    position += 157;
                }
            }

            return list;
        }
    }
}
