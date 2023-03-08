using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input with settings for adults.
    /// </summary>
    public class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetCustomService"/> class.
        /// Validate a new record from user input with settings for adults.
        /// </summary>
        /// <param name="validator">Returns validator parameters for adult.</param>
        public FileCabinetCustomService(IRecordValidator validator)
            : base(new CustomValidator())
        {
        }
    }
}
