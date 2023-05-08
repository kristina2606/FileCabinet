using System;

namespace FileCabinetApp.Models
{
    /// <summary>
    /// Gets new data from user input.
    /// </summary>
    public class FileCabinetRecordNewData
    {
        private readonly string firstName;

        private readonly string lastName;

        private readonly DateTime dateOfBirth;

        private readonly char gender;

        private readonly short height;

        private readonly decimal weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordNewData"/> class.
        /// </summary>
        /// <param name="firstName">Gets first name from user input.</param>
        /// <param name="lastName">Gets last name from user input.</param>
        /// <param name="dateOfBirth">Gets date of birth from user input.</param>
        /// <param name="gender">Gets gender from user input.</param>
        /// <param name="height">Gets height from user input.</param>
        /// <param name="weight">Gets weight from user input.</param>
        public FileCabinetRecordNewData(string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.height = height;
            this.weight = weight;
        }

        /// <summary>
        ///  Gets first name from user input.
        /// </summary>
        /// <value> New first name of a person. </value>
        public string FirstName => this.firstName;

        /// <summary>
        /// Gets last name from user input.
        /// </summary>
        /// <value> New last name of a person. </value>
        public string LastName => this.lastName;

        /// <summary>
        /// Gets date of birth from user input.
        /// </summary>
        /// <value> New date of birth of a person. </value>
        public DateTime DateOfBirth => this.dateOfBirth;

        /// <summary>
        /// Gets gender from user input.
        /// </summary>
        /// <value> New gender of a person. </value>
        public char Gender => this.gender;

        /// <summary>
        /// Gets height from user input.
        /// </summary>
        /// <value> New height of a person. </value>
        public short Height => this.height;

        /// <summary>
        /// Gets weight from user input.
        /// </summary>
        /// <value> New weight of a person. </value>
        public decimal Weight => this.weight;
    }
}
