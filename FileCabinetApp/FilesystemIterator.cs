using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    public class FilesystemIterator : IRecordIterator
    {
        private readonly BinaryReader reader;
        private readonly List<long> offsets;
        private int indexInList;

        public FilesystemIterator(FileStream fileStream, List<long> offsets)
        {
            this.reader = new BinaryReader(fileStream, Encoding.ASCII, true);
            this.offsets = offsets;
        }

        public FileCabinetRecord GetNext()
        {
            if (!this.HasMore())
            {
                this.Dispose();
                return null;
            }

            this.reader.BaseStream.Seek(this.offsets[this.indexInList], SeekOrigin.Begin);

            FileCabinetRecord record = new FileCabinetRecord();

            short status = this.reader.ReadInt16();
            record.Id = this.reader.ReadInt32();
            record.FirstName = new string(this.reader.ReadChars(60)).TrimEnd((char)0);
            record.LastName = new string(this.reader.ReadChars(60)).TrimEnd((char)0);
            record.DateOfBirth = new DateTime(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
            record.Gender = this.reader.ReadChar();
            record.Height = this.reader.ReadInt16();
            record.Weight = this.reader.ReadDecimal();

            this.indexInList++;

            return record;
        }

        public bool HasMore()
        {
            return this.indexInList < this.offsets.Count;
        }

        private void Dispose()
        {
            this.reader.Dispose();
        }
    }
}
