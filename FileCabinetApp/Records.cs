using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    ///  Fields for creating a list of record.
    /// </summary>
    [XmlRoot("records")]
    public class Records
    {
        /// <summary>
        /// Gets or sets a list of record.
        /// </summary>
        /// <value>
        /// A list of record.
        /// </value>
        [XmlElement("record")]
        public List<FileCabinetRecord> Record { get; set; }
    }
}