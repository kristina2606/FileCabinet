using System;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Fields for creating a record.
    /// </summary>
    public class FileCabinetRecordSeralization
    {
        /// <summary>
        /// Gets or sets record id.
        /// </summary>
        /// <value>Record id of a person.</value>
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of a person.
        /// </summary>
        /// <value>The full name of a person.</value>
        [XmlElement("name")]
        public FullNameSeralization FullName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of a person.
        /// </summary>
        /// <value>The date of birth of a person.</value>
        [XmlIgnore]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets dummy string with correct DateTime format.
        /// </summary>
        /// <value>
        /// Dummy string with correct DateTime format.
        /// </value>
        [XmlElement("dateOfBirth")]
        public string DateOfBirthString
        {
            get { return this.DateOfBirth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }
            set => this.DateOfBirth = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        /// <summary>
        /// Gets or sets the gender of a person.
        /// </summary>
        /// <value>The gender of a person.</value>
        [XmlIgnore]
        public char Gender { get; set; }

        /// <summary>
        /// Gets or sets dummy string with correct char format.
        /// </summary>
        /// <value>
        /// Dummy string with correct char format.
        /// </value>
        [XmlElement("gender")]
        public string GenderString
        {
            get { return this.Gender.ToString(); }
            set { this.Gender = value.Single(); }
        }

        /// <summary>
        /// Gets or sets the height of a person.
        /// </summary>
        /// <value>The height of a person.</value>
        [XmlElement("height")]
        public short Height { get; set; }

        /// <summary>
        /// Gets or sets the weight of a person.
        /// </summary>
        /// <value>The weight of a person.</value>
        [XmlElement("weight")]
        public decimal Weight { get; set; }
    }
}