using System;
using System.Collections.Generic;

namespace FileCabinetApp
{
    public class MemoryIterator : IRecordIterator
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private int currentPosition;

        public MemoryIterator(List<FileCabinetRecord> list)
        {
            this.list = list;
        }

        public FileCabinetRecord GetNext()
        {
            if (!this.HasMore())
            {
                return null;
            }

            return this.list[this.currentPosition++];
        }

        public bool HasMore()
        {
            return this.currentPosition < this.list.Count;
        }
    }
}
