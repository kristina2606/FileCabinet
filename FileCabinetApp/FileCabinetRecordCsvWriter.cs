using System.IO;

namespace FileCabinetApp
{
    /// <summary>
    /// Export application data to a CSV file.
    /// </summary>
    public class FileCabinetRecordCsvWriter
    {
        private readonly TextWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvWriter"/> class.
        /// </summary>
        /// <param name="writer">Path to create a file with records.</param>
        public FileCabinetRecordCsvWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Write the record to the file.
        /// </summary>
        /// <param name="record">Record for write.</param>
        public void Write(FileCabinetRecord record)
        {
            this.writer.WriteLine("Id,First Name,Last Name,Date of Birth,Gender,Height,Weight");
            this.writer.WriteLine(record.Id + "," + record.FirstName + "," + record.LastName + "," + record.DateOfBirth + "," + record.Gender + "," + record.Height + "," + record.Weight);
        }
    }
}
