using System.Xml.Serialization;

namespace FileCabinetApp.Serializers.Xml
{
    /// <summary>
    /// Represents the fields for creating the full name of a person in XML serialization.
    /// </summary>
    public class FullNameSeralization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullNameSeralization"/> class.
        /// </summary>
        /// <param name="firstName">The first name of a person.</param>
        /// <param name="lastName">The last name of a person.</param>
        public FullNameSeralization(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullNameSeralization"/> class.
        /// </summary>
        public FullNameSeralization()
        {
        }

        /// <summary>
        /// Gets or sets the first name of a person.
        /// </summary>
        /// <value>The first name of a person.</value>
        [XmlAttribute("first")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a person.
        /// </summary>
        /// <value>The last name of a person.</value>
        [XmlAttribute("last")]
        public string LastName { get; set; }
    }
}