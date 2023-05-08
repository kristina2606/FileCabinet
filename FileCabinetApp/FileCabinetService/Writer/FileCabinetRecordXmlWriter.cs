using System.Globalization;
using System.Xml;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService.Writer
{
    /// <summary>
    /// Export application data to a XML file.
    /// </summary>
    public class FileCabinetRecordXmlWriter
    {
        private readonly XmlWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlWriter"/> class.
        /// </summary>
        /// <param name="writer">Path to create a file with records.</param>
        public FileCabinetRecordXmlWriter(XmlWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Write the record to the file.
        /// </summary>
        /// <param name="record">Record for write.</param>
        public void Write(FileCabinetRecord record)
        {
            writer.WriteStartElement("record");
            writer.WriteAttributeString("id", record.Id.ToString(CultureInfo.InvariantCulture));

            writer.WriteStartElement("name");
            writer.WriteAttributeString("first", record.FirstName);
            writer.WriteAttributeString("last", record.LastName);
            writer.WriteEndElement();

            writer.WriteStartElement("dateOfBirth");
            writer.WriteString(record.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("gender");
            writer.WriteString(record.Gender.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("height");
            writer.WriteString(record.Height.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("weight");
            writer.WriteString(record.Weight.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
