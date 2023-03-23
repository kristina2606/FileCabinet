using System.Xml.Serialization;

namespace FileCabinetApp
{
    /// <summary>
    /// Fields for creating the full name of a person.
    /// </summary>
    public class FullName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullName"/> class.
        /// </summary>
        /// <param name="firstName">The first name of a person.</param>
        /// <param name="lastName">The last name of a person.</param>
        public FullName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullName"/> class.
        /// </summary>
        public FullName()
        {
        }

        /// <summary>
        /// Gets or sets the first name of a person.
        /// </summary>
        /// <value>The first name of a person.</value>
        [XmlAttribute(AttributeName = "first")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a person.
        /// </summary>
        /// <value>The last name of a person.</value>
        [XmlAttribute(AttributeName = "last")]
        public string LastName { get; set; }
    }
}