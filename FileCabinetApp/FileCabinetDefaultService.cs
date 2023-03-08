using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Сhecking the values of the input parameters of the FileCabinetService class.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetDefaultService"/> class.
        /// Validate a new record from user input.
        /// </summary>
        /// <param name="validator">Returns validator parameters.</param>
        public FileCabinetDefaultService(IRecordValidator validator)
            : base(new DefaultValidator())
        {
        }
    }
}
