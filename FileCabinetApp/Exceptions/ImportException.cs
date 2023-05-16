using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileCabinetApp.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a record fails validation during import.
    /// </summary>
    public class ImportException : Exception
    {
        private readonly Dictionary<int, string> importExceptionByRecordId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportException"/> class.
        /// </summary>
        public ImportException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportException"/> class.
        /// </summary>
        /// <param name="exceptions">A dictionary containing the exceptions associated with record IDs.</param>
        public ImportException(Dictionary<int, string> exceptions)
            : base()
        {
            this.importExceptionByRecordId = exceptions;
        }

        /// <summary>
        /// Gets a read-only dictionary containing the exceptions associated with record IDs.
        /// </summary>
        /// <value>A read-only dictionary with record IDs as keys and exception messages as values.</value>
        public ReadOnlyDictionary<int, string> ImportExceptionByRecordId => new ReadOnlyDictionary<int, string>(this.importExceptionByRecordId);
    }
}
