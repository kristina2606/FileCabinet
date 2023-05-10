using FileCabinetApp.Models;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Validate a new record from user input with new validate parametrs.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>
        /// Validate a new record from user input with new validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new data in the record.</param>
        void ValidateParametrs(FileCabinetRecordNewData fileCabinetRecordNewData);
    }
}
