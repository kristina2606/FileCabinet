using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileCabinetApp
{
    /// <summary>
    /// An ImportException is thrown when a record fails validation.
    /// </summary>
    public class ImportException : Exception
    {
        private readonly Dictionary<int, string> importExeptionsDictionary;

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
            this.importExeptionsDictionary = dictionary;
        }

        /// <summary>
        /// Gets returns dictionary with key 'id of records' and value 'exeption'.
        /// </summary>
        /// <value>
        /// Returns dictionary with key 'id of records' and value 'exeption'.
        /// </value>
        public ReadOnlyDictionary<int, string> Dictionary => new ReadOnlyDictionary<int, string>(this.importExeptionsDictionary);
    }
}
