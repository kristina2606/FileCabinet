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
            XmlWriter xmlWriter = XmlWriter.Create(this.writer);

            xmlWriter.WriteStartElement("record");
            xmlWriter.WriteAttributeString("id", record.Id.ToString(CultureInfo.InvariantCulture));

            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteAttributeString("firs", record.FirstName);
            xmlWriter.WriteAttributeString("last", record.LastName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("dateOfBirth");
            xmlWriter.WriteString(record.DateOfBirth.ToShortDateString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("gender");
            xmlWriter.WriteString(record.Gender.ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("height");
            xmlWriter.WriteString(record.Height.ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("weight");
            xmlWriter.WriteString(record.Weight.ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }
    }
}
