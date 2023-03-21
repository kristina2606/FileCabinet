using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

[assembly: CLSCompliant(true)]

namespace FileCabinetApp
{
    /// <summary>
    /// Creates, edits and checks in entries. Finds records by parameters.
    /// </summary>
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetMemoryService"/> class.
        /// The class constructor takes a validation parameter.
        /// </summary>
        /// <param name="validator">Validation parameter.</param>
        public FileCabinetMemoryService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.Validate(fileCabinetRecordNewData);
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = fileCabinetRecordNewData.FirstName,
                LastName = fileCabinetRecordNewData.LastName,
                DateOfBirth = fileCabinetRecordNewData.DateOfBirth,
                Gender = fileCabinetRecordNewData.Gender,
                Height = fileCabinetRecordNewData.Height,
                Weight = fileCabinetRecordNewData.Weight,
            };

            this.list.Add(record);

            AddToIndex(record, this.firstNameDictionary, fileCabinetRecordNewData.FirstName.ToLowerInvariant());
            AddToIndex(record, this.lastNameDictionary, fileCabinetRecordNewData.LastName.ToLowerInvariant());
            AddToIndex(record, this.dateOfBirthDictionary, fileCabinetRecordNewData.DateOfBirth);

            return record.Id;
        }

        /// <summary>
        /// Gets all existing records.
        /// </summary>
        /// <returns>Returns all existing records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.list);
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
        /// Edits an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">if records with the specified ID do not exist.</exception>
        public void EditRecord(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            var result = this.list.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }

            EditDictionary(this.firstNameDictionary, result.FirstName.ToLowerInvariant(), result, fileCabinetRecordNewData.FirstName.ToLowerInvariant());

            EditDictionary(this.lastNameDictionary, result.LastName.ToLowerInvariant(), result, fileCabinetRecordNewData.LastName.ToLowerInvariant());

            EditDictionary(this.dateOfBirthDictionary, result.DateOfBirth, result, fileCabinetRecordNewData.DateOfBirth);

            result.FirstName = fileCabinetRecordNewData.FirstName;
            result.LastName = fileCabinetRecordNewData.LastName;
            result.DateOfBirth = fileCabinetRecordNewData.DateOfBirth;
            result.Gender = fileCabinetRecordNewData.Gender;
            result.Height = fileCabinetRecordNewData.Height;
            result.Weight = fileCabinetRecordNewData.Weight;
        }

        /// <summary>
        /// Finds all records by first name.
        /// </summary>
        /// <param name="firstName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns  all records by first name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName.ToLowerInvariant(), out List<FileCabinetRecord> allValueOfKey))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(allValueOfKey);
            }
            else
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Finds all records by last name.
        /// </summary>
        /// <param name="lastName">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by last name.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName.ToLowerInvariant(), out List<FileCabinetRecord> allValueOfKey))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(allValueOfKey);
            }
            else
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Finds all records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The parameter by which you want to find all existing records.</param>
        /// <returns>Returns all records by date of birth.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            if (this.dateOfBirthDictionary.TryGetValue(dateOfBirth, out List<FileCabinetRecord> allValueOfKey))
            {
                return new ReadOnlyCollection<FileCabinetRecord>(allValueOfKey);
            }
            else
            {
                return new ReadOnlyCollection<FileCabinetRecord>(Array.Empty<FileCabinetRecord>());
            }
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list.ToArray());
        }

        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var records = fileCabinetServiceSnapshot.Records;
            foreach (var record in records)
            {
                var recordNew = new FileCabinetRecordNewData(record.FirstName, record.LastName, record.DateOfBirth, record.Gender, record.Height, record.Weight);
                try
                {
                    this.validator.Validate(recordNew);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error! Record with id {record.Id}, {ex.Message}!");
                    continue;
                }

                if (this.list.Any(x => x.Id == record.Id))
                {
                    this.EditRecord(record.Id, recordNew);
                }
                else
                {
                    this.CreateRecord(recordNew);
                }
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

        private static void EditDictionary<T>(Dictionary<T, List<FileCabinetRecord>> dictionary, T existingKey, FileCabinetRecord record, T newKey)
        {
            if (dictionary.TryGetValue(existingKey, out List<FileCabinetRecord> allValueOfExistingKey))
            {
                allValueOfExistingKey.Remove(record);
            }

            AddToIndex(record, dictionary, newKey);
        }

        private static void AddToIndex<T>(FileCabinetRecord record, Dictionary<T, List<FileCabinetRecord>> dictionary, T key)
        {
            if (dictionary.TryGetValue(key, out List<FileCabinetRecord> allValueOfKey))
            {
                allValueOfKey.Add(record);
            }
            else
            {
                List<FileCabinetRecord> valueForDictionary = new List<FileCabinetRecord>() { record };
                dictionary.Add(key, valueForDictionary);
            }
        }
    }
}