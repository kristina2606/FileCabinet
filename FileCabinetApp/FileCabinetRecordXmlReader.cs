using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Imports data from a xml file.
    /// </summary>
    public class FileCabinetRecordXmlReader
    {
        private readonly XmlReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlReader"/> class.
        /// </summary>
        /// <param name="reader">Path to import a xml file with records.</param>
        public FileCabinetRecordXmlReader(XmlReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Imports all datas from a xml file.
        /// </summary>
        /// <returns>Returns all datas from a xml file.</returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            var serializer = new XmlSerializer(typeof(Records));

            var records = (Records)serializer.Deserialize(this.reader);

            return records.Record;
        }
    }
}
