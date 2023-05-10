using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using FileCabinetApp.Models;
using FileCabinetApp.Serializers.Xml;

namespace FileCabinetApp.FileCabinetService.Reader
{
    /// <summary>
    /// Reads data from an XML file and imports it.
    /// </summary>
    public class FileCabinetRecordXmlReader
    {
        private readonly XmlReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlReader"/> class.
        /// </summary>
        /// <param name="reader">The XmlReader for the XML file.</param>
        public FileCabinetRecordXmlReader(XmlReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Reads all data from the XML file and returns it as a list of FileCabinetRecord objects.
        /// </summary>
        /// <returns>A list of FileCabinetRecord objects.</returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            var serializer = new XmlSerializer(typeof(RecordsSeralization));

            var records = (RecordsSeralization)serializer.Deserialize(this.reader);

            IList<FileCabinetRecord> result = new List<FileCabinetRecord>();
            foreach (var record in records.Records)
            {
                result.Add(new FileCabinetRecord
                {
                    Id = record.Id,
                    FirstName = record.FullName.FirstName,
                    LastName = record.FullName.LastName,
                    DateOfBirth = record.DateOfBirth,
                    Gender = record.Gender,
                    Height = record.Height,
                    Weight = record.Weight,
                });
            }

            return result;
        }
    }
}
