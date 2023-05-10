using System;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace FileCabinetApp.Serializers.Xml
{
    /// <summary>
    /// Represents the fields for creating a record in XML serialization.
    /// </summary>
    public class FileCabinetRecordSeralization
    {
        /// <summary>
        /// Gets or sets record id.
        /// </summary>
        /// <value>The record id of a person.</value>
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
        /// Gets or sets the string representation of the date of birth.
        /// </summary>
        /// <value>The string representation of the date of birth.</value>
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
        /// Gets or sets the string representation of the gender.
        /// </summary>
        /// <value>The string representation of the gender.</value>
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