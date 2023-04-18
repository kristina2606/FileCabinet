using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    public class FilesystemIterator : IEnumerable<FileCabinetRecord>, IEnumerator<FileCabinetRecord>
    {
        private readonly BinaryReader reader;
        private readonly List<long> offsets;
        private int indexInList = -1;

        public FilesystemIterator(FileStream fileStream, List<long> offsets)
        {
            this.reader = new BinaryReader(fileStream, Encoding.ASCII, true);
            this.offsets = offsets;
        }

        public FileCabinetRecord Current
        {
            get
            {
                if (this.indexInList < 0 || this.indexInList >= this.offsets.Count)
                {
                    throw new InvalidOperationException();
                }

                this.reader.BaseStream.Seek(this.offsets[this.indexInList], SeekOrigin.Begin);

                FileCabinetRecord record = new FileCabinetRecord();

                this.reader.ReadInt16();
                record.Id = this.reader.ReadInt32();
                record.FirstName = new string(this.reader.ReadChars(60)).TrimEnd((char)0);
                record.LastName = new string(this.reader.ReadChars(60)).TrimEnd((char)0);
                record.DateOfBirth = new DateTime(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
                record.Gender = this.reader.ReadChar();
                record.Height = this.reader.ReadInt16();
                record.Weight = this.reader.ReadDecimal();

                return record;
            }
        }

        object IEnumerator.Current => this.Current;

        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            this.indexInList++;

            return this.indexInList < this.offsets.Count;
        }

        public void Reset()
        {
            this.indexInList = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.reader?.Dispose();
            }
        }
    }
}
