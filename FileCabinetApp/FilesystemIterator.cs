using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    public class FilesystemIterator : IEnumerable<FileCabinetRecord>
    {
        private readonly FileStream fileStream;
        private readonly List<long> offsets;

        public FilesystemIterator(FileStream fileStream, List<long> offsets)
        {
            this.offsets = offsets;
            this.fileStream = fileStream;
        }

        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            using (var reader = new BinaryReader(this.fileStream, Encoding.ASCII, true))
            {
                foreach (var offset in this.offsets)
                {
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);

                    FileCabinetRecord record = new FileCabinetRecord();

                    reader.ReadInt16();
                    record.Id = reader.ReadInt32();
                    record.FirstName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.LastName = new string(reader.ReadChars(60)).TrimEnd((char)0);
                    record.DateOfBirth = new DateTime(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    record.Gender = reader.ReadChar();
                    record.Height = reader.ReadInt16();
                    record.Weight = reader.ReadDecimal();

                    yield return record;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
