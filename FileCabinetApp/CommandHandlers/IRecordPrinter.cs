using System.Collections.Generic;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Print with screen print style.
    /// </summary>
    public interface IRecordPrinter
    {
        /// <summary>
        /// Print with screen print style.
        /// </summary>
        /// <param name="records">All exiting records.</param>
        void Print(IEnumerable<FileCabinetRecord> records);
    }
}
