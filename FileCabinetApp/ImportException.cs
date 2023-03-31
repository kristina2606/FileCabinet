using System;
using System.Collections.Generic;

namespace FileCabinetApp
{
    /// <summary>
    /// An ImportException is thrown when a record fails validation.
    /// </summary>
    public class ImportException : ArgumentException
    {
        private readonly Dictionary<int, string> dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportException"/> class.
        /// </summary>
        public ImportException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportException"/> class.
        /// </summary>
        /// <param name="dictionary">Dictionary with key 'id of rrecords' and value 'exeption'.</param>
        public ImportException(Dictionary<int, string> dictionary)
        {
            this.dictionary = dictionary;
        }

        /// <summary>
        /// Gets returns dictionary with key 'id of records' and value 'exeption'.
        /// </summary>
        /// <value>
        /// Returns dictionary with key 'id of records' and value 'exeption'.
        /// </value>
        public virtual Dictionary<int, string> Dictionary => this.dictionary;
    }
}
