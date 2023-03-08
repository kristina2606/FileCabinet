using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Сhecking the values of the input parameters of the FileCabinetService class.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Validate a new record from user input.
        /// </summary>
        /// <returns>Returns validator parameters.</returns>
        protected override IRecordValidator CreateValidator()
        {
            return new DefaultValidator();
        }
    }
}
