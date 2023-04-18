using System;
using System.Collections;
using System.Collections.Generic;

namespace FileCabinetApp
{
    public class MemoryIterator : IEnumerable<FileCabinetRecord>, IEnumerator<FileCabinetRecord>
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private int currentPosition = -1;

        public MemoryIterator(List<FileCabinetRecord> list)
        {
            this.list = list;
        }

        public FileCabinetRecord Current
        {
            get { return this.list[this.currentPosition]; }
        }

        object IEnumerator.Current => this.Current;

        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            this.currentPosition++;
            return this.currentPosition < this.list.Count;
        }

        public void Reset()
        {
            this.currentPosition = -1;
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
        }
    }
}
