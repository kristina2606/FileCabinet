using System;

namespace FileCabinetApp.Models
{
    /// <summary>
    /// Represents a record in the FileCabinetApp.
    /// </summary>
    public class FileCabinetRecord
    {
        /// <summary>
        /// Gets or sets the record id.
        /// </summary>
        /// <value>The id of the record.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        /// <value>The first name of the person.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        /// <value>The last name of the person.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the person.
        /// </summary>
        /// <value>The date of birth of the person.</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the person.
        /// </summary>
        /// <value>The gender of the person.</value>
        public char Gender { get; set; }

        /// <summary>
        /// Gets or sets the height of the person.
        /// </summary>
        /// <value>The height of the person.</value>
        public short Height { get; set; }

        /// <summary>
        /// Gets or sets the weight of the person.
        /// </summary>
        /// <value>The weight of the person.</value>
        public decimal Weight { get; set; }
    }
}