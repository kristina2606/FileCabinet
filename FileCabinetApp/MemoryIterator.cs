using System;
using System.Collections;
using System.Collections.Generic;

namespace FileCabinetApp
{
    /// <summary>
    /// Return an element from a pre-created list.
    /// </summary>
    public class MemoryIterator : IEnumerable<FileCabinetRecord>
    {
        private readonly List<FileCabinetRecord> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryIterator"/> class.
        /// </summary>
        /// <param name="list">List with all existing records.</param>
        public MemoryIterator(List<FileCabinetRecord> list)
        {
            this.list = list;
        }

        /// <summary>
        /// Defines the MoveNext(), Reset() and Current methods.
        /// </summary>
        /// <returns>Returns the object IEnumerator 'FileCabinetRecord'.</returns>
        public IEnumerator<FileCabinetRecord> GetEnumerator()
        {
            foreach (var record in this.list)
            {
                yield return record;
            }
        }

        /// <summary>
        /// Implicit implementation of IEnumerable.
        /// </summary>
        /// <returns>Returns object IEnumerator type.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
