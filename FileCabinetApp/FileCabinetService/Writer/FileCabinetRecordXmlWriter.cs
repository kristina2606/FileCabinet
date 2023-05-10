using System.Globalization;
using System.Xml;
using FileCabinetApp.Models;

namespace FileCabinetApp.FileCabinetService.Writer
{
    /// <summary>
    /// Provides functionality to export application data to an XML file.
    /// </summary>
    public class FileCabinetRecordXmlWriter
    {
        private readonly XmlWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The XML writer used to create the file with records.</param>
        public FileCabinetRecordXmlWriter(XmlWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes a record to the file.
        /// </summary>
        /// <param name="record">The record to write.</param>
        public void Write(FileCabinetRecord record)
        {
            this.writer.WriteStartElement("record");
            this.writer.WriteAttributeString("id", record.Id.ToString(CultureInfo.InvariantCulture));

            this.writer.WriteStartElement("name");
            this.writer.WriteAttributeString("first", record.FirstName);
            this.writer.WriteAttributeString("last", record.LastName);
            this.writer.WriteEndElement();

            this.writer.WriteElementString("dateOfBirth", record.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            this.writer.WriteElementString("gender", record.Gender.ToString(CultureInfo.InvariantCulture));
            this.writer.WriteElementString("height", record.Height.ToString(CultureInfo.InvariantCulture));
            this.writer.WriteElementString("weight", record.Weight.ToString(CultureInfo.InvariantCulture));

            this.writer.WriteEndElement();
        }
    }
}
