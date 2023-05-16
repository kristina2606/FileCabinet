using System.IO;
using FileCabinetApp.Models;

namespace FileCabinetApp.Serializers.Writer
{
    /// <summary>
    /// Exports application data to a CSV file.
    /// </summary>
    public class FileCabinetRecordCsvWriter
    {
        private readonly TextWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvWriter"/> class.
        /// </summary>
        /// <param name="writer">The writer to create a file with records.</param>
        public FileCabinetRecordCsvWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes the record to the file.
        /// </summary>
        /// <param name="record">The record to write.</param>
        public void Write(FileCabinetRecord record)
        {
            this.writer.WriteLine($"{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth}, {record.Gender}, {record.Height}, {record.Weight}");
        }
    }
}
