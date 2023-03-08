using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with settings for adults.
    /// </summary>
    public class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Validate a new record from user input with settings for adults.
        /// </summary>
        /// <returns>Returns validator parameters for adult.</returns>
        protected override IRecordValidator CreateValidator()
        {
            return new CustomValidator();
        }
    }
}
