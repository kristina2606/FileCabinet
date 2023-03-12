using System.IO;

namespace FileCabinetApp
{
    /// <summary>
    /// Passes the state of an object.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private FileCabinetRecord[] records;

        /// <summary>
        /// Gets an array of all records.
        /// </summary>
        /// <param name="records">All existing records.</param>
        public void GetAllExistingRecords(FileCabinetRecord[] records)
        {
            this.records = records;
        }

        /// <summary>
        /// Creates an instance of the class and call the function to write the record to the file.
        /// </summary>
        /// <param name="streamWriter">Path to create a file with records.</param>
        public void SaveToCVS(StreamWriter streamWriter)
        {
            FileCabinetRecordCsvWriter fileCabinetRecordCsv = new FileCabinetRecordCsvWriter(streamWriter);

            foreach (var record in this.records)
            {
                fileCabinetRecordCsv.Write(record);
            }
        }
    }
}
