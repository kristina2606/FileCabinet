using System.Globalization;
using System.Xml;

namespace FileCabinetApp
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
            this.writer.WriteStartElement("record");
            this.writer.WriteAttributeString("id", record.Id.ToString(CultureInfo.InvariantCulture));

            this.writer.WriteStartElement("name");
            this.writer.WriteAttributeString("first", record.FullName.FirstName);
            this.writer.WriteAttributeString("last", record.FullName.LastName);
            this.writer.WriteEndElement();

            this.writer.WriteStartElement("dateOfBirth");
            this.writer.WriteString(record.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            this.writer.WriteEndElement();

            this.writer.WriteStartElement("gender");
            this.writer.WriteString(record.Gender.ToString(CultureInfo.InvariantCulture));
            this.writer.WriteEndElement();

            this.writer.WriteStartElement("height");
            this.writer.WriteString(record.Height.ToString(CultureInfo.InvariantCulture));
            this.writer.WriteEndElement();

            this.writer.WriteStartElement("weight");
            this.writer.WriteString(record.Weight.ToString(CultureInfo.InvariantCulture));
            this.writer.WriteEndElement();

            this.writer.WriteEndElement();
        }
    }
}
