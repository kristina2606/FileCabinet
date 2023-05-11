using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using FileCabinetApp.FileCabinetService.Reader;
using FileCabinetApp.FileCabinetService.Writer;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService.ServiceComponents
{
    /// <summary>
    /// Represents a snapshot of the FileCabinetService.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private readonly FileCabinetRecord[] records;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// </summary>
        /// <param name="records">All existing records.</param>
        public FileCabinetServiceSnapshot(FileCabinetRecord[] records)
        {
            this.records = records;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// </summary>
        public FileCabinetServiceSnapshot()
        {
        }

        /// <summary>
        /// Gets all records in the snapshot.
        /// </summary>
        /// <value>
        /// All records in the snapshot.
        /// </value>
        public ReadOnlyCollection<FileCabinetRecord> Records { get; private set; }

        /// <summary>
        /// Saves the records to a CSV file.
        /// </summary>
        /// <param name="streamWriter">The StreamWriter for the CSV file.</param>
        public void SaveToCsv(StreamWriter streamWriter)
        {
            var fileCabinetRecordCsv = new FileCabinetRecordCsvWriter(streamWriter);
            streamWriter.WriteLine("Id,First Name,Last Name,Date of Birth,Gender,Height,Weight");

            foreach (FileCabinetRecord record in this.records)
            {
                fileCabinetRecordCsv.Write(record);
            }
        }

        /// <summary>
        /// Saves the records to an XML file.
        /// </summary>
        /// <param name="streamWriter">The StreamWriter for the XML file.</param>
        public void SaveToXml(StreamWriter streamWriter)
        {
            using XmlWriter xmlWriter = XmlWriter.Create(streamWriter);
            var fileCabinetRecordXml = new FileCabinetRecordXmlWriter(xmlWriter);
            xmlWriter.WriteStartElement("records");

            foreach (FileCabinetRecord record in this.records)
            {
                fileCabinetRecordXml.Write(record);
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Loads records from a CSV file.
        /// </summary>
        /// <param name="reader">The StreamReader for the CSV file.</param>
        public void LoadFromCsv(StreamReader reader)
        {
            var fileCabinetRecordCsvReader = new FileCabinetRecordCsvReader(reader);
            this.Records = new ReadOnlyCollection<FileCabinetRecord>(fileCabinetRecordCsvReader.ReadAll());
        }

        /// <summary>
        /// Loads records from an XML file.
        /// </summary>
        /// <param name="reader">The StreamReader for the XML file.</param>
        public void LoadFromXml(StreamReader reader)
        {
            using XmlReader xmlReader = XmlReader.Create(reader);
            var fileCabinetRecordXmlReader = new FileCabinetRecordXmlReader(xmlReader);
            this.Records = new ReadOnlyCollection<FileCabinetRecord>(fileCabinetRecordXmlReader.ReadAll());
        }
    }
}
