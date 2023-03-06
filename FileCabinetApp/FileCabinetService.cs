using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp
{
    /// <summary>
    /// Creates, edits and checks in entries. Finds records by parameters.
    /// </summary>
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="firstName">The first name entered by the user.</param>
        /// <param name="lastName">The last name entered by the user.</param>
        /// <param name="dateOfBirth">The date of birth entered by the user.</param>
        /// <param name="gender">The gender entered by the user.</param>
        /// <param name="height">The height entered by the user.</param>
        /// <param name="weight">The weight entered by the user.</param>
        /// <returns>Returns the id of the created record.</returns>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (firstName.Length < 2 || firstName.Length > 60)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 60");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (lastName.Length < 2 || lastName.Length > 60)
            {
                throw new ArgumentException("last name length is less than 2 or greater than 60");
            }

            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentException("date of birth is less than 01-Jun-1950 or greater today's date");
            }

            if (gender != 'f' && gender != 'm')
            {
                throw new ArgumentException("gender must be 'f' or 'm'.");
            }

            if (height <= 0 || height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }

            if (weight <= 0)
            {
                throw new ArgumentException("weight very small or very large.");
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Height = height,
                Weight = weight,
            };

            this.list.Add(record);

            AddToIndex(record, this.firstNameDictionary, firstName.ToLowerInvariant());
            AddToIndex(record, this.lastNameDictionary, lastName.ToLowerInvariant());
            AddToIndex(record, this.dateOfBirthDictionary, dateOfBirth);

            return record.Id;
        }

        /// <summary>
        /// Gets all existing records.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        /// <summary>
        /// Gets the count of all existing records.
        /// </summary>
        /// <returns>Returns the count of all existing records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>
        /// Edits an already existing entry by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="firstName">The new first name in the record.</param>
        /// <param name="lastName">The new last name in the record.</param>
        /// <param name="dateOfBirth">The new date of birth in the record.</param>
        /// <param name="gender">The new gender in the record.</param>
        /// <param name="height">The new height in the record.</param>
        /// <param name="weight">The new weight in the record.</param>
        /// <exception cref="ArgumentException">if records with the specified ID do not exist.</exception>
        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            var result = this.list.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }

            EditDictionary(this.firstNameDictionary, result.FirstName, result, firstName);

            EditDictionary(this.lastNameDictionary, result.LastName, result, lastName);

            EditDictionary(this.dateOfBirthDictionary, result.DateOfBirth, result, dateOfBirth);

            result.FirstName = firstName;
            result.LastName = lastName;
            result.DateOfBirth = dateOfBirth;
            result.Gender = gender;
            result.Height = height;
            result.Weight = weight;
        }

        /// <summary>
        /// Finds all records by first name.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName.ToLowerInvariant(), out List<FileCabinetRecord> value))
            {
                return value.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        /// <summary>
        /// Finds all records by last name.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName.ToLowerInvariant(), out List<FileCabinetRecord> value))
            {
                return value.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        /// <summary>
        /// Finds all records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateOfBirth)
        {
            if (this.dateOfBirthDictionary.TryGetValue(dateOfBirth, out List<FileCabinetRecord> value))
            {
                return value.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            return this.list.Any(x => x.Id == id);
        }

        private static void EditDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string existingKey, FileCabinetRecord record, string newKey)
        {
            existingKey = existingKey.ToLowerInvariant();
            if (dictionary.TryGetValue(existingKey, out List<FileCabinetRecord> value))
            {
                value.Remove(record);
            }

            AddToIndex(record, dictionary, newKey.ToLowerInvariant());
        }

        private static void EditDictionary(Dictionary<DateTime, List<FileCabinetRecord>> dictionary, DateTime existingKey, FileCabinetRecord record, DateTime newKey)
        {
            if (dictionary.TryGetValue(existingKey, out List<FileCabinetRecord> value))
            {
                value.Remove(record);
            }

            AddToIndex(record, dictionary, newKey);
        }

        private static void AddToIndex(FileCabinetRecord record, Dictionary<string, List<FileCabinetRecord>> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out List<FileCabinetRecord> value))
            {
                value.Add(record);
            }
            else
            {
                List<FileCabinetRecord> valueForDictionary = new List<FileCabinetRecord>();
                valueForDictionary.Add(record);
                dictionary.Add(key, valueForDictionary);
            }
        }

        private static void AddToIndex(FileCabinetRecord record, Dictionary<DateTime, List<FileCabinetRecord>> dictionary, DateTime key)
        {
            if (dictionary.TryGetValue(key, out List<FileCabinetRecord> value))
            {
                value.Add(record);
            }
            else
            {
                List<FileCabinetRecord> valueForDictionary = new List<FileCabinetRecord>();
                valueForDictionary.Add(record);
                dictionary.Add(key, valueForDictionary);
            }
        }
    }
}