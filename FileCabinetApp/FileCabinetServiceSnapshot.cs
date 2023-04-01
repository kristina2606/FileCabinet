using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    /// Passes the state of an object.
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
        /// Gets all imported data from a csv file.
        /// </summary>
        /// <value>
        /// All imported data from a csv file.
        /// </value>
        public ReadOnlyCollection<FileCabinetRecord> Records { get; private set; }

        /// <summary>
        /// Creates an instance of the class and call the function to write the record to the .csv file.
        /// </summary>
        /// <param name="streamWriter">Path to create a file with records.</param>
        public void SaveToCsv(StreamWriter streamWriter)
        {
            FileCabinetRecordCsvWriter fileCabinetRecordCsv = new FileCabinetRecordCsvWriter(streamWriter);
            streamWriter.WriteLine("Id,First Name,Last Name,Date of Birth,Gender,Height,Weight");

            foreach (var record in this.records)
            {
                fileCabinetRecordCsv.Write(record);
            }
        }

        /// <summary>
        /// Creates an instance of the class and call the function to write the record to the .xml file.
        /// </summary>
        /// <param name="streamWriter">Path to create a file with records.</param>
        public void SaveToXml(StreamWriter streamWriter)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter))
            {
                FileCabinetRecordXmlWriter fileCabinetRecordXml = new FileCabinetRecordXmlWriter(xmlWriter);

                xmlWriter.WriteStartElement("records");

                foreach (var record in this.records)
                {
                    fileCabinetRecordXml.Write(record);
                }

                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Gets all imported data from a csv file.
        /// </summary>
        /// <param name="reader">Path to import a file with records.</param>
        public void LoadFromCsv(StreamReader reader)
        {
            FileCabinetRecordCsvReader fileCabinetRecordCsvReader = new FileCabinetRecordCsvReader(reader);
            this.Records = new ReadOnlyCollection<FileCabinetRecord>(fileCabinetRecordCsvReader.ReadAll());
        }

        /// <summary>
        /// Gets all imported data from a xml file.
        /// </summary>
        /// <param name="reader">Path to import a file with records.</param>
        public void LoadFromXml(StreamReader reader)
        {
            using (XmlReader xmlReader = XmlReader.Create(reader))
            {
                FileCabinetRecordXmlReader fileCabinetRecordXmlReader = new FileCabinetRecordXmlReader(xmlReader);
                this.Records = new ReadOnlyCollection<FileCabinetRecord>(fileCabinetRecordXmlReader.ReadAll());
            }
        }
    }
}
