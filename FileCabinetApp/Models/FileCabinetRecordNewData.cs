using System;

namespace FileCabinetApp.Models
{
    /// <summary>
    /// Represents the new data obtained from user input.
    /// </summary>
    public class FileCabinetRecordNewData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordNewData"/> class.
        /// </summary>
        /// <param name="firstName">The new first name obtained from user input.</param>
        /// <param name="lastName">The new last name obtained from user input.</param>
        /// <param name="dateOfBirth">The new date of birth obtained from user input.</param>
        /// <param name="gender">The new gender obtained from user input.</param>
        /// <param name="height">The new height obtained from user input.</param>
        /// <param name="weight">The new weight obtained from user input.</param>
        public FileCabinetRecordNewData(string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Height = height;
            this.Weight = weight;
        }

        /// <summary>
        ///  Gets the new first name obtained from user input.
        /// </summary>
        /// <value>The new first name of a person. </value>
        public string FirstName { get; }

        /// <summary>
        /// Gets the new last name obtained from user input.
        /// </summary>
        /// <value>The new last name of a person. </value>
        public string LastName { get; }

        /// <summary>
        /// Gets the new date of birth obtained from user input.
        /// </summary>
        /// <value>The new date of birth of a person. </value>
        public DateTime DateOfBirth { get; }

        /// <summary>
        /// Gets the new gender obtained from user input.
        /// </summary>
        /// <value>The new gender of a person. </value>
        public char Gender { get; }

        /// <summary>
        /// Gets the new height obtained from user input.
        /// </summary>
        /// <value>The new height of a person. </value>
        public short Height { get; }

        /// <summary>
        /// Gets the new weight obtained from user input.
        /// </summary>
        /// <value>The new weight of a person. </value>
        public decimal Weight { get; }
    }
}
