using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with new validate parametrs.
    /// </summary>
    internal interface IRecordValidator
    {
        /// <summary>
        /// Validate a new record from user input with new validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData);
    }
}
