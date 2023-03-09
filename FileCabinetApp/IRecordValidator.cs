namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with new validate parametrs.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>
        /// Validate a new record from user input with new validate parametrs.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        void Validate(FileCabinetRecordNewData fileCabinetRecordNewData);
    }
}
