using System;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Fields for creating a record.
    /// </summary>
    [XmlRoot("record")]
    public class FileCabinetRecord
    {
        /// <summary>
        /// Gets or sets record id.
        /// </summary>
        /// <value>Record id of a person.</value>
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name of a person.</value>
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a person.
        /// </summary>
        /// <value>The last name of a person.</value>
        [XmlElement("LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of a person.
        /// </summary>
        /// <value>The date of birth of a person.</value>
        [XmlElement("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of a person.
        /// </summary>
        /// <value>The gender of a person.</value>
        [XmlElement("Gender")]
        public char Gender { get; set; }

        /// <summary>
        /// Gets or sets the height of a person.
        /// </summary>
        /// <value>The height of a person.</value>
        [XmlElement("Height")]
        public short Height { get; set; }

        /// <summary>
        /// Gets or sets the weight of a person.
        /// </summary>
        /// <value>The weight of a person.</value>
        [XmlElement("Weight")]
        public decimal Weight { get; set; }
    }
}