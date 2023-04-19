using System;
using System.Collections;
using System.Collections.Generic;

namespace FileCabinetApp
{
    public class MemoryIterator : IEnumerable<FileCabinetRecord>
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public MemoryIterator(List<FileCabinetRecord> list)
        {
            this.list = list;
        }

        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            foreach (var record in this.list)
            {
                yield return record;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
