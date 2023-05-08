using System.Collections.Generic;
using System.Xml.Serialization;

namespace FileCabinetApp.Serializers.Xml
{
    /// <summary>
    ///  Fields for creating a list of record.
    /// </summary>
    [XmlRoot("records")]
    public class RecordsSeralization
    {
        /// <summary>
        /// Gets or sets a list of record.
        /// </summary>
        /// <value>
        /// A list of record.
        /// </value>
        [XmlElement("record")]
        public List<FileCabinetRecordSeralization> Records { get; set; }
    }
}