using System.IO;
using System.Xml;

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
            }
        }
    }
}
